using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using StackExchange.Redis;
using WebAPI.Connectors;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class PostgresService : IDisposable
    {
        private readonly PostgresConnector postgresConnector;
        private readonly NpgsqlConnection postgresDatabase;

        public PostgresService(string postgresIp) {
            postgresConnector = new PostgresConnector(postgresIp);
            postgresDatabase = postgresConnector.GetDatabase();
        }

        public async Task<AccountModel> GetAccount(int id) {
            using (var cmd = new NpgsqlCommand("SELECT * FROM accounts WHERE account_id = @id", postgresDatabase)) {
                cmd.Parameters.AddWithValue("id", id);
                
                using (var reader = await cmd.ExecuteReaderAsync()) {
                    if(await reader.ReadAsync())
                        return new AccountModel() {
                            Id = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            Firstname = reader.GetString(2),
                            Lastname = reader.GetString(3)
                        };
                }
            }
            return null;
        }

        public async Task<List<AccountModel>> GetAllAccounts() {
            using (var cmd = new NpgsqlCommand("SELECT * FROM accounts", postgresDatabase)) {
                using (var reader = await cmd.ExecuteReaderAsync()) {
                    List<AccountModel> accounts = new List<AccountModel>();
                    while (await reader.ReadAsync())
                        accounts.Add(new AccountModel() {
                            Id = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            Firstname = reader.GetString(2),
                            Lastname = reader.GetString(3)
                        });
                    return accounts;
                }
            }
        }

        public async Task<AccountModel> CreateAccount(AccountModel accountModel) {
            using (var cmd = new NpgsqlCommand(
                    @"INSERT INTO accounts 
                    (email, firstname, lastname, password)
                        VALUES
                    (@email, @firstname, @lastname, @password)
                        RETURNING account_id", postgresDatabase)) {
                cmd.Parameters.AddWithValue("email", accountModel.Email);
                cmd.Parameters.AddWithValue("firstname", accountModel.Firstname);
                cmd.Parameters.AddWithValue("lastname", accountModel.Lastname);
                cmd.Parameters.AddWithValue("password", accountModel.Password);

                var id = await cmd.ExecuteScalarAsync();
                accountModel.Id = (int)id;
                accountModel.Password = null;
                return accountModel;
            }
        }

        public void Dispose() {
            postgresConnector.Dispose();
        }
    }
}