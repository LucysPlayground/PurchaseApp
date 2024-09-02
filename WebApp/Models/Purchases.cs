
namespace DotnetAPI.Models
{
    public partial class Purchases
    {
        public int PurchaseId {get; set;}
        public string Name {get; set;}
        public string TypeOfPurchase {get; set;}
        public string NameOfPurchase {get; set;}
        public decimal Price {get; set;}
        public decimal Day_ {get; set;}
        public decimal Month_ {get; set;}
        public decimal Year_ {get; set;}

        public Purchases()
        {
            if (Name == null)
            {
                Name = "";
            }

            if (TypeOfPurchase == null)
            {
                TypeOfPurchase = "";
            }

            if (NameOfPurchase == null)
            {
                NameOfPurchase = "";
            }
        }
    }
}