using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace SwipeJob.Model.EF
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("name=DBConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<JobSeekerTempProfile> JobSeekerTempProfiles { get; set; }

        public virtual DbSet<CompanyHisotry> CompanyHisotries { get; set; }

        public virtual DbSet<Employer> Employers { get; set; }

        public virtual DbSet<Inbox> Inboxes { get; set; }

        public virtual DbSet<Feedback> Feedbacks { get; set; }

        public virtual DbSet<Job> Jobs { get; set; }

        public virtual DbSet<JobSeeker> JobSeekers { get; set; }

        public virtual DbSet<Language> Languages { get; set; }

        public virtual DbSet<Applicant> Applicants { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Industry> Industries { get; set; }

        public virtual DbSet<Location> Locations { get; set; }

        public virtual DbSet<Education> Educations { get; set; }

        // ----------------------------------------------------------------------------------------------------

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var exceptionMessage = GetValidationExceptionMessage(ex);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public override Task<int> SaveChangesAsync()
        {
            try
            {
                return base.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var exceptionMessage = GetValidationExceptionMessage(ex);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        private string GetValidationExceptionMessage(DbEntityValidationException ex)
        {
            var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors)
                                                         .Select(x => x.ErrorMessage);
            // Join the list to a single string.
            var fullErrorMessage = string.Join("; ", errorMessages);

            // Combine the original exception message with the new one.
            var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
            return exceptionMessage;
        }
    }
}
