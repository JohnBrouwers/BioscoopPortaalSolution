using BioscoopPortaalMVC.Models.ValidationMessages;
using System.ComponentModel.DataAnnotations;

namespace BioscoopPortaalMVC.Models
{
    public class MovieUpSertViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ValidationMessage.Required)]

        //[StringLength(100, ErrorMessage = ValidationMessage.)]
        public string Name { get; set; }

        [StringLength(500, "")]
        public string Description { get; set; }
    }
}
