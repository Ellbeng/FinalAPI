using APIFinal.DBContext;
using APIFinal.Models;
using Dapper;
using System.Data;

namespace APIFinal.Services
{
    public interface ITokenService
    {
        public TokenModel ReturnPrToken(string token);
    }
    public class TokenService : ITokenService
    {

        public readonly DapperContext? _context;
        public TokenService(DapperContext context)
        {
            _context = context;


        }


        public TokenModel ReturnPrToken(string token)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PublicToken", token);


            parameters.Add("returnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);


            using (IDbConnection dbConnection = _context.Connection)
            {
                dbConnection.Open();
                var result=dbConnection.QueryFirstOrDefault<TokenModel>("returnPrToken", parameters, commandType: CommandType.StoredProcedure);
                



                int returnValue = parameters.Get<int>("returnValue");
                if (returnValue != 0)
                {

                    result = new TokenModel();
                    result.StatusCode = returnValue;
                }
            
                dbConnection.Close();

                 return result;

            }



           
        }

    }
}
