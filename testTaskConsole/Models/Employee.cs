namespace testTaskConsole.Models;

public sealed class Employee
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public int? ChiefId { get; set; }
    public string Name { get; set; }
    public int Salary { get; set; }
}