using APIFinal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace APIFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("info")]
        public JsonResult GetPlayerInfo(string privateToken)
        {
            try
            {
                var userInfo = _userService.GetUserInformation(privateToken);

                // Assuming there's only one user information returned for the given private token

                if (userInfo.StatusCode != null)
                {
                    return new JsonResult(new { StatusCode = userInfo.StatusCode });
                }
                else
                {
                    var playerInfo = new
                    {
                        UserId = userInfo.UserId,
                        UserName = userInfo.UserName,
                        FirstName = userInfo.FirstName,
                        LastName = userInfo.LastName,
                        Email = userInfo.Email,
                        CountryCode = "GE",
                        CountryName = "Georgia",
                        Gender = 1,
                        Currency= "GEL",
                        CurrentBalance = userInfo.Balance
                    };

                    var result = new
                    {
                        StatusCode = 200,
                        Data = playerInfo
                    };

                    return new JsonResult(result);


                }
            }
            catch (Exception)
            {
                return new JsonResult(new { StatusCode = 500 });
            }
        }




        [HttpPost("GetBalance")]
        public JsonResult GetBalance(string privateToken)
        {
            try
            {
                var balance = _userService.GetCurrentBalance(privateToken);

 

                var result = new
                {
                    StatusCode = 200,
                    Data = balance
                };

                return new JsonResult(result);


            }
            catch (Exception)
            {
                return new JsonResult(new { StatusCode = 500 });
            }
        }
    }
}
