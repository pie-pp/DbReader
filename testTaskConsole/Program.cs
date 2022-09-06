// See https://aka.ms/new-console-template for more information

using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using testTaskConsole;

Console.WriteLine("Нажмите любую клавишу, чтобы получить результаты запросов");
Console.ReadKey();
Console.WriteLine();

IServiceProvider serviceProvider = new ServiceCollection()
    .AddDbContext<DataBaseContext>(x => 
        x.UseSqlite("Data Source=test_task.db"))
    .BuildServiceProvider();
 
DbService dbService = new(serviceProvider.GetRequiredService<DataBaseContext>());

Dictionary<string, int> sumSalaryInDepartmentsWithoutChiefs = dbService.GetSumSalaryInDepartmentsWithoutChiefs();
Dictionary<string, int> sumSalaryInDepartmentsWithChiefs = dbService.GetSumSalaryInDepartmentsWithChiefs();
string departmentNameWithMaxSalary = dbService.GetDepartmentNameWithMaxSalary();
Dictionary<int, int> chiefSalary = dbService.GetSalaryChiefByDescending();

#region Output

Console.WriteLine(ReadDictionary(sumSalaryInDepartmentsWithoutChiefs, 
    "Суммарная з.п. в департаментах без учёта руководителей: "));
Console.WriteLine(ReadDictionary(sumSalaryInDepartmentsWithChiefs, 
    "Суммарная з.п. в департаментах с учётом руководителей: "));
Console.WriteLine(ReadDictionary(chiefSalary, "З.п. руководителей по убыванию: "));
Console.WriteLine($"Отдел, в котором у сотрудника зарплата максимальна: {departmentNameWithMaxSalary}");
Console.WriteLine("Выполнение программы завершено. Нажмите любую клавиши, чтобы выйти");
Console.ReadKey();

#endregion


string ReadDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary, string title)
{
    StringBuilder stringBuilder = new StringBuilder()
        .AppendLine(title);
    
    Type dictType = dictionary.GetType();
    if (dictType == typeof(Dictionary<string, int>) || dictType == typeof(Dictionary<int, int>))
    {
        foreach ((TKey key, TValue val) in dictionary)
        {
            stringBuilder.AppendLine($"{key} {val}");;
        }
    }
    
    return stringBuilder.ToString();
}
