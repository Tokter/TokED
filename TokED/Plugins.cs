using Autofac;
using Autofac.Core;
using Autofac.Integration.Mef;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TokED.Properties;

namespace TokED
{
    public class Plugins
    {
        public static IContainer Container
        {
            get
            {
                if (_container == null)
                {
                    throw new NullReferenceException(Resources.LoadPluginsNotCalled);
                }
                return _container;
            }
        }
        private static IContainer _container = null;
        private static AggregateCatalog _catalog = new AggregateCatalog();

        public static string PluginPath
        {
            get
            {
                return System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);
            }
        }

        public static void LoadPlugins()
        {
            if (_container == null)
            {
                string[] plugins = Directory.GetFiles(PluginPath, "Plugin*.dll");

                //Load plugins
                foreach (var plugin in plugins)
                {
                    var assembly = Assembly.LoadFile(plugin);
                    _catalog.Catalogs.Add(new AssemblyCatalog(assembly));
                }
                _catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

                try
                {
                    var builder = new ContainerBuilder();
                    builder.RegisterComposablePartCatalog(_catalog);
                    _container = builder.Build();
                }
                catch (Exception ex)
                {
                    if (ex is System.Reflection.ReflectionTypeLoadException)
                    {
                        var typeLoadException = ex as ReflectionTypeLoadException;
                        var loaderExceptions = typeLoadException.LoaderExceptions;
                    }

                    throw ex;
                }

            }
        }

        public static List<string> GetKeys<T>()
        {
            // We're going to find each service which was registered
            // with a key, and for those which match the type T we'll store the key
            // and later supplement the default output with individual resolve calls to those
            // keyed services
            var allKeys = new List<string>();
            foreach (var componentRegistration in Plugins.Container.ComponentRegistry.Registrations)
            {
                // Get the services which match the KeyedService type
                var typedServices = componentRegistration.Services.Where(x => x is KeyedService).Cast<KeyedService>();
                // Add the key to our list so long as the registration is for the correct type T
                allKeys.AddRange(typedServices.Where(y => y.ServiceType == typeof(T)).Select(x => x.ServiceKey.ToString()));
            }
            return allKeys;
        }

        public static IDictionary<string, object> GetMetadata<T>(string name)
        {
            return Plugins.Container.ComponentRegistry.Registrations
                .SelectMany(r => r.Services.Where(x => x is KeyedService).Cast<KeyedService>(), (r, s) => new { r, s })
                .Where(rs => rs.s.ServiceType == typeof(T) && rs.s.ServiceKey.ToString() == name)
                .Select(rs => rs.r).First().Metadata;
        }
    }
}
