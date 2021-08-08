using DialoguesDbStuff.DataAccess;
using System.Data.Entity.Migrations;
using System.Data.SQLite.EF6.Migrations;

namespace DialoguesDbStuff.Migrations
{
  internal sealed class
    Configuration : DbMigrationsConfiguration<ApplicationDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
      SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());
    }

    protected override void Seed(ApplicationDbContext context)
    {
      //  This method will be called after migrating to the latest version.

      //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
      //  to avoid creating duplicate seed data. E.g.
      //
      //    context.People.AddOrUpdate(
      //      p => p.FullName,
      //      new Person { FullName = "Andrew Peters" },
      //      new Person { FullName = "Brice Lambson" },
      //      new Person { FullName = "Rowan Miller" }
      //    );
      //
    }
  }
}