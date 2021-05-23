using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace JumboServer.Functions
{
    public class CollectionSynchronizator
    {
        private ServerReportsModule serverEvents = new ServerReportsModule();
        public async Task<List<T>> SynchronizeCollection<T>(List<T> collection, string dirPath, string fileName)
        {
            try
            {
                if(collection != null)
                {
                    serverEvents.BlockReport("Collection was loaded successful", ConsoleColor.Green);
                    if (File.Exists($"{dirPath}/{fileName}"))
                    {
                        using (var writer = new StreamWriter($"{dirPath}/{fileName}"))
                        {
                            var dataJson = JsonConvert.SerializeObject(collection);
                            await writer.WriteAsync(dataJson);
                            serverEvents.BlockReport("Collection saved", ConsoleColor.Green);
                        }
                    }
                    else
                    {
                        serverEvents.BlockReport("Reserve dir not found! Creating new dir...", ConsoleColor.Red);
                        Directory.CreateDirectory(dirPath);
                        using (var stream = File.CreateText($"{dirPath}/{fileName}"))
                        {
                            var dataJson = JsonConvert.SerializeObject(collection);
                            await stream.WriteAsync(dataJson);
                            serverEvents.BlockReport("Collection saved", ConsoleColor.Green);
                        }
                    }
                }
                return collection;
            }
            catch (NullReferenceException)
            {
                serverEvents.BlockReport("News cannot be retrieved from the database!", ConsoleColor.Red);
                serverEvents.BlockReport($"Trying execute load from ./{dirPath}/...", ConsoleColor.Yellow);
                using (var reader = new StreamReader($"{dirPath}/{fileName}"))
                {
                    var reserveCollectionJson = reader.ReadToEnd();
                    var reserveCollection = JsonConvert.DeserializeObject<List<T>>(reserveCollectionJson);
                    return reserveCollection;
                }
            }
        }
    }
}
