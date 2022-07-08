using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using ChuckNorris.Models;

namespace ChuckNorris.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuckNorrisController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        List<string> info = new List<string>();
        public ChuckNorrisController(IHttpClientFactory httpClientFactory)
        {
            _clientFactory = httpClientFactory;
        } 
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var client = _clientFactory.CreateClient("ChuckNorris");
                var response = await client.GetAsync("jokes/categories");


                if (response.IsSuccessStatusCode)
                {
                    var categoriass = await response.Content.ReadAsStringAsync();
                    var n = JsonConvert.DeserializeObject<string[]>(categoriass);
                    return Ok(n.ToList());
                }
                
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("f");
            }
            return BadRequest("m");
        }



        [HttpGet]
        [Route("random")]
        public async Task<IActionResult> GetRandom()
        {
            
            try
            {
                var client = _clientFactory.CreateClient("ChuckNorris");
                var response = await client.GetAsync("jokes/random");


                if (response.IsSuccessStatusCode)
                {
              
                   /* var random = await response.Content.ReadFromJsonAsync<ChuckResponse>();
                    return Ok(random.value);*/
                    var categoriass = await response.Content.ReadAsStringAsync();
                    var n = JsonConvert.DeserializeObject<ChuckResponse>(categoriass);
                   return Ok(n.value);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("f");
            }
            return BadRequest("m");
        }
    }
}
