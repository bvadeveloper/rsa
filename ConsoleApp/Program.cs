using System;
using Autofac;
using ConsoleApp.Commands;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello RSA!");

            using (var server = new Server())
            {
                server.RegisterComponents(builder =>
                {
                    builder.RegisterType<KeyMaker>().As<IKeyMaker>();
                    builder.RegisterType<EncryptProvider>().As<IEncryptProvider>();
                    builder.RegisterType<DecryptProvider>().As<IDecryptProvider>();
                });

                server.RunAndBlock();
            }
        }
    }
}