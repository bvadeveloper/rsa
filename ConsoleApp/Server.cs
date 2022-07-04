using System;
using System.Text;
using Autofac;
using ConsoleApp.Commands;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp
{
    public class Server : IDisposable
    {
        private readonly IConfiguration _configuration;

        private IContainer _container;

        public Server()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddEnvironmentVariables();

            _configuration = configurationBuilder.Build();
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _container.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void RegisterComponents(Action<ContainerBuilder> customRegistration)
        {
            var builder = new ContainerBuilder();
            builder.Register(context => _configuration).As<IConfiguration>().SingleInstance();

            customRegistration(builder); // register commands

            _container = builder.Build();
        }

        public void RunAndBlock()
        {
            var (e, d, n) = _container.Resolve<IKeyMaker>().Make();

            Console.WriteLine("Keys: " +
                              $"{Environment.NewLine} " +
                              $"e: {e}{Environment.NewLine} " +
                              $"d: {d}{Environment.NewLine} " +
                              $"n: {n}{Environment.NewLine}");

            Console.WriteLine("Waiting for input...");

            var b = Encoding.UTF8.GetBytes(Console.ReadLine());

            var eb = _container.Resolve<IEncryptProvider>().Encrypt(e, n, b); // return encrypted bytes

            Console.WriteLine($"Encrypted message: {Encoding.UTF8.GetString(eb)} {Environment.NewLine}");

            var db = _container.Resolve<IDecryptProvider>().Decrypt(d, n, eb); // return decrypted bytes

            var dm = Encoding.UTF8.GetString(db);

            Console.WriteLine($"Decrypted message: {dm}");
        }
    }
}
