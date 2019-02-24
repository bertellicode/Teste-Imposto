using System;
using AutoMapper;
using Imposto.Application.AutoMapper.CustomTypeConverter;
using Imposto.Application.ViewModels;
using Imposto.Domain.NotaFiscalAggregate.Entities;

namespace Imposto.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<PedidoViewModel, NotaFiscal>().ConvertUsing<PedidoVwToNotaFiscalConverter>();
        }

    }
}