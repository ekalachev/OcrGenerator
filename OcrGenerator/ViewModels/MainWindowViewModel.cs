using System.Collections.ObjectModel;

namespace OcrGenerator.ViewModels
{
    public class MainWindowViewModel : Observable
    {
        public ObservableCollection<FieldViewModel> Fields { get; set; }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        private EnvironmentViewModel _selectedEnvironment;
        public EnvironmentViewModel SelectedEnvironment
        {
            get { return _selectedEnvironment; }
            set
            {
                _selectedEnvironment = value;
                OnPropertyChanged(nameof(SelectedEnvironment));
            }
        }

        private IndexViewModel _selectedIndex;
        public IndexViewModel SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }

        private ObservableCollection<EnvironmentViewModel> _environments;
        public ObservableCollection<EnvironmentViewModel> Environments
        {
            get { return _environments; }
            set
            {
                _environments = value;
                OnPropertyChanged(nameof(Environments));
            }
        }

        private ObservableCollection<IndexViewModel> _indexes;
        public ObservableCollection<IndexViewModel> Indexes
        {
            get { return _indexes; }
            set
            {
                _indexes = value;
                OnPropertyChanged(nameof(Indexes));
            }
        }
    }
}
