
namespace DotnetAPI.Models
{
    public partial class GetPurchases
    {
        public string Name {get; set;}
        public string Name_of_Purchase {get; set;}
        public decimal Quantity {get; set;}
        public decimal Min {get; set;}
        public decimal Max {get; set;}
        public decimal Average {get; set;}
        public decimal Day {get; set;}
        public decimal Month {get; set;}
        public decimal Year {get; set;}

        public GetPurchases()
        {
            if (Name == null)
            {
                Name = "";
            }
            if (Name_of_Purchase == null)
            {
                Name_of_Purchase = "";
            }
        }
    }
}