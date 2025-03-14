using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Training.Models;


namespace Training.DataAccess.Data;

public class AppDbContext : IdentityDbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Category> Categories { get; set; }

    public DbSet<FoodType> FoodTypes { get; set; }

    public DbSet<MenuItem> MenuItems { get; set; }  

    public DbSet<AppUser> AppUsers { get; set; }

    public DbSet<ShoppingCart> ShoppingCart {  get; set; }

    public DbSet<OrderHeader> OrderHeader { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }

    // Seeding the table Categories 
    // public object Category { get; internal set; }

    // protected override void OnModelCreating(ModelBuilder modelbuilder)
    // {
    //     modelbuilder.Entity<Category>().HasData(
    //         new Category
    //         {
    //             Id = 1,
    //             Name = "Action",
    //             Order = 1
    //         },
    //         new Category
    //         {
    //             Id = 2,
    //             Name = "SF",
    //             Order = 2
    //         }
    //     );
    // }

}
