namespace Example.Domain
{
    public class Player : IHasId
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Score { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}