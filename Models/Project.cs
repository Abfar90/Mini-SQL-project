using System;
using System.Collections.Generic;

namespace Project_Timesheet_Report.Models;

public partial class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TimeSheet> TimeSheets { get; } = new List<TimeSheet>();

    public Project(string name)
    {
        Name = name;
    }
}
