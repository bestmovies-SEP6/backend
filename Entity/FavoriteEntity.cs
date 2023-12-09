using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity; 

public class FavoriteEntity {
    
    [ForeignKey(nameof(MovieEntity))]
    public int MovieId { get; set; }

    [ForeignKey(nameof(UserEntity))]
    public string Username { get; set; }

    [Required]
    public DateTime FavoritedAt { get; set; }

    // EFC stuffs
    public UserEntity? User { get; set; }

    public MovieEntity? Movie { get; set; }
}