using System;
using System.Collections.Generic;

namespace Project_Timesheet_Report.Models;

public partial class Person
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TimeSheet> TimeSheets { get; } = new List<TimeSheet>();

    public Person(string name)
    {
        Name = name;
    }
}
