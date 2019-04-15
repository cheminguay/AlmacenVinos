using AlmacenVinos.Domain.Enums;
using AlmacenVinos.Domain.Models;
using AlmacenVinos.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace AlmacenVinos.WebApi.Controllers
{
    public class BodegaController : ApiController
    {
        private readonly IDataService _service;
        private readonly ILogService _logs;

        public BodegaController(IDataService service, ILogService logs)
        {
            this._service = service;
            this._logs = logs;
        }

        // GET: api/Bodega
        [HttpGet]
        [ResponseType(typeof(List<BodegaDto>))]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(_service.GetBodegas()); ;
            }
            catch (DbEntityValidationException efEx)
            {
                _logs.LogDbEntityValidationException(efEx);
            }
            catch (Exception ex)
            {
                _logs.LogException(ex);
            }
            return Content(HttpStatusCode.NotFound, StringEnum.GetStringValue(MensajeError.NoRecuperado));
        }

        // GET: api/Bodega/id
        [HttpGet]
        [ResponseType(typeof(BodegaDto))]
        [Route("api/Bodega/{idBotella:int}")]
        public IHttpActionResult GetBodega(int idBotella)
        {
            try
            {
                return Ok(_service.GetBodega(idBotella)); ;
            }
            catch (DbEntityValidationException efEx)
            {
                _logs.LogDbEntityValidationException(efEx);
            }
            catch (Exception ex)
            {
                _logs.LogException(ex);
            }
            return Content(HttpStatusCode.NotFound, StringEnum.GetStringValue(MensajeError.NoRecuperado));
        }

        // GET: api/Bodega/Notificacion
        [HttpGet]
        [ResponseType(typeof(BodegaDto))]
        [Route("api/Bodega/Notificacion")]
        public IHttpActionResult NotificaCaducados(int idBotella)
        {
            try
            {
                return Ok(_service.NotificaCaducados()); ;
            }
            catch (DbEntityValidationException efEx)
            {
                _logs.LogDbEntityValidationException(efEx);
            }
            catch (Exception ex)
            {
                _logs.LogException(ex);
            }
            return Content(HttpStatusCode.NotFound, StringEnum.GetStringValue(MensajeError.ErrorNotificacionCaducado));
        }

        // POST: api/Bodega
        [HttpPost]
        [ResponseType(typeof(BodegaDto))]
        public IHttpActionResult Add(BodegaDto bodega)
        {
            try
            {
                return Ok(_service.AddBodega(bodega)); ;
            }
            catch (DbEntityValidationException efEx)
            {
                _logs.LogDbEntityValidationException(efEx);
            }
            catch (Exception ex)
            {
                _logs.LogException(ex);
            }
            return Content(HttpStatusCode.NotFound, StringEnum.GetStringValue(MensajeError.NoRecuperado));
        }

        // GET: api/Botella/Suma
        [HttpGet]
        [ResponseType(typeof(int))]
        [Route("api/Bodega/Suma/{idBotella:int}")]
        public IHttpActionResult SumaBodega(int idBotella)
        {
            try
            {
                return Ok(_service.SumaBodega(idBotella)); ;
            }
            catch (DbEntityValidationException efEx)
            {
                _logs.LogDbEntityValidationException(efEx);
            }
            catch (Exception ex)
            {
                _logs.LogException(ex);
            }
            return Content(HttpStatusCode.NotFound, StringEnum.GetStringValue(MensajeError.NoRecuperado));
        }

        // GET: api/Botella/Suma
        [HttpGet]
        [ResponseType(typeof(int))]
        [Route("api/Bodega/Resta/{idBotella:int}")]
        public IHttpActionResult RestaBodega(int idBotella)
        {
            try
            {
                return Ok(_service.RestaBodega(idBotella)); ;
            }
            catch (DbEntityValidationException efEx)
            {
                _logs.LogDbEntityValidationException(efEx);
            }
            catch (Exception ex)
            {
                _logs.LogException(ex);
            }
            return Content(HttpStatusCode.NotFound, StringEnum.GetStringValue(MensajeError.NoRecuperado));
        }
    }
}
