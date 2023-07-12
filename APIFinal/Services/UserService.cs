using APIFinal.DBContext;
using APIFinal.Models;
using Dapper;
using System.Data;

namespace APIFinal.Services
{

    public interface IUserService
    {
        public UserModel GetUserInformation(string token);
        public BalanceModel GetCurrentBalance(string token);
        public BetReturnModel Bet(TransactionModel transaction);
        public BetReturnModel Win(TransactionModel transaction);
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


        public BalanceModel GetCurrentBalance(string token)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PrivateToken", token);
            parameters.Add("returnValue", dbType: System.Data.DbType.Int32, direction: ParameterDirection.ReturnValue);

            using (IDbConnection dbConnection = _context.Connection)
            {
                dbConnection.Open();
                var result = dbConnection.QueryFirstOrDefault<BalanceModel>("GetWalletBalance", parameters, commandType: CommandType.StoredProcedure);
                
               
                dbConnection.Close();

                int returnValue = parameters.Get<int>("returnValue");
                if (returnValue != 0)
                {

                    result = new BalanceModel();
                    result.StatusCode = returnValue;
                }
                return result;
            }
        }



        public BetReturnModel Bet(TransactionModel transaction)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PrivateToken", transaction.PrivateToken);
            parameters.Add("BetTypeId", transaction.BetTypeId);
            parameters.Add("Amount", transaction.Amount);
            parameters.Add("Currency", transaction.Currency);
            parameters.Add("PaymentType", transaction.PaymentType);
            parameters.Add("CreateDate", transaction.CreateDate);
            parameters.Add("TransactionId", transaction.TransactionId);
            parameters.Add("Status", transaction.Status);
            parameters.Add("GameId", transaction.GameId);
            parameters.Add("RoundId", transaction.RoundId);

            parameters.Add("returnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);




            using (IDbConnection dbConnection = _context.Connection)
            {
                dbConnection.Open();


                var result = dbConnection.QueryFirstOrDefault<BetReturnModel>("InsertAPITransaction", parameters, commandType: CommandType.StoredProcedure);


                dbConnection.Close();

                int returnValue = parameters.Get<int>("returnValue");
                if (returnValue != 0)
                {

                    result = new BetReturnModel();
                    result.StatusCode = returnValue;
                }


                return result;
            }
        }



        public BetReturnModel Win(TransactionModel transaction)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PrivateToken", transaction.PrivateToken);
            parameters.Add("BetTypeId", transaction.BetTypeId);
            parameters.Add("Amount", transaction.Amount);
            parameters.Add("Currency", transaction.Currency);
            parameters.Add("PaymentType", transaction.PaymentType);
            parameters.Add("CreateDate", transaction.CreateDate);
            parameters.Add("TransactionId", transaction.TransactionId);
            parameters.Add("Status", transaction.Status);
            parameters.Add("GameId", transaction.GameId);
            parameters.Add("RoundId", transaction.RoundId);

            parameters.Add("returnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);




            using (IDbConnection dbConnection = _context.Connection)
            {
                dbConnection.Open();


                var result = dbConnection.QueryFirstOrDefault<BetReturnModel>("Win", parameters, commandType: CommandType.StoredProcedure);


                dbConnection.Close();

                int returnValue = parameters.Get<int>("returnValue");
                if (returnValue != 0)
                {

                    result = new BetReturnModel();
                    result.StatusCode = returnValue;
                }


                return result;
            }
        }
    }
}
