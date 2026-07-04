using mymainproject.Data.Migrations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mymainproject.Models
{
   
        public class Student
        {
            public int Id { get; set; }

            // Basic Info
            public string Name { get; set; }
            public int Age { get; set; }

            // New Fields
            public string Email { get; set; }
            public string Phone { get; set; }

            // Changed from House → Address
            public string Address { get; set; }

            // Academic Info
            public int CourseId { get; set; }

        public CouseModel? Course { get; set; }

        [NotMapped]
        public List<CouseModel>? Courses { get; set; }

        public int Year { get; set; }

            // Optional
            public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
