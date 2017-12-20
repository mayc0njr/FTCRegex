using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTCRegex.Models
{
    [Table("Groups")]
    public class Group : IComparable
    {
        public const string GROUP_DEFAULT = "No Group defined. Using 'Default' instead.";
        public const string GROUP_NEW = "Created Group {0}.";
        public int GroupId { get; set; }

        public string Name { get; set; }

        public List<Tag> Tags { get; set; }

        // override object.Equals
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
            return Equals (obj as Group);
        }

        public bool Equals(Group g)
        {
            return Name.Equals(g.Name);
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if(obj == null || obj.GetType() != GetType())
                throw new ArgumentException("Different types comparison");
            return GroupId.CompareTo((obj as Group).GroupId);
        }

        public override string ToString(){
            return Name;
        }
    }
}