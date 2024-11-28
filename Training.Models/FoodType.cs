using System;
using System.ComponentModel.DataAnnotations;

namespace Training.Models;

public class FoodType
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
}
