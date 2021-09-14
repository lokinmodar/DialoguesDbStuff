using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DialoguesDbStuff.Models
{
  [Table("dialoguemessages")]
  public class DialogueMessage : BaseEntity
  {
    [Key] public int Id { get; set; }
    public string SenderName { get; set; }
    [Required] public string OriginalTextMessage { get; set; }
    public string OriginalLang { get; set; }
    [Required] public string TranslatedTextMessage { get; set; }
    public string TranslationLang { get; set; }


    public DialogueMessage(string senderName, string originalTextMessage, string originalLang,
      string translatedTextMessage, string translationLang)
    {
      SenderName = senderName;
      OriginalTextMessage = originalTextMessage;
      OriginalLang = originalLang;
      TranslatedTextMessage = translatedTextMessage;
      TranslationLang = translationLang;
    }
  }
  public class BaseEntity
  {
    [Timestamp] [Required] public DateTime CreatedDate { get; set; }
    [Timestamp] [Required] public DateTime UpdatedDate { get; set; }
  }
}
