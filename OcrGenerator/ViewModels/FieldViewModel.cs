using OcrGenerator.ViewModels;

namespace OcrGenerator
{
    public class FieldViewModel : Observable
    {
        private string _value;

        public string Name { get; set; }
        public FieldType Type { get; set; }
        public int Length { get; set; }
        public string Prefix { get; set; }
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        public bool IsNotRandom { get; set; }
    }

    public enum FieldType
    {
        DebtorName,
        Numeric,
        String
    }
}
