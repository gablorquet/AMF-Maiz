using System.Data.Entity;
using AMF.Core.Migrations;
using AMF.Core.Storage;
using Ninject.Modules;

namespace AMF.Core
{
    public class CoreNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<Configuration>()
                .ToSelf();

            Kernel.Bind<DbContext>()
                .To<AMFDbContext>();

            Kernel.Bind<ISession>()
                .To<EntitySession>();

        }
    }

}