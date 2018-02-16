using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTCRegex.Models
{
    [Table("senha_lembretes")]
    public class PasswordReminder : IComparable
    {
        [Column("id_senha_lembrete")]
        public int PasswordReminderId { get; set; }

        [Column("descricao")]
        public string Description { get; set; }

        public int CompareTo(object obj)
        {
            if(obj == null || obj.GetType() != GetType())
                throw new ArgumentException("Different types comparison");
            return PasswordReminderId.CompareTo((obj as PasswordReminder).PasswordReminderId);
        }
    }
}