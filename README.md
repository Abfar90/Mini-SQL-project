# Project_Timesheet_Report

Simple console app to report time on projects for users created with C#, Spectre.Console, MSSQL and Entity Framework.
In the app its possible to create new users and projects, report time, see previously reported time and edit the entries.
This project is part of an database course.

## General Info
 
* [Styling](#styling)  
* [Class Structure](#class-structure) 
* [Database](#database)
* [UI](#ui)
* [App](#app)


### Styling

This project utilizes Spectre.Console.

It is an open-source .NET library that makes it easier to create beautiful console applications.

### Class Structure

Classes are arranged in four separate folders:

* "Models" folder contains classes mapping DB tables.

* "UI" folder contains the menu class and respective functions.

### Repository

The "Repository" folder contains a DataAccess class that connects to the MSSQL database with the help of Entity.

The connection string is then used throughout our source code for sending and reveiving SQL queries.

### UI

The "UI" folder contains the menus shown and the main function that runs the app.

### App

From here the main run function is called to run the application.
