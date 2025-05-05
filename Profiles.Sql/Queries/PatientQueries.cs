
namespace Profiles.Sql.Queries
{
    public class PatientQueries
    {
        public static string All => @"SELECT * FROM ""Patients""";

        public static string ById => @"SELECT * FROM ""Patients"" WHERE ""Id"" = @Id";

        public static string Add => @"
        INSERT INTO ""Patients"" 
            (""Id"",""ImageUrl"", ""FirstName"", ""LastName"", ""MiddleName"", ""DateOfBirth"", ""AccountId"") 
        VALUES 
            (@Id,@ImageUrl, @FirstName, @LastName, @MiddleName, @DateOfBirth, @AccountId)";

        public static string Update => @"
        UPDATE ""Patients"" 
        SET 
            ""ImageUrl"" = @ImageUrl,
            ""FirstName"" = @FirstName, 
            ""LastName"" = @LastName, 
            ""MiddleName"" = @MiddleName, 
            ""DateOfBirth"" = @DateOfBirth,
            ""AccountId"" = @AccountId
        WHERE ""Id"" = @Id";

        public static string Delete => @"DELETE FROM ""Patients"" WHERE ""Id"" = @Id";
    }
}
