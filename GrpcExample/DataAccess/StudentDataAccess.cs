using GrpcExample.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcExample.DataAccess
{
    public class StudentDataAccess
    {
        private readonly IMongoCollection<Student> _students;
        private readonly IMongoCollection<Course> _courses;
        public StudentDataAccess(ISchoolDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _students = database.GetCollection<Student>(settings.StudentsCollectionName);
            _courses = database.GetCollection<Course>(settings.CoursesCollectionName);
        }
        public async Task<List<Student>> GetAllAsync()
        {
            return await _students.Find(s => true).ToListAsync();
        }
        public async Task<Student> GetByIdAsync(string id)
        {
            return await _students.Find<Student>(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Student> GetByIdWithCoursesAsync(string id)
        {
            var student = await GetByIdAsync(id);
            if (student.Courses != null && student.Courses.Count > 0)
            {
                student.CourseList = await _courses.Find<Course>(c => student.Courses.Contains(c.Id)).ToListAsync();
            }
            return student;
        }
        public async Task<Student> CreateAsync(Student student)
        {
            await _students.InsertOneAsync(student);
            return student;
        }
        public async Task UpdateAsync(string id, Student student)
        {
            await _students.ReplaceOneAsync(s => s.Id == id, student);
        }
        public async Task DeleteAsync(string id)
        {
            await _students.DeleteOneAsync(s => s.Id == id);
        }
    }
}