using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Stealer
{
    public class Spy
    {
        public FieldInfo[] StealFieldInfo(string className)
        {
            Type myClass = Type.GetType(className);

            FieldInfo[] fieldInfos = myClass.GetFields(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.Static);

            return fieldInfos;
        }

        public MethodInfo[] StealMethodsInfo(string className)
        {
            Type myClass = Type.GetType(className);

            MethodInfo[] methodsInfo = myClass.GetMethods(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.Static);

            return methodsInfo;
        }

        public string AnalyzeAccessModifiers(string className)
        {
            FieldInfo[] fields = StealFieldInfo("Stealer."+className);

            MethodInfo[] privateGetters = StealMethodsInfo("Stealer." + className)
                .Where(x => !x.IsPublic && x.Name.StartsWith("get")).ToArray();
            MethodInfo[] publicSetters = StealMethodsInfo("Stealer." + className)
                .Where(x => !x.IsPrivate && x.Name.StartsWith("set")).ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var field in fields)
            {
                sb.AppendLine($"{field.Name} must be private!");
            }

            foreach (var method in privateGetters)
            {
                sb.AppendLine($"{method.Name} have to be public!");
            }

            foreach (var method in publicSetters)
            {
                sb.AppendLine($"{method.Name} have to be private!");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
