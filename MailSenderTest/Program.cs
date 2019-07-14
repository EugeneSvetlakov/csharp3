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
            int ms = DateTime.Now.Millisecond;

            using (var db = new data.SongsDb())
            {
                Console.WriteLine("Songs count - {0}", db.Songs.Count());
            }

            Pause();

            using (var db = new SongsDb())
            {
                db.Database.Log = str => Console.WriteLine("{0:T}:{1}", DateTime.Now, str);

                var song = new Song
                {
                    Name = $"Песня 5",
                    Artist = new Artist { Name = $"Исполнитель 04" }
                };

                var s1 = db.Songs.FirstOrDefault(s => (s.Name == song.Name && s.Artist.Name == song.Artist.Name ));
                var s2 = db.Songs.FirstOrDefault(s => s.Name == "Песня 6");

                if (s1 is null)
                {
                    db.Songs.Add(song);
                    db.SaveChanges();
                }
            }

            //PrintListSongs();

            Pause();

            //using (var db = new data.SongsDb())
            //{
            //    db.Database.Log = str => Console.WriteLine("{0:T}:{1}", DateTime.Now, str);
            //    var song = db.Songs.FirstOrDefault(s => s.Artist.Name == "Исполнитель 4");

            //    Pause();

            //    if (song != null)
            //        db.Songs.Remove(song);

            //    Pause();

            //    db.SaveChanges();
            //}

            //PrintListSongs();

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

            Pause();
        }

        private static void PrintListSongs()
        {
            using (var db = new data.SongsDb())
            {
                foreach (var item in db.Songs)
                {
                    Console.WriteLine($"Song {item.Name} - Artist {item.Artist.Name}");
                }
            }
        }

        private static void Pause()
        {
            Console.WriteLine("Для продолжения нажмите любую клавишу...");
            Console.ReadKey();
        }

    }
}
