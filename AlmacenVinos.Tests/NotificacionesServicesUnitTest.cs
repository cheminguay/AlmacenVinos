using AlmacenVinos.Services;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlmacenVinos.Infraestructure.Data;

namespace AlmacenVinos.Tests
{
    [TestClass]
    public class NotificacionesServicesUnitTest
    {
        private INotificacionService _service;

        [TestInitialize]
        public void SetUp()
        {
            _service = new NotificacionService();
        }

        [TestMethod]
        public void NotificacionCaducidadNotThrow()
        {
            //Arrange
            Bodega bodega = new Bodega() { Botella = new Botella() { Caducidad = DateTime.Today, Vino = new Vino() { Nombre = "nombre" } } };
            try
            {
                _service.NotificacionCaducidad(bodega);
                Assert.IsTrue(true);
            }
            catch 
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void NotificacionExtraidoNotThrow()
        {
            //Arrange
            Bodega bodega = new Bodega() { Botella = new Botella() { Caducidad = DateTime.Today, Vino = new Vino() { Nombre = "nombre" } } };
            try
            {
                _service.NotificacionExtraido(1, bodega);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void NotificacionIngresadoNotThrow()
        {
            //Arrange
            Bodega bodega = new Bodega() { Botella = new Botella() { Caducidad = DateTime.Today, Vino = new Vino() { Nombre = "nombre" } } };
            try
            {
                _service.NotificacionIngresado(1, bodega);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
    }
}
