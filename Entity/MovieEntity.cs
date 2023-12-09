using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity; 

public class MovieEntity {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // Do not auto-generate the primary key.
    public int Id { get; set; }
    public ICollection<WishListEntity>? WishLists { get; set; }
    public ICollection<FavoriteEntity>? Favorites
    
    { get; set; }
    public ICollection<ReviewEntity>? Reviews { get; set; }
}