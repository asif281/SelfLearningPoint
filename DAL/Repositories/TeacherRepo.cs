using DomainModels;
using DTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Teacher teacher = SetTeacherEntity(teacherDTO);
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];          
            teacherAdd(connectionString, teacher);
        }

        public List<TeacherDTO> displayTeacher()
        {            
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            return TeacherList(connectionString);                    
        }

        public void updateTeacher(TeacherDTO teacherDTO)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            teacherUpdate(connectionString, SetTeacherEntity(teacherDTO));
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
                string sql = $"Insert Into Teacher (Name, Skills, TotalStudents, Salary) Values ('{teacher.Name}', '{teacher.Skills}','{teacher.TotalStudents}','{teacher.Salary}')";

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
                        teacher.Name = Convert.ToString(dataReader["Name"]);
                        teacher.Skills = Convert.ToString(dataReader["Skills"]);
                        teacher.TotalStudents = Convert.ToInt32(dataReader["TotalStudents"]);
                        teacher.Salary = Convert.ToDecimal(dataReader["Salary"]);
                        teacher.AddedOn = Convert.ToDateTime(dataReader["AddedOn"]);

                        teacherList.Add(teacher);
                    }
                }

                connection.Close();

                return (List<TeacherDTO>)teacherList.Select(x => SetTeacherDTO(x)).ToList();
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
                        teacherDTO.Name = Convert.ToString(dataReader["Name"]);
                        teacherDTO.Skills = Convert.ToString(dataReader["Skills"]);
                        teacherDTO.TotalStudents = Convert.ToInt32(dataReader["TotalStudents"]);
                        teacherDTO.Salary = Convert.ToDecimal(dataReader["Salary"]);
                        teacherDTO.AddedOn = Convert.ToDateTime(dataReader["AddedOn"]);
                    }
                }

                connection.Close();
            }
            return teacherDTO;
        }
        private Teacher SetTeacherEntity(TeacherDTO teacherDTO)
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

        private TeacherDTO SetTeacherDTO(Teacher teacher)
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
