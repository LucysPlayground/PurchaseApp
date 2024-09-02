using System.Data;
using Dapper;
using DotnetAPI.Data;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly DataContextDapper _dapper;
    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetUsers/{Name}")]
    //public IEnumerable<User> GetUsers()
    public IEnumerable<User> GetUsers(String Name, int Month, int Year)
    {
        string sql = @"EXEC MoneyUsageAppSchema.Users_Get";
        string stringParameters = "";
        DynamicParameters sqlParameters = new DynamicParameters();

        if(Name != null)
        {
            stringParameters += ", @Name= @NameParameter";
            sqlParameters.Add("@NameParameter", Name, DbType.String);
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
        
        IEnumerable<User> users = _dapper.LoadDataWithParameters<User>(sql, sqlParameters);
        return users;
    }

    [HttpPut("AddUser")]
    public IActionResult UpserUser(User user)
    {   
        DynamicParameters sqlParameters = new DynamicParameters();

        string sql = @"MoneyUsageAppSchema.Users_Upsert
                @UserId = @UserIdParameter,
                @Name = @NameParameter, 
                @Salary = @SalaryParameter,
                @Meal_Tickets = @Meal_TicketsParameter,
                @Bonuses = @BonusesParameter";

        sqlParameters.Add("@UserIdParameter", user.UserId, DbType.Int32);
        sqlParameters.Add("@NameParameter", user.Name, DbType.String);
        sqlParameters.Add("@SalaryParameter", user.Salary, DbType.Int32);
        sqlParameters.Add("@Meal_TicketsParameter", user.Meal_Tickets, DbType.Int32);
        sqlParameters.Add("@BonusesParameter", user.Bonuses, DbType.Int32);

        if (_dapper.ExecuteSqlWithParameter(sql, sqlParameters))
        {
            return Ok();
        } 

        throw new Exception("Failed to Update User");

    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"MoneyUsageAppSchema.Users_Delete 
            @UserId = @UserIdParameter";

        DynamicParameters sqlParameters = new DynamicParameters();
        sqlParameters.Add("@UserIdParameter", userId, DbType.Int32);

        if(_dapper.ExecuteSqlWithParameter(sql, sqlParameters))
        {
            return Ok();
        }

        throw new Exception("Failed to delete user!");
    }

    [HttpGet("GetRemaining/{name}")]
    public IEnumerable<RemainingMoney> GetRemaining(String name)
    {
        string sql = @"EXEC MoneyUsageAppSchema.Remaining_Money @Name = @NameParameter";
        string stringParameters = "";
        DynamicParameters sqlParameters = new DynamicParameters();

        if(name != null)
        {
            stringParameters += ", @Name= @NameParameter";
            sqlParameters.Add("@NameParameter", name, DbType.String);
        }
        
        IEnumerable<RemainingMoney> users = _dapper.LoadDataWithParameters<RemainingMoney>(sql, sqlParameters);

        return users;
    }


}
