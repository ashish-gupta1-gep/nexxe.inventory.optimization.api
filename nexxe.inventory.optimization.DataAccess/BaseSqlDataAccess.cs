using dm.lib.azuresqlstorage.nuget;
using dm.lib.core.nuget;
using nexxe.inventory.optimization.Core;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace nexxe.inventory.optimization.DataAccess
{
    public abstract class BaseSqlDataAccess : BaseDataAccess
    {
        public ReliableSqlDatabase sqlHelper;
        protected BaseSqlDataAccess(IGepService gepservice) : base(gepservice)
        {
            string constring = UserContext.GetConfig(PartnerConfigKey.SQL_CONNECTION);

            // This condition will not be there in real world scenario
            if (!string.IsNullOrEmpty(constring))
                sqlHelper = new ReliableSqlDatabase(constring, internalLogger);
        }
        protected async Task<T> ExecuteSqlReaderWithStoreProcAsync<T>(string spName, SqlParameter[] parameters, Func<IDataReader, T> action)
        {
            T result;
            using (var conn = (SqlConnection)sqlHelper.CreateConnection())
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    using (var read = await sqlHelper.ExecuteReaderAsync(cmd, CommandBehavior.CloseConnection))
                    {
                        result = (T)action(read);
                    }
                }
            }
            return result;
        }
    }
}
