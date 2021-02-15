using System;
using System.Threading.Tasks;
using Multi_Server_Test.ServerData;
using Multi_Server_Test.Server.Packages;
using System.Collections.Generic;
using System.IO;

namespace Multi_Server_Test
{
    public class Program
    {
        //public static List<News> newsList = new List<News>()
        //{
        //    new News("Регулярные выражения", "Регулярное выражение (regular expression или regex, или regexp) — это последовательность символов, которая определяет шаблон. Шаблон может состоять из литералов, чисел, символов, операторов или конструкций. Шаблон используется для поиска соответствий в строке или файле"),
        //    new News("Паттерн", "Па́ттерн или регулярность — схема-образ, действующая как посредствующее представление, или чувственное понятие, благодаря которому в режиме одновременности восприятия и мышления выявляются закономерности, как они существуют в природе и обществе. Паттерн понимается в этом плане как повторяющийся шаблон или образец"),
        //    new News("Интерфейсы", "Интерфейс представляет ссылочный тип, который может определять некоторый функционал - набор методов и свойств без реализации. Затем этот функционал реализуют классы и структуры, которые применяют данные интерфейсы.")
        //};
        static async Task Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.White;
            MyServer myServer = new MyServer("127.0.0.1", 8090);
            await myServer.Start();
            
            //TODO: сделать отправку сообщений председателю
            //TODO: сделать клиент председателя
            myServer.ServeAndResponseToClient();
            Console.WriteLine("Server disconnect.");
        }
        
    }
    //var bytesImage1 = await File.ReadAllBytesAsync(@"C:\Users\Dom\Desktop\Репозитории\tenant-client-server\Тесты\Client-Test\Multi-Server-Test\Refs\image1.jpg");
    //Console.WriteLine(bytesImage1);
    //var news1 = new News("Windows Presentation Foundation", "Windows Presentation Foundation — аналог WinForms, система для построения клиентских приложений Windows с визуально привлекательными возможностями взаимодействия с пользователем, графическая подсистема в составе .NET Framework, использующая язык XAML");
    //news1.Image = bytesImage1;
    //newsList.Add(news1);

    //MemoryStream ms = new MemoryStream(news1.Image);
    //Bitmap returnImage = (Bitmap)Bitmap.FromStream(ms);
    //Console.WriteLine(returnImage.Width);

    //await myServer.AddNewsCollection(new NewsCollection(newsList));
}

