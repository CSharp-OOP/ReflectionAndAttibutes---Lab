using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Stealer
{
    public class Spy
    {
        public MethodInfo[] StealMethodInfo(string className)
        {
            Type myClass = Type.GetType(className);

            MethodInfo[] methods = myClass.GetMethods(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.NonPublic);

            return methods;
        }

        public string CollectGettersAndSetters(string className)
        {
            MethodInfo[] privateGetters = StealMethodInfo(className)
                    .Where(x => x.Name.StartsWith("get")).ToArray();
            MethodInfo[] publicSetters = StealMethodInfo(className)
                .Where(x => x.Name.StartsWith("set")).ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var method in privateGetters)
            {
                sb.AppendLine($"{method.Name} will return {method.ReturnType}");
            }

            foreach (var method in publicSetters)
            {
                sb.AppendLine($"{method.Name} will set field of {method.GetParameters().First().ParameterType}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
