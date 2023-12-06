namespace Dto; 

public class MovieFilterDto {
    public string Query { get; set; }
    public int? PageNo { get; set; }
    public string? Region { get; set; }
    public int? Year { get; set; }

    public List<string>? Genres { get; set; }
}