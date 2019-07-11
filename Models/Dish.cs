using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ChefsAndDishes.Models
{
    public class Dish
    {
        [Key]
        public int DishId {get;set;}

        [Required(ErrorMessage = "Please enter a dish name!")]
        [MinLength(2)]
        public string Name {get;set;}

        [Required(ErrorMessage = "Please enter a level of tastiness!")]
        public int Tastiness {get;set;}
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage="I bet it has more calories than that!")]
        public int Calories {get;set;}
        [Required]
        public string Description {get;set;}
        public int ChefId {get;set;}
        public Chef Creator {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}