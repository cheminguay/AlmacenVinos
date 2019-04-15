using AlmacenVinos.Domain.Enums;
using AlmacenVinos.Domain.Models;
using AlmacenVinos.Web.ViewModels;
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
    public class BotellaController : Controller
    {
        private static string urlApi = ConfigurationManager.AppSettings["urlApi"];

        // GET: Botella
        public ActionResult Index()
        {
            BotellaViewModel botella = GetBotellaViewModel();
            return View(botella);
        }

        [HttpGet]
        public ActionResult ListaBotellas()
        {
            List<BotellaDto> botellas = new List<BotellaDto>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                var responseTask = client.GetAsync("Botella");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    botellas = JsonConvert.DeserializeObject<List<BotellaDto>>(readTask.Result);
                }
            }
            return PartialView("_ListaBotella", botellas);
        }

        [HttpPost]
        public ActionResult Agregar(BotellaDto botella)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlApi);
                    var responseTask = client.GetAsync(String.Format("Botella/{0}", botella.Descripcion));
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        BotellaDto bot = JsonConvert.DeserializeObject<BotellaDto>(readTask.Result);
                        if (bot != null)
                        {
                            BotellaViewModel b = GetBotellaViewModel();
                            ModelState.AddModelError(string.Empty, String.Format(StringEnum.GetStringValue(MensajeError.ExisteBotella), botella.Descripcion));
                            ViewBag.ModalAgregar = true;
                            return View("Index", b);
                        }
                    }
                    var myContent = JsonConvert.SerializeObject(botella);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    responseTask = client.PostAsync("Botella", byteContent);
                    responseTask.Wait();

                    result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        botella = JsonConvert.DeserializeObject<BotellaDto>(readTask.Result);
                    }
                }

                return RedirectToAction("Index");
            }
            else
            {
                BotellaViewModel b = GetBotellaViewModel();
                ViewBag.ModalAgregar = true;
                return View("Index", b);
            }
        }

        [HttpGet]
        public ActionResult Borrar(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                var responseTask = client.DeleteAsync(String.Format("Botella/{0}", id));
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

        private BotellaViewModel GetBotellaViewModel()
        {
            BotellaViewModel b = new BotellaViewModel();
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
            b.Botella = new BotellaDto();
            b.Vinos = vinos;
            return b;
        }
    }
}