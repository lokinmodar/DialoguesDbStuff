using DialoguesDbStuff.DataAccess;
using DialoguesDbStuff.Migrations;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using DialogueMessage = DialoguesDbStuff.Models.DialogueMessage;

namespace DialoguesDbStuff
{
  public class MainLogic : IDisposable
  {

    //TODO: Check this!
    private static string _dbPath = Directory.GetParent(Assembly.GetExecutingAssembly().Location)?.ToString();
    private string _connectionString = $"DATA Source={_dbPath}";

    private SQLiteConnection _sqLiteConnection;
    private ApplicationDbContext _context;


    public bool ManageSave(DialogueMessage dialogueMessage)
    {
      _sqLiteConnection = new SQLiteConnection(_connectionString);
      _context = new ApplicationDbContext(_sqLiteConnection);

      // Get the providerName using code.
      // You can use "System.Data.SQLite" directly without using any code
      // https://stackoverflow.com/questions/36060478/dbmigrator-does-not-detect-pending-migrations-after-switching-database
      var internalContext = _context.GetType()
        .GetProperty("InternalContext", BindingFlags.Instance | BindingFlags.NonPublic)
        ?.GetValue(_context);

      if (internalContext != null)
      {
        var providerName =
          (string)internalContext.GetType().GetProperty("ProviderName")?.GetValue(internalContext);

        // Generate the Configuration used by the Migrator.
        // TargetDatabase has no effect unless it is set to Configuration, not DbMigrator.
        var configuration = new Configuration
        {
          TargetDatabase = new DbConnectionInfo(_context.Database.Connection.ConnectionString, providerName)
        };

        // Generate DbMigrator
        var migrator = new DbMigrator(configuration);

        // There is no problem with EF6.13, but in the case of EF6.2, measures to prevent the following exception from being thrown at the update timing
        // System.ObjectDisposedException:'Unable to access the disposed object. The object name is 'SQLiteConnection'.'
        // https://stackoverflow.com/questions/47329496/updating-to-ef-6-2-0-from-ef-6-1-3-causes-cannot-access-a-disposed-object-error/47518197
        var historyRepository = migrator.GetType()
          .GetField("_historyRepository", BindingFlags.Instance | BindingFlags.NonPublic)
          ?.GetValue(migrator);
        var memberInfo = historyRepository?.GetType().BaseType;
        if (memberInfo != null)
        {
          var existingConnection = memberInfo.GetField("_existingConnection",
            BindingFlags.Instance | BindingFlags.NonPublic);
          existingConnection?.SetValue(historyRepository, null);
        }

        // Execute Migration.
        migrator.Update();
      }

      _context.DialogueMessages.Add(dialogueMessage); // add new item to table
      _context.SaveChanges();


      return true;
    }

    //private DialogueMessage dialogue = new DialogueMessage("vovó", "maria", "en", "venha cá", "pt");

    #region IDisposable

    public void Dispose()
    {
      _sqLiteConnection?.Dispose();
      _context?.Dispose();
    }

    #endregion
  }
}
