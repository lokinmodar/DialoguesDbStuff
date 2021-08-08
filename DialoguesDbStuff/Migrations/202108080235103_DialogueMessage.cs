namespace DialoguesDbStuff.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class DialogueMessage : DbMigration
  {
    public override void Up()
    {
      CreateTable(
          "dbo.dialoguemessages",
          c => new
          {
            Id = c.Int(nullable: false, identity: true),
            SenderName = c.String(maxLength: 2147483647),
            OriginalTextMessage = c.String(nullable: false, maxLength: 2147483647),
            OriginalLang = c.String(maxLength: 2147483647),
            TranslatedTextMessage = c.String(nullable: false, maxLength: 2147483647),
            TranslationLang = c.String(maxLength: 2147483647),
            Timestamp = c.DateTime(nullable: false),
          })
          .PrimaryKey(t => t.Id);
    }

    public override void Down()
    {
      DropTable("dbo.dialoguemessages");
    }
  }
}
