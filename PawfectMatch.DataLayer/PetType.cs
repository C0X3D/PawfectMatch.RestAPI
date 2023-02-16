using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawfectMatch.DataLayer
{
    public class PetType
    {
        [Key]
        public int PetTypeId { get; set; }

        //dog, cat, bunny...
        public string PetTypeName { get; set; } = null!;
    }
}
