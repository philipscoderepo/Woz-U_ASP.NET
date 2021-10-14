namespace ContactsEntityFramework.Models
{
    public class Contact {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }

        public string GetFullName() {
            return this.FirstName + " " + this.Lastname;
        }
    }
}