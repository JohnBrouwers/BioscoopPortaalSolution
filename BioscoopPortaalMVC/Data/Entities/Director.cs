using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioscoopPortaalMVC.Data.Entities
{
    public class Director
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}