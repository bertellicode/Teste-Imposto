using System;
using Imposto.Domain.NotaFiscalAggregate.Entities;
using Imposto.Domain.NotaFiscalAggregate.Interfaces.Repositories;
using Imposto.Domain.NotaFiscalAggregate.Interfaces.Services;

namespace Imposto.Domain.NotaFiscalAggregate.Services
{
    public class NotaFiscalItemService : INotaFiscalItemService
    {
        private readonly INotaFiscalItemRepository _notaFiscalItemRepository;

        public NotaFiscalItemService(INotaFiscalItemRepository notaFiscalItemRepository)
        {
            _notaFiscalItemRepository = notaFiscalItemRepository;
        }

        public void Dispose()
        {
            _notaFiscalItemRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public bool Salvar(NotaFiscalItem notaFiscalItem)
        {
            return _notaFiscalItemRepository.Salvar(notaFiscalItem); ;
        }

    }
}
