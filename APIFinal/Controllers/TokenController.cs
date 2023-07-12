﻿using APIFinal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace APIFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {

        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        public TokenController(ITokenService tokenService, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("returnPrToken")]
        public JsonResult ReturnPrToken(string publicToken)
        {
            try
            {
                
                var tokenModel = _tokenService.ReturnPrToken(publicToken);
                if (tokenModel.StatusCode != null && tokenModel.StatusCode != 0)
                {
                    return new JsonResult(new { StatusCode = tokenModel.StatusCode });
                }

                if (!string.IsNullOrEmpty(tokenModel.PrivateToken))
                {
                   
                    var responseData = new { PrivateToken = tokenModel.PrivateToken };
                    return new JsonResult(new { StatusCode = 200, Data = responseData });
                }
                else
                {
                   
                    return new JsonResult(new { StatusCode = 500 });
                }
            }
            catch (Exception ex)
            {
                
                return new JsonResult(new { StatusCode = 500, ErrorMessage = ex.Message });
            }
        }

    }
}
