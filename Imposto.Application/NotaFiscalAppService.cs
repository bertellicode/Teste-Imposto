using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Imposto.Application.Interfaces;
using Imposto.Application.ViewModels;
using Imposto.Domain.Core.Interfaces;
using Imposto.Domain.Core.Notifications;
using Imposto.Domain.NotaFiscalAggregate.DTOs;
using Imposto.Domain.NotaFiscalAggregate.Entities;
using Imposto.Domain.NotaFiscalAggregate.Interfaces.Services;
using Imposto.Infra.CrossCutting.Util;
using Imposto.Infra.Data.Interfaces;

namespace Imposto.Application
{
    public class NotaFiscalAppService : ApplicationService, INotaFiscalAppService
    {
        private readonly INotaFiscalService _notaFiscalService;
        private readonly INotaFiscalItemService _notaFiscalItemService;

        public NotaFiscalAppService(INotaFiscalService notaFiscalService,
                                    INotaFiscalItemService notaFiscalItemService,
                                    IUnitOfWork uow,
                                    INotificationHandler notificationHandler) : base(uow, notificationHandler)
        {
            _notaFiscalService = notaFiscalService;
            _notaFiscalItemService = notaFiscalItemService;
        }

        public void Dispose()
        {
            _notaFiscalService.Dispose();
            GC.SuppressFinalize(this);
        }

        public List<Notification> GerarNotaFiscal(PedidoViewModel pedido)
        {
            InitializeNotifications();

            var notaFiscal = Mapper.Map<PedidoViewModel, NotaFiscal>(pedido);

            BeginTransactionQuery();

            var validacaoRetorno = notaFiscal.Validar();

            if (validacaoRetorno)
            {
                var notaFiscalXmlDto = Mapper.Map<NotaFiscal, NotaFiscalXmlDto>(notaFiscal);
                _notaFiscalService.GerarXml(notaFiscalXmlDto);
            }

            var notaFiscalRetorno = _notaFiscalService.Salvar(notaFiscal);

            if (notaFiscalRetorno.HasValue)
            {
                foreach (NotaFiscalItem notaFiscalItem in notaFiscal.ItensDaNotaFiscal)
                {
                    notaFiscalItem.IdNotaFiscal = notaFiscalRetorno.Value;
                    _notaFiscalItemService.Salvar(notaFiscalItem);
                }
            }

            CommitQuery();

            return GetNotifications();
        }
    }
}
