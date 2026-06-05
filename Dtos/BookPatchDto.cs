namespace BibliotecaAPI.Dtos
{
    public class BookPatchDto
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public int? Year { get; set; }
        public int? Quantity { get; set; }
    }
}