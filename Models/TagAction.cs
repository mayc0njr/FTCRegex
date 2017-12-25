using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTCRegex.Models
{
    [Table("tags_acao")]
    public class TagAction : IComparable
    {
        public int TagActionId { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Descricao { get; set; }
        
        public int CompareTo(object obj)
        {
            if(obj == null || obj.GetType() != GetType())
                throw new ArgumentException("Different types comparison");
            return TagActionId.CompareTo((obj as TagAction).TagActionId);
        }
    }
}