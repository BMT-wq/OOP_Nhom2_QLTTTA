using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTTTA.Models
{
    public class Teacher : Person
    {
        public string TeacherCode { get; set; }
        public string Specialization { get; set; }
        public decimal BaseSalary { get; set; }

        public Teacher(string id, string fullName, string email, string phone,
                       string teacherCode, string specialization, decimal baseSalary)
            : base(id, fullName, email, phone)
        {
            TeacherCode = teacherCode;
            Specialization = specialization;
            BaseSalary = baseSalary;
        }

        public override string GetInfo() => $"[Giảng viên] MGV: {TeacherCode} | Tên: {FullName} | Chuyên môn: {Specialization}";
    }
}
