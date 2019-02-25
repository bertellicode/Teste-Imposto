using System;
using Imposto.Domain.Core.Interfaces;
using Imposto.Domain.Core.Services;
using Imposto.Domain.NotaFiscalAggregate.Entities;
using Imposto.Domain.NotaFiscalAggregate.Interfaces.Repositories;
using Imposto.Domain.NotaFiscalAggregate.Interfaces.Services;

namespace Imposto.Domain.NotaFiscalAggregate.Services
{
    public class NotaFiscalItemService : Service, INotaFiscalItemService
    {
        private readonly INotaFiscalItemRepository _notaFiscalItemRepository;

        public NotaFiscalItemService(INotaFiscalItemRepository notaFiscalItemRepository,
                                        INotificationHandler notificationHandler) : base(notificationHandler)
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
            var retorno = _notaFiscalItemRepository.Salvar(notaFiscalItem);

            if (!retorno)
                NotificarValidacao(errorMessage: "Erro ao Salvar um item da Nota Fiscal!");

            return retorno;
        }

    }
}
