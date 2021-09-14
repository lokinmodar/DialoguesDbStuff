using DialoguesDbStuff.Models;
using SQLite.CodeFirst;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DialoguesDbStuff.DataAccess
{
  public class ApplicationDbContext : DbContext
  {
    // Your context has been configured to use a 'ApplicationDbContext' connection string from your application's 
    // configuration file (App.config or Web.config). By default, this connection string targets the 
    // 'DialoguesDbStuff.ApplicationDbContext' database on your LocalDb instance. 
    // 
    // If you wish to target a different database and/or database provider, modify the 'ApplicationDbContext' 
    // connection string in the application configuration file.

    private static readonly string s_migrationSqlitePath;

    /// <summary>
    ///   Constructor used when running Update-Database.
    ///   If you do not set the second argument of the constructor called by base to false, Update-Database
    ///   will throw an exception of "System.ObjectDisposedException: The disposed object cannot be accessed."
    /// </summary>
    public ApplicationDbContext() : base(new SQLiteConnection($"DATA Source={s_migrationSqlitePath}"), false)
    {
    }

    /// <summary>
    ///   Constructor used except when updating Update-Database
    /// </summary>
    /// <param name="connection"></param>
    public ApplicationDbContext(DbConnection connection) : base(connection, true)
    {
    }

    /// <summary>
    ///   Set the DB path of sqlite for Update-Database
    /// </summary>
    static ApplicationDbContext()
    {
      //var exeDir = AppDomain.CurrentDomain.BaseDirectory;
      //var exeDirInfo = new DirectoryInfo(exeDir);

      var exeDirInfo = Directory.GetParent(Assembly.GetExecutingAssembly().Location)?.ToString();
      if (exeDirInfo != null)
      {
        var projectDir = exeDirInfo;
        s_migrationSqlitePath = $@"{projectDir}DialoguesDB.sqlite";
      }
    }

    // Add a DbSet for each entity type that you want to include in your model. For more information 
    // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.
    /// <summary>
    ///   Accessing the DialogueMessage table
    /// </summary>
    public virtual DbSet<DialogueMessage> DialogueMessages { get; set; }


    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<ApplicationDbContext>(modelBuilder);
      Database.SetInitializer(sqliteConnectionInitializer);
    }

    public override int SaveChanges()
    {
      var entries = ChangeTracker
        .Entries()
        .Where(e => e.Entity is BaseEntity && e.State is EntityState.Added or EntityState.Modified);

      foreach (var entityEntry in entries)
      {
        ((BaseEntity) entityEntry.Entity).UpdatedDate = DateTime.Now;

        if (entityEntry.State == EntityState.Added) ((BaseEntity) entityEntry.Entity).CreatedDate = DateTime.Now;
      }

      return base.SaveChanges();
    }
  }
}