using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Backend.Models;

public partial class User //:IdentityUser
{
    public string? UserName { get; set; }

    public string NatId { get; set; } = null!;

    // [EmailAddress]
    // [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }
    
    // [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}
