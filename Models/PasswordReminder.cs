using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTCRegex.Models
{
    [Table("senha_lembretes")]
    public class PasswordReminder : IComparable
    {
        public int PasswordReminderId { get; set; }

        public string Description { get; set; }

        public int CompareTo(object obj)
        {
            if(obj == null || obj.GetType() != GetType())
                throw new ArgumentException("Different types comparison");
            return PasswordReminderId.CompareTo((obj as PasswordReminder).PasswordReminderId);
        }
    }
}