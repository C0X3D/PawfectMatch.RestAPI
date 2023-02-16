using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawfectMatch.DataLayer
{
    public class PetBreed
    {
        [Key]
        public int PetBreedId { get; set; }

        [ForeignKey(nameof(PetType))]
        public int PetClassId { get; set; }

        //labrador
        public string BreedName { get; set; } = null!;

        public PetType BreedType { get; set;} = null!;
    }
}
