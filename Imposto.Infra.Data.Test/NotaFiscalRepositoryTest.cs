using System;
using System.Collections.Generic;
using Imposto.Domain.NotaFiscalAggregate.DTOs;
using Imposto.Domain.NotaFiscalAggregate.Entities;
using Imposto.Domain.NotaFiscalAggregate.Interfaces.Repositories;
using Imposto.Infra.Data.Repositories;
using Imposto.Infra.Ioc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;

namespace Imposto.Infra.Data.Test
{
    /// <summary>
    /// Responsável pelos testes unitários da classe NotaFiscalRepository.
    /// </summary>
    [TestClass]
    public class NotaFiscalRepositoryTest
    {
        static Container container;
        private NotaFiscal notaFiscal;
        private NotaFiscalXmlDto notaFiscalXmlDto;
        private INotaFiscalRepository _notaFiscalRepository;

        public NotaFiscalRepositoryTest()
        {
            container = new Container();
            BootStrapper.RegisterServices(container);
            container.Verify();

            PopularNotaFiscal();
            PopularNotaFiscalXmlDto();

            _notaFiscalRepository = container.GetInstance<NotaFiscalRepository>();
        }

        /// <summary>
        /// Popula a nota fiscal usada nos métodos
        /// </summary>
        private void PopularNotaFiscal()
        {
            notaFiscal = new NotaFiscal
            {
                Id = 0,
                NumeroNotaFiscal = 999999,
                Serie = new Random().Next(Int32.MaxValue),
                NomeCliente = "TESTE MOCK",
                EstadoDestino = "SP",
                EstadoOrigem = "RJ",
                ItensDaNotaFiscal = new List<NotaFiscalItem>()
            };
        }

        /// <summary>
        /// Popula nota fiscal usada no método de gerar XML
        /// </summary>
        private void PopularNotaFiscalXmlDto()
        {
            notaFiscalXmlDto = new NotaFiscalXmlDto()
            {
                Id = 0,
                NumeroNotaFiscal = 999999,
                Serie = new Random().Next(Int32.MaxValue),
                NomeCliente = "TESTE MOCK",
                EstadoDestino = "SP",
                EstadoOrigem = "RJ",
            };

            NotaFiscalItemXmlDto notaFiscalItem = new NotaFiscalItemXmlDto()
            {
                NomeProduto = "Produto teste mock",
                CodigoProduto = "123-5548-555-00",
                BaseIcms = 100,
                BaseCalculoIpi = 100,
                AliquotaIpi = (decimal)0.10
            };

            notaFiscalXmlDto.ItensDaNotaFiscal.Add(notaFiscalItem);
        }

        /// <summary>
        /// Testa o método responsável por salvar a nota fiscal.
        /// </summary>
        [TestMethod]
        public void Salvar()
        {
            var id = _notaFiscalRepository.Salvar(notaFiscal);

            Assert.AreNotEqual(0, id, "Criação de um novo registro retorna ID diferente de zero.");
        }

        /// <summary>
        /// Testa o métdodo responsável por gerar o XML.
        /// </summary>
        [TestMethod]
        public void SalvarXmlTest()
        {
            var salvo = _notaFiscalRepository.SalvarXml(notaFiscalXmlDto);

            Assert.AreEqual(true, salvo, "Criação de um novo arquivo XML retorna um valor verdadeiro.");
        }
    }
}
