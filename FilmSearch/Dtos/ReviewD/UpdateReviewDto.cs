namespace FilmSearch.Dtos.ReviewD
{
    public class UpdateReviewDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Stars { get; set; }
    }
}
