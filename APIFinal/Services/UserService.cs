using APIFinal.DBContext;
using APIFinal.Models;
using Dapper;
using System.Data;

namespace APIFinal.Services
{

    public interface IUserService
    {
        public UserModel GetUserInformation(string token);
        public decimal GetCurrentBalance(string token);
    }
    public class UserService :IUserService
    {
        public readonly DapperContext? _context;
        public UserService(DapperContext context)
        {
            _context = context;

        }

        public UserModel? GetUserInformation(string token)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PrivateToken", token);

            parameters.Add("returnValue", dbType: System.Data.DbType.Int32, direction: ParameterDirection.ReturnValue);
           



            using (IDbConnection dbConnection = _context.Connection)
            {
                dbConnection.Open();
                
                
                var result = dbConnection.Query<UserModel>("GetUserInformation", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                
               
                dbConnection.Close();

                int returnValue = parameters.Get<int>("returnValue");
                if (returnValue != 0)
                {
                    result = new UserModel();
                    result.StatusCode = returnValue;
                }
                
                
                return result;
            }
        }


        public decimal GetCurrentBalance(string token)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PrivateToken", token);


            using (IDbConnection dbConnection = _context.Connection)
            {
                dbConnection.Open();
                var result2 = dbConnection.ExecuteScalar("GetWalletBalance", parameters, commandType: CommandType.StoredProcedure);
                var result = Convert.ToDecimal(result2);
                return result;
                dbConnection.Close();

            }
        }
    }
}
