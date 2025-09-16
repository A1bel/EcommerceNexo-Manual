namespace EcommerceNexo.Global
{
    public class Config
    {
        public static string dbHost = string.Empty;
        public static string dbPort = string.Empty;
        public static string dbName = string.Empty;
        public static string dbUser = string.Empty;
        public static string dbPass = string.Empty;

        public static void LoadConfiguration()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            try
            {
                dbHost = config.GetValue<string>("Database:dbHost");
                dbPort = config.GetValue<string>("Database:dbPort");
                dbName = config.GetValue<string>("Database:dbName");
                dbUser = config.GetValue<string>("Database:dbUser");
                dbPass = config.GetValue<string>("Database:dbPass");
            }
            catch(Exception ex)
            {

            }
        }
    }
}
