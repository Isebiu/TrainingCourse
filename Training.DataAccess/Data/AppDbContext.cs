using System;
using Microsoft.EntityFrameworkCore;
using Training.Models;


namespace Training.DataAccess.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Category> Categories { get; set; }

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
