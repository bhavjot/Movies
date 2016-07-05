using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA.Framework.Autofac.Interfaces
{
    public interface IFileSystem
    {
        IEnumerable<string> GetFiles(string directory, string searchPattern, SearchOption searchOption);
    }
}
