using System.ComponentModel.DataAnnotations;

namespace FormModule.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public long Mobile { get; set; }
        public string Salary { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
