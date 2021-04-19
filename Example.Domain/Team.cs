namespace Example.Domain
{
    public class Team : IHasId
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}