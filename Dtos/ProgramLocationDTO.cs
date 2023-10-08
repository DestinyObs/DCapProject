using System.ComponentModel.DataAnnotations;

namespace DCapProject.Dtos
{
    public class ProgramLocationDTO
    {
        //[Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        //[Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
    }

}
