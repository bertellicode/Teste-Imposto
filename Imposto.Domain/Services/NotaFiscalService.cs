using System;
using Imposto.Domain.Entities;
using Imposto.Domain.Interfaces.Repositories;
using Imposto.Domain.Interfaces.Services;

namespace Imposto.Domain.Services
{
    public class NotaFiscalService : INotaFiscalService
    {
        private readonly INotaFiscalRepository _notaFiscalRepository;

        public NotaFiscalService(INotaFiscalRepository notaFiscalRepository)
        {
            _notaFiscalRepository = notaFiscalRepository;
        }

        public void Dispose()
        {
            _notaFiscalRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public int Salvar(NotaFiscal notaFiscal)
        {
            int id = 0;

            var gerouXml = _notaFiscalRepository.SalvarXml(notaFiscal);

            if (gerouXml)
            {
                id = _notaFiscalRepository.Salvar(notaFiscal);
            }

            return id;
        }

    }
}
