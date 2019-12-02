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

        public string Generate(FieldType type, string prefix, int length)
        {
            if (type == FieldType.DebtorName)
                return string.Format("{0}{1} {2}", prefix, ToTitleCase(RandomString(length / 2, true)), ToTitleCase(RandomString(length / 2, true)));

            if (type == FieldType.String)
                return string.Format("{0}{1}", prefix, RandomString(length, false));

            string maxRandomNumStr = "";
            for (int i = 0; i < length; i++)
                maxRandomNumStr += "9";

            if (!int.TryParse(maxRandomNumStr, out int maxRandomVal))
                maxRandomVal = int.MaxValue;

            var pattern = GeneratePattern(length);

            return string.Format(pattern, prefix, _random.Next(maxRandomVal));
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

            return pattern + "}";
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
