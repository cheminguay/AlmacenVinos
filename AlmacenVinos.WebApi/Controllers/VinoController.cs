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
    public class VinoController : ApiController
    {
        private readonly IDataService _service;
        private readonly ILogService _logs;

        public VinoController(IDataService service, ILogService logs)
        {
            this._service = service;
            this._logs = logs;
        }

        // GET: api/Vino
        [HttpGet]
        [ResponseType(typeof(List<VinoDto>))]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(_service.GetVinos()); ;
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

        // GET: api/Vino/{nombre}
        [HttpGet]
        [ResponseType(typeof(VinoDto))]
        [Route("api/vino/{nombre}")]
        public IHttpActionResult GetVino(string nombre)
        {
            try
            {
                return Ok(_service.GetVino(nombre)); ;
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

        // GET: api/Vino/id
        [HttpGet]
        [ResponseType(typeof(VinoDto))]
        [Route("api/vino/{id:int}")]
        public IHttpActionResult GetVino(int id)
        {
            try
            {
                return Ok(_service.GetVino(id)); ;
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

        // POST: api/Vino
        [HttpPost]
        [ResponseType(typeof(VinoDto))]
        public IHttpActionResult Add(VinoDto vino)
        {
            try
            {
                return Ok(_service.AddVino(vino)); ;
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

        // DELETE: api/Vino/5
        [HttpDelete]
        [ResponseType(typeof(int))]
        [Route("api/vino/{id:int}")]
        [AcceptVerbs("Delete")]
        public IHttpActionResult BajaVino(int id)
        {
            try
            {
                int i = _service.DeleteVino(id);
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
