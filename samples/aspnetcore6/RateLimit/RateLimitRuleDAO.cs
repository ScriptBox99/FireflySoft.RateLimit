using System.Collections.ObjectModel;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace aspnetcore6.RateLimit;

/* 
First execute the sql in mysql.
Then add 'DbConn' section in appsettings.json.

CREATE TABLE `rate_limit_rule` (
  `Id` varchar(40) NOT NULL,
  `Path` varchar(100) NOT NULL,
  `PathType` int(11) NOT NULL,
  `TokenCapacity` int(11) NOT NULL,
  `TokenSpeed` int(11) NOT NULL,
  `AddTime` datetime NOT NULL,
  `UpdateTime` datetime NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4
*/

public class RateLimitRule
{
    public string? Id { get; set; }
    public string? Path { get; set; }
    public LimitPathType PathType { get; set; }
    public int TokenCapacity { get; set; }
    public int TokenSpeed { get; set; }
    public DateTime AddTime { get; set; }
    public DateTime UpdateTime { get; set; }
}

public class RateLimitRuleDAO
{
    private readonly IConfiguration _configuration;

    public RateLimitRuleDAO(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<RateLimitRule>> GetAllRulesAsync()
    {
        var conn = _configuration.GetValue<string>("DbConn");
        using (IDbConnection db = new MySqlConnection(conn))
        {
            return await db.QueryAsync<RateLimitRule>("select * from rate_limit_rule");
        }
    }
}