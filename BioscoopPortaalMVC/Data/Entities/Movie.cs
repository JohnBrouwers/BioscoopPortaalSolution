using System;

namespace BioscoopPortaalMVC.Data.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public TimeSpan Duration { get; set; }

        public int DirectorId { get; set; }

        public Director Director { get; set; }
    }
}
