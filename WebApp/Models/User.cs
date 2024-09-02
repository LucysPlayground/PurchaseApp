
namespace DotnetAPI.Models
{
    public partial class User
    {
        public int UserId {get; set;}
        public string Name {get; set;}
        public decimal Salary {get; set;}
        public decimal Meal_Tickets {get; set;}
        public decimal Bonuses {get; set;}
        public decimal Total_Money{get; set;}
        public decimal Day_ {get; set;}
        public decimal Month_ {get; set;}
        public decimal Year_ {get; set;}

        public User()
        {
            if (Name == null)
            {
                Name = "";
            }
        }
    }
}