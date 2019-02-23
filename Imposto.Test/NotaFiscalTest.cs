using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Imposto.Data;
using Imposto.Domain;

namespace Imposto.Test
{
    /// <summary>
    /// Summary description for NotaFiscalTest
    /// </summary>
    [TestClass]
    public class NotaFiscalTest
    {
        public NotaFiscalTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void SalvarXmlTest()
        {
            NotaFiscalRepository notaFiscalRepository = new NotaFiscalRepository();

            NotaFiscal notaFiscal = new NotaFiscal(){
                Id = 1,
                NumeroNotaFiscal = 1,
                Serie = 1,
                NomeCliente = "TESTE 0",
                EstadoDestino = "SP",
                EstadoOrigem = "RJ"
            };

            notaFiscalRepository.CreateXML(notaFiscal);

            //notaFiscalRepository.SalvarXml(notaFiscal);

            //NotaFiscal notaFiscalList = notaFiscalRepository.CarregarXml().FirstOrDefault();

        }
    }
}
