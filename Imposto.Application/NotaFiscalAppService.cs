using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Imposto.Application.Interfaces;
using Imposto.Application.ViewModels;
using Imposto.Domain.Entities;
using Imposto.Domain.Interfaces.Services;
using Imposto.Infra.CrossCutting.Util;
using Imposto.Infra.Data.Interfaces;

namespace Imposto.Application
{
    public class NotaFiscalAppService : ApplicationService, INotaFiscalAppService
    {
        private readonly INotaFiscalService _notaFiscalService;
        private readonly INotaFiscalItemService _notaFiscalItemService;


        public NotaFiscalAppService(INotaFiscalService notaFiscalService
            , INotaFiscalItemService notaFiscalItemService
            , IUnitOfWork uow) : base(uow)
        {
            _notaFiscalService = notaFiscalService;
            _notaFiscalItemService = notaFiscalItemService;
        }

        public void Dispose()
        {
            _notaFiscalService.Dispose();
            GC.SuppressFinalize(this);
        }

        public List<CustomValidationResult> GerarNotaFiscal(PedidoViewModel pedido)
        {
            var validacoes = new List<CustomValidationResult>();

            var notaFiscal = Mapper.Map<PedidoViewModel, NotaFiscal>(pedido);

            BeginTransactionQuery();

            var id = _notaFiscalService.Salvar(notaFiscal);

            if (id == 0)
            {
                validacoes.Add(new CustomValidationResult("Problema ao gravar a Nota Fiscal!"));
            }

            foreach (NotaFiscalItem notaFiscalItem in notaFiscal.ItensDaNotaFiscal)
            {
                notaFiscalItem.IdNotaFiscal = id;
                if (!_notaFiscalItemService.Salvar(notaFiscalItem))
                {
                    validacoes.Add(new CustomValidationResult("Problema ao gravar um Item da Nota Fiscal!"));
                }
            }

            if (!validacoes.Any())
            {
                CommitQuery();
            }

            return validacoes;
        }
    }
}
