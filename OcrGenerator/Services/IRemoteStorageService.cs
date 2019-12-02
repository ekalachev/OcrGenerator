using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OcrGenerator.Services
{
    public interface IRemoteStorageService
    {
        Task Send(string environment, string index, string location, ObservableCollection<FieldViewModel> sourceFields);
    }
}
