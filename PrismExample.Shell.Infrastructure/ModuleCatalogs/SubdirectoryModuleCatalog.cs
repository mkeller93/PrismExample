using System.IO;
using Prism.Modularity;

namespace PrismExample.Shell.Infrastructure.ModuleCatalogs
{
    public class SubdirectoryModuleCatalog : DirectoryModuleCatalog
    {
        protected override void InnerLoad()
        {
            base.InnerLoad();

            DirectoryInfo[] directoryInfoArray = new DirectoryInfo(ModulePath).GetDirectories("*.*", SearchOption.AllDirectories);

            foreach (DirectoryInfo directoryInfo in directoryInfoArray)
            {
                ModulePath = directoryInfo.FullName;
                base.InnerLoad();
            }
        }
    }
}
