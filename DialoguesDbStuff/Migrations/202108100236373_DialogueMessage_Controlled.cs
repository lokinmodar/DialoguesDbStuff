namespace DialoguesDbStuff.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class DialogueMessage_Controlled : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.dialoguemessages", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.dialoguemessages", "UpdatedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.dialoguemessages", "Timestamp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.dialoguemessages", "Timestamp", c => c.DateTime(nullable: false));
            DropColumn("dbo.dialoguemessages", "UpdatedDate");
            DropColumn("dbo.dialoguemessages", "CreatedDate");
        }
    }
}
