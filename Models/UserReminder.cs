using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTCRegex.Models
{
    [Table("usuario_lembretes")]
    public class UserReminder : IComparable
    {
        [Column("id_usuario_lembretes")]
        public int UserReminderId { get; set; }

        [Column("resposta")]
        public string Answer { get; set; }

        // public int UserId { get; set; }
        [Column("id_usuario")]
        public User User { get; set; }

        // public int PasswordReminderId { get; set; }
        [Column("id_senha_lembrete")]
        public PasswordReminder PasswordReminder { get; set; }
        public int CompareTo(object obj)
        {
            if(obj == null || obj.GetType() != GetType())
                throw new ArgumentException("Different types comparison");
            return UserReminderId.CompareTo((obj as UserReminder).UserReminderId);
        }
    }
}