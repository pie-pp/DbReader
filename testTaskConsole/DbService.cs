namespace testTaskConsole;

public sealed class DbService
{
    private readonly DataBaseContext _dataBaseContext;
    
    public DbService(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public Dictionary<string, int> GetSumSalaryInDepartmentsWithoutChiefs()
    {
        return _dataBaseContext.Departments
            .AsEnumerable()
            .GroupJoin(
                _dataBaseContext.Employees,
                d => d.Id,
                e => e.DepartmentId,
                (d, emp) => new
                {
                    Name = d.Name,
                    Salary = emp.Sum(x => x.Salary)
                })
            .ToDictionary(g => g.Name, g => g.Salary);
    }

    public Dictionary<string, int> GetSumSalaryInDepartmentsWithChiefs()
    {
        return _dataBaseContext.Employees
            .Where(x => GetChiefsIds().Contains(x.Id))
            .AsEnumerable()
            .GroupJoin(
                _dataBaseContext.Employees,
                c => c.Id,
                e => e.ChiefId,
                (c, emp) => new
                {
                    SumSalary = c.Salary + emp.Sum(e => e.Salary),
                    DepId = emp.First().DepartmentId
                })
            .Join(
                _dataBaseContext.Departments,
                e => e.DepId,
                d => d.Id,
                (e, d) => new
                {
                    Salary = e.SumSalary,
                    DepName = d.Name
                })
            .ToDictionary(g => g.DepName, g => g.Salary);
    }

    public string GetDepartmentNameWithMaxSalary()
    {
        return _dataBaseContext.Departments
            .Join(
                _dataBaseContext.Employees,
                d => d.Id,
                e => e.DepartmentId,
                (d, e) => new
                {
                    d.Name, 
                    e.Salary
                })
            .OrderByDescending(x => x.Salary)
            .First()
            .Name;
    }

    public Dictionary<int, int> GetSalaryChiefByDescending()
    {
        return _dataBaseContext.Employees
            .Where(x => GetChiefsIds().Contains(x.Id))
            .OrderByDescending(x => x.Salary)
            .ToDictionary(x => x.Id, x => x.Salary);
    }

    private IEnumerable<int?> GetChiefsIds()
    {
        return _dataBaseContext.Employees
            .Where(x => x.ChiefId != null)
            .Select(x => x.ChiefId)
            .Distinct();
    }
}