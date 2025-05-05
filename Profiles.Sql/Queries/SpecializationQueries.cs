

namespace Profiles.Sql.Queries
{
    public class SpecializationQueries
    {
        public static string All => @"SELECT * FROM ""Specializations""";

        public static string ById => @"SELECT * FROM ""Specializations"" WHERE ""Id"" = @Id";
    }

}
