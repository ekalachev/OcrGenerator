using System;
using System.Globalization;
using System.Text;

namespace OcrGenerator.Services
{
    public class FieldsGenerator
    {
        private readonly Random _random;

        public FieldsGenerator()
        {
            _random = new Random();
        }

        public string Generate(FieldViewModel field)
        {
            if (field == null)
                throw new ArgumentNullException(nameof(field));
            
            if (field.Type == FieldType.DebtorName)
                return string.Format("{0}{1} {2}{3}",
                    field.Prefix,
                    ToTitleCase(RandomString(field.Length / 2, true)),
                    ToTitleCase(RandomString(field.Length / 2, true)),
                    field.Postfix);

            if (field.Type == FieldType.String)
                return string.Format("{0}{1}{2}",
                    field.Prefix,
                    RandomString(field.Length, false),
                    field.Postfix);

            string maxRandomNumStr = "";
            for (int i = 0; i < field.Length; i++)
                maxRandomNumStr += "9";

            if (!int.TryParse(maxRandomNumStr, out int maxRandomVal))
                maxRandomVal = int.MaxValue;

            var pattern = GeneratePattern(field.Length);

            return string.Format(pattern, field.Prefix, _random.Next(maxRandomVal), field.Postfix);
        }

        private string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        private string GeneratePattern(int length)
        {
            string pattern = "{0}{1:";
            for (int i = 0; i < length; i++)
                pattern += "0";

            return pattern + "}{2}";
        }

        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * _random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}
