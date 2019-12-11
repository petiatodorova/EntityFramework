# Decimal

[Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
public decimal Price { get; set; }

# StringLength

[StringLength(30, MinimumLength = 3)]
public string Name { get; set; }

# Range
[Range(1, int.MaxValue)]
public int Quantity { get; set; }


[Key]
public int Id { get; set; }

[Required]
[MinLength(3), MaxLength(40)]
[RegularExpression(@"^[A-Z]+\d*(?![a-z])$|^[a-z]+\d*(?![A-Z])$")]
//Should contain only lower or upper case letters and/or digits. (required)
public string Username { get; set; }

[Required]
[EmailAddress]
public string Email { get; set; }

[Required]
[RegularExpression(@"^\d{3}-\d{3}-\d{4}$")]
public string Phone { get; set; }
public ICollection<EmployeeTask> EmployeesTasks { get; set; } = new HashSet<EmployeeTask>();
    

# ManyToMany

## public class EmployeeTask
    {
        [Required]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        [Required]
        public int TaskId { get; set; }

        public Task Task { get; set; }
    }
    
## public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(40)]
        [RegularExpression(@"^[A-Z]+\d*(?![a-z])$|^[a-z]+\d*(?![A-Z])$")]
        //Should contain only lower or upper case letters and/or digits. (required)
        public string Username { get; set; }

        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$")]
        public string Phone { get; set; }

        public ICollection<EmployeeTask> EmployeesTasks { get; set; } = new HashSet<EmployeeTask>();
    }    
    
    
## public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2), MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public DateTime OpenDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public ExecutionType ExecutionType { get; set; }

        [Required]
        public LabelType LabelType { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public ICollection<EmployeeTask> EmployeesTasks { get; set; } = new HashSet<EmployeeTask>();
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