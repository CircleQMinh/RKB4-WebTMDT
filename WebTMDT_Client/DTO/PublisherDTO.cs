namespace WebTMDT_Client.DTO
{
    public class PublisherDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DetailPublisherDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IList<SimpleBookInfoDTO> Books { get; set; }
    }
}
