using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ChefsAndDishes.Models
{
    public class Chef
    {
        [Key]
        public int ChefId {get;set;}

        [Required(ErrorMessage = "Please enter a first name!")]
        [MinLength(2)]
        public string FName {get;set;}
        [Required(ErrorMessage = "Please enter a last name!")]
        [MinLength(2)]
        public string LName {get;set;}
        [Required(ErrorMessage = "Please enter a birthday!")]
        public DateTime Birthday{get;set;}
        public List<Dish> CreatedDishes {get;set;}
        public int Age {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}