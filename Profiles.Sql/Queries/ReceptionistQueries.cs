
namespace Profiles.Sql.Queries
{
    public class ReceptionistQueries
    {
        public static string All => @"SELECT * FROM ""Receptionists""";

        public static string ById => @"SELECT * FROM ""Receptionists"" WHERE ""Id"" = @Id";

        public static string Add => @"
        INSERT INTO ""Receptionists"" 
            (""Id"",""ImageUrl"", ""FirstName"", ""LastName"", ""MiddleName"", ""AccountId"", ""OfficeId"") 
        VALUES 
            (@Id,@ImageUrl, @FirstName, @LastName, @MiddleName, @AccountId, @OfficeId)";

        public static string Update => @"
        UPDATE ""Receptionists"" 
        SET 
            ""ImageUrl"" = @ImageUrl,
            ""FirstName"" = @FirstName, 
            ""LastName"" = @LastName, 
            ""MiddleName"" = @MiddleName, 
            ""AccountId"" = @AccountId,
            ""OfficeId"" = @OfficeId
        WHERE ""Id"" = @Id";

        public static string Delete => @"DELETE FROM ""Receptionists"" WHERE ""Id"" = @Id";
    }

}
