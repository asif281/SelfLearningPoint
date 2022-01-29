using DTOs;

namespace DAL
{
    public interface ITeacher
    {
        List<TeacherDTO> displayTeacher();
        void addTeacher(TeacherDTO teacherDTO);
        void removeTeacher(int Id);
        void updateTeacher(TeacherDTO teacherDTO);
        TeacherDTO GetTeacherById(int Id);
    }
}