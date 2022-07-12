using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string name = "kevinzOnzinzin";
            var nn = name.Substring(name.Length-8);

            Console.WriteLine(nn);
           /* if (name.Substring(8, name.Length) == "fied.png")
            {
                log.LogInformation("Archivo ya procesado");
            }*/
            /*
             * 
             * 
             * 
             * 
                        mmm client = new mmm();

                        var mm = await client.Getget();
                        for (int i = 0; i < mm.Count; i++)
                        {
                            Console.WriteLine(mm[i]);
                        }
            */
            /* IEnumerable<string> l = mm as IEnumerable<string>;

             foreach (var item in l)
             {

                 Console.WriteLine(l);
             }
             */


        }
    }
    public class mmm
    {
        private readonly HttpClient httpClient;
        public mmm()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.chucknorris.io/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task<List<string>> Getget()
        {
            var response = await httpClient.GetAsync("jokes/categories");
            if (response.IsSuccessStatusCode)
            {
                var categoriass = await response.Content.ReadAsStringAsync();
                var n = JsonConvert.DeserializeObject<string[]>(categoriass);
                return n.ToList();
            }
            return null;
           
        }
       
    }
    public class categorias
    {
        public string []categoria {  get; set; }
    }
}
