using OcrGenerator.ViewModels;

namespace OcrGenerator
{
    public class IndexViewModel : Observable
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }
}
