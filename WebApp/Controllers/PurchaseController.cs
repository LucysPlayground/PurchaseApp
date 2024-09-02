using System.Data;
using Dapper;
using DotnetAPI.Data;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PurchaseController : ControllerBase
{
    private readonly DataContextDapper _dapper;
    public PurchaseController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetPurchases/{Name}")]
    //public IEnumerable<User> GetUsers()
    public IEnumerable<GetPurchases> GetPurchases(String Name, int Day, int Month, int Year)
    {
        string sql = @"EXEC MoneyUsageAppSchema.GetPurchasesByName";
        string stringParameters = "";
        DynamicParameters sqlParameters = new DynamicParameters();

        if(Name != null)
        {
            stringParameters += ", @Name= @NameParameter";
            sqlParameters.Add("@NameParameter", Name, DbType.String);
        }

        if(Day > 0)
        {
            stringParameters += ", @Day= @DayParameter";
            sqlParameters.Add("@DayParameter", Day, DbType.Int32);
        }

        if(Month > 0)
        {
            stringParameters += ", @Month= @MonthParameter";
            sqlParameters.Add("@MonthParameter", Month, DbType.Int32);
        }

        if(Year > 0)
        {
            stringParameters += ", @Year= @YearParameter";
            sqlParameters.Add("@YearParameter", Year, DbType.Int32);
        }
        
        if(stringParameters.Length > 0)
        {
            sql += stringParameters.Substring(1);
        }
        
        IEnumerable<GetPurchases> users = _dapper.LoadDataWithParameters<GetPurchases>(sql, sqlParameters);
        return users;
    }

    [HttpPut("AddPurchase")]
    public IActionResult AddPurchase(Purchases expences)
    {   
        DynamicParameters sqlParameters = new DynamicParameters();

        string sql = @"MoneyUsageAppSchema.Purchase_Upsert
                    @Name = @NameParameter,
                    @TypePurchase = @TypePurchaseParameter,
                    @NamePurchase = @NamePurchaseParameter,
                    @Price = @PriceParameter";

        sqlParameters.Add("@NameParameter", expences.Name, DbType.String);
        sqlParameters.Add("@TypePurchaseParameter", expences.TypeOfPurchase, DbType.String);
        sqlParameters.Add("@NamePurchaseParameter", expences.NameOfPurchase, DbType.String);
        sqlParameters.Add("@PriceParameter", expences.Price, DbType.Decimal);

        if (_dapper.ExecuteSqlWithParameter(sql, sqlParameters))
        {
            return Ok();
        } 

        throw new Exception("Failed to Update User");

    }

    [HttpDelete("DeletePurchase/{purchaseId}")]
    public IActionResult DeletePurchase(int purchaseId)
    {
        string sql = @"MoneyUsageAppSchema.Purchase_Delete 
            @PurchaseId = @PurchaseIdParameter";

        DynamicParameters sqlParameters = new DynamicParameters();
        sqlParameters.Add("@PurchaseIdParameter", purchaseId, DbType.Int32);

        if(_dapper.ExecuteSqlWithParameter(sql, sqlParameters))
        {
            return Ok();
        }

        throw new Exception("Failed to delete user!");
    }
}
