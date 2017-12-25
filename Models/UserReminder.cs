using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTCRegex.Models
{
    [Table("usuarios_lembretes")]
    public class UserReminder : IComparable
    {
        public int UserReminderId { get; set; }

        public string Answer { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PasswordReminderId { get; set; }
        public PasswordReminder PasswordReminder { get; set; }
        public int CompareTo(object obj)
        {
            if(obj == null || obj.GetType() != GetType())
                throw new ArgumentException("Different types comparison");
            return UserReminderId.CompareTo((obj as UserReminder).UserReminderId);
        }
    }
}