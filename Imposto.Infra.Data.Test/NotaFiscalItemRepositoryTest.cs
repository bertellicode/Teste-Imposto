using System;
using System.Linq;
using Imposto.Domain.NotaFiscalAggregate.Entities;
using Imposto.Domain.NotaFiscalAggregate.Interfaces.Repositories;
using Imposto.Infra.Data.Repositories;
using Imposto.Infra.Ioc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;

namespace Imposto.Infra.Data.Test
{
    /// <summary>
    /// Responsável pelos testes unitários da classe NotaFiscalItemRepository.
    /// </summary>
    [TestClass]
    public class NotaFiscalItemRepositoryTest
    {
        static Container container;
        private NotaFiscalItem notaFiscalItem;
        private INotaFiscalItemRepository _notaFiscalItemRepository;
        private INotaFiscalRepository _notaFiscalRepository;

        public NotaFiscalItemRepositoryTest()
        {
            container = new Container();
            BootStrapper.RegisterServices(container);
            container.Verify();

            PopularNotaFiscalItem();

            _notaFiscalItemRepository = container.GetInstance<NotaFiscalItemRepository>();
            _notaFiscalRepository = container.GetInstance<NotaFiscalRepository>();
        }

        private void PopularNotaFiscalItem()
        {
            notaFiscalItem = new NotaFiscalItem
            {
                NomeProduto = Guid.NewGuid().ToString(),
                CodigoProduto = Guid.NewGuid().ToString("N").Substring(0, 12),
                BaseIcms = 100,
                BaseCalculoIpi = 100,
                AliquotaIpi = (decimal)0.10
            };
        }

        /// <summary>
        /// Testa o método responsável por salvar o item da nota fiscal.
        /// </summary>
        [TestMethod]
        public void Salvar()
        {
            var notaFiscal = _notaFiscalRepository.GetAll().FirstOrDefault();

            notaFiscal.AdicionarItemDaNotaFiscal(notaFiscalItem, false);

            var salvo = _notaFiscalItemRepository.Salvar(notaFiscalItem);

            Assert.AreEqual(true, salvo, "Criação de um novo Item de Nota Fiscal retorna um valor verdadeiro.");
        }
    }
}
