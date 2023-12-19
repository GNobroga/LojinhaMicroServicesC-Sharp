// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;

namespace Template.Pages.Create;

public class InputModel
{   
    [Display(Name = "Usuário")]
    [Required]
    public string Username { get; set; }

    [Display(Name = "Senha")]
    [Required]
    public string Password { get; set; }

    [Display(Name = "Nome")]
    public string FirstName { get; set; }
    public string Email { get; set; }

    [Display(Name = "Sobrenome")]
    public string LastName { get; set; }

    public string? ReturnUrl { get; set; } 

    public string Button { get; set; }
}