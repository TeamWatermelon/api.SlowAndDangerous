namespace SlowAndDangerous.WebAPI.Models
{
    using SlowAndDangerous.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    public class ExamModel
    {
        public static Expression<Func<Exam, ExamModel>> FromExam
        {
            get
            {
                return a => new ExamModel
                {
                    Id = a.Id,
                    Date = a.Date,
                    Instructor = a.Instructor.UserName
                };
            }
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Instructor { get; set; }
    }
}