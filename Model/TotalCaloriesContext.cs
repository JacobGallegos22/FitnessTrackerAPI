using System;
using Microsoft.EntityFrameworkCore;
namespace MyFitnessTrackerAPI.Model
{
    public class TotalCaloriesContext: DbContext
    {
        public TotalCaloriesContext(DbContextOptions<TotalCaloriesContext> options)
              : base(options)
        {
        }

        public DbSet<TotalCalories> TotalCalories { get; set; }
    }
}
