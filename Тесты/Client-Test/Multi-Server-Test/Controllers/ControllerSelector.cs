using JumboServer.Controllers;
using System;
using System.Linq;
using System.Reflection;

namespace Multi_Server_Test.JumboServer.Controllers
{
    public class ControllerSelector
    {
        public Controller SelectOrDefault(string controllerName)
        {
            Type parent = typeof(Controller);
            Type[] allControllers = Assembly.GetExecutingAssembly()
                                                    .GetTypes()
                                                    .Where(type => parent.IsAssignableFrom(type) &&
                                                                !type.IsInterface &&
                                                                !type.IsAbstract).ToArray();
            if (allControllers.Length == 0)
                return null;

            foreach (var type in allControllers)
            {
                var instance = (Controller)Activator.CreateInstance(Type.GetType(type.FullName));
                if (instance.ControllerName == controllerName)
                    return instance;
            }
            return null;
        }
    }
}
