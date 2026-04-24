using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTTTA.Models
{
    public class Student : Person
    {
        public string StudentCode { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string CurrentLevel { get; set; }

        public Student(string id, string fullName, string email, string phone,
                       string studentCode, DateTime enrollmentDate, string currentLevel)
            : base(id, fullName, email, phone)
        {
            StudentCode = studentCode;
            EnrollmentDate = enrollmentDate;
            CurrentLevel = currentLevel;
        }

        public override string GetInfo() => $"[Học viên] MSSV: {StudentCode} | Tên: {FullName} | Trình độ: {CurrentLevel}";
    }
}
