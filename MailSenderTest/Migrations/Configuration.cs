namespace MailSenderTest.Migrations
{
    using MailSenderTest.data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MailSenderTest.data.SongsDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MailSenderTest.data.SongsDb db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            if (!db.Songs.Any())
            {
                var songs = new Song[100];
                for(int i = 0; i < songs.Length; i++)
                {
                    songs[i] = new Song
                    {
                        Name = $"Песня {i + 1}",
                        Artist = new Artist { Name = $"Исполнитель {i + 1}" }
                    };
                }

                db.Songs.AddRange(songs);

                db.SaveChanges();
            }
        }
    }
}
