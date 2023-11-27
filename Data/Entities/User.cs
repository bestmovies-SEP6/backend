using System.ComponentModel.DataAnnotations;

namespace Data.Entities; 

public class User {
    [Key]
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

}