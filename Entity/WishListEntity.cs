namespace Entity; 

public class WishListEntity {

    public int MovieId { get; set; }
    public string Username { get; set; }
    public UserEntity User { get; set; }
    public MovieEntity Movie { get; set; }
}