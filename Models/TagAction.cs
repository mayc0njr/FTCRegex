using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTCRegex.Models
{
    [Table("tags_acao")]
    public class TagAction : IComparable
    {
        [Column("id_acao")]
        public int TagActionId { get; set; }

        [Column("descricao",TypeName = "varchar(200)")]
        public string Description { get; set; }
        
        public int CompareTo(object obj)
        {
            if(obj == null || obj.GetType() != GetType())
                throw new ArgumentException("Different types comparison");
            return TagActionId.CompareTo((obj as TagAction).TagActionId);
        }
    }
}