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

    }
}