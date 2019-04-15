using System;
using System.Configuration;
using System.Net.Http;

namespace AlmacenVinos.ProcesoBatch
{
    class Program
    {
        private static string urlApi = ConfigurationManager.AppSettings["urlApi"];

        static void Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                var responseTask = client.GetAsync("Bodega/Notificacion");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    //JsonConvert.DeserializeObject<List<BodegaDto>>(readTask.Result);
                }
            }
        }
    }
}
