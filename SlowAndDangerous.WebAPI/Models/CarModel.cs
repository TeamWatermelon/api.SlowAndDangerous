namespace SlowAndDangerous.WebAPI.Models
{
    using SlowAndDangerous.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    public class CarModel
    {
        public static Expression<Func<Car, CarModel>> FromCar
        {
            get
            {
                return a => new CarModel
                {
                    Id = a.Id,
                    Model = a.Model,
                    Manufacturer = a.Manufacturer
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Number { get; set; }
    }
}