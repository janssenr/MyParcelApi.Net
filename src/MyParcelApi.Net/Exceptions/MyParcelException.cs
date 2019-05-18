using System;

namespace MyParcelApi.Net.Exceptions
{
    public class MyParcelException : Exception
    {
        public MyParcelException(string message) : base(message) { }
    }
}
