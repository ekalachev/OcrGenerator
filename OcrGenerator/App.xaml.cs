using Autofac;
using OcrGenerator.Services;
using OcrGenerator.ViewModels;
using System.Windows;
using OcrGenerator.Views;

namespace OcrGenerator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IRemoteStorageService _remoteStorageService;
        private SettingsManager _settingsManager;
        private FieldsGenerator _fieldsGenerator;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var bootstrapper = new Bootstrapper();
            IContainer container = bootstrapper.Bootstrap();

            _settingsManager = container.Resolve<SettingsManager>();
            _remoteStorageService = container.Resolve<IRemoteStorageService>();
            _fieldsGenerator = container.Resolve<FieldsGenerator>();

            MainWindow = new MainWindow(_settingsManager, _remoteStorageService, _fieldsGenerator);
            MainWindow.Show();
        }
    }
}
