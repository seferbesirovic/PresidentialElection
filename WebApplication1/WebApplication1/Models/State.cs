using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class State
    {
        public int Id { get; init; }

        [Required]
        public string Name { get; set; }
        public List<Result> Results { get; set; }
    }
}
