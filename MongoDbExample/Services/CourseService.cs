using MongoDbExample.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDbExample.Contracts;

namespace MongoDbExample.Services
{
    public class CourseService
    {
        private readonly IMongoCollection<Course> _courses;
        public CourseService(ISchoolDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _courses = database.GetCollection<Course>(settings.CoursesCollectionName);
        }
        public async Task<List<Course>> GetAllAsync()
        {
            return await _courses.Find(s => true).ToListAsync();
        }
        public async Task<Course> GetByIdAsync(string id)
        {
            return await _courses.Find<Course>(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Course> CreateAsync(Course course)
        {
            await _courses.InsertOneAsync(course);
            return course;
        }
        public async Task UpdateAsync(string id, Course course)
        {
            await _courses.ReplaceOneAsync(c => c.Id == id, course);
        }
        public async Task DeleteAsync(string id)
        {
            await _courses.DeleteOneAsync(c => c.Id == id);
        }
    }
}