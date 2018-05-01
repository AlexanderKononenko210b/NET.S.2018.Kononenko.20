using LabExam.Interfaces;
using LabExam.Service;
using Ninject;

namespace LabExam
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            kernel.Bind<IPrinterManager>().To<PrintManagerService>();
            kernel.Bind<ILogger>().To<Logger>();
        }
    }
}
