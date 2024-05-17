using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IWebHostEnvironment _environment;

        public DoctorService(IDoctorRepository doctorRepository, IWebHostEnvironment environment = null)
        {
            _doctorRepository = doctorRepository;
            _environment = environment;
        }

        public void Add(Doctor doctor)
        {
            if (doctor == null) throw new DoctorNullException("", "Doctor null ola bilmez!");
            if (!doctor.PhotoFile.ContentType.Contains("image/")) throw new ContentTypeException("PhotoFile", "Faylin tipi dogru deyil");
            if (doctor.PhotoFile.Length > 2092157) throw new FileSizeException("", "MAx olcu 2 mb ola biler");

            string path = _environment.WebRootPath + @"\uploads\" + doctor.PhotoFile.FileName;

            using(FileStream fileName = new FileStream(path, FileMode.Create))
            {
                doctor.PhotoFile.CopyTo(fileName);
            }

            doctor.ImgUrl = doctor.PhotoFile.FileName;
            _doctorRepository.Add(doctor);
            _doctorRepository.Commit();
        }

        public void Delete(int id)
        {
            var existDoctor = _doctorRepository.Get(x => x.Id == id);

            if (existDoctor == null) throw new NullReferenceException("Id null ola bilmez!");
            string path = _environment.WebRootPath + @"\uploads\" + existDoctor.ImgUrl;

            if (!File.Exists(path)) throw new Exceptions.FileNotFoundException("", "Fayl yoxdur!");

            File.Delete(path);
            _doctorRepository.Delete(existDoctor);
            _doctorRepository.Commit();

        }

        public Doctor Get(Func<Doctor, bool> func = null)
        {
           return _doctorRepository.Get(func);
        }

        public List<Doctor> GetAll(Func<Doctor, bool> func = null)
        {
            return _doctorRepository.GetAll(func);
        }

        public void Update(int id, Doctor doctor)
        {
            var existDoctor = _doctorRepository.Get(x => x.Id == id);
            if (existDoctor == null) throw new DoctorNullException("", "Doctor yoxdur!");

            if (doctor == null) throw new DoctorNotFoundException("", "Doctir bos ola bilmez!");

            if (doctor.PhotoFile != null)
            {
                if (!doctor.PhotoFile.ContentType.Contains("image/")) throw new ContentTypeException("PhotoFile", "Faylin tipi dogru deyil");
                if (doctor.PhotoFile.Length > 2092157) throw new FileSizeException("", "MAx olcu 2 mb ola biler");

                string path = _environment.WebRootPath + @"\uploads\" + doctor.PhotoFile.FileName;

                using (FileStream fileName = new FileStream(path, FileMode.Create))
                {
                    doctor.PhotoFile.CopyTo(fileName);
                }
                existDoctor.ImgUrl = doctor.PhotoFile.FileName;

            }

            existDoctor.FullName = doctor.FullName;
            existDoctor.Position = doctor.Position;
            _doctorRepository.Commit();
        }


    }
}
