using Dapper;
using System.Data;
using University.Students.Models;

namespace University.Students.DataProvider
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly IDbConnection _dbConnection;

        public StudentsRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task<IReadOnlyList<Student>> GetStudentsAsync()
        {
            const string query = "SELECT * FROM dbo.Students";

            return (await _dbConnection.QueryAsync<Student>(query)).ToList();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            const string studentProcedureName = "dbo.uspGetStudentById";
            const string subjectProcedureName = "dbo.uspGetEnrolmentsByStudentId";

            var param = new { Id = id };

            var student = (await _dbConnection.QueryAsync<Student>(studentProcedureName, param, commandType: CommandType.StoredProcedure))
                .SingleOrDefault();

            if (student == null)
            {
                throw new InvalidOperationException($"Unable to find student with ID {id}");
            }

            var enrolmentParam = new { StudentId = id };

            student.Subjects = await _dbConnection.QueryAsync<string>(subjectProcedureName, enrolmentParam, commandType: CommandType.StoredProcedure);

            return student;
        }

        public async Task<int> CreateStudentAsync(CreateStudent student)
        {
            const string procedureName = "dbo.uspInsertStudent";

            var id = await _dbConnection.ExecuteScalarAsync<int>(procedureName, student, commandType: CommandType.StoredProcedure);

            return id;
        }

        public async Task UpdateStudentAsync(Student student)
        {
            const string procedureName = "dbo.uspUpdateStudent";

            await _dbConnection.ExecuteAsync(procedureName, student, commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteStudentAsync(int id)
        {
            var query = $"DELETE FROM dbo.Students WHERE Id = {id}";

            await _dbConnection.ExecuteAsync(query);
        }
    }
}
