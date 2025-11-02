using OnlineSettlement.Model;
using Dapper;
using System.Data;
using Microsoft.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace OnlineSettlement.Data
{
    public interface IDebitRepository
    {
        Task<List<Debit>> GetAllDebitsAsync();
        Task<List<Debit>> GetAllDebitsAsync2();
        Task<Debit> GetDebitByIdAsync(int debitId);
        Task<Debit> GetDebitByLinkIdAsync(string linkId);
        Task AddDebitAsync(Debit debit);
        Task UpdateDebitAsync(Debit debit);
        Task UpdateDebitStatus(int debitId);
        Task DeleteDebitAsync(int debitId);
    }

    public class DebitRepository : IDebitRepository
    {
        private string _connectionString;

        public DebitRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Debit>> GetAllDebitsAsync()
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                var sql = @"SELECT [DebitId], [FullName], [NationCode], [Mobile], 
                            [Description],DoctorName,FactorNumber, [DebitAmount], [LinkId], [CreateDate], 
                            [IsPayed], [PayDate] 
                            FROM [dbo].[Debit]";
                var result = await dbConnection.QueryAsync<Debit>(sql);
                return result.ToList();
            }
        }
        public async Task<List<Debit>> GetAllDebitsAsync2()
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                var sql = @"SELECT D.[DebitId], d.[FullName], d.[NationCode], d.[Mobile], 
                            d.[Description],d.DoctorName,d.FactorNumber, d.[DebitAmount], d.[LinkId], d.[CreateDate], 
                            d.[IsPayed], d.[PayDate] ,ISNULL(P.ResultCode,'') AS ResultCode
                            FROM [dbo].[Debit] As d
                            Left Join Payment P On P.DebitId=d.DebitId And P.PaymentStatus=4";

                var result = await dbConnection.QueryAsync<Debit>(sql);
                return result.ToList();
            }
        }

        public async Task<Debit> GetDebitByIdAsync(int debitId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                 dbConnection.Open();
                var sql = @"SELECT [DebitId], [FullName], [NationCode], [Mobile], 
                            [Description],DoctorName,FactorNumber, [DebitAmount], [LinkId], [CreateDate], 
                            [IsPayed], [PayDate] 
                            FROM [dbo].[Debit] 
                            WHERE [DebitId] = @DebitId";
                return await dbConnection.QueryFirstOrDefaultAsync<Debit>(sql, new { DebitId = debitId });
            }
        }

        public async Task<Debit> GetDebitByLinkIdAsync(string linkId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                var sql = @"SELECT [DebitId], [FullName], [NationCode], [Mobile], 
                            [Description], DoctorName,FactorNumber,[DebitAmount], [LinkId], [CreateDate], 
                            [IsPayed], [PayDate] 
                            FROM [dbo].[Debit] 
                            WHERE [LinkId] = @LinkId";
                return await dbConnection.QueryFirstOrDefaultAsync<Debit>(sql, new { LinkId = linkId });
            }
        }

        public async Task AddDebitAsync(Debit debit)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                var sql = @"INSERT INTO [dbo].[Debit] 
                            ([FullName], [NationCode], [Mobile], [Description], DoctorName,FactorNumber,
                                [DebitAmount], [LinkId], [CreateDate], [IsPayed], [PayDate]) 
                            VALUES 
                            (@FullName, @NationCode, @Mobile, @Description, @DoctorName,@FactorNumber,
                                     @DebitAmount, @LinkId, @CreateDate, @IsPayed, @PayDate)";
                await dbConnection.ExecuteAsync(sql, debit);
            }
        }

        public async Task UpdateDebitAsync(Debit debit)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                var sql = @"UPDATE [dbo].[Debit] 
                            SET [FullName] = @FullName, 
                                [NationCode] = @NationCode, 
                                [Mobile] = @Mobile, 
                                [Description] = @Description, 
                                DoctorName = @DoctorName,
                                FactorNumber = @FactorNumber,
                                [DebitAmount] = @DebitAmount, 
                                [LinkId] = @LinkId, 
                                [CreateDate] = @CreateDate, 
                                [IsPayed] = @IsPayed, 
                                [PayDate] = @PayDate 
                            WHERE [DebitId] = @DebitId";
                await dbConnection.ExecuteAsync(sql, debit);
            }
        }

        public async Task DeleteDebitAsync(int debitId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                var sql = "DELETE FROM [dbo].[Debit] WHERE [DebitId] = @DebitId";
                await dbConnection.ExecuteAsync(sql, new { DebitId = debitId });
            }
        }

        public async Task UpdateDebitStatus(int debitId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                var sql = $@"UPDATE [dbo].[Debit] 
                            SET 
                                [IsPayed] = 1, 
                                [PayDate] = '{DateTime.Now}' 
                            WHERE [DebitId] ={debitId} ";
                await dbConnection.ExecuteAsync(sql);
            }
        }
    }
}