using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity; 

public class ReviewEntity {

    [ForeignKey(nameof(UserEntity))]
    public string Author { get; set; }

    [ForeignKey(nameof(MovieId))]
    public int MovieId { get; set; }

    [Required]
    public DateTime AuthoredAt { get; set; }

    [Required] public double Rating { get; set; }

    public string? ReviewDescription { get; set; }


    // EFC stuffs
    public UserEntity? User { get; set; }
    public MovieEntity? Movie { get; set; }





}