using AlmacenVinos.Domain.Enums;
using AlmacenVinos.Domain.Models;
using AlmacenVinos.Infraestructure.Data;
using AlmacenVinos.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlmacenVinos.Services
{
    public class DataService : IDataService
    {
        private readonly IBodegaRepository _bodega;
        private readonly INotificacionService _notificacionService;
        private readonly ILogService _logService;

        public DataService(IBodegaRepository bodega, INotificacionService notificacionService, ILogService logService)
        {
            _bodega = bodega;
            _logService = logService;
            _notificacionService = notificacionService;
        }

        #region Vinos

        public IQueryable<VinoDto> GetVinos(bool baja = false)
        {
            return (from v in _bodega.GetAll<Vino>()
                    where (baja == true || v.Baja == baja)
                    select new VinoDto()
                    {
                        Id = v.Id,
                        Nombre = v.Nombre,
                        Denominacion = v.Denominacion,
                        Capacidad = v.Capacidad,
                        Color = v.Color,
                        Crianza = v.Crianza,
                        Variedad = v.Variedad,
                        Baja = v.Baja
                    });
        }

        public VinoDto GetVino(int Id)
        {
            Vino vino = _bodega.Find<Vino>(x => x.Id == Id);
            return VinoADto(vino);
        }

        public VinoDto GetVino(string nombre)
        {
            Vino v = _bodega.Find<Vino>(x => x.Nombre == nombre);
            return VinoADto(v);
        }

        

        public VinoDto AddVino(VinoDto vino)
        {
            Vino v = _bodega.Create<Vino>(DtoAVino(vino));
            vino.Id = v.Id;
            return vino;
        }

        public int DeleteVino(int id)
        {
            //es un borrado lógico
            Vino vino = _bodega.Find<Vino>(x => x.Id == id);
            vino.Baja = true;
            vino =_bodega.Update<Vino>(id, vino);
            return (vino.Baja) ? 1 : 0;
        }

        #endregion

        #region Botellas

        public IQueryable<BotellaDto> GetBotellas(bool caduca = false, bool disponible = true)
        {
            return (from b in _bodega.GetAll<Botella>()
                    where (caduca == false || b.Caducidad >= DateTime.Today) 
                    && (disponible == false || b.Disponible == disponible)
                    select new BotellaDto()
                    {
                        Id = b.Id,
                        Descripcion = b.Descripcion,
                        Añada = b.Añada,
                        Caducidad = b.Caducidad,
                        Disponible = b.Disponible,
                        IdVino = b.IdVino,
                        Vino = new VinoDto()
                        {
                            Id = b.Vino.Id,
                            Nombre = b.Vino.Nombre,
                            Denominacion = b.Vino.Denominacion,
                            Capacidad = b.Vino.Capacidad,
                            Color = b.Vino.Color,
                            Crianza = b.Vino.Crianza,
                            Variedad = b.Vino.Variedad,
                            Baja = b.Vino.Baja
                        }
                    });
        }

        public BotellaDto GetBotella(int Id)
        {
            Botella b = _bodega.Find<Botella>(x => x.Id == Id);
            return BotellaADto(b);
        }

        public BotellaDto GetBotella(string nombre)
        {
            Botella b = _bodega.Find<Botella>(x => x.Descripcion == nombre);
            return BotellaADto(b);
        }

        public BotellaDto AddBotella(BotellaDto botella)
        {
            Botella b = _bodega.Create<Botella>(DtoABotella(botella));
            botella.Id = b.Id;
            return botella;
        }

        public int DeleteBotella(int id)
        {
            //es un borrado lógico
            Botella botella = _bodega.Find<Botella>(x => x.Id == id);
            botella.Disponible = false;
            botella = _bodega.Update<Botella>(id, botella);
            return (!botella.Disponible) ? 1 : 0; ;
        }

        #endregion

        #region Bodega

        public bool NotificaCaducados()
        {
            List<Bodega> lista = (from v in _bodega.GetAll<Bodega>()
                                  where v.Botella.Caducidad <= DateTime.Today.AddMonths(1)
                                  && v.NotificadoCaducidad == false
                                  select v).ToList();
            foreach (Bodega b in lista)
            {
                _notificacionService.NotificacionCaducidad(b);
                b.NotificadoCaducidad = true;
                _bodega.Update(b.Id, b);
            }
            return true;
        }

        public IQueryable<BodegaDto> GetBodegas()
        {
            return (from v in _bodega.GetAll<Bodega>()
                    select new BodegaDto()
                    {
                        Id = v.Id,
                        IdBotella = v.IdBotella,
                        Unidades = v.Unidades,
                        NotificadoCaducidad = v.NotificadoCaducidad,
                        Botella = new BotellaDto()
                        {
                            Id = v.Botella.Id,
                            Descripcion = v.Botella.Descripcion,
                            Añada = v.Botella.Añada,
                            Caducidad = v.Botella.Caducidad,
                            Disponible = v.Botella.Disponible,
                            IdVino = v.Botella.IdVino,
                            Vino = new VinoDto()
                            {
                                Id = v.Botella.Vino.Id,
                                Nombre = v.Botella.Vino.Nombre,
                                Denominacion = v.Botella.Vino.Denominacion,
                                Capacidad = v.Botella.Vino.Capacidad,
                                Color = v.Botella.Vino.Color,
                                Crianza = v.Botella.Vino.Crianza,
                                Variedad = v.Botella.Vino.Variedad,
                                Baja = v.Botella.Vino.Baja
                            }
                        }
                    });
        }

        public BodegaDto GetBodega(int idBotella)
        {
            Bodega b = _bodega.Find<Bodega>(x => x.IdBotella == idBotella);
            return BodegaADto(b);
        }

        public BodegaDto AddBodega(BodegaDto bodega)
        {
            Bodega b = _bodega.Create<Bodega>(DtoABodega(bodega));
            bodega.Id = b.Id;
            _notificacionService.NotificacionIngresado(b.Unidades, b);
            _logService.LogInventario(String.Format(StringEnum.GetStringValue(MensajeNotificaciones.Ingreso), bodega.Unidades, bodega.Botella.Descripcion));
            return bodega;
        }

        public int SumaBodega(int idBotella)
        {
            Bodega bodega = _bodega.Find<Bodega>(x => x.IdBotella == idBotella);
            bodega.Unidades = bodega.Unidades + 1;
            _bodega.Update(bodega.Id, bodega);
            _notificacionService.NotificacionIngresado(1, bodega);
            _logService.LogInventario(String.Format(StringEnum.GetStringValue(MensajeNotificaciones.Ingreso), "1", bodega.Botella.Descripcion));
            return bodega.Unidades;
        }

        public int RestaBodega(int idBotella)
        {
            //es un borrado lógico
            Bodega bodega = _bodega.Find<Bodega>(x => x.IdBotella == idBotella);
            int result = bodega.Unidades - 1;
            if (bodega.Unidades == 1)
            {
                _bodega.Delete(bodega);
            }
            else
            {
                bodega.Unidades = result;
                _bodega.Update(bodega.Id, bodega);
            }
            _notificacionService.NotificacionExtraido(1, bodega);
            _logService.LogInventario(String.Format(StringEnum.GetStringValue(MensajeNotificaciones.Extraccion), "1",  bodega.Botella.Descripcion));
            return result;
        }

        #endregion

        #region Metodos Privados

        private VinoDto VinoADto(Vino v)
        {
            if (v == null) return null;
            return (new VinoDto()
            {
                Id = v.Id,
                Nombre = v.Nombre,
                Denominacion = v.Denominacion,
                Capacidad = v.Capacidad,
                Color = v.Color,
                Crianza = v.Crianza,
                Variedad = v.Variedad,
                Baja = v.Baja
            });
        }

        private Vino DtoAVino(VinoDto v)
        {
            if (v == null) return null;
            Vino vino = new Vino()
            {
                Id = v.Id,
                Nombre = v.Nombre,
                Denominacion = v.Denominacion,
                Capacidad = v.Capacidad,
                Color = v.Color,
                Crianza = v.Crianza,
                Variedad = v.Variedad,
                Baja = v.Baja
            };
            return vino;
        }

        private BotellaDto BotellaADto(Botella b)
        {
            if (b == null) return null;
            BotellaDto botella = new BotellaDto()
            {
                Id = b.Id,
                Descripcion = b.Descripcion,
                Añada = b.Añada,
                Caducidad = b.Caducidad,
                Disponible = b.Disponible,
                IdVino = b.IdVino
            };
            if (b.Vino != null) botella.Vino = VinoADto(b.Vino);
            return botella;
        }

        private Botella DtoABotella(BotellaDto b)
        {
            if (b == null) return null;
            Botella botella = new Botella()
            {
                Id = b.Id,
                Descripcion = b.Descripcion,
                Añada = b.Añada,
                Caducidad = b.Caducidad,
                Disponible = b.Disponible,
                IdVino = b.IdVino
            };
            if (b.Vino != null) botella.Vino = DtoAVino(b.Vino);
            return botella;
        }

        private BodegaDto BodegaADto(Bodega b)
        {
            if (b == null) return null;
            return (new BodegaDto()
            {
                Id = b.Id,
                IdBotella = b.IdBotella,
                Unidades = b.Unidades,
                NotificadoCaducidad = b.NotificadoCaducidad,
                Botella = BotellaADto(b.Botella)
            });
        }


        private Bodega DtoABodega(BodegaDto b)
        {
            if (b == null) return null;
            Bodega bodega = (new Bodega()
            {
                Id = b.Id,
                IdBotella = b.IdBotella,
                Unidades = b.Unidades,
                NotificadoCaducidad = b.NotificadoCaducidad
            });
            if (b.Botella != null) bodega.Botella = DtoABotella(b.Botella);
            return bodega;
        }

        
        #endregion
    }
}
