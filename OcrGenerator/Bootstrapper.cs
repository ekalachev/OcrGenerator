using Autofac;
using OcrGenerator.Services;
using System.Net.Http;

namespace OcrGenerator
{
    internal class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SettingsManager>().AsSelf();
            builder.RegisterType<SolrService>().As<IRemoteStorageService>();
            builder.RegisterType<HttpClient>().AsSelf().SingleInstance();
            builder.RegisterType<FieldsGenerator>().AsSelf().SingleInstance();
            return builder.Build();
        }
    }
}
