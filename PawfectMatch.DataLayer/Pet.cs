using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawfectMatch.DataLayer
{
    public class Pet
    {
        [Key]
        public int PetId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public int OwnerId { get; set; }
        public string PetName { get; set; } = null!;


        public ApplicationUser Owner = null!;
        public PetBreed PetBreed { get; set; } = null!;


        //testing example
        public Pet() {
            OwnerId = 0;
            PetName = "Lucky";
            PetBreed.BreedName = "Amstaff";
            PetBreed.BreedType.PetTypeName = "Dog";
        }
    }
}
