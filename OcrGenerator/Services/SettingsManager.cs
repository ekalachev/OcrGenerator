using Newtonsoft.Json;
using OcrGenerator.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace OcrGenerator.Services
{
    public class SettingsManager
    {
        private readonly string _settingsPath = "settings.json";

        public void SaveMainWindowViewModel(MainWindowViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            viewModel.Status = "";

            File.WriteAllText(_settingsPath, JsonConvert.SerializeObject(viewModel));
        }

        public MainWindowViewModel GetMainWindowViewModel()
        {
            if (!File.Exists(_settingsPath))
            {
                using (File.Create(_settingsPath)) { }

                var viewModel = new MainWindowViewModel
                {
                    Fields = JsonConvert.DeserializeObject<ObservableCollection<FieldViewModel>>(Properties.Settings.Default.Fields),
                    Location = Properties.Settings.Default.Location,
                    Environments = JsonConvert.DeserializeObject<ObservableCollection<EnvironmentViewModel>>(Properties.Settings.Default.Environments),
                    Indexes = JsonConvert.DeserializeObject<ObservableCollection<IndexViewModel>>(Properties.Settings.Default.Indexes)
                };

                SaveMainWindowViewModel(viewModel);

                return viewModel;
            }
            else
            {
                return JsonConvert.DeserializeObject<MainWindowViewModel>(File.ReadAllText(_settingsPath));
            }
        }
    }
}
