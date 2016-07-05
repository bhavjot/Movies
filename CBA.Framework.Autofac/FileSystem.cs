using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Framework.Autofac.Interfaces;

namespace CBA.Framework.Autofac
{
    public class FileSystem:IFileSystem
    {
        public IEnumerable<string> GetFiles(string directory, string searchPattern, SearchOption searchOption)
        {
            return Directory.EnumerateFiles(directory, searchPattern, searchOption);
        }
    }
}
