using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTCRegex.Models
{
    [Table("tags_historico")]
    public class TagHistory : IComparable
    {
        public int TagHistoryId { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Description { get; set; }

        public DateTime Created { get; set; }

        public int CompareTo(object obj)
        {
            if(obj == null || obj.GetType() != GetType())
                throw new ArgumentException("Different types comparison");
            return TagHistoryId.CompareTo((obj as TagHistory).TagHistoryId);
        }
    }
}