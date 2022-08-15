namespace UserService.Config
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string DoctorsCollectionName { get; set; } = null!;

        public string PatientsCollectionName { get; set; } = null!;
    }
}
