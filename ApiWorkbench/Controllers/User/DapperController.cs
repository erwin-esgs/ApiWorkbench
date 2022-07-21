using Microsoft.AspNetCore.Mvc;
using ApiWorkbench.Properties;
using System.Data.SqlClient;
using ApiWorkbench.Models;
using Dapper;

namespace ApiWorkbench.Controllers.User;

[ApiController]
//[Route("[controller]")]
[Route("dapper")]
public class DapperController : Controller
{

    [HttpGet]
    //[Route("show")]
    public IActionResult show()
    {
        List<dynamic> list = new List<dynamic>();
        DapperContext dapper = new();
        using (var conn = dapper.GetConnection())
        {
            var sql = "select * from Users";
            var results = conn.Query(sql);
            foreach (var item in results)
            {
                dynamic obj = new System.Dynamic.ExpandoObject();
                obj.UserId = item.UserId;
                obj.Name = item.Name;
                obj.Username = item.Username;
                list.Add(obj);
            }
        }
        return Ok(list.ToArray());
    }

    [HttpPost]
    //[Route("add")]
    public async Task<IActionResult> add()
    {

        var stringBody = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var body = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(stringBody);


        if (body["Name"] != null && body["Username"] != null && body["Password"] != null)
        {
            DapperContext dapper = new();
            using (var conn = dapper.GetConnection())
            {
                UserModel user = new();
                user.Name = body["Name"];
                user.Username = body["Username"];
                user.Password = Operation.sha256_hash(body["Password"].ToString() ) ;
                user.LastLogin = 0;

                var sql = "INSERT INTO Users (Name, Username, Password, LastLogin) VALUES(@Name, @Username, @Password, @LastLogin)";
                int rowsAffected = conn.Execute(sql, user);
                return Ok(rowsAffected);
            }
        }
        return Ok(false);
    }

    [HttpPatch]
    [Route("{_id}")]
    public async Task<IActionResult> edit(string _id)
    {

        var stringBody = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var body = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(stringBody);

        int id=0;
        if ( (body["Name"] != null || body["Username"] != null || body["Password"] != null || body["LastLogin"] != null) && Int32.TryParse(_id, out id) )
        {

            DapperContext dapper = new();
            using (var conn = dapper.GetConnection())
            {
                var sql = "SELECT TOP 1 * FROM Users WHERE UserId = @Id ";
                var results = conn.Query(sql, new { Id = id });

                UserModel user = new();
                foreach (var item in results)
                {
                    user.UserId = item.UserId;
                    user.Name = item.Name;
                    user.Username = item.Username;
                    user.Password = item.Password;
                    user.LastLogin = item.LastLogin;
                }

                if (body["Name"] != null) user.Name = body["Name"];
                if (body["Username"] != null) user.Username = body["Username"];
                if (body["Password"] != null) user.Password = Operation.sha256_hash(body["Password"].ToString() );
                if (body["LastLogin"] != null) user.Name = body["LastLogin"];

                sql = "UPDATE Users SET Name = @Name , Username = @Username , Password = @Password , LastLogin = @LastLogin  WHERE UserId = @UserId;";
                int rowsAffected = conn.Execute(sql, user);
                return Ok(rowsAffected);
            }
        }
        return Ok(false);
    }

    [HttpDelete]
    [Route("{_id}")]
    public async Task<IActionResult> delete(string _id)
    {
        int id = 0;
        if (Int32.TryParse(_id, out id))
        {
            DapperContext dapper = new();
            using (var conn = dapper.GetConnection())
            {
                string sql = "DELETE FROM Users WHERE UserId = @Id;";
                int rowsAffected = conn.Execute(sql, new { Id = id });
                return Ok(rowsAffected);
            }
        }
        return Ok(false);
    }

}
