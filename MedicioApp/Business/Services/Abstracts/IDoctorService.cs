using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface IDoctorService
    {
        void Add(Doctor doctor);
        void Delete(int id);
        void Update(int id, Doctor doctor);
        Doctor Get(Func<Doctor, bool> func = null);
        List<Doctor> GetAll(Func<Doctor, bool> func = null);
    }
}
