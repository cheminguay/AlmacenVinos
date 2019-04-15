using AlmacenVinos.Domain.Enums;
using AlmacenVinos.Domain.Models;
using AlmacenVinos.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;

namespace AlmacenVinos.Web.Controllers
{
    public class HomeController : Controller
    {
        private static string urlApi = ConfigurationManager.AppSettings["urlApi"];

        // GET: Home
        public ActionResult Index()
        {
            BodegaViewModel bodega = GetBodegaViewModel();
            return View(bodega);
        }

        [HttpGet]
        public ActionResult ListaBodega()
        {
            List<BodegaDto> bodega = new List<BodegaDto>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                var responseTask = client.GetAsync("Bodega");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    bodega = JsonConvert.DeserializeObject<List<BodegaDto>>(readTask.Result);
                }
            }
            return PartialView("_ListaBodega", bodega);
        }

        [HttpPost]
        public ActionResult Agregar(BodegaDto bodega)
        {

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlApi);
                    var responseTask = client.GetAsync(String.Format("Bodega/{0}", bodega.IdBotella));
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        BodegaDto bod = JsonConvert.DeserializeObject<BodegaDto>(readTask.Result);
                        if (bod != null)
                        {
                            ModelState.AddModelError(string.Empty, StringEnum.GetStringValue(MensajeError.ExisteBodega));
                            ViewBag.ModalAgregar = true;
                            BodegaViewModel b = GetBodegaViewModel();
                            return View("Index", b);
                        }
                    }
                    var myContent = JsonConvert.SerializeObject(bodega);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    responseTask = client.PostAsync("Bodega", byteContent);
                    responseTask.Wait();

                    result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        bodega = JsonConvert.DeserializeObject<BodegaDto>(readTask.Result);
                    }
                }

                return RedirectToAction("Index");
            }
            else
            {
                BodegaViewModel b = GetBodegaViewModel();
                ViewBag.ModalAgregar = true;
                return View("Index", b);
            }
        }

        [HttpGet]
        public ActionResult Suma(int idBotella)
        {
            List<BodegaDto> bodega = new List<BodegaDto>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                var responseTask = client.GetAsync("Bodega/Suma/" + idBotella);
                responseTask.Wait();

                var result = responseTask.Result;

                responseTask = client.GetAsync("Bodega");
                responseTask.Wait();

                result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    bodega = JsonConvert.DeserializeObject<List<BodegaDto>>(readTask.Result);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Resta(int idBotella)
        {
            List<BodegaDto> bodega = new List<BodegaDto>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                var responseTask = client.GetAsync("Bodega/Resta/" + idBotella);
                responseTask.Wait();

                var result = responseTask.Result;

                responseTask = client.GetAsync("Bodega");
                responseTask.Wait();

                result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    bodega = JsonConvert.DeserializeObject<List<BodegaDto>>(readTask.Result);
                }
            }
            return RedirectToAction("Index");
        }

        private BodegaViewModel GetBodegaViewModel()
        {
            BodegaViewModel bodega = new BodegaViewModel();
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
            bodega.Bodega = new BodegaDto();
            bodega.Botellas = botellas;
            return bodega;
        }
    }
}