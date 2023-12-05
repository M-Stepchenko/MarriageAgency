using MarriageAgency.Models;
using Microsoft.EntityFrameworkCore;

namespace MarriageAgency.Scripts
{
    public class SQLCommands
    {
        public static void InitialCreation(MarriageAgencyContext db)
        {
            /*db.Database.ExecuteSql($"IF object_id('AllServices', 'U') is null CREATE TABLE AllServices (id int IDENTITY(1,1) PRIMARY KEY, name varchar(100), description varchar(100), cost int)");
            db.Database.ExecuteSql($"IF object_id('Nationalities', 'U') is null CREATE TABLE Nationalities (id int IDENTITY(1,1) PRIMARY KEY, name varchar(100), remark varchar(100))");
            db.Database.ExecuteSql($"IF object_id('ZodiacSigns', 'U') is null CREATE TABLE ZodiacSigns (id int IDENTITY(1,1) PRIMARY KEY, name varchar(100), description varchar(100))");
            db.Database.ExecuteSql($"IF object_id('Employees', 'U') is null CREATE TABLE Employees (id int IDENTITY(1,1) PRIMARY KEY, name varchar(100), position varchar(50), bithdate date)");
            db.Database.ExecuteSql($"IF object_id('Clients', 'U') is null CREATE TABLE Clients (id int IDENTITY(1,1) PRIMARY KEY, name varchar(100), photo image, sex varchar(1), job varchar(50), bithdate date, height int, weight int, children int, familyStatus varchar(50), badHabbits varchar(100), hobby varchar(100), zodiacSignID int, nationalityID int, address varchar(100), phoneNumber int, passport varchar(100), desiredPartner varchar(100), FOREIGN KEY (zodiacSignID) REFERENCES zodiacSigns(id), FOREIGN KEY (nationalityID) REFERENCES nationalities(id))");
            db.Database.ExecuteSql($"IF object_id('ProvidedServices', 'U') is null CREATE TABLE ProvidedServices (id int IDENTITY(1,1) PRIMARY KEY, clientID int, date date, serviceID int, employeeID int FOREIGN KEY (employeeID) REFERENCES employees(id), FOREIGN KEY (clientID) REFERENCES clients(id), FOREIGN KEY (serviceID) REFERENCES AllServices(id))");*/
        }
    }
}