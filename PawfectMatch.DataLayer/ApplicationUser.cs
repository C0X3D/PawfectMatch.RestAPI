namespace PawfectMatch.DataLayer
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;



        public UserProfile? Profile { get; set; } = null!;

        public IList<Pet>? Pets { get; set; }

        public ApplicationUser()
        {
            
        }
    }
}