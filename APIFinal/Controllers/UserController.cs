using APIFinal.Models;
using APIFinal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Transactions;

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

                if (userInfo.StatusCode != null && userInfo.StatusCode != 0)
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
                if (balance.StatusCode != null && balance.StatusCode != 0)
                {
                    return new JsonResult(new { StatusCode = balance.StatusCode });
                }


                var result = new
                {
                    StatusCode = 200,
                    Data = balance.Balance
                };

                return new JsonResult(result);


            }
            catch (Exception)
            {
                return new JsonResult(new { StatusCode = 500 });
            }
        }





        [HttpPost("BetPost")]
        public JsonResult Bet(string PrivateToken, string TransactionId, long Amount, string Currency, int BetTypeId,int GameId, int RoundId)
        {
            try
            {
                var tranModel = new TransactionModel
                {
                    TransactionId = TransactionId,
                    BetTypeId =BetTypeId,
                    Amount = Amount,
                    CreateDate=DateTime.Now,
                    PaymentType="Deposit",
                    Status=1,
                    Currency=Currency,
                    PrivateToken=PrivateToken,
                    GameId=GameId,
                    RoundId=RoundId


                    
                };
                var userInfo = _userService.Bet(tranModel);

                // Assuming there's only one user information returned for the given private token

                if (userInfo.StatusCode != null && userInfo.StatusCode != 0)
                {
                    return new JsonResult(new { StatusCode = userInfo.StatusCode });
                }
                else
                {
                    var success = new
                    {
                        TransactionId=userInfo.Id,
                        CurrentBalance = userInfo.CurrentBalance
                    };

                    var result = new
                    {
                        StatusCode = 200,
                        Data = success
                    };

                    return new JsonResult(result);


                }
            }
            catch (Exception)
            {
                return new JsonResult(new { StatusCode = 500 });
            }
        }




        [HttpPost("Win")]
        public JsonResult Win(string PrivateToken, string TransactionId, long Amount, string Currency, int BetTypeId, int GameId, int RoundId)
        {
            try
            {
                var tranModel = new TransactionModel
                {
                    TransactionId = TransactionId,
                    BetTypeId = BetTypeId,
                    Amount = Amount,
                    CreateDate = DateTime.Now,
                    PaymentType = "Deposit",
                    Status = 1,
                    Currency = Currency,
                    PrivateToken = PrivateToken,
                    GameId = GameId,
                    RoundId = RoundId



                };
                var userInfo = _userService.Win(tranModel);

                // Assuming there's only one user information returned for the given private token

                if (userInfo.StatusCode != null && userInfo.StatusCode != 0)
                {
                    return new JsonResult(new { StatusCode = userInfo.StatusCode });
                }
                else
                {
                    var success = new
                    {
                        TransactionId = userInfo.Id,
                        CurrentBalance = userInfo.CurrentBalance
                    };

                    var result = new
                    {
                        StatusCode = 200,
                        Data = success
                    };

                    return new JsonResult(result);


                }
            }
            catch (Exception)
            {
                return new JsonResult(new { StatusCode = 500 });
            }
        }

    }
}
