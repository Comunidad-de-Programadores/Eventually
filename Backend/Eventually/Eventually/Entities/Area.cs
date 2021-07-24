using System.Collections.Generic;

namespace Eventually.Entities
{
    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserArea> UsersInArea { get; set; }
    }
}
