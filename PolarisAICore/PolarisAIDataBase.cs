using Newtonsoft.Json.Linq;
using System;
using System.Data.SqlClient;
using System.Globalization;

namespace PolarisAICore {
    public class PolarisAIDataBase {

        readonly SqlConnection _connection;

        public PolarisAIDataBase(string host, string database, string id, string password) {

            _connection = new SqlConnection($"Data Source={host};Initial Catalog={database};User ID={id};Password={password};Connect Timeout=15;");
        }

        public void InsertRequestDetails(Utterance u) {

            var cultureInfo = new CultureInfo("en-US");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            try {

                using (SqlDataAdapter adapter = new SqlDataAdapter()) {

                    _connection.Open();

                    String sql =
                    $"INSERT INTO Request (query, [response-code], response) OUTPUT INSERTED.[request-id] " +
                    $"VALUES ('{u.Query.Replace("'", "''")}', {u.Code}, '{u.Response?.Replace("'", "''")}')";

                    adapter.InsertCommand = new SqlCommand(sql, _connection);
                    var id = adapter.InsertCommand.ExecuteScalar();

                    sql =
                        $"BEGIN " +
                        $"  INSERT INTO Entity ([request-id], [entity-content], [start-index], [end-index], date, time, type) " +
                        $"  VALUES ({id}, {GetJTokenStringFormatted(u.Entity["entity"])}, {GetJTokenNumberFormatted(u.Entity["startIndex"])}, {GetJTokenNumberFormatted(u.Entity["endIndex"])}, " +
                        $"  {GetJTokenStringFormatted(u.Entity["date"])}, {GetJTokenStringFormatted(u.Entity["time"])}, {GetJTokenStringFormatted(u.Entity["type"])}) " +
                        $"END " +
                        $"BEGIN " +
                        $"  IF NOT EXISTS (SELECT [intent-name] FROM Intent " +
                        $"                 WHERE [intent-name] = '{u.TopScoringIntent.Name}')" +
                        $"  BEGIN " +
                        $"      INSERT INTO Intent ([intent-name]) " +
                        $"      VALUES ('{u.TopScoringIntent.Name}') " +
                        $"  END " +
                        $"END ";

                    foreach (Intent intent in u.Intents) {
                        sql = sql +
                            $"BEGIN " +
                            $"  IF NOT EXISTS (SELECT [intent-name] FROM Intent " +
                            $"                 WHERE [intent-name] = '{intent.Name}') " +
                            $"  BEGIN " +
                            $"      INSERT INTO Intent ([intent-name]) " +
                            $"      VALUES ('{intent.Name}') " +
                            $"  END " +
                            $"END " +
                            $"BEGIN " +
                            $"  INSERT INTO RequestIntent ([request-id], [intent-name], [is-top-scoring], [intent-score]) " +
                            $"  VALUES ({id}, '{intent.Name}', {(intent == u.TopScoringIntent ? 1 : 0)}, {intent.Score}) " +
                            $"END ";
                    }

                    adapter.InsertCommand = new SqlCommand(sql, _connection);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex) {

                Console.WriteLine(ex);
            }
            finally {
                _connection.Close();
            }
        }

        String GetJTokenStringFormatted(JToken input) {

            return input != null && input.Type != JTokenType.Null ? $"'{input.ToString().Replace("'", "''")}'" : "null";
        }

        String GetJTokenNumberFormatted(JToken input) {

            return input != null && input.Type != JTokenType.Null ? $"{input}" : "null";
        }
    }
}
