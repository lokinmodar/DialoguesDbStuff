using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using DialoguesDbStuff.Models;
using SQLite.CodeFirst;

namespace DialoguesDbStuff.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        // Your context has been configured to use a 'DialogueModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DialoguesDbStuff.DialogueModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DialogueModel' 
        // connection string in the application configuration file.

        private static string s_migrationSqlitePath;


        public ApplicationDbContext() : base(new SQLiteConnection($"DATA Source={s_migrationSqlitePath}"), false)
        {
        }
        public ApplicationDbContext(DbConnection connection) : base(connection, true)
        {
        }
        static ApplicationDbContext()
           // : base("name=DialogueModel")
        {
            var exeDir = AppDomain.CurrentDomain.BaseDirectory;
            var exeDirInfo = new DirectoryInfo(exeDir);
            if (exeDirInfo.Parent != null)
            {
                var projectDir = exeDirInfo.Parent.Parent.FullName;
                s_migrationSqlitePath = $@"{projectDir}\DialoguesDB.sqlite";
            }
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<DialogueMessage> DialogueMessages { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<ApplicationDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }


}