using System;
using System.ComponentModel.DataAnnotations;

namespace Training.Model;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Range(0, 100, ErrorMessage = "Order must be between 0 and 100!")]
    public int Order { get; set; }

}
