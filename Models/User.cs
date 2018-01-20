using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTCRegex.Models
{
    [Table("usuarios")]
    public class User : IComparable
    {
        //Columns
        [Column("id_usuario")]
        public int UserId { get; set; }

        [Column("nome", TypeName = "varchar(200)")]
        public string Name { get; set; }
        
        [Column("email", TypeName = "varchar(200)")]
        public string Email { get; set; }
        
        [Column("senha", TypeName = "varchar(200)")]
        public string Password { get; set; }

        [Column("data_criacao")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

        [InverseProperty("Follower")]
        public List<UserFollow> Followers { get; set; }

        [InverseProperty("Following")]
        public List<UserFollow> Followings { get; set; }

        //Not Mapped
        public List<Tag> Tags { get; set; }

        public int CompareTo(object obj)
        {
            if(obj == null || obj.GetType() != GetType())
                throw new ArgumentException("Different types comparison");
            return UserId.CompareTo((obj as User).UserId);
        }
    }
}