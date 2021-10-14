namespace SqliteFromScratch{
    public class Track{
        public Track(){}
        //If the column explicitly states not null, it must be initialized
        public int Id { get; set; }
        public string Name { get; set; }
        //The question mark allows the field to be null
        public int? AlbumId { get; set; }
        public int MediaTypeId { get; set; }
        public int? GenreId { get; set; }
        public string Composer { get; set; }
        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }
        public int UnitPrice { get; set; }
    }
}