using AlmacenVinos.Domain.Enums;
using AlmacenVinos.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace AlmacenVinos.Web.Controllers
{
    public class VinoController : Controller
    {
        private static string urlApi = ConfigurationManager.AppSettings["urlApi"];

        // GET: Vino
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListaVinos()
        {
            List<VinoDto> vinos = new List<VinoDto>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                var responseTask = client.GetAsync("Vino");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    vinos = JsonConvert.DeserializeObject<List<VinoDto>>(readTask.Result);
                }
            }
            return PartialView("_ListaVino", vinos);
        }

        [HttpPost]
        public ActionResult Agregar(VinoDto vino)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlApi);
                    var responseTask = client.GetAsync(String.Format("Vino/{0}", vino.Nombre));
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        VinoDto vin = JsonConvert.DeserializeObject<VinoDto>(readTask.Result);
                        if (vin != null)
                        {
                            ModelState.AddModelError(string.Empty, String.Format(StringEnum.GetStringValue(MensajeError.ExisteVino), vino.Nombre));
                            ViewBag.ModalAgregar = true;
                            return View("Index");
                        }
                    }
                    var myContent = JsonConvert.SerializeObject(vino);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    responseTask = client.PostAsync("Vino", byteContent);
                    responseTask.Wait();

                    result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        vino = JsonConvert.DeserializeObject<VinoDto>(readTask.Result);
                    }
                }

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ModalAgregar = true;
                return View("Index");
            }
        }

        [HttpGet]
        public ActionResult Borrar(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                var responseTask = client.DeleteAsync(String.Format("Vino/{0}", id));
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var resultado = readTask.Result;
                }
            }
            return RedirectToAction("Index");
        }
    }
}