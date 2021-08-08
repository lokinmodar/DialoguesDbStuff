using System.Data.Entity;
using DialoguesDbStuff.Models;
using SQLite.CodeFirst;

namespace DialoguesDbStuff.DataAccess
{
    public class DialogueModel : DbContext
    {
        // Your context has been configured to use a 'DialogueModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DialoguesDbStuff.DialogueModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DialogueModel' 
        // connection string in the application configuration file.
        public DialogueModel()
            : base("name=DialogueModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<DialogueMessage> DialogueMessages { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<DialogueModel>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }


}