using AlmacenVinos.Domain.Enums;
using AlmacenVinos.Domain.Models;
using AlmacenVinos.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace AlmacenVinos.WebApi.Controllers
{
    public class BotellaController : ApiController
    {
        private readonly IDataService _service;
        private readonly ILogService _logs;

        public BotellaController(IDataService service, ILogService logs)
        {
            this._service = service;
            this._logs = logs;
        }

        // GET: api/Botella
        [HttpGet]
        [ResponseType(typeof(List<BotellaDto>))]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(_service.GetBotellas()); ;
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

        // GET: api/Botella/{nombre}
        [HttpGet]
        [ResponseType(typeof(BotellaDto))]
        [Route("api/Botella/{nombre}")]
        public IHttpActionResult GetNotella(string nombre)
        {
            try
            {
                return Ok(_service.GetBotella(nombre)); ;
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

        // GET: api/Botella/id
        [HttpGet]
        [ResponseType(typeof(BotellaDto))]
        [Route("api/Botella/{id:int}")]
        public IHttpActionResult GetBotella(int id)
        {
            try
            {
                return Ok(_service.GetBotella(id)); ;
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

        // POST: api/Botella
        [HttpPost]
        [ResponseType(typeof(BotellaDto))]
        public IHttpActionResult Add(BotellaDto botella)
        {
            try
            {
                return Ok(_service.AddBotella(botella)); ;
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

        // DELETE: api/Botella/5
        [HttpDelete]
        [ResponseType(typeof(int))]
        [Route("api/Botella/{id:int}")]
        [AcceptVerbs("Delete")]
        public IHttpActionResult BajaBotella(int id)
        {
            try
            {
                int i = _service.DeleteBotella(id);
                return Ok(i);
            }
            catch (DbEntityValidationException efEx)
            {
                _logs.LogDbEntityValidationException(efEx);
                return Content(HttpStatusCode.Conflict, StringEnum.GetStringValue(MensajeError.NoGuardado));
            }
            catch (Exception ex)
            {
                _logs.LogException(ex);
                return Content(HttpStatusCode.InternalServerError, StringEnum.GetStringValue(MensajeError.NoGuardado));
            }
        }
    }
}
