using System;
using System.Collections.Generic;
using DjnCommon.Exceptions;

namespace DjnCommon.Attributes
{
    /// <summary>
    /// Defines any class that can be auto-discovered and loaded.
    /// Class must have a constructor with zero required parameters.
    /// </summary>
    public class Discoverable : Attribute
    {
        /// <summary>
        /// Discovers and instantiates the specified
        /// type within the specified namespace
        /// </summary>
        /// <param name="ns">The namespace to search within</param>
        /// <typeparam name="T">Type type to look for</typeparam>
        /// <returns>An array of objects</returns>
        public static T[] DiscoverType<T>(string ns)
        {
            Type callingType = typeof(T);
            Type[] allTypes = callingType.Assembly.GetTypes();
            List<T> finalList = new List<T>();
            foreach (var type in allTypes)
            {
                if (!type.IsSubclassOf(callingType)) continue;
                if (type.Namespace == null) continue;
                if (!type.Namespace.Equals(ns)) continue;
                DjnLogging.GetLogger().Debug("Discovered type {type}", type.ToString());
                
                // Instantiate
                var constr = type.GetConstructor(new Type[] { });
                if (constr == null)
                {
                    throw new InvalidDiscoverable(type);
                }

                object x = constr.Invoke(new object[] { });
                finalList.Add((T)x);
            }

            return finalList.ToArray();
        }
    }
}