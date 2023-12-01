using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity; 

public class WishListEntity {

    [ForeignKey(nameof(MovieEntity))]
    public int MovieId { get; set; }

    [ForeignKey(nameof(UserEntity))]
    public string Username { get; set; }

    [Required]
    public DateTime WishListedAt { get; set; }

    // EFC stuffs
    public UserEntity? User { get; set; }

    public MovieEntity? Movie { get; set; }
}