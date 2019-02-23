﻿using AutoMapper;
using Imposto.Application.ViewModels;
using Imposto.Domain.Entities;

namespace Imposto.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<NotaFiscal, PedidoViewModel>();

            CreateMap<NotaFiscalItem, PedidoItemViewModel>();
        }

    }
}