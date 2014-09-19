namespace SlowAndDangerous.Data
{
    using System;
    using System.Collections.Generic;
    using SlowAndDangerous.Data.Repositories;
    using SlowAndDangerous.Models;

    public class SlowAndDangerousData : ISlowAndDangerousData
    {
        private ISlowAndDangerousDbContext context;
        private IDictionary<Type, object> repositories;

        public SlowAndDangerousData()
            : this(new SlowAndDangerousDbContext())
        {
        }

        public SlowAndDangerousData(ISlowAndDangerousDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Appointment> Appointments
        {
            get
            {
                return this.GetRepository<Appointment>();
            }
        }

        public IRepository<Car> Cars
        {
            get
            {
                return this.GetRepository<Car>();
            }
        }

        public IRepository<Exam> Exams
        {
            get
            {
                return this.GetRepository<Exam>();
            }
        }

        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IRepository<City> Cities
        {
            get
            {
                return this.GetRepository<City>();
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(Repository<T>);

                //if (typeOfModel.IsAssignableFrom(typeof(Student)))
                //{
                //    type = typeof(StudentsRepository);
                //}

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }
    }
}