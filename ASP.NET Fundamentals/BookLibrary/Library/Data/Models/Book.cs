﻿//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Library.Data.Models
//{
//    public class Book
//    {
//        public int Id { get; set; }

//        [Required]
//        [StringLength(50, MinimumLength = 10)]
//        public string Title { get; set; }

//        [Required]
//        [StringLength(50, MinimumLength = 5)]
//        public string Author { get; set; }

//        [Required]
//        [StringLength(5000, MinimumLength = 5)]
//        public string Description { get; set; }

//        [Required]
//        public string ImageUrl { get; set; }

//        [Required]
//        public decimal Rating { get; set; }

//        [ForeignKey(nameof(Category))]
//        public int CategoryId { get; set; }

//        [Required]
//        public Category Category { get; set; }

//        public List<ApplicationUserBook> ApplicationUsersBooks { get; set; } = new List<ApplicationUserBook>();
//    }
//}
