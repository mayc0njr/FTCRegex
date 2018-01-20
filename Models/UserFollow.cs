using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTCRegex.Models
{
    [Table("usuarios_relacao")]
    public class UserFollow : IComparable
    {
        [Column("id_relacao")]
        public int UserFollowId { get; set; }

        // public int FollowerId { get; set; }
        // [ForeignKey("UserForeignKey")]
        [Column("id_seguidor")]
        public User Follower { get; set; }
        
        // public int FollowingId { get; set; }
        // [ForeignKey("UserForeignKey")]
        [Column("id_seguindo")]
        public User Following { get; set; }

        public int CompareTo(object obj)
        {
            if(obj == null || obj.GetType() != GetType())
                throw new ArgumentException("Different types comparison");
            return UserFollowId.CompareTo((obj as UserFollow).UserFollowId);
        }
    }
}