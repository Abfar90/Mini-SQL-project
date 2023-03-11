using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_Timesheet_Report.Models;
using Project_Timesheet_Report.Repository;
using Spectre.Console;

namespace Project_Timesheet_Report.UI
{

    public class Menu
    {
        DataAccess access = new DataAccess();
        public void AppMenu()
        {
            Console.Clear();

            List<string> main_Menu = new List<string>()
            {
                    "Create user or project",
                    "See project timesheet",
                    "Report time",
                    "Edit",
                    "Exit"
            };

            bool[] choices = { true, false, false, false, false };

            int x = 0;

            bool showMenu = true;

            while (showMenu)
            {

                if (choices[0] == true)
                {
                    Console.WriteLine("[ " + main_Menu[0] + " ]");
                }
                else if (choices[0] == false)
                {
                    Console.WriteLine(" " + " " + main_Menu[0]);
                }
                if (choices[1] == true)
                {
                    Console.WriteLine("[ " + main_Menu[1] + " ]");
                }
                else if (choices[1] == false)
                {
                    Console.WriteLine(" " + " " + main_Menu[1]);
                }
                if (choices[2] == true)
                {
                    Console.WriteLine("[ " + main_Menu[2] + " ]");
                }
                else if (choices[2] == false)
                {
                    Console.WriteLine(" " + " " + main_Menu[2]);
                }
                if (choices[3] == true)
                {
                    Console.WriteLine("[ " + main_Menu[3] + " ]");
                }
                else if (choices[3] == false)
                {
                    Console.WriteLine(" " + " " + main_Menu[3]);
                }
                if (choices[4] == true)
                {
                    Console.WriteLine("[ " + main_Menu[3] + " ]");
                }
                else if (choices[4] == false)
                {
                    Console.WriteLine(" " + " " + main_Menu[4]);
                }

                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.DownArrow)
                {
                    if (x == 5)
                    {
                        choices[0] = true;
                        choices[x] = false;
                        x = 0;
                    }
                    else
                    {
                        choices[x + 1] = true;
                        choices[x] = false;
                        x++;
                    }

                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    if (x == 0)
                    {
                        choices[5] = true;
                        choices[x] = false;
                        x = 5;
                    }
                    else
                    {
                        choices[x - 1] = true;
                        choices[x] = false;
                        x--;
                    }
                }

                else if (key.Key == ConsoleKey.Enter)
                {
                    switch (x)
                    {
                        case 0:
                            Console.WriteLine("Would you like to create new user or project (enter U for user an P for project)");
                            string choice = Console.ReadLine();

                            if (choice.ToLower() == "u")
                            {
                                Console.WriteLine("You have selected to create a new user");
                                Console.WriteLine("enter name of user");
                                string name = Console.ReadLine();
                                Person newPerson = new Person(name);
                                access.Create(newPerson);
                            }

                            else if (choice.ToLower()=="p")
                            {
                                Console.WriteLine("You have selected to create a new project");
                                Console.WriteLine("enter name of project");
                                string name = Console.ReadLine();
                                Project newProject = new Project(name);
                                access.CreateProject(newProject);
                            }
                            break;

                        case 1:
                            Console.WriteLine("Which users timesheet would you like to view?");
                            string view = Console.ReadLine();
                            var showTable = new Table();
                            var user = access.getPerson(view);
                            var results = access.PresentTimeSheets(user.Id);
                            //var project = access1.getProject();
                            showTable.Border = TableBorder.HeavyHead;
                            showTable.AddColumn("Project");
                            showTable.AddColumn(new TableColumn("Hours").Centered());
                            Console.WriteLine();
                            foreach (var result in results)
                            {
                                showTable.AddRow($"{result.projectName}", $"{result.sheet.Hours}");
                            }
                            AnsiConsole.Write(showTable);
                            Console.ReadKey();
                            break;

                        case 2:
                            Console.WriteLine("For which user would you like to report");
                            string userToReport = Console.ReadLine();

                            Console.WriteLine("For which project would you like to report");
                            string projectToReport = Console.ReadLine();

                            Console.WriteLine("how many new hours on the project?");
                            int hoursReported = int.Parse(Console.ReadLine());

                            Project projectReport = access.getProject(projectToReport);
                            Person userReport = access.getPerson(userToReport);
                            TimeSheet report = new TimeSheet(hoursReported, projectReport.Id, userReport.Id);

                            access.RegisterTime(report);

                            break;

                        case 3:
                            Console.WriteLine("What would you like to edit: 1) Reported time on project, 2) project name, 3) Username");
                            int up = int.Parse(Console.ReadLine());

                            if (up == 1)
                            {
                                Console.WriteLine("enter name of project to be edited");
                                string projectName = Console.ReadLine();

                                Console.WriteLine("Enter user that this change affects");
                                string userChangeP = Console.ReadLine();

                                Project chosenProject = access.getProject(projectName);
                                Person chosenPerson = access.getPerson(userChangeP);

                                Console.WriteLine("change amount of hours to");
                                int newHours = int.Parse(Console.ReadLine());

                                TimeSheet updatedProject = new TimeSheet(newHours, chosenPerson.Id, chosenProject.Id);
                                access.editTimeSheet(updatedProject);

                                //en metod i UI folder som tar in en lista och returnerar en tabell i spectre format. Mycket repeterad kod här.
                                var resultsNew = access.PresentTimeSheets(updatedProject.PersonId);
                                var showTableNew = new Table();
                                showTableNew.Border = TableBorder.HeavyHead;
                                showTableNew.AddColumn("Project");
                                showTableNew.AddColumn(new TableColumn("Hours").Centered());
                                Console.WriteLine();
                                foreach (var result in resultsNew)
                                {
                                    showTableNew.AddRow($"{result.projectName}", $"{result.sheet.Hours}");
                                }
                                AnsiConsole.Write(showTableNew);
                                Console.ReadKey();
                            }

                            else if (up == 2)
                            {
                                Console.WriteLine("enter name of project to be edited");
                                string old = Console.ReadLine();

                                Project oldProject = new Project(old);

                                Console.WriteLine("Enter updated project name");
                                string newP = Console.ReadLine();

                                Project personUpdated = new Project(newP);

                                access.updateProject(personUpdated, oldProject);
                            }

                            else if (up == 3)
                            {
                                Console.WriteLine("enter name of user to be edited");
                                string old = Console.ReadLine();
                                Person personOriginal = new Person(old);
                                Console.WriteLine("Enter updated name");
                                string newP = Console.ReadLine();
                                Person personUpdated = new Person(newP);
                                
                                access.updateUser(personUpdated, personOriginal);

                            }
                            break;

                    }

                }

                Console.Clear();

            }
        }
    }
}
