using AlmacenVinos.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmacenVinos.Tests
{
    [TestClass]
    public class LogServiceUnitTest
    {
        private ILogService _service;

        [TestInitialize]
        public void SetUp()
        {
            _service = new LogService();
        }

        [TestMethod]
        public void LogExceptionNotThrow()
        {
            //Arrange
            Exception ex = new Exception();
            try
            {
                _service.LogException(ex);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void LogDbEntityValidationExceptionNotThrow()
        {
            //Arrange
            DbEntityValidationException ex = new DbEntityValidationException();
            try
            {
                _service.LogDbEntityValidationException(ex);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void LogInventarioNotThrow()
        {
            //Arrange
            try
            {
                _service.LogInventario("aaaaa");
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
    }
}
