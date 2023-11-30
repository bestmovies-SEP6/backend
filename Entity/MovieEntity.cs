using System.ComponentModel.DataAnnotations;

namespace Entity; 

public class MovieEntity {
    [Key]
    public int Id { get; set; }

    public ICollection<WishListEntity>? WishLists { get; set; }
}