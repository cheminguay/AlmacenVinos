using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AlmacenVinos.Domain.Models;
using AlmacenVinos.Infraestructure.Data;
using AlmacenVinos.Infraestructure.Interfaces;
using AlmacenVinos.Infraestructure.Repositories;
using AlmacenVinos.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AlmacenVinos.Tests
{
    [TestClass]
    public class DataServicesUnitTest
    {
        private IDataService _service;
        private Mock<IBodegaRepository> _bodegaRepository;
        private Mock<INotificacionService> _notificacionService;
        private Mock<ILogService> _logService;

        [TestInitialize]
        public void SetUp()
        {
            _bodegaRepository = new Mock<IBodegaRepository>();
            _notificacionService = new Mock<INotificacionService>();
            _logService = new Mock<ILogService>();

            _service = new DataService(_bodegaRepository.Object, _notificacionService.Object, _logService.Object);
        }

        #region Vinos

        [TestMethod]
        public void GetVinosReturnCorrectType()
        {
            //Arrange
            List<Vino> list = new List<Vino>
            {
                new Vino()
                {
                    Id = It.IsAny<int>(),
                    Nombre = It.IsAny<string>(),
                    Variedad = It.IsAny<string>(),
                    Crianza = It.IsAny<string>(),
                    Denominacion = It.IsAny<string>(),
                    Color = It.IsAny<string>(),
                    Baja = false,
                    Capacidad = It.IsAny<decimal>()
                }
            };
            _bodegaRepository.Setup(x => x.GetAll<Vino>()).Returns(list.AsQueryable());
            
            //Act
            var result = _service.GetVinos(false);

            //Assert
            CollectionAssert.AllItemsAreInstancesOfType(result.ToList(), typeof(VinoDto));
        }

        [TestMethod]
        public void GetVinosNoReturnBaja()
        {
            //Arrange
            List<Vino> list = new List<Vino>
            {
                new Vino()
                {
                    Id = It.IsAny<int>(),
                    Nombre = It.IsAny<string>(),
                    Variedad = It.IsAny<string>(),
                    Crianza = It.IsAny<string>(),
                    Denominacion = It.IsAny<string>(),
                    Color = It.IsAny<string>(),
                    Baja = true,
                    Capacidad = It.IsAny<decimal>()
                }
            };
            _bodegaRepository.Setup(x => x.GetAll<Vino>()).Returns(list.AsQueryable());

            //Act
            var result = _service.GetVinos(false);

            //Assert
            Assert.AreEqual(result.Count(), 0);
        }

        [TestMethod]
        public void GetVinosReturnCorrectData()
        {
            //Arrange
            List<Vino> list = new List<Vino>
            {
                new Vino()
                {
                    Id = 5,
                    Nombre = "nombre",
                    Variedad = "variedad",
                    Crianza = "crianza",
                    Denominacion = "denominacion",
                    Color = "color",
                    Baja = false,
                    Capacidad = 0.15m
                }
            };
            _bodegaRepository.Setup(x => x.GetAll<Vino>()).Returns(list.AsQueryable());

            //Act
            var result = _service.GetVinos(false);

            //Assert
            Assert.AreEqual(result.ToList()[0].Id, 5);
            Assert.AreEqual(result.ToList()[0].Nombre, "nombre");
            Assert.AreEqual(result.ToList()[0].Variedad, "variedad");
            Assert.AreEqual(result.ToList()[0].Crianza, "crianza");
            Assert.AreEqual(result.ToList()[0].Denominacion, "denominacion");
            Assert.AreEqual(result.ToList()[0].Color, "color");
            Assert.AreEqual(result.ToList()[0].Baja, false);
            Assert.AreEqual(result.ToList()[0].Capacidad, 0.15m);
        }

        [TestMethod]
        public void GetVinoIdReturnCorrectVino()
        {
            //Arrange
            Vino vino = new Vino()
                {
                    Id = 4,
                    Nombre = "Producto 4",
                    Variedad = It.IsAny<string>(),
                    Crianza = It.IsAny<string>(),
                    Denominacion = It.IsAny<string>(),
                    Color = It.IsAny<string>(),
                    Baja = true,
                    Capacidad = It.IsAny<decimal>()
                };
            _bodegaRepository.Setup(x => x.Find<Vino>(It.IsAny<Expression<Func<Vino, bool>>>())).Returns(vino);

            //Act
            var result = _service.GetVino(4);

            //Assert
            Assert.AreEqual(result.Id, 4);
            Assert.AreNotEqual(result.Id, 5);
        }

        [TestMethod]
        public void GetVinoNombreReturnCorrectVino()
        {
            //Arrange
            Vino vino = new Vino()
            {
                Id = 4,
                Nombre = "Producto 4",
                Variedad = It.IsAny<string>(),
                Crianza = It.IsAny<string>(),
                Denominacion = It.IsAny<string>(),
                Color = It.IsAny<string>(),
                Baja = true,
                Capacidad = It.IsAny<decimal>()
            };
            _bodegaRepository.Setup(x => x.Find<Vino>(It.IsAny<Expression<Func<Vino, bool>>>())).Returns(vino);

            //Act
            var result = _service.GetVino("Producto 4");

            //Assert
            Assert.AreEqual(result.Id, 4);
            Assert.AreNotEqual(result.Id, 5);
        }

        [TestMethod]
        public void 
            AddVinoReturnCorrectVino()
        {
            //Arrange
            Vino vino = new Vino()
            {
                Nombre = "Producto 4",
                Variedad = It.IsAny<string>(),
                Crianza = It.IsAny<string>(),
                Denominacion = It.IsAny<string>(),
                Color = It.IsAny<string>(),
                Baja = false,
                Capacidad = 0.15m
            };
            
            VinoDto vinodto = new VinoDto()
            {
                Nombre = "Producto 4",
                Variedad = It.IsAny<string>(),
                Crianza = It.IsAny<string>(),
                Denominacion = It.IsAny<string>(),
                Color = It.IsAny<string>(),
                Baja = false,
                Capacidad = 0.15m
            };
            _bodegaRepository.Setup(x => x.Create(It.IsAny<Vino>())).Returns(vino);

            //Act
            var result = _service.AddVino(vinodto);

            //Assert
            Assert.AreEqual(result.Nombre, "Producto 4");
        }

        [TestMethod]
        public void
            DeleteVinoReturn1()
        {
            //Arrange
            Vino vino = new Vino()
            {
                Id = 4,
                Nombre = "Producto 4",
                Variedad = It.IsAny<string>(),
                Crianza = It.IsAny<string>(),
                Denominacion = It.IsAny<string>(),
                Color = It.IsAny<string>(),
                Baja = true,
                Capacidad = It.IsAny<decimal>()
            };
            _bodegaRepository.Setup(x => x.Find<Vino>(It.IsAny<Expression<Func<Vino, bool>>>())).Returns(vino);
            _bodegaRepository.Setup(x => x.Update<Vino>(It.IsAny<int>(), It.IsAny<Vino>())).Returns(vino);
            //Act
            var result = _service.DeleteVino(4);

            //Assert
            Assert.AreEqual(result, 1);
        }

        #endregion

        #region Botellas

        [TestMethod]
        public void GetBotellasReturnCorrectType()
        {
            //Arrange
            List<Botella> list = new List<Botella>
            {
                new Botella()
                {
                    Id = It.IsAny<int>(),
                    Descripcion = It.IsAny<string>(),
                    Añada = It.IsAny<int>(),
                    Caducidad = It.IsAny<DateTime>(),
                    Disponible = true,
                    IdVino = It.IsAny<int>(),
                    Vino = new Vino()
                        {
                            Id = It.IsAny<int>(),
                            Nombre = It.IsAny<string>(),
                            Variedad = It.IsAny<string>(),
                            Crianza = It.IsAny<string>(),
                            Denominacion = It.IsAny<string>(),
                            Color = It.IsAny<string>(),
                            Baja = false,
                            Capacidad = It.IsAny<decimal>()
                        }
                }
            };
            _bodegaRepository.Setup(x => x.GetAll<Botella>()).Returns(list.AsQueryable());

            //Act
            var result = _service.GetBotellas();

            //Assert
            CollectionAssert.AllItemsAreInstancesOfType(result.ToList(), typeof(BotellaDto));
        }

        [TestMethod]
        public void GetBotellasNoReturnNoDisponible()
        {
            //Arrange
            List<Botella> list = new List<Botella>
            {
                new Botella()
                {
                    Id = It.IsAny<int>(),
                    Descripcion = It.IsAny<string>(),
                    Añada = It.IsAny<int>(),
                    Caducidad = It.IsAny<DateTime>(),
                    Disponible = false,
                    IdVino = It.IsAny<int>(),
                    Vino = new Vino()
                        {
                            Id = It.IsAny<int>(),
                            Nombre = It.IsAny<string>(),
                            Variedad = It.IsAny<string>(),
                            Crianza = It.IsAny<string>(),
                            Denominacion = It.IsAny<string>(),
                            Color = It.IsAny<string>(),
                            Baja = false,
                            Capacidad = It.IsAny<decimal>()
                        }
                }
            };
            _bodegaRepository.Setup(x => x.GetAll<Botella>()).Returns(list.AsQueryable());

            //Act
            var result = _service.GetBotellas(false, true);

            //Assert
            Assert.AreEqual(result.Count(), 0);
        }

        [TestMethod]
        public void GetBotellasReturnOnlyCaducas()
        {
            //Arrange
            List<Botella> list = new List<Botella>
            {
                new Botella()
                {
                    Id = It.IsAny<int>(),
                    Descripcion = It.IsAny<string>(),
                    Añada = It.IsAny<int>(),
                    Caducidad = DateTime.Today.AddDays(5),
                    Disponible = true,
                    IdVino = It.IsAny<int>(),
                    Vino = new Vino()
                        {
                            Id = It.IsAny<int>(),
                            Nombre = It.IsAny<string>(),
                            Variedad = It.IsAny<string>(),
                            Crianza = It.IsAny<string>(),
                            Denominacion = It.IsAny<string>(),
                            Color = It.IsAny<string>(),
                            Baja = false,
                            Capacidad = It.IsAny<decimal>()
                        }
                },
                new Botella()
                {
                    Id = It.IsAny<int>(),
                    Descripcion = It.IsAny<string>(),
                    Añada = It.IsAny<int>(),
                    Caducidad = DateTime.Today.AddDays(-5),
                    Disponible = true,
                    IdVino = It.IsAny<int>(),
                    Vino = new Vino()
                        {
                            Id = It.IsAny<int>(),
                            Nombre = It.IsAny<string>(),
                            Variedad = It.IsAny<string>(),
                            Crianza = It.IsAny<string>(),
                            Denominacion = It.IsAny<string>(),
                            Color = It.IsAny<string>(),
                            Baja = false,
                            Capacidad = It.IsAny<decimal>()
                        }
                }
            };
            _bodegaRepository.Setup(x => x.GetAll<Botella>()).Returns(list.AsQueryable());

            //Act
            var result = _service.GetBotellas(true);

            //Assert
            Assert.AreEqual(result.Count(), 1);
        }

        [TestMethod]
        public void GetBotellaIdReturnCorrectBotella()
        {
            //Arrange
            Botella botella = new Botella()
            {
                Id = 4,
                Descripcion = It.IsAny<string>(),
                Añada = It.IsAny<int>(),
                Caducidad = It.IsAny<DateTime>(),
                Disponible = true,
                IdVino = It.IsAny<int>(),
                Vino = new Vino()
                {
                    Id = It.IsAny<int>(),
                    Nombre = It.IsAny<string>(),
                    Variedad = It.IsAny<string>(),
                    Crianza = It.IsAny<string>(),
                    Denominacion = It.IsAny<string>(),
                    Color = It.IsAny<string>(),
                    Baja = false,
                    Capacidad = It.IsAny<decimal>()
                }
            };
            _bodegaRepository.Setup(x => x.Find<Botella>(It.IsAny<Expression<Func<Botella, bool>>>())).Returns(botella);

            //Act
            var result = _service.GetBotella(4);

            //Assert
            Assert.AreEqual(result.Id, 4);
            Assert.AreNotEqual(result.Id, 5);
        }

        public void GetBotellaDescripcionReturnCorrectBotella()
        {
            //Arrange
            Botella botella = new Botella()
            {
                Id = 4,
                Descripcion = "Botella 4",
                Añada = It.IsAny<int>(),
                Caducidad = It.IsAny<DateTime>(),
                Disponible = true,
                IdVino = It.IsAny<int>(),
                Vino = new Vino()
                {
                    Id = It.IsAny<int>(),
                    Nombre = It.IsAny<string>(),
                    Variedad = It.IsAny<string>(),
                    Crianza = It.IsAny<string>(),
                    Denominacion = It.IsAny<string>(),
                    Color = It.IsAny<string>(),
                    Baja = false,
                    Capacidad = It.IsAny<decimal>()
                }
            };
            _bodegaRepository.Setup(x => x.Find<Botella>(It.IsAny<Expression<Func<Botella, bool>>>())).Returns(botella);

            //Act
            var result = _service.GetBotella("Botella 4");

            //Assert
            Assert.AreEqual(result.Descripcion, "Botella 4");
        }

        [TestMethod]
        public void
            AddBotellaReturnCorrectBotella()
        {
            //Arrange
            Botella botella = new Botella()
            {
                Descripcion = "Botella 4",
                Añada = It.IsAny<int>(),
                Caducidad = It.IsAny<DateTime>(),
                Disponible = true,
                IdVino = It.IsAny<int>(),
                Vino = new Vino()
                {
                    Id = It.IsAny<int>(),
                    Nombre = It.IsAny<string>(),
                    Variedad = It.IsAny<string>(),
                    Crianza = It.IsAny<string>(),
                    Denominacion = It.IsAny<string>(),
                    Color = It.IsAny<string>(),
                    Baja = false,
                    Capacidad = It.IsAny<decimal>()
                }
            };

            BotellaDto botelladto = new BotellaDto()
            {
                Descripcion = "Botella 4",
                Añada = It.IsAny<int>(),
                Caducidad = It.IsAny<DateTime>(),
                Disponible = true,
                IdVino = It.IsAny<int>(),
                Vino = new VinoDto()
                {
                    Id = It.IsAny<int>(),
                    Nombre = It.IsAny<string>(),
                    Variedad = It.IsAny<string>(),
                    Crianza = It.IsAny<string>(),
                    Denominacion = It.IsAny<string>(),
                    Color = It.IsAny<string>(),
                    Baja = false,
                    Capacidad = It.IsAny<decimal>()
                }
            };

            _bodegaRepository.Setup(x => x.Create(It.IsAny<Botella>())).Returns(botella);

            //Act
            var result = _service.AddBotella(botelladto);

            //Assert
            Assert.AreEqual(result.Descripcion, "Botella 4");
        }

        [TestMethod]
        public void
            DeleteBotellaReturn1()
        {
            //Arrange
            Botella botella = new Botella()
            {
                Descripcion = "Botella 4",
                Añada = It.IsAny<int>(),
                Caducidad = It.IsAny<DateTime>(),
                Disponible = true,
                IdVino = It.IsAny<int>(),
                Vino = new Vino()
                {
                    Id = It.IsAny<int>(),
                    Nombre = It.IsAny<string>(),
                    Variedad = It.IsAny<string>(),
                    Crianza = It.IsAny<string>(),
                    Denominacion = It.IsAny<string>(),
                    Color = It.IsAny<string>(),
                    Baja = false,
                    Capacidad = It.IsAny<decimal>()
                }
            };
            _bodegaRepository.Setup(x => x.Find<Botella>(It.IsAny<Expression<Func<Botella, bool>>>())).Returns(botella);
            _bodegaRepository.Setup(x => x.Update<Botella>(It.IsAny<int>(), It.IsAny<Botella>())).Returns(botella);
            //Act
            var result = _service.DeleteBotella(4);

            //Assert
            Assert.AreEqual(result, 1);
        }

        #endregion

        #region Bodega

        [TestMethod]
        public void NotificaCaducadosReturnTrue()
        {
            //Arrange
            List<Bodega> list = new List<Bodega>
            {
                new Bodega()
                {
                    Id = It.IsAny<int>(),
                    NotificadoCaducidad = false,
                    Unidades = It.IsAny<int>(),
                    Botella = new Botella()
                    {
                        Descripcion = "Botella 4",
                        Añada = It.IsAny<int>(),
                        Caducidad = DateTime.Today.AddMonths(-2),
                        Disponible = true,
                        IdVino = It.IsAny<int>(),
                        Vino = new Vino()
                        {
                            Id = It.IsAny<int>(),
                            Nombre = It.IsAny<string>(),
                            Variedad = It.IsAny<string>(),
                            Crianza = It.IsAny<string>(),
                            Denominacion = It.IsAny<string>(),
                            Color = It.IsAny<string>(),
                            Baja = false,
                            Capacidad = It.IsAny<decimal>()
                        }
                    }
                }
            };
            
            _bodegaRepository.Setup(x => x.GetAll<Bodega>()).Returns(list.AsQueryable());
            _bodegaRepository.Setup(x => x.Update<Bodega>(It.IsAny<int>(), It.IsAny<Bodega>())).Returns(list[0]);
            _notificacionService.Setup(x => x.NotificacionExtraido(It.IsAny<int>(), It.IsAny<Bodega>()));

            //Act
            var result = _service.NotificaCaducados();

            //Assert
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void GetBodegaReturnCorrectType()
        {
            //Arrange
            List<Bodega> list = new List<Bodega>
            {
                new Bodega()
                {
                    Id = It.IsAny<int>(),
                    NotificadoCaducidad = It.IsAny<bool>(),
                    Unidades = It.IsAny<int>(),
                    Botella = new Botella()
                    {
                        Descripcion = "Botella 4",
                        Añada = It.IsAny<int>(),
                        Caducidad = It.IsAny<DateTime>(),
                        Disponible = true,
                        IdVino = It.IsAny<int>(),
                        Vino = new Vino()
                        {
                            Id = It.IsAny<int>(),
                            Nombre = It.IsAny<string>(),
                            Variedad = It.IsAny<string>(),
                            Crianza = It.IsAny<string>(),
                            Denominacion = It.IsAny<string>(),
                            Color = It.IsAny<string>(),
                            Baja = false,
                            Capacidad = It.IsAny<decimal>()
                        }
                    }
                }
            };

            _bodegaRepository.Setup(x => x.GetAll<Bodega>()).Returns(list.AsQueryable());

            //Act
            var result = _service.GetBodegas();

            //Assert
            CollectionAssert.AllItemsAreInstancesOfType(result.ToList(), typeof(BodegaDto));
        }

        [TestMethod]
        public void GetBodegaIdReturnCorrectBodega()
        {
            //Arrange
            Bodega bodega = new Bodega()
            {
                Id = 4,
                NotificadoCaducidad = It.IsAny<bool>(),
                Unidades = It.IsAny<int>(),
                Botella = new Botella()
                {
                    Id = 4,
                    Descripcion = "Botella 4",
                    Añada = It.IsAny<int>(),
                    Caducidad = It.IsAny<DateTime>(),
                    Disponible = true,
                    IdVino = It.IsAny<int>(),
                    Vino = new Vino()
                    {
                        Id = It.IsAny<int>(),
                        Nombre = It.IsAny<string>(),
                        Variedad = It.IsAny<string>(),
                        Crianza = It.IsAny<string>(),
                        Denominacion = It.IsAny<string>(),
                        Color = It.IsAny<string>(),
                        Baja = false,
                        Capacidad = It.IsAny<decimal>()
                    }
                }
            };
            _bodegaRepository.Setup(x => x.Find<Bodega>(It.IsAny<Expression<Func<Bodega, bool>>>())).Returns(bodega);

            //Act
            var result = _service.GetBodega(4);

            //Assert
            Assert.AreEqual(result.Id, 4);
            Assert.AreNotEqual(result.Id, 5);
        }

        [TestMethod]
        public void
            AddBodegaReturnCorrectBodega()
        {
            //Arrange
            Bodega bodega = new Bodega()
            {
                NotificadoCaducidad = It.IsAny<bool>(),
                Unidades = It.IsAny<int>(),
                Botella = new Botella()
                {
                    Id = 4,
                    Descripcion = "Botella 4",
                    Añada = It.IsAny<int>(),
                    Caducidad = It.IsAny<DateTime>(),
                    Disponible = true,
                    IdVino = It.IsAny<int>(),
                    Vino = new Vino()
                    {
                        Id = It.IsAny<int>(),
                        Nombre = It.IsAny<string>(),
                        Variedad = It.IsAny<string>(),
                        Crianza = It.IsAny<string>(),
                        Denominacion = It.IsAny<string>(),
                        Color = It.IsAny<string>(),
                        Baja = false,
                        Capacidad = It.IsAny<decimal>()
                    }
                }
            };

            BodegaDto bodegadto = new BodegaDto()
            {
                NotificadoCaducidad = It.IsAny<bool>(),
                Unidades = 25,
                Botella = new BotellaDto()
                {
                    Id = 4,
                    Descripcion = "Botella 4",
                    Añada = It.IsAny<int>(),
                    Caducidad = It.IsAny<DateTime>(),
                    Disponible = true,
                    IdVino = It.IsAny<int>(),
                    Vino = new VinoDto()
                    {
                        Id = It.IsAny<int>(),
                        Nombre = It.IsAny<string>(),
                        Variedad = It.IsAny<string>(),
                        Crianza = It.IsAny<string>(),
                        Denominacion = It.IsAny<string>(),
                        Color = It.IsAny<string>(),
                        Baja = false,
                        Capacidad = It.IsAny<decimal>()
                    }
                }
            };
            _bodegaRepository.Setup(x => x.Create(It.IsAny<Bodega>())).Returns(bodega);

            //Act
            var result = _service.AddBodega(bodegadto);

            //Assert
            Assert.AreEqual(result.Unidades, 25);
        }

        [TestMethod]
        public void SumaBodegaMayorUnidades()
        {
            //Arrange
            Bodega bodega = new Bodega()
            {
                Id = 4,
                NotificadoCaducidad = It.IsAny<bool>(),
                Unidades = 5,
                Botella = new Botella()
                {
                    Id = 4,
                    Descripcion = "Botella 4",
                    Añada = It.IsAny<int>(),
                    Caducidad = It.IsAny<DateTime>(),
                    Disponible = true,
                    IdVino = It.IsAny<int>(),
                    Vino = new Vino()
                    {
                        Id = It.IsAny<int>(),
                        Nombre = It.IsAny<string>(),
                        Variedad = It.IsAny<string>(),
                        Crianza = It.IsAny<string>(),
                        Denominacion = It.IsAny<string>(),
                        Color = It.IsAny<string>(),
                        Baja = false,
                        Capacidad = It.IsAny<decimal>()
                    }
                }
            };
            _bodegaRepository.Setup(x => x.Find<Bodega>(It.IsAny<Expression<Func<Bodega, bool>>>())).Returns(bodega);
            _bodegaRepository.Setup(x => x.Update<Bodega>(It.IsAny<int>(), It.IsAny<Bodega>())).Returns(bodega);
            _notificacionService.Setup(x => x.NotificacionIngresado(It.IsAny<int>(), It.IsAny<Bodega>()));
            _logService.Setup(x => x.LogInventario(It.IsAny<string>()));
            //Act
            var result = _service.SumaBodega(4);

            //Assert
            Assert.AreEqual(result, 6);
        }

        [TestMethod]
        public void RestaBodegaMenorUnidades()
        {
            //Arrange
            Bodega bodega = new Bodega()
            {
                Id = 4,
                NotificadoCaducidad = It.IsAny<bool>(),
                Unidades = 5,
                Botella = new Botella()
                {
                    Id = 4,
                    Descripcion = "Botella 4",
                    Añada = It.IsAny<int>(),
                    Caducidad = It.IsAny<DateTime>(),
                    Disponible = true,
                    IdVino = It.IsAny<int>(),
                    Vino = new Vino()
                    {
                        Id = It.IsAny<int>(),
                        Nombre = It.IsAny<string>(),
                        Variedad = It.IsAny<string>(),
                        Crianza = It.IsAny<string>(),
                        Denominacion = It.IsAny<string>(),
                        Color = It.IsAny<string>(),
                        Baja = false,
                        Capacidad = It.IsAny<decimal>()
                    }
                }
            };
            _bodegaRepository.Setup(x => x.Find<Bodega>(It.IsAny<Expression<Func<Bodega, bool>>>())).Returns(bodega);
            _bodegaRepository.Setup(x => x.Update<Bodega>(It.IsAny<int>(), It.IsAny<Bodega>())).Returns(bodega);
            _bodegaRepository.Setup(x => x.Delete<Bodega>(It.IsAny<Bodega>())).Returns(1);
            _notificacionService.Setup(x => x.NotificacionExtraido(It.IsAny<int>(), It.IsAny<Bodega>()));
            _logService.Setup(x => x.LogInventario(It.IsAny<string>()));
            //Act
            var result = _service.RestaBodega(4);

            //Assert
            Assert.AreEqual(result, 4);
        }

        [TestMethod]
        public void RestaBodegaUnidades0Delete()
        {
            //Arrange
            Bodega bodega = new Bodega()
            {
                Id = 4,
                NotificadoCaducidad = It.IsAny<bool>(),
                Unidades = 1,
                Botella = new Botella()
                {
                    Id = 4,
                    Descripcion = "Botella 4",
                    Añada = It.IsAny<int>(),
                    Caducidad = It.IsAny<DateTime>(),
                    Disponible = true,
                    IdVino = It.IsAny<int>(),
                    Vino = new Vino()
                    {
                        Id = It.IsAny<int>(),
                        Nombre = It.IsAny<string>(),
                        Variedad = It.IsAny<string>(),
                        Crianza = It.IsAny<string>(),
                        Denominacion = It.IsAny<string>(),
                        Color = It.IsAny<string>(),
                        Baja = false,
                        Capacidad = It.IsAny<decimal>()
                    }
                }
            };
            _bodegaRepository.Setup(x => x.Find<Bodega>(It.IsAny<Expression<Func<Bodega, bool>>>())).Returns(bodega);
            _bodegaRepository.Setup(x => x.Update<Bodega>(It.IsAny<int>(), It.IsAny<Bodega>())).Returns(bodega);
            _bodegaRepository.Setup(x => x.Delete<Bodega>(It.IsAny<Bodega>())).Returns(1);
            _notificacionService.Setup(x => x.NotificacionExtraido(It.IsAny<int>(), It.IsAny<Bodega>()));
            _logService.Setup(x => x.LogInventario(It.IsAny<string>()));
            //Act
            var result = _service.RestaBodega(4);

            //Assert
            Assert.AreEqual(result, 0);
        }

        #endregion
    }
}
