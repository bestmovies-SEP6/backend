using System.ComponentModel.DataAnnotations;

namespace Entity; 

public class UserEntity {

    [Key]
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    public ICollection<WishListEntity>? WishLists { get; set; }
}