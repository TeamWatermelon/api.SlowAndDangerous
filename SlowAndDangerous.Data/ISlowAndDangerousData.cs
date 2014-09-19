namespace SlowAndDangerous.Data
{
    using SlowAndDangerous.Data.Repositories;
    using SlowAndDangerous.Models;

    public interface ISlowAndDangerousData
    {
		IRepository<Appointment> Appointments { get; }        IRepository<Car> Cars { get; }

        IRepository<City> Cities { get; }

        IRepository<Exam> Exams { get; }

        IRepository<User> Users { get; }

        void SaveChanges();
    }
}