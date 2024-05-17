using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicioApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public IActionResult Index()
        {
            var doctors = _doctorService.GetAll();
            return View(doctors);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _doctorService.Add(doctor);
            }
            catch(DoctorNullException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(ContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _doctorService.Delete(id);
            }
            catch(NullReferenceException ex)
            {
                ModelState.AddModelError("",ex.Message);
            }
            catch(Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
           return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var doctor = _doctorService.Get(x => x.Id == id);
            if (doctor == null) return View(doctor);
            return View(doctor);
        }
        [HttpPost]
        public IActionResult Update(Doctor doctor)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                _doctorService.Update(doctor.Id, doctor);

            }
            catch(ContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (DoctorNullException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (DoctorNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
