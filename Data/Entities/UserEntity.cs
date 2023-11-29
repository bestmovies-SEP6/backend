﻿using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Data.Entities; 

public class UserEntity {
    [Key]
    public string Username { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }

  

}