

namespace Profiles.Sql.Queries
{
    public class DoctorQueries
    {
        public static string All => @"SELECT * FROM ""Doctors""";

        public static string ById => @"SELECT * FROM ""Doctors"" WHERE ""Id"" = @Id";

        public static string Add => @"
        INSERT INTO ""Doctors"" 
            (""Id"",""ImageUrl"", ""FirstName"", ""LastName"", ""MiddleName"", ""DateOfBirth"", 
             ""AccountId"", ""SpecializationId"", ""OfficeId"", ""CareerStartYear"", ""Status"")
        VALUES 
            (@Id,@ImageUrl, @FirstName, @LastName, @MiddleName, @DateOfBirth, 
             @AccountId, @SpecializationId, @OfficeId, @CareerStartYear, @Status)";

        public static string Update => @"
        UPDATE ""Doctors""
        SET 
            ""ImageUrl"" = @ImageUrl,
            ""FirstName"" = @FirstName, 
            ""LastName"" = @LastName, 
            ""MiddleName"" = @MiddleName, 
            ""DateOfBirth"" = @DateOfBirth,
            ""AccountId"" = @AccountId,
            ""SpecializationId"" = @SpecializationId,
            ""OfficeId"" = @OfficeId,
            ""CareerStartYear"" = @CareerStartYear,
            ""Status"" = @Status
        WHERE ""Id"" = @Id";

        public static string Delete => @"DELETE FROM ""Doctors"" WHERE ""Id"" = @Id";
    }
}
