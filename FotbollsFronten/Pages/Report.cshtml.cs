using FotbollsFronten.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;

namespace FotbollsFronten.Pages
{
    public class ReportModel : PageModel
    {
        private readonly HttpClient _httpClient;
       

        public List<Report> Reports { get; set; }

        public ReportModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task OnGetAsync()
        {
            await GetReports();
        }



        public async Task GetReports()
        {
            var response = await _httpClient.GetAsync("https://fotbollsfrontenapi.azurewebsites.net/api/report");


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Reports = JsonConvert.DeserializeObject<List<Report>>(content);
            }

        }
    }
}
