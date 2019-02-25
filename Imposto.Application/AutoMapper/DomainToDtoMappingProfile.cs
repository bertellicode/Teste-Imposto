using AutoMapper;
using Imposto.Application.ViewModels;
using Imposto.Domain.NotaFiscalAggregate.DTOs;
using Imposto.Domain.NotaFiscalAggregate.Entities;

namespace Imposto.Application.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<NotaFiscal, NotaFiscalXmlDto>();

            CreateMap<NotaFiscalItem, NotaFiscalItemXmlDto>();
        }

    }
}