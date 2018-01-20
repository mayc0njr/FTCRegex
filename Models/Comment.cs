using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTCRegex.Models
{
    [Table("comentarios")]
    public class Comment : IComparable
    {
        [Column("id_comentario")]
        public int CommentId { get; set; }

        [Column("descricao")]
        public string Description { get; set; }

        [Column("id_tag")]
        public Tag Tag { get; set; }

        [Column("id_usuario")]
        public User User { get; set; }

        [Column("data_criacao")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Created{ get; set; }
        public int CompareTo(object obj)
        {
            if(obj == null || obj.GetType() != GetType())
                throw new ArgumentException("Different types comparison");
            return CommentId.CompareTo((obj as Comment).CommentId);
        }
    }
}