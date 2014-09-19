namespace SlowAndDangerous.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;
    using SlowAndDangerous.Data.Migrations;
    using SlowAndDangerous.Models;

    public class SlowAndDangerousDbContext : IdentityDbContext<User>, ISlowAndDangerousDbContext
    {
        public SlowAndDangerousDbContext()
            : base("SlowAndDangerousConnectionSQL", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SlowAndDangerousDbContext, Configuration>());
        }

        public IDbSet<Appointment> Appointments { get; set; }

        public IDbSet<Car> Cars { get; set; }

        public IDbSet<City> Cities { get; set; }

        public IDbSet<Exam> Exams { get; set; }

        public static SlowAndDangerousDbContext Create()
        {
            return new SlowAndDangerousDbContext();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}