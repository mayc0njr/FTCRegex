using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTCRegex.Models
{
    [Table("tags_historico")]
    public class TagHistory : IComparable
    {
        [Column("id_historico")]
        public int TagHistoryId { get; set; }

        [Column("observacao",TypeName = "varchar(200)")]
        public string Description { get; set; }

        [Column("data")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

        [Column("id_tag")]
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        [Column("id_acao")]
        public int TagActionId { get; set; }
        public TagAction TagAction { get; set; }

        public int CompareTo(object obj)
        {
            if(obj == null || obj.GetType() != GetType())
                throw new ArgumentException("Different types comparison");
            return TagHistoryId.CompareTo((obj as TagHistory).TagHistoryId);
        }
    }
}