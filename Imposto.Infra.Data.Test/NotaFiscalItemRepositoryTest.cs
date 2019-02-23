using System;
using Imposto.Domain.Entities;
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
        private NotaFiscalItemRepository _notaFiscalItemRepository;

        public NotaFiscalItemRepositoryTest()
        {
            container = new Container();
            BootStrapper.RegisterServices(container);
            container.Verify();

            _notaFiscalItemRepository = container.GetInstance<NotaFiscalItemRepository>();
        }

        /// <summary>
        /// Testa o método responsável por salvar o item da nota fiscal.
        /// </summary>
        [TestMethod]
        public void Atualizar()
        {
            var idTeste = 1011;

            notaFiscalItem = _notaFiscalItemRepository.GetById(idTeste);
           
            var novoCodigoProduto = Guid.NewGuid().ToString("N").Substring(0, 12);

            notaFiscalItem.CodigoProduto = novoCodigoProduto;

            _notaFiscalItemRepository.Salvar(notaFiscalItem);

            var notaFiscalItemAtualizada = _notaFiscalItemRepository.GetById(idTeste);

            Assert.AreEqual(novoCodigoProduto, notaFiscalItemAtualizada.CodigoProduto, "Record is not updated.");
        }
    }
}
