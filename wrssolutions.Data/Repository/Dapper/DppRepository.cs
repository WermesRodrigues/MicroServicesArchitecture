using wrssolutions.Data.Repository.Dapper.Interface;
using Dapper;
using Microsoft.Data.SqlClient;

namespace wrssolutions.Data.Repository.Dapper
{
    public class DppRepository : IDppRepository
    {
        private readonly string conn;

        public DppRepository(string _conn)
        {
            conn = _conn;
        }

        public string GetDatabaseName()
        {
            var dbConn = new SqlConnection(conn);

            try
            {
                using (dbConn)
                {
                    string? result = dbConn.Query<string>("SELECT DB_NAME()").FirstOrDefault();

                    if (!string.IsNullOrEmpty(result))
                    {
                        return result.ToString();
                    }

                    return "";
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(this.GetType().Name + "|" + ex.Message.ToString());
            }
            finally
            {
                dbConn.Close();
                dbConn.Dispose();
                GC.SuppressFinalize(this);
            }
        }
        /// <summary>
        /// Method: Update manual para campos especificos
        /// </summary>
        /// <param name="sql">Consulta SQL</param>
        public virtual bool UpdateGeneric(string sql)
        {
            var dbConn = new SqlConnection(conn);

            try
            {
                if (!string.IsNullOrEmpty(sql))
                {
                    int vRet = 0;
                    using (dbConn)
                    {
                        vRet = dbConn.Execute(sql);
                    }
                    if (vRet > 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(this.GetType().Name + "|" + ex.Message.ToString());
            }
            finally
            {
                dbConn.Close();
                dbConn.Dispose();
                GC.SuppressFinalize(this);
            }
        }
        /// <summary>
        /// Method: Select manual para trazer o primeiro resultado
        /// </summary>
        /// <param name="sql">Consulta SQL</param>
        /// <param name="parametros">Parametros de Pesquisa</param>
        /// <param name="timeout">Tempo de Timeout</param>
        /// <typeparam name="T">Tipo Retorno</typeparam>
        public virtual T QueryFirstOrDefault<T>(string sql)
        {
            var dbConn = new SqlConnection(conn);

            try
            {
                if (!string.IsNullOrEmpty(sql))
                {
                    using (dbConn)
                    {
                        var result = dbConn.QueryFirstOrDefault<T>(sql);

                        if (result != null)
                        {
                            return result;
                        }

                        return default!;
                    }
                }
                else
                {
                    return default!;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(this.GetType().Name + "|" + ex.Message.ToString());
            }
            finally
            {
                dbConn.Close();
                dbConn.Dispose();
                GC.SuppressFinalize(this);
            }
        }
        /// <summary>
        /// Method: Select manual para trazer lista
        /// </summary>
        /// <param name="sql">Consulta SQL</param>
        /// <param name="parametros">Parametros de Pesquisa</param>
        /// <param name="timeout">Tempo de Timeout</param>
        /// <typeparam name="T">Tipo Retorno</typeparam>
        public virtual List<T> QueryList<T>(string sql)
        {
            var dbConn = new SqlConnection(conn);

            try
            {
                if (!string.IsNullOrEmpty(sql))
                {
                    using (dbConn)
                    {
                        return dbConn.Query<T>(sql).ToList();
                    }
                }
                else
                {
                    return default!;
                }
            }
            catch (SqlException ex)
            {

                throw new Exception(this.GetType().Name + "|" + ex.Message.ToString());
            }
            finally
            {
                dbConn.Close();
                dbConn.Dispose();
                GC.SuppressFinalize(this);
            }
        }
        /// <summary>
        /// Method: Select manual para trazer contar o numero de resultado
        /// </summary>
        /// <param name="sql">Consulta SQL</param>
        /// <param name="parametros">Parametros de Pesquisa</param>
        /// <param name="timeout">Tempo de Timeout</param>
        /// <typeparam name="T">Tipo Retorno</typeparam>
        public virtual int QueryAsCount(string sql)
        {
            var dbConn = new SqlConnection(conn);
            try
            {
                if (!string.IsNullOrEmpty(sql))
                {
                    using (dbConn)
                    {
                        return dbConn.Query(sql).ToList().Count();
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (SqlException ex)
            {

                throw new Exception(this.GetType().Name + "|" + ex.Message.ToString());
            }
            finally
            {
                dbConn.Close();
                dbConn.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}
