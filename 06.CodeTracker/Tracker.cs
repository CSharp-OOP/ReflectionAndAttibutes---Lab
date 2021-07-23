using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AuthorProblem
{
    public class Tracker
    {
        public MethodInfo[] GetAuthors(string className)
        {
            Type myClass = Type.GetType(className);

            MethodInfo[] methods = myClass.GetMethods(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.Static);

            return methods;
        }
        public void PrintMethodsByAuthor()
        {
            Type type = typeof(StartUp);

            foreach (var method in GetAuthors(type.ToString()))
            {
                if (method.CustomAttributes.Any(n=>n.AttributeType==typeof(AuthorAttribute)))
                {
                    var attributes = method.GetCustomAttributes(false);

                    foreach (AuthorAttribute attribute in attributes)
                    {
                        Console.WriteLine($"{method.Name} is written by {attribute.Name}");
                    }
                }
            }
        }
    }
}
