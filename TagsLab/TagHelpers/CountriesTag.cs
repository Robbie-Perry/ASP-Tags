using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TagsLab.Models;

namespace TagsLab.TagHelpers
{
    [HtmlTargetElement("countries")]
    public class CountriesTag : TagHelper
    {
        private string baseUrl = "https://restcountries.eu";

        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            IEnumerable<Country> countries = await GetCountriesAsync();
            string html = string.Empty;
            html += "<table><tr><th>Name</th><th>Capital</th><th>Flag</th></tr>";
            foreach (var item in countries)
            {
                html += "<tr>";
                html += "<td>" + item.name + "</td>";
                html += "<td>" + item.capital + "</td>";
                html += "<td><img src='" + item.flag + "' style='width: 50px' /></td>";
                html += "</tr>";
            }
            html += "</table>";
            output.Content.SetHtmlContent(html);
        }

        async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            IEnumerable<Country> countries = null;
            try
            {
                // Get all cartoon characters
                HttpResponseMessage response = await client.GetAsync("/rest/v2/all");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    countries = JsonConvert.DeserializeObject<IEnumerable<Country>>(json);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            return countries;
        }
    }
}
