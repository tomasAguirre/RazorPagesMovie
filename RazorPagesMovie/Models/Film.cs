using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RazorPagesMovie.Models
{
    public class Film
    {
        public Film()
        {
        }

        public Film(int id, string name, DateTime commingOut)
        {
            Id = id;
            Name = name;
            this.commingOut = commingOut;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime commingOut { get; set; }
    }
}
