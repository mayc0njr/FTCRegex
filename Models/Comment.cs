using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTCRegex.Models
{
    [Table("comentarios")]
    public class Comment : IComparable
    {
        public int CommentId { get; set; }

        public string Description { get; set; }

        public int CompareTo(object obj)
        {
            if(obj == null || obj.GetType() != GetType())
                throw new ArgumentException("Different types comparison");
            return CommentId.CompareTo((obj as Comment).CommentId);
        }
    }
}