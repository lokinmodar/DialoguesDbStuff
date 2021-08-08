using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DialoguesDbStuff.Models
{
  [Table("dialoguemessages")]
  public class DialogueMessage
  {
    [Key] public int Id { get; set; }
    public string SenderName { get; set; }
    [Required] public string OriginalTextMessage { get; set; }
    public string OriginalLang { get; set; }
    [Required] public string TranslatedTextMessage { get; set; }
    public string TranslationLang { get; set; }
    [Timestamp] public DateTime Timestamp { get; set; }

    public DialogueMessage(string senderName, string originalTextMessage, string originalLang,
      string translatedTextMessage, string translationLang)
    {
      SenderName = senderName;
      OriginalTextMessage = originalTextMessage;
      OriginalLang = originalLang;
      TranslatedTextMessage = translatedTextMessage;
      TranslationLang = translationLang;
      Timestamp = DateTime.Now;
    }
  }
}