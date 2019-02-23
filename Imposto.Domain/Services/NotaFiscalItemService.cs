using System;
using Imposto.Domain.Entities;
using Imposto.Domain.Interfaces.Repositories;
using Imposto.Domain.Interfaces.Services;

namespace Imposto.Domain.Services
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
