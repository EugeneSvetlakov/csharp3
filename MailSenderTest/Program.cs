using MailSenderTest.data;
using System;
using System.Linq;
using System.Threading;

namespace MailSenderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var db = new data.SongsDb())
            {
                Console.WriteLine("Songs count - {0}",db.Songs.Count());
            }

            using( var db = new SongsDb())
            {
                if (!db.Songs.Any())
                {
                    var songs = new Song[100];
                    for (int i = 0; i < songs.Length; i++)
                    {
                        songs[i] = new Song
                        {
                            Name = $"Песня {i + 1}",
                            Artist = new Artist { Name = $"Исполнитель {i}" }
                        };
                    }
                    db.Songs.AddRange(songs);
                    db.SaveChanges();
                }
            }

            using (var db = new data.SongsDb())
            {
                foreach (var item in db.Songs)
                {
                    Console.WriteLine($"Song {item.Name} - Artist {item.Artist.Name}");
                }
            }

            //ThreadTest.Start();

            //ThreadSyncronizationTest.Start();

            //var threads = new Thread[10];
            //for (var i = 0; i < threads.Length; i++)
            //{
            //    int i0 = i;
            //    threads[i] = new Thread(() => Console.WriteLine($"Сообщение №{i0}"));
            //}
            //for (var i = 0; i < threads.Length; i++)
            //{
            //    threads[i].Start();
            //}

            Console.WriteLine("Для завершения нажмите любую кнопку...");
            Console.ReadKey();
        }

        
    }
}
