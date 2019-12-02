using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OcrGenerator.Services
{
    public class SolrService : IRemoteStorageService
    {
        private readonly HttpClient _client;

        public SolrService(HttpClient client)
        {
            _client = client;
        }

        public async Task Send(string environment, string index, string location, ObservableCollection<FieldViewModel> sourceFields)
        {
            if (string.IsNullOrEmpty(environment))
                throw new ArgumentNullException(nameof(environment), string.Format("Select {0}!", nameof(environment)));
            if (string.IsNullOrEmpty(index))
                throw new ArgumentNullException(nameof(index), string.Format("Select {0}!", nameof(index)));
            if (string.IsNullOrEmpty(location))
                throw new ArgumentNullException(nameof(location), string.Format("Enter {0}!", nameof(location)));
            if (sourceFields == null)
                throw new ArgumentNullException(nameof(sourceFields), string.Format("Select {0}!", nameof(sourceFields)));


            var jsonObj = GenerateJsonObject(location, sourceFields);

            Uri solrSettingsUrl = new Uri(string.Format(environment, index));

            var content = GenerateStringContent(jsonObj);

            Console.WriteLine(content);

            HttpResponseMessage response = await _client.PostAsync(
                solrSettingsUrl,
                content);

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
        }

        private string GenerateJsonObject(string location, ObservableCollection<FieldViewModel> sourceFields)
        {
            var @case = sourceFields.FirstOrDefault(x => x.Type == FieldType.Numeric && x.Name.ToLower() == "case");
            if (@case == null)
                @case = sourceFields.FirstOrDefault(x => x.Type == FieldType.Numeric);

            var fields = sourceFields.Select(x => new { x.Name, x.Value }).ToDictionary(x => x.Name, x => x.Value);

            return JsonConvert.SerializeObject(new
            {
                key = location + "_case_" + @case,
                type = "case",
                location,
                fields
            });
        }

        private StringContent GenerateStringContent(string jsonObject)
        {
            return new StringContent(jsonObject, Encoding.UTF8, "application/json");
        }
    }
}
