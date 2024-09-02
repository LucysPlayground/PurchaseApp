
namespace DotnetAPI.Models
{
    public partial class RemainingMoney
    {
        public string Name {get; set;}
        public decimal Total_Money {get; set;}
        public decimal WhatRemains {get; set;}
        public decimal Day {get; set;}
        public decimal Month {get; set;}
        public decimal Year {get; set;}

        public RemainingMoney()
        {
            if (Name == null)
            {
                Name = "";
            }
        }
    }
}