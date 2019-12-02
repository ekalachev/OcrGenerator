using OcrGenerator.Services;
using OcrGenerator.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace OcrGenerator.Views
{
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _timer;

        private readonly SettingsManager _settingsManager;
        private readonly IRemoteStorageService _remoteStorageService;
        private readonly FieldsGenerator _fieldsGenerator;
        private readonly MainWindowViewModel _viewModel;

        public MainWindow(
            SettingsManager settingsManager,
            IRemoteStorageService remoteStorageService,
            FieldsGenerator fieldsGenerator)
        {
            _timer = new DispatcherTimer();

            _settingsManager = settingsManager;
            _remoteStorageService = remoteStorageService;
            _fieldsGenerator = fieldsGenerator;

            _timer.Tick += new EventHandler(TimerTick);
            _timer.Interval = new TimeSpan(0, 0, 5);

            InitializeComponent();

            _viewModel = _settingsManager.GetMainWindowViewModel();
            DataContext = _viewModel;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            _viewModel.Status = string.Empty;
            _timer.Stop();
        }

        private void GenerateBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var field in _viewModel.Fields)
            {
                if (field.IsNotRandom)
                    continue;

                field.Value = _fieldsGenerator.Generate(field);
            }

            _settingsManager.SaveMainWindowViewModel(_viewModel);

            _viewModel.Status = "Generated!";
            _timer.Start();
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _remoteStorageService.Send(
                    EnvironmentComboBox.SelectedValue.ToString(),
                    IndexComboBox.SelectedValue.ToString(),
                    _viewModel.Location,
                    _viewModel.Fields);

                _viewModel.Status = "Saved!";
                _timer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            new Settings(_settingsManager, _viewModel).ShowDialog();
        }

        private void EnvironmentComboBox_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                _settingsManager.SaveMainWindowViewModel(_viewModel);
            }
        }

        private void IndexComboBox_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                _settingsManager.SaveMainWindowViewModel(_viewModel);
            }
        }

        private void FieldsDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            _settingsManager.SaveMainWindowViewModel(_viewModel);
        }
    }
}
