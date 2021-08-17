using Movie.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Models.ViewModels
{
    public class IndexViewModel3
    {
        public IEnumerable<MovieModel> Movies { get; set; }
        public string Title { get; set; }
        public Genre? Genre { get; set; }
    }
}
