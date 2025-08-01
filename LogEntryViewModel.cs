using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;
using TrailAndTailLogBook.Data;
using TrailAndTailLogBook.Models;
using TrailAndTailLogBook.Pages;

namespace TrailAndTailLogBook.ViewModels
{
    public class LogEntryViewModel : BaseViewModel
    {
        private readonly DatabaseContext _database;
        private byte[] _photo;
        private LogEntry _selectedLogEntry;
        private LogEntry _pendingLogEntry;
        private bool _isRefreshing;
        private string _searchText;
        private ObservableCollection<LogEntry> _filteredLogEntries = new();

        // Form properties
        private string _title;
        private string _description;
        private DateTime _date = DateTime.Now;
        private string _location;

        public ObservableCollection<LogEntry> LogEntries { get; } = new ObservableCollection<LogEntry>();

        public ObservableCollection<LogEntry> FilteredLogEntries
        {
            get => _filteredLogEntries;
            set => SetProperty(ref _filteredLogEntries, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public string Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                SearchLogs();
            }
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public LogEntry SelectedLogEntry
        {
            get => _selectedLogEntry;
            set
            {
                if (SetProperty(ref _selectedLogEntry, value) && value != null)
                {
                    LoadLogEntryDetails(value);
                }
            }
        }

        public byte[] Photo
        {
            get => _photo;
            set
            {
                if (SetProperty(ref _photo, value))
                {
                    OnPropertyChanged(nameof(PhotoSource));
                }
            }
        }

        public ImageSource PhotoSource =>
            _photo != null ? ImageSource.FromStream(() => new MemoryStream(_photo)) : "placeholder.png";

        // Commands
        public ICommand TakePhotoCommand { get; }
        public ICommand SelectPhotoCommand { get; }
        public ICommand SaveLogCommand { get; }
        public ICommand ConfirmSaveCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand ViewCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand SearchCommand { get; }

        public LogEntryViewModel(DatabaseContext database)
        {
            _database = database;

            // Initialize commands
            TakePhotoCommand = new Command(async () => await CapturePhoto());
            SelectPhotoCommand = new Command(async () => await PickPhoto());
            SaveLogCommand = new Command(async () => await PrepareForSave());
            ConfirmSaveCommand = new Command(async () => await FinalizeSave());
            BackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
            DeleteCommand = new Command<LogEntry>(async (entry) => await DeleteLog(entry));
            CancelCommand = new Command(async () => await Cancel());
            EditCommand = new Command<LogEntry>(async (entry) => await EditLogEntry(entry));
            ViewCommand = new Command<LogEntry>(async (entry) => await ViewLogDetails(entry));
            RefreshCommand = new Command(() => LoadLogEntries());
            SearchCommand = new Command(() => SearchLogs());

            LoadLogEntries();
        }

        private async Task PrepareForSave()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                await Shell.Current.DisplayAlert("Error", "Title is required", "OK");
                return;
            }

            _pendingLogEntry = new LogEntry
            {
                Title = Title,
                Description = Description,
                Date = Date,
                Location = Location,
                Photo = Photo
            };

            await Shell.Current.GoToAsync(nameof(SaveLogPage));
        }

        private async Task FinalizeSave()
{
    try
    {
        if (_pendingLogEntry == null) return;

        _database.LogEntries.Add(_pendingLogEntry);
        await _database.SaveChangesAsync();

        // Navigate to LogSavedPage after successful save
        await Shell.Current.GoToAsync(nameof(LogSavedPage));
    }
    catch (Exception ex)
    {
        await Shell.Current.DisplayAlert("Error", $"Failed to save: {ex.Message}", "OK");
    }
    finally
    {
        _pendingLogEntry = null;
    }
}

        public void LoadLogEntries()
        {
            IsRefreshing = true;

            try
            {
                LogEntries.Clear();
                var entries = _database.LogEntries
                    .OrderByDescending(e => e.Date)
                    .ToList();

                foreach (var entry in entries)
                {
                    LogEntries.Add(entry);
                }

                FilteredLogEntries = new ObservableCollection<LogEntry>(LogEntries);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading entries: {ex}");
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private void SearchLogs()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredLogEntries = new ObservableCollection<LogEntry>(LogEntries);
                return;
            }

            var filtered = LogEntries.Where(l =>
                (l.Title?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (l.Description?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (l.Location?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false))
                .ToList();

            FilteredLogEntries = new ObservableCollection<LogEntry>(filtered);
        }

        private void LoadLogEntryDetails(LogEntry entry)
        {
            Title = entry.Title;
            Description = entry.Description;
            Date = entry.Date;
            Location = entry.Location;
            Photo = entry.Photo;
        }

        private async Task ViewLogDetails(LogEntry entry)
        {
            if (entry == null) return;

            var parameters = new Dictionary<string, object>
            {
                { "EntryId", entry.Id }
            };

            await Shell.Current.GoToAsync(nameof(LogEntryDetailPage), parameters);
        }

        private async Task EditLogEntry(LogEntry entry)
        {
            if (entry == null) return;

            SelectedLogEntry = entry;
            await Shell.Current.GoToAsync(nameof(EditLogPage));
        }

        private async Task DeleteLog(LogEntry entry)
        {
            if (entry == null) return;

            bool confirm = await Shell.Current.DisplayAlert(
                "Confirm Delete",
                $"Delete '{entry.Title}'?",
                "Delete",
                "Cancel");

            if (confirm)
            {
                try
                {
                    _database.LogEntries.Remove(entry);
                    await _database.SaveChangesAsync();
                    LoadLogEntries();

                    await Shell.Current.DisplayAlert("Success", "Log deleted", "OK");
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        private async Task CapturePhoto()
        {
            try
            {
                if (!MediaPicker.Default.IsCaptureSupported)
                {
                    await Shell.Current.DisplayAlert("Error", "Camera not supported", "OK");
                    return;
                }

                var photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo != null)
                {
                    await ProcessPhoto(photo);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Camera error: {ex.Message}", "OK");
            }
        }

        private async Task PickPhoto()
        {
            try
            {
                var photo = await MediaPicker.Default.PickPhotoAsync();
                if (photo != null)
                {
                    await ProcessPhoto(photo);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Photo selection failed: {ex.Message}", "OK");
            }
        }

        private async Task ProcessPhoto(FileResult photo)
        {
            try
            {
                using var stream = await photo.OpenReadAsync();
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                Photo = memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to process photo: {ex.Message}", "OK");
            }
        }

        private async Task Cancel()
        {
            ClearForm();
            await Shell.Current.GoToAsync("..");
        }

        private void ClearForm()
        {
            Title = string.Empty;
            Description = string.Empty;
            Location = string.Empty;
            Photo = null;
            SelectedLogEntry = null;
        }
    }
}