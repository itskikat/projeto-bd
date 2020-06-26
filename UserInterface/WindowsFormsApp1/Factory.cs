using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Services;

namespace Factorys
{
    class ServiceFactory
    {
        private static Dictionary<String,Service> services = new Dictionary<String, Service>();
        public static Service GetInstance(String ServiceName)
        {
            String fullName = "Services." + ServiceName;
            if (services.ContainsKey(fullName))
            {
                return services[fullName];
            }
            Type type = Type.GetType(fullName);
            Service service = (Service) Activator.CreateInstance(type);
            services.Add(fullName, service);
            return service;
        }
        
    }
}
