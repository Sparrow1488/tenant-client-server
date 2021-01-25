using System;
using System.Threading.Tasks;

namespace Server_Test
{
    public class Program
    {
        static string ip = "127.0.0.1";
        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Server launch");
            Console.ForegroundColor = ConsoleColor.White;

            ServerPorts server_8090 = new ServerPorts(ip, 8090, "server90");
            ServerPorts server_8080 = new ServerPorts(ip, 8080, "server80");

            while (true)
            {
                await WriteCommands();
            }

        }
        public static async Task WriteCommands()
        {
            await Task.Run(() =>
            {
                Console.Write(">");
                string command = Console.ReadLine();
            });
        }
    }
}
