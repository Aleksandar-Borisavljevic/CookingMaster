using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingMaster.Models
{
    public class CuisineType
    {
        public int CuisineId { get; set; }
        public required string CuisineName { get; set; } 
        public required string Uid { get; set; } 
    }
}
