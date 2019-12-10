namespace TeisterMask.Data
{
    using Microsoft.EntityFrameworkCore;
    using TeisterMask.Data.Models;

    public class TeisterMaskContext : DbContext
    {
        public TeisterMaskContext() { }

        public TeisterMaskContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<EmployeeTask> EmployeeTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeTask>()
                .HasKey(et => new { et.EmployeeId, et.TaskId });

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeesTasks)
                .WithOne(gt => gt.Employee)
                .HasForeignKey(gt => gt.EmployeeId);

            modelBuilder.Entity<Task>()
                .HasMany(t => t.EmployeesTasks)
                .WithOne(gt => gt.Task)
                .HasForeignKey(gt => gt.EmployeeId);
        }
    }
}