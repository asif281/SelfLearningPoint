using DomainModels;
using DTOs;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Repositories
{
    public class TeacherRepo : ITeacher
    {
        public IConfiguration Configuration { get; }

        public TeacherRepo(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        #region Public Region
        public void addTeacher(TeacherDTO teacherDTO)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Teacher teacher = SetTeacherEntity(teacherDTO);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
#pragma warning disable CS8604 // Possible null reference argument.
            teacherAdd(connectionString, teacher);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public List<TeacherDTO> displayTeacher()
        {            
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            return TeacherList(connectionString);                    
        }

        public void updateTeacher(TeacherDTO teacherDTO)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];

#pragma warning disable CS8604 // Possible null reference argument.
            teacherUpdate(connectionString, teacher: SetTeacherEntity(teacherDTO));
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public void removeTeacher(int Id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            teacherRemove(connectionString, Id);
        }
        
        public TeacherDTO GetTeacherById(int Id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            return GetTeacher(Id, connectionString);
        }

        #endregion

        #region Private Methods
        private void teacherAdd(string connectionString, Teacher teacher)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Insert Into Teacher (Name, Skills, TotalStudents, Salary, AddedOn) Values ('{teacher.Name}', '{teacher.Skills}','{teacher.TotalStudents}','{teacher.Salary}', '{teacher.AddedOn}')";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        private List<TeacherDTO> TeacherList(string connectionString)
        {
            List<Teacher> teacherList = new List<Teacher>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //SqlDataReader
                connection.Open();

                string sql = "Select * From Teacher";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Teacher teacher = new Teacher();
                        teacher.Id = Convert.ToInt32(dataReader["Id"]);
#pragma warning disable CS8601 // Possible null reference assignment.
                        teacher.Name = Convert.ToString(dataReader["Name"]);
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning disable CS8601 // Possible null reference assignment.
                        teacher.Skills = Convert.ToString(dataReader["Skills"]);
#pragma warning restore CS8601 // Possible null reference assignment.
                        teacher.TotalStudents = Convert.ToInt32(dataReader["TotalStudents"]);
                        teacher.Salary = Convert.ToDecimal(dataReader["Salary"]);
                        teacher.AddedOn = Convert.ToDateTime(dataReader["AddedOn"]);

                        teacherList.Add(teacher);
                    }
                }

                connection.Close();

                return teacherList.Select(x => SetTeacherDTO(x)).ToList() as List<TeacherDTO>;
            }
        }

        private void teacherRemove(string connectionString, int Id)
        {
            List<Teacher> teacherList = new List<Teacher>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From Teacher Where Id='{Id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    connection.Close();
                }
            }
        }

        private void teacherUpdate(string connectionString, Teacher teacher)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update Teacher SET Name='{teacher.Name}', Skills='{teacher.Skills}', TotalStudents='{teacher.TotalStudents}', Salary='{teacher.Salary}' Where Id='{teacher.Id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

        }

        private TeacherDTO GetTeacher(int Id, string connectionString)
        {
            TeacherDTO teacherDTO = new TeacherDTO();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From Teacher Where Id='{Id}'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        teacherDTO.Id = Convert.ToInt32(dataReader["Id"]);
#pragma warning disable CS8601 // Possible null reference assignment.
                        teacherDTO.Name = Convert.ToString(dataReader["Name"]);
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning disable CS8601 // Possible null reference assignment.
                        teacherDTO.Skills = Convert.ToString(dataReader["Skills"]);
#pragma warning restore CS8601 // Possible null reference assignment.
                        teacherDTO.TotalStudents = Convert.ToInt32(dataReader["TotalStudents"]);
                        teacherDTO.Salary = Convert.ToDecimal(dataReader["Salary"]);
                        teacherDTO.AddedOn = Convert.ToDateTime(dataReader?["AddedOn"]);
                    }
                }

                connection.Close();
            }
            return teacherDTO;
        }
        private Teacher? SetTeacherEntity(TeacherDTO teacherDTO)
        {
            if (teacherDTO != null)
            {
                return new Teacher()
                {   Id = teacherDTO.Id,
                    Name = teacherDTO.Name,
                    AddedOn = DateTime.Now,
                    Salary = teacherDTO.Salary,
                    TotalStudents = teacherDTO.TotalStudents,
                    Skills = teacherDTO.Skills
                };
            }
            return null;
        }

        private TeacherDTO? SetTeacherDTO(Teacher teacher)
        {
            if (teacher != null)
            {
                return new TeacherDTO()
                {
                    Id = teacher.Id,
                    Name = teacher.Name,
                    AddedOn = DateTime.Now,
                    Salary = teacher.Salary,
                    TotalStudents = teacher.TotalStudents,
                    Skills =teacher.Skills
                };
            }
            return null;
        }

        #endregion
    }
}
