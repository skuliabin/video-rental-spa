using System.Collections.Generic;

namespace VideoRental.Entities
{
    public class Genre : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }

        public Genre()
        {
            Movies = new List<Movie>();
        }
    }
}