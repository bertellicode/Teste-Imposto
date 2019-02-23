using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Imposto.Application.AutoMapper;
using Imposto.Infra.Ioc;
using SimpleInjector;

namespace TesteImposto
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AutoMapperConfig.RegisterMappings();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormImposto());
        }
    }
}
