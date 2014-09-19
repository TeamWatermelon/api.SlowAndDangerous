namespace SlowAndDangerous.Models
{
    using System;
    using System.Collections.Generic;

    public class Exam
    {
        private ICollection<User> students;

        public Exam()
        {
            this.students = new HashSet<User>();
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public Guid InstructorId { get; set; }

        public virtual User Instructor { get; set; }

        public virtual ICollection<User> Students
        {
            get
            {
                return this.students;
            }

            set
            {
                this.students = value;
            }
        }
    }
}