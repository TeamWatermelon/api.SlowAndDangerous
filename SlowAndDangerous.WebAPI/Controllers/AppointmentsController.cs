namespace SlowAndDangerous.WebAPI.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using SlowAndDangerous.Data;
    using SlowAndDangerous.WebAPI.Models;
    using SlowAndDangerous.Models;

    [Authorize]
    [EnableCors("*", "*", "*")]
    public class AppointmentsController : ApiController
    {
        private ISlowAndDangerousData data;

        public AppointmentsController()
            : this(new SlowAndDangerousData())
        {
        }

        public AppointmentsController(ISlowAndDangerousData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            return Ok(this.data.Appointments.All());
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var result = this.data.Appointments.Find(id);
            if (result == null)
            {
                return this.BadRequest("There is no such appointment.");
            }

            return this.Ok(result);
        }

        [HttpPost]
        public IHttpActionResult Create(AppointmentModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var instructorId = this.data.Users.All().Where(c => c.UserName == model.Instructor).Select(i => i.Id).FirstOrDefault();
            if (instructorId == null)
            {
                return this.BadRequest("There is no such instructor.");
            }

            var studentId = this.data.Users.All().Where(c => c.UserName == model.Student).Select(s => s.Id).FirstOrDefault();

            var city = this.data.Cities.All().Where(c => c.Name == model.City).FirstOrDefault();
            if (city == null)
            {
                return this.BadRequest("There is no such city.");
            }

            var car = this.data.Cars.All().Where(c => c.Number == model.CarNumber).FirstOrDefault();
            if (car == null)
            {
                return this.BadRequest("There is no such car.");
            }


            var appointment = new Appointment()
            {
                Status = Status.Open,
                Date = model.Date,
                InstructorId = instructorId,
                StudentId = studentId,
                CityId = city.Id,
                CarId = car.Id
            };

            this.data.Appointments.Add(appointment);
            this.data.SaveChanges();

            model.Id = appointment.Id;

            return this.Ok(model);
        }

        public IHttpActionResult Update(int id, AppointmentModel appointment)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingAppointment = this.data.Appointments.All().FirstOrDefault(a => a.Id == id);
            if (existingAppointment == null)
            {
                return BadRequest("Such appointment does not exist!");
            }

            var city = data.Cities.All().FirstOrDefault(c => c.Name == appointment.City);
            if (city == null)
            {
                return BadRequest("Such city does not exist!");
            }

            var student = data.Users.All().FirstOrDefault(c => c.UserName == appointment.Student);
            if (student == null)
            {
                return BadRequest("Such student does not exist!");
            }

            var instructor = data.Users.All().FirstOrDefault(u => u.UserName == appointment.Instructor);
            if (instructor == null)
            {
                return BadRequest("Such instructor does not exist!");
            }


            existingAppointment.City = city;
            existingAppointment.Date = appointment.Date;
            existingAppointment.Status = appointment.Status;
            existingAppointment.Student = student;
            existingAppointment.Instructor = instructor;
            this.data.SaveChanges();

            appointment.Id = id;
            return Ok(appointment);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingAppointment = this.data.Appointments.All().FirstOrDefault(a => a.Id == id);
            if (existingAppointment == null)
            {
                return BadRequest("Such appointment does not exist!");
            }

            this.data.Appointments.Delete(existingAppointment);
            this.data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddStudent(int appointmentId, string username)
        {
            var appointment = this.data.Appointments.All().FirstOrDefault(a => a.Id == appointmentId);
            if (appointment == null)
            {
                return BadRequest("Such appointment does not exist - invalid id!");
            }

            var student = this.data.Users.All().FirstOrDefault(b => b.UserName == username);
            if (student == null)
            {
                return BadRequest("Such student does not exist - invalid id!");
            }

            appointment.Student = student;
            this.data.SaveChanges();

            return Ok();
        }

    }
}