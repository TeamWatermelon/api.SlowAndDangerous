namespace SlowAndDangerous.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }
        
        [Required]
        public string InstructorId { get; set; }

        [ForeignKey("InstructorId")]
        public virtual User Instructor { get; set; }

        public string StudentId { get; set; }

        [ForeignKey("StudentId")]
        public virtual User Student { get; set; }

        public int CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}