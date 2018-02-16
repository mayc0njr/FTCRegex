

namespace FTCRegex.Models
{
    public class FTCRequest
    {
        public int UserId { get; set; }
        public int TagId { get; set; }
        public string Name { get; set; }
        public string Definition { get; set; }
        public string Group { get; set; }


        public override string ToString(){
            return $"Name: {Name}\nDefinition: {Definition}\nGroup: {Group}\nUserId: {UserId}";
        }
    }
}