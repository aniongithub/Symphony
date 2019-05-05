using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Symphony.Core
{
    public static class Api
    {
        private const string ContractName = "Symphony.Core.Instrument";

        public static IEnumerable<object> FindInstruments(this string path)
        {
            // Find and load all the DLLs in the folder 
            var assemblies = from dllFile in Directory.GetFiles(path, "*.dll")
                             let assembly = Assembly.LoadFile(dllFile)
                             where assembly != null
                             select assembly;
            // Add the loaded assemblies to the container 
            var configuration = new ContainerConfiguration()
              .WithAssemblies(assemblies);
            var container = configuration.CreateContainer();

            yield break;
            // container.GetExports();
        }

        public static T Edit<T>(this ref T t) where T: struct, IInstrument
        {
            return t;
        }
    }
}
