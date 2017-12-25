using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTCRegex.Models
{
    [Table("usuarios_seguidores")]
    public class UserFollow : IComparable
    {
        public int UserFollowId { get; set; }

        // public int FollowerId { get; set; }
        // [ForeignKey("UserForeignKey")]
        public User Follower { get; set; }
        
        // public int FollowingId { get; set; }
        // [ForeignKey("UserForeignKey")]
        public User Following { get; set; }

        public int CompareTo(object obj)
        {
            if(obj == null || obj.GetType() != GetType())
                throw new ArgumentException("Different types comparison");
            return UserFollowId.CompareTo((obj as UserFollow).UserFollowId);
        }
    }
}