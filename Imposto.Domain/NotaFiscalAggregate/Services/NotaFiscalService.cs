using System;
using Imposto.Domain.Core.Interfaces;
using Imposto.Domain.Core.Services;
using Imposto.Domain.NotaFiscalAggregate.DTOs;
using Imposto.Domain.NotaFiscalAggregate.Entities;
using Imposto.Domain.NotaFiscalAggregate.Interfaces.Repositories;
using Imposto.Domain.NotaFiscalAggregate.Interfaces.Services;

namespace Imposto.Domain.NotaFiscalAggregate.Services
{
    public class NotaFiscalService : Service, INotaFiscalService
    {
        private readonly INotaFiscalRepository _notaFiscalRepository;

        public NotaFiscalService(INotaFiscalRepository notaFiscalRepository,
                                    INotificationHandler notificationHandler) : base(notificationHandler)
        {
            _notaFiscalRepository = notaFiscalRepository;
        }

        public void Dispose()
        {
            _notaFiscalRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public bool GerarXml(NotaFiscalXmlDto notaFiscal)
        {
            var gerouXml = _notaFiscalRepository.SalvarXml(notaFiscal);

            if (!gerouXml)
                NotificarValidacao(errorMessage: "Erro ao gerar XML da Nota Fiscal!");

            return gerouXml;
        }

        public int? Salvar(NotaFiscal notaFiscal)
        {
            var retorno = (int?)null;

            if (!notaFiscal.EhValido)
            {
                NotificarValidacoesErro(notaFiscal.ValidationResult);
                return retorno;
            }

            if (!HasNotifications())
            {
                retorno = _notaFiscalRepository.Salvar(notaFiscal);

                if (!retorno.HasValue)
                    NotificarValidacao(errorMessage: "Erro ao persistir Nota Fiscal!");
            }

            return retorno;
        }

    }
}
