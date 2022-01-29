using DAL;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADOCore.Controllers
{
    public class TeacherController : Controller
    {
        ITeacher _teacherRepo;
        public TeacherController(ITeacher teacherRepo)
        {
            _teacherRepo = teacherRepo;
        }
        // GET: TeacherController
        public ActionResult Index()
        {
           var list = _teacherRepo.displayTeacher();
            return View(list);
        }

        // GET: TeacherController/Create
        public ActionResult Create()
        {          
            return View();
        }

        // POST: TeacherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeacherDTO teacherDTO)
        {
            try
            {
                _teacherRepo.addTeacher(teacherDTO);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TeacherController/Edit/5
        public ActionResult Update(int Id)
        {
            var teacher = _teacherRepo.GetTeacherById(Id);
            return View(teacher);
        }

        // POST: TeacherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Update")]
        public IActionResult Update_Post(TeacherDTO teacherDTO) 
        { 
            try
            {
                _teacherRepo.updateTeacher(teacherDTO);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TeacherController/Delete/5
        public ActionResult Delete()
        {            
            return View();
        }

        // POST: TeacherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _teacherRepo.removeTeacher(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
