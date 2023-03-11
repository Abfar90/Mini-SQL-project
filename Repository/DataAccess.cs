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

        public Project CreateProject(Project newProject)
        {
            _connection.Projects.Add(newProject);
            _connection.SaveChanges();
            return newProject;
        }

        public void RegisterTime(TimeSheet entry)
        {
            bool duplicateEntrySearch = false;
            duplicateEntrySearch = _connection.TimeSheets.Any(s => s.ProjectId == entry.ProjectId && s.PersonId == entry.PersonId);
            if(duplicateEntrySearch)
            {
                var oldSheet = _connection.TimeSheets.SingleOrDefault(x => x.ProjectId == entry.ProjectId && x.PersonId == entry.PersonId);
                entry.Hours += oldSheet.Hours;
                entry.Id = oldSheet.Id;
                editTimeSheet(entry);
                _connection.SaveChanges();
                return;

            }
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

        //public TimeSheet getTimeSheet(TimeSheet timeSheet)
        //{
        //    return _connection.TimeSheets.SingleOrDefault(t => t.ProjectId == timeSheet.ProjectId && t.PersonId == timeSheet.PersonId);
        //}

        public List<TimeSheetProject> PresentTimeSheets(int id)
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

        public void updateUser(Person newName, Person oldName)
        {
            Person original = _connection.People.SingleOrDefault(p => p.Name == oldName.Name);

            //_connection.Entry(oldName).CurrentValues.SetValues(newName);

            original.Name = newName.Name;

            _connection.SaveChanges();

        }

        public void updateProject(Project newProject, Project oldProject)
        {
            Project original = _connection.Projects.SingleOrDefault(x => x.Name == oldProject.Name);
            original.Name = newProject.Name;

            _connection.SaveChanges();

        }

        public void editTimeSheet(TimeSheet editEntry)
        {
            TimeSheet original = _connection.TimeSheets.SingleOrDefault(x => x.ProjectId == editEntry.ProjectId && x.PersonId == editEntry.PersonId);
            
            _connection.Entry(original).CurrentValues.SetValues(editEntry);

            _connection.SaveChanges();

        }
    }
}
