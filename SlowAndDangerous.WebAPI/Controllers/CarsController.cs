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
    public class CarsController : ApiController
    {
        private ISlowAndDangerousData data;

        public CarsController()
            : this(new SlowAndDangerousData())
        {

        }

        public CarsController(ISlowAndDangerousData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var cars = this.data
                .Cars
                .All()
                .Select(CarModel.FromCar);

            return Ok(cars);
        }

        [HttpPost]
        public IHttpActionResult Create(CarModel car)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCar = new Car
            {
                Model = car.Model,
                Manufacturer = car.Manufacturer,
            };

            this.data.Cars.Add(newCar);
            this.data.SaveChanges();

            car.Id = newCar.Id;
            return Ok(car);
        }

        public IHttpActionResult Update(int id, CarModel car)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCar = this.data.Cars.All().FirstOrDefault(a => a.Id == id);
            if (existingCar == null)
            {
                return BadRequest("Such car does not exists!");
            }

            existingCar.Model = car.Model;
            existingCar.Manufacturer = car.Manufacturer;
            this.data.SaveChanges();

            car.Id = id;
            return Ok(car);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingCar = this.data.Cars.All().FirstOrDefault(a => a.Id == id);
            if (existingCar == null)
            {
                return BadRequest("Such car does not exist!");
            }

            this.data.Cars.Delete(existingCar);
            this.data.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IHttpActionResult AddAppointment(int carId, int appointmentId)
        {
            var car = this.data.Cars.All().FirstOrDefault(a => a.Id == carId);
            if (car == null)
            {
                return BadRequest("Such car does not exist - invalid id!");
            }

            var appointment = this.data.Appointments.All().FirstOrDefault(b => b.Id == appointmentId);
            if (appointment == null)
            {
                return BadRequest("Such appointment does not exist - invalid id!");
            }

            car.Appointments.Add(appointment);
            this.data.SaveChanges();

            return Ok();
        }
    }
}