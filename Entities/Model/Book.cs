using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Book
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public decimal Price { get; set; }

        //ref : Collection navigation property
        public int CategoryId {  get; set; }
        public Category Category { get; set; }
    }
}
