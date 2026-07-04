using System.ComponentModel.DataAnnotations;

namespace mymainproject.Models
{
   
        public class CouseModel
    {
        public int Id { get; set; }

        [Required]
        public string CourseName { get; set; }

        public string Description { get; set; }
    }
}
