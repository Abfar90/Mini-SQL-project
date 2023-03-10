using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_Timesheet_Report.Models;

namespace Project_Timesheet_Report.Repository
{
    public class DataAccess
    {
        private TimesheetDbContext _connection;

        public DataAccess()
        {
            _connection = new TimesheetDbContext();
        }

        public Person Create(Person newPerson)
        {
            _connection.People.Add(newPerson);
            _connection.SaveChanges();
            return newPerson;
        }

        public void RegisterTime(TimeSheet entry)
        {
            _connection.TimeSheets.Add(entry);
            _connection.SaveChanges();
        }

        public Person getPerson(string name)
        {
            return _connection.People.SingleOrDefault(p => p.Name == name);
        }

        public Project getProject(string name)
        {
            return _connection.Projects.SingleOrDefault(p => p.Name == name);
        }

        public List<TimeSheetProject> GetTimeSheets(int id)
        {
            var select = _connection.TimeSheets.Where(p => p.PersonId == id).ToList();

            var query = (from TimeSheet in @select
                        join project in _connection.Projects
                            on TimeSheet.ProjectId equals project.Id
                        select new TimeSheetProject() 
                        { 
                            projectName = project.Name, 
                            sheet = TimeSheet
                        }).ToList();
            return query;
        }
    }
}
