using DotnetAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly DataContextDapper _dapper;
    public TestController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet]
    public string Test()
    {
        return "Your App is up!";
    }
   
}