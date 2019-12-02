using OcrGenerator.Services;
using OcrGenerator.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace OcrGenerator.Views
{
    public partial class Settings : Window
    {
        private readonly SettingsManager _settingsManager;
        public MainWindowViewModel _viewModel { get; set; }

        public Settings(SettingsManager settingsManager, MainWindowViewModel viewModel)
        {
            _settingsManager = settingsManager;
            _viewModel = viewModel;

            InitializeComponent();

            DataContext = _viewModel;
        }

        private void EnvironmentsDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            _settingsManager.SaveMainWindowViewModel(_viewModel);
        }

        private void IndexesDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            _settingsManager.SaveMainWindowViewModel(_viewModel);
        }
    }
}
