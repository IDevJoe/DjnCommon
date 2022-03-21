using System;

namespace DjnCommon.Exceptions
{
    public class InvalidDiscoverable : Exception
    {
        public InvalidDiscoverable(Type t) : base("The Discoverable "+t.FullName+" does not contain a valid constructor, and cannot be instantiated.")
        {
            
        }
    }
}