using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using System.Text.Json;
using System.Text;
using NZWalks.UI.Models.DTO;

namespace NZWalks.UI.Controllers
{
    public class WalksController : Controller
    {

        private readonly IHttpClientFactory httpClientFactory;

        public WalksController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<WalkDto> response = new List<WalkDto>();
            try
            {
                //Get All Walks from the Web API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7002/api/walks");

                //Checking is successfull the response message 200.
                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<WalkDto>>());



            }
            catch (Exception)
            {
                //Log exception

            }

            return View(response);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddWalkViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7002/api/walks"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };


            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<WalkDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "walks");
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<WalkDto>($"https://localhost:7002/api/walks/{id.ToString()}");
            if (response is not null)
            {
                return View(response);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WalkDto request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7002/api/walks/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };


            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<WalkDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "walks");
            }

            return View("walks");
        }


        //Deleting resource.
        [HttpPost]
        public async Task<IActionResult> Delete(WalkDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7002/api/walks/{request.Id}");
                //Make sure it print the 200 OK Status.
                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "walks");
            }
            catch (Exception)
            {
                //Console                
            }

            return View("Edit");

        }
    





    }
}
