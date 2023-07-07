using APIFinal.DBContext;
using Dapper;
using System.Data;

namespace APIFinal.Services
{
    public interface ITokenService
    {
        public string ReturnPrToken(string token);
    }
    public class TokenService : ITokenService
    {

        public readonly DapperContext? _context;
        public TokenService(DapperContext context)
        {
            _context = context;


        }


        public string ReturnPrToken(string token)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PublicToken", token);
 


            using (IDbConnection dbConnection = _context.Connection)
            {
                dbConnection.Open();
                var result=dbConnection.ExecuteScalar("returnPrToken", parameters, commandType: CommandType.StoredProcedure);
                string resultAsString = result?.ToString();
                return resultAsString;
                dbConnection.Close();

             

            }



           
        }

    }
}
