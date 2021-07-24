namespace Eventually.Entities
{
    public class UserArea
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
    }
}
