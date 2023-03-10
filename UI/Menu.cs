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
        public void AppMenu()
        {
            Console.Clear();

            List<string> main_Menu = new List<string>()
            {
                    "Create user",
                    "See project timesheet",
                    "Edit",
                    "Exit"
            };

            bool[] choices = { true, false, false, false};

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
                            Console.WriteLine("enter name of user");
                            string name = Console.ReadLine();
                            Person newPerson = new Person(name);
                            DataAccess access = new DataAccess();
                            access.Create(newPerson);
                            break;

                        case 1:
                            Console.WriteLine("Which users timesheet would you like to view?");
                            string view = Console.ReadLine();
                            var showTable = new Table();
                            DataAccess access1 = new DataAccess();
                            var user = access1.getPerson(view);
                            var results = access1.GetTimeSheets(user.Id);
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

                    }

                }

                Console.Clear();

            }
        }
    }
}
