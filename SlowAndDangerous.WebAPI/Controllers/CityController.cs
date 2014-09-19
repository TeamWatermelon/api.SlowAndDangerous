namespace SlowAndDangerous.WebAPI.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Models;
    using SlowAndDangerous.Data;
    using SlowAndDangerous.Models;

    [Authorize]
    [EnableCors("*", "*", "*")]
    public class CityController : ApiController
    {
        private ISlowAndDangerousData data;

        public CityController()
            : this(new SlowAndDangerousData())
        {
        }

        public CityController(ISlowAndDangerousData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var cities = this.data
                .Cities
                .All();

            return Ok(cities);
        }

        [HttpPost]
        public IHttpActionResult Create(CityModel city)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCity = new City
            {
                Name = city.Name,
            };

            this.data.Cities.Add(newCity);
            this.data.SaveChanges();

            city.Id = newCity.Id;
            return Ok(city);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, CityModel city)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCity = this.data.Cities.All().FirstOrDefault(a => a.Id == id);
            if (existingCity == null)
            {
                return BadRequest("Such city does not exists!");
            }

            existingCity.Name = city.Name;
            this.data.SaveChanges();

            city.Id = id;
            return Ok(city);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingCity = this.data.Cities.All().FirstOrDefault(a => a.Id == id);
            if (existingCity == null)
            {
                return BadRequest("Such city does not exists!");
            }

            this.data.Cities.Delete(existingCity);
            this.data.SaveChanges();

            return Ok();
        }
    }
}