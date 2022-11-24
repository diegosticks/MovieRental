﻿using System.ComponentModel.DataAnnotations;

namespace MovieRental.Models
{
    public class Genre
    {
        [Key]
        public byte Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
