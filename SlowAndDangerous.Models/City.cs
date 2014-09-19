namespace SlowAndDangerous.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class City
    {
        private ICollection<Appointment> appointments;

        public City()
        {
            this.appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Appointment> Appointments
        {
            get
            {
                return this.appointments;
            }

            set
            {
                this.appointments = value;
            }
        }
    }
}