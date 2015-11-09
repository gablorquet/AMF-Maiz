using System.Web.Http.Dependencies;
using Ninject;

namespace AMF.Core.DI
{
    public class NinjectWebApiDependencyResolver : NinjectScope, IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectWebApiDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            _kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectScope(_kernel.BeginBlock());
        }
    }
}