using Imposto.Application;
using Imposto.Application.Interfaces;
using Imposto.Domain.Interfaces.Repositories;
using Imposto.Domain.Interfaces.Services;
using Imposto.Domain.Services;
using Imposto.Infra.Data.Contexto;
using Imposto.Infra.Data.Interfaces;
using Imposto.Infra.Data.Repositories;
using Imposto.Infra.Data.UoW;
using SimpleInjector;

namespace Imposto.Infra.Ioc
{
    public class BootStrapper
    {
        /// <summary>
        /// Método responsável por resgistrar os serviços gerenciados pelo Simple Injector. 
        /// </summary>
        /// <param name="container">Container responsável por carregar as configurações.</param>
        public static void RegisterServices(Container container)
        {
            //App
            container.Register<INotaFiscalAppService, NotaFiscalAppService>(Lifestyle.Singleton);

            //ServiceDomain
            container.Register<INotaFiscalService, NotaFiscalService>(Lifestyle.Singleton);
            container.Register<INotaFiscalItemService, NotaFiscalItemService>(Lifestyle.Singleton);

            //Infra Dados
            container.Register(typeof(IRepositoryBase<>), typeof(RepositoryBase<>), Lifestyle.Singleton);
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Singleton);
            container.Register<TesteImpostoContext>(Lifestyle.Singleton);

            container.Register<INotaFiscalRepository, NotaFiscalRepository>(Lifestyle.Singleton);
            container.Register<INotaFiscalItemRepository, NotaFiscalItemRepository>(Lifestyle.Singleton);

        }
    }
}
