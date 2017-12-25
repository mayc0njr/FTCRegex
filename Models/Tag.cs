using System;
using System.ComponentModel.DataAnnotations.Schema;

using FTCRegex.Parser;

namespace FTCRegex.Models
{
    [Table("tags")]
    public class Tag : IComparable
    {
        public const char ESCAPE_CHAR = '\\';
        public const int WILDCARD = 0;
        public const int DOT = 1;
        public const int PLUS = 2;
        public const int NEWLINE = 3;
        public const int BAR = 4;
        public const int LAMBDA = 5;
        public static readonly char[] ESCAPE = {'*', '.', '+', 'n', '\\', 'l'};
        public const string INVALID_NAME = "Tag name invalid.";
        public const string INVALID_DEFINITION = "Tag definition invalid.";
        public const string TAG_DEFINED = "Tag {0} defined sucessful.";
        public const string TAG_EXISTS = "Already exists a Tag with this name or definition.";

        //Columns
        public int TagId { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Name { get; set; }
        
        [Column(TypeName = "varchar(200)")]
        public string Definition { get; set; }
        
        [Column(TypeName = "varchar(200)")]
        public string Status { get; set; }
        
        public DateTime Created { get; set; }

        [NotMapped]
        public Automaton Automaton { get; set; }


        //Principal entity
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public override bool Equals(object obj)
        {
            //
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //
            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            // TODO: write your implementation of Equals() here
            return Equals (obj as Tag);
        }

        public bool Equals(Tag t)
        {
            return Name.Equals(t.Name) || Definition.Equals(t.Definition);
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Definition.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if(obj == null || obj.GetType() != GetType())
                throw new ArgumentException("Different types comparison");
            return TagId.CompareTo((obj as Tag).TagId);
        }

    }
}