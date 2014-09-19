namespace SlowAndDangerous.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using SlowAndDangerous.Data;
    using SlowAndDangerous.Models;
    using SlowAndDangerous.WebAPI.Models;

    [Authorize]
    [EnableCors("*", "*", "*")]
    public class ExamController : ApiController
    {
        private ISlowAndDangerousData data;

        public ExamController()
            : this(new SlowAndDangerousData())
        {

        }

        public ExamController(ISlowAndDangerousData data)
        {
            this.data = data;
        }

        [HttpGet]

        public IHttpActionResult All()
        {
            var exams = this.data
                .Exams
                .All()
                .Select(ExamModel.FromExam);

            return Ok(exams);
        }

        [HttpPost]
        public IHttpActionResult Create(ExamModel exam)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instructor = data.Users.All().FirstOrDefault(u => u.UserName == exam.Instructor);

            if (instructor == null)
            {
                return BadRequest("Such instructor does not exist!");
            }

            var newExam = new Exam
            {
                Date = exam.Date,
                Instructor = instructor,

            };

            this.data.Exams.Add(newExam);
            this.data.SaveChanges();

            exam.Id = newExam.Id;
            return Ok(exam);
        }

        public IHttpActionResult Update(int examId, ExamModel exam)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingExam = this.data.Exams.All().FirstOrDefault(a => a.Id == examId);
            if (existingExam == null)
            {
                return BadRequest("Such exam does not exist!");
            }

            var instructor = data.Users.All().FirstOrDefault(u => u.UserName == exam.Instructor);

            if (instructor == null)
            {
                return BadRequest("Such instructor does not exist!");
            }

            existingExam.Instructor = instructor;
            existingExam.Date = exam.Date;
            this.data.SaveChanges();

            exam.Id = examId;
            return Ok(exam);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingExam = this.data.Exams.All().FirstOrDefault(a => a.Id == id);
            if (existingExam == null)
            {
                return BadRequest("Such exam does not exist!");
            }

            this.data.Exams.Delete(existingExam);
            this.data.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IHttpActionResult AddStudent(int examId, string username)
        {
            var exam = this.data.Exams.All().FirstOrDefault(a => a.Id == examId);
            if (exam == null)
            {
                return BadRequest("Such exam does not exist - invalid id!");
            }

            var student = this.data.Users.All().FirstOrDefault(b => b.UserName == username);
            if (student == null)
            {
                return BadRequest("Such appointment does not exist - invalid id!");
            }

            exam.Students.Add(student);
            this.data.SaveChanges();

            return Ok();
        }
    }
}