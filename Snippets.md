Deserialize json:

### 1. Правят се DTO-та, ако има нужда
### 2. json се налива в колекция (невалидирана)

var allDepartments = JsonConvert.DeserializeObject<DepartmentDto[]>(jsonString);

??  DepartmentDto[] deserializedGames = JsonConvert.DeserializeObject<DepartmentDto[]>(jsonString);

### 3. var validDepartments = new List<Department>(); -- Това, което си е в моята база
var sb = new StringBuilder();
    
    
### 4. 
var isValid = isValid(department) && department.Cells.All(x => isValid(x));

foreach (var department in allDepartments) {
    var isValid = isValid(department) && department.Cells.All(isValid);

    if (isValid){
        // Добави този department към validDepartments;
        validDepartments.Add(department;)
        // Append SuccessMessage
        sb.AppendLine(съобщението);
        }
        else{
        // Грешни съобщения
        sb.AppendLine("Invalid Data!");
        }
    }

### 5. var isValid = isValid(department) && department.Cells.All(isValid); --> изцикля колекцията Cells
ili
var isValid = isValid(department) && department.Cells.All(x => isValid(x));


## Аки имаме DateTime трябва да го направим Required в DTO-to, защото в DTO е string, а string-a e nullable
DateTime? - nullable
DateTime - required

При нестнат масив в json си правим ново DTO и си го лсгаме като колекция в Главното DTO.
DTOFather and DTOChild

public class DTOFather{
-----------
---------
---------
public DTOChild[] Childs {get; set;}

}

foreach (var dto in fathersDtoAllElementsFromJson) {
    var isValid = isValid(dto) && dto.Childs.All(isValid);

    if (isValid){
        // Правим си new Father{
        FullName = dto.FullName;
        -------------
        --------
        ---
        // ako e data
        propData = DateTime.ParseExact(dto.propData, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        ако е null (от стринга) проверяваме дали е null
        }
        // Добави този department към validDepartments;
        validDepartments.Add(dto;)
        // Append SuccessMessage
        sb.AppendLine(съобщението);
        }
        else{
        // Грешни съобщения
        sb.AppendLine("Invalid Data!");
        }
    }

var isValid = isValid(department) && department.Cells.All(x => isValid(x));


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