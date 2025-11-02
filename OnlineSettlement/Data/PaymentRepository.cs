using OnlineSettlement.Model;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace OnlineSettlement.Data
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllPaymentsAsync();
        Task<Payment> GetPaymentByIdAsync(int paymentId);
        Task<Payment> GetPaymentByVerifyCode(string verifyCode);
        Task<List<Payment>> GetPaymentsByDebitIdAsync(int debitId);
        Task AddPaymentAsync(Payment payment);
        Task UpdatePaymentAsync(Payment payment);
        Task DeletePaymentAsync(int paymentId);
    }



    public class PaymentRepository : IPaymentRepository
    {
        private  string _connectionString;

        public PaymentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Payment>> GetAllPaymentsAsync()
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                await dbConnection.OpenAsync();
                var sql = @"SELECT [PaymentId], [DebitId], [ResultCode], 
                            [ResultMessage], [ResultBody], [PaymentStatus], 
                            [VerifyBody], [VerifyCode], [VerifyMessage], 
                            [CallBackUrl], [CreateDate] 
                            FROM [dbo].[Payment]";
                var result = await dbConnection.QueryAsync<Payment>(sql);
                return result.ToList();
            }
        }

        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                await dbConnection.OpenAsync();
                var sql = @"SELECT [PaymentId], [DebitId], [ResultCode], 
                            [Amount],
                            [ResultMessage], [ResultBody], [PaymentStatus], 
                            [VerifyBody], [VerifyCode], [VerifyMessage], 
                            [CallBackUrl], [CreateDate] 
                            FROM [dbo].[Payment] 
                            WHERE [PaymentId] = @PaymentId";
                return await dbConnection.QueryFirstOrDefaultAsync<Payment>(sql, new { PaymentId = paymentId });
            }
        }
        public async Task<Payment> GetPaymentByVerifyCode(string verifyCode)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                await dbConnection.OpenAsync();
                var sql = @"SELECT [PaymentId], [DebitId], [ResultCode], 
                            [Amount],[ResultMessage], [ResultBody], [PaymentStatus], 
                            [VerifyBody], [VerifyCode], [VerifyMessage], 
                            [CallBackUrl], [CreateDate] 
                            FROM [dbo].[Payment] 
                            WHERE [VerifyCode] = @VerifyCode";
                return await dbConnection.QueryFirstOrDefaultAsync<Payment>(sql, new { VerifyCode = verifyCode });
            }
        }
        public async Task<List<Payment>> GetPaymentsByDebitIdAsync(int debitId)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                await dbConnection.OpenAsync();
                var sql = @"SELECT [PaymentId], [DebitId], [ResultCode], 
                            [Amount],[ResultMessage], [ResultBody], [PaymentStatus], 
                            [VerifyBody], [VerifyCode], [VerifyMessage], 
                            [CallBackUrl], [CreateDate] 
                            FROM [dbo].[Payment] 
                            WHERE [DebitId] = @DebitId";
                var result = await dbConnection.QueryAsync<Payment>(sql, new { DebitId = debitId });
                return result.ToList();
            }
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                await dbConnection.OpenAsync();
                var sql = @"INSERT INTO [dbo].[Payment] 
                            ([DebitId], [ResultCode], [ResultMessage], [ResultBody], 
                            [PaymentStatus], [VerifyBody], [VerifyCode], [VerifyMessage], 
                            [CallBackUrl], [CreateDate],[Amount]) 
                            VALUES 
                            (@DebitId, @ResultCode, @ResultMessage, @ResultBody, 
                            @PaymentStatus, @VerifyBody, @VerifyCode, @VerifyMessage, 
                            @CallBackUrl, @CreateDate,@Amount)";
                await dbConnection.ExecuteAsync(sql, payment);
            }
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                await dbConnection.OpenAsync();
                var sql = @"UPDATE [dbo].[Payment] 
                            SET [DebitId] = @DebitId, 
                                [ResultCode] = @ResultCode, 
                                [ResultMessage] = @ResultMessage, 
                                [ResultBody] = @ResultBody, 
                                [PaymentStatus] = @PaymentStatus, 
                                [VerifyBody] = @VerifyBody, 
                                [VerifyMessage] = @VerifyMessage, 
                                [CallBackUrl] = @CallBackUrl, 
                                [CreateDate] = @CreateDate,
                                [Amount]=@Amount
                            WHERE [PaymentId] = @PaymentId";
                await dbConnection.ExecuteAsync(sql, payment);
            }
        }

        public async Task DeletePaymentAsync(int paymentId)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                await dbConnection.OpenAsync();
                var sql = "DELETE FROM [dbo].[Payment] WHERE [PaymentId] = @PaymentId";
                await dbConnection.ExecuteAsync(sql, new { PaymentId = paymentId });
            }
        }

        //public Task<Payment> GetPaymentByVerifyCode(string verifyCode)
        //{
        //    throw new NotImplementedException();
        //}
    }



}