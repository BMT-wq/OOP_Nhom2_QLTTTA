using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTTTA.Interface
{   
    internal interface IObserver
    {
        void Update();
    }

    // Lớp Course đại diện cho Subject trong Observer pattern.
    // Giữ danh sách các observers và thông báo khi lịch học thay đổi.
    internal class Course
    {
        private readonly List<IObserver> observers = new List<IObserver>();
        private string schedule;

        public string Name { get; }
        public string Schedule => schedule;

        public Course(string name, string initialSchedule)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            Name = name;

            if (initialSchedule == null)
            {
                schedule = string.Empty;
            }
            else
            {
                schedule = initialSchedule;
            }
        }

        // Đăng ký một observer để nhận thông báo.
        public void Register(IObserver observer)
        {
            if (observer == null) throw new ArgumentNullException(nameof(observer));
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        // Hủy đăng ký một observer.
        public void Unregister(IObserver observer)
        {
            if (observer == null) return;
            observers.Remove(observer);
        }

        // Thay đổi lịch học của khóa học và thông báo cho các observer nếu có thay đổi.
        public void ChangeSchedule(string newSchedule)
        {
            if (newSchedule == null) throw new ArgumentNullException(nameof(newSchedule));
            if (schedule == newSchedule) return;

            schedule = newSchedule;
            NotifyObservers();
        }

        private void NotifyObservers()
        {
            // Lặp qua một bản sao để tránh lỗi khi các observer thay đổi danh sách trong Update()
            foreach (var observer in observers.ToList())
            {
                try
                {
                    observer.Update();
                }
                catch
                {
                    // Bắt và bỏ qua ngoại lệ ở đây để đơn giản; ghi log hoặc xử lý theo nhu cầu.
                }
            }
        }
    }

    // Lớp StudentObserver đại diện cho Observer trong Observer pattern.
    // Nhận thông báo khi lịch học của khóa học thay đổi và hiển thị thông tin cho sinh viên.
    internal class StudentObserver : IObserver
    {
        private readonly string StudentName;
        private readonly Course Course;

        public StudentObserver(string studentName, Course course)
        {
            if (studentName == null) throw new ArgumentNullException(nameof(studentName));
            StudentName = studentName;

            if (course == null) throw new ArgumentNullException(nameof(course));
            Course = course;
        }

        public void Update()
        {
            Console.WriteLine("{0} Nhận thông báo: Lịch của khóa {1} đã thay đổi thành \"{2}\"",
                StudentName, Course.Name, Course.Schedule);
        }
    }
}