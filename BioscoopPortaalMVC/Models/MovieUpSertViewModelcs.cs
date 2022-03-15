using BioscoopPortaalMVC.Data.Entities;
using BioscoopPortaalMVC.Models.ValidationMessages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BioscoopPortaalMVC.Models
{
    public class MovieUpSertViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ValidationMessage.Required)]
        [StringLength(100, ErrorMessage = ValidationMessage.MaxLength)]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = ValidationMessage.MaxLength)]
        public string Description { get; set; }

        public DateTime? ReleaseDate { get; set; }
        public TimeSpan Duration { get; set; }

        public int DirectorId { get; set; }


        //SelectList DirectorList = new SelectList(_context.Directors, "Id", nameof(Director.Name));
        public SelectList DirectorsList { 
            get { 
                return Directors == null ? null : new SelectList(Directors, "Id", nameof(Director.Name)); 
            } 
        }

        private IEnumerable<Director> _directors;

        public IEnumerable<Director> Directors
        {
            get { return _directors; }
            set { _directors = value; }
        }

        public MovieUpSertViewModel(IEnumerable<Director> directors)
        {
            this.Directors = directors;
        }

        public MovieUpSertViewModel() : this(null)
        {
        }
    }
}
