using System;
using HasAIDependency;
using NoAIDependency;

namespace AppInsigthsSeparationSpike
{
    class Program
    {
        static void Main(string[] args)
        {
            var testing = new ActivityTesting();
            testing.W3CExample();
            testing.HttpCorrelationExample();
            var testingOld = new ActivityTestingOld();
            testingOld.W3CExample();
            testingOld.HttpCorrelationExample();
        }
    }
}
