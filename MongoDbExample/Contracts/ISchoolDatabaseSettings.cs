namespace MongoDbExample.Contracts
{
    public interface ISchoolDatabaseSettings
    {
        string StudentsCollectionName { get; set; }
        string CoursesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
