using APIAnnuaire.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace APIAnnuaire
{
    public static class EmployeeData
    {
        public static List<Employees> LoadDataFromDatabase()
        {
            var options = new DbContextOptionsBuilder<APIDbContext>()
                .UseSqlite("Data Source=data.db") // Spécifiez le chemin de votre fichier SQLite
                .Options;

            using var context = new APIDbContext(options);

            // Utilisez context.Employees pour accéder aux données de la table Employee.
            return context.Employees.ToList();
        }

        public static void SaveDataToDatabase(List<Employees> employees)
        {
            var options = new DbContextOptionsBuilder<APIDbContext>()
                .UseSqlite("Data Source=data.db") // Spécifiez le chemin de votre fichier SQLite
                .Options;

            using var context = new APIDbContext(options);

            // Supprimez les données existantes (facultatif) et ajoutez de nouvelles données.
            context.Employees.RemoveRange(context.Employees);
            context.Employees.AddRange(employees);

            context.SaveChanges();
        }
    }
}
