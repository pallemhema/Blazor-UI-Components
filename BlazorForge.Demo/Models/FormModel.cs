using System.ComponentModel.DataAnnotations;
namespace BlazorForge.Demo.Models;
public sealed class FormModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 2)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string? Address { get; set;}

        [Required(ErrorMessage = "Please select a country.")]
        public string? Country { get; set; }
    }