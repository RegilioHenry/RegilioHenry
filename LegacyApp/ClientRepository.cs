using LegacyApp.Enums;
using LegacyApp.Interfaces;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LegacyApp
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientRepository : IClientRepository
    {
        #region local declares
        private readonly IClientRepository clientRepository;
        #endregion

        #region constructors
        public ClientRepository(IClientRepository clientrepository) => clientRepository = clientrepository;
        public ClientRepository() => clientRepository = this;
        #endregion

        #region GetById
        public IClient GetById(int clientid)
        {
            Client client = null;
            var connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "uspGetClientById"
                };

                var parameter = new SqlParameter("@ClientId", SqlDbType.Int) { Value = clientid };
                command.Parameters.Add(parameter);

                connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    client = new Client
                                      {
                                          Id = int.Parse(reader["ClientId"].ToString()),
                                          Name = reader["Name"].ToString(),
                                          ClientStatus = (ClientStatus)int.Parse(reader["ClientStatusId"].ToString())
                                      };
                }
            }

            return client;
        }
        #endregion
        #region GetByIdAsync
        public async Task<IClient> GetByIdAsync(int clientid)
        {
            var result = await Task.Run(() => GetById(clientid));
            
            return result;
        }
        #endregion

    }
}
