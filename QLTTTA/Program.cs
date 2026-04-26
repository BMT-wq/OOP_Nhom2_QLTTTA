using System;
using System.IO;
using System.Collections.Generic;

namespace QLTTTA
{
    // === LỚP FILESERVICE ===
    public class FileService
    {
        public List<string[]> DocFileCSV(string duongDan)
        {
            List<string[]> ds = new List<string[]>();

            if (!File.Exists(duongDan))
            {
                Console.WriteLine("Khong tim thay file!");
                return ds;
            }

            using (StreamReader sr = new StreamReader(duongDan))
            {
                string dong;
                bool isHeader = true;
                while ((dong = sr.ReadLine()) != null)
                {
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }

                    string[] p = dong.Split(',');
                    if (p.Length >= 5)
                    {
                        ds.Add(p);
                    }
                }
            }
            return ds;
        }

        public void GhiFileCSV(string duongDan, List<string[]> ds)
        {
            using (StreamWriter sw = new StreamWriter(duongDan))
            {
                sw.WriteLine("StudentID,FullName,Age,Email,Course");
                foreach (var hv in ds)
                {
                    sw.WriteLine(string.Join(",", hv));
                }
            }
        }
    }

    // === CHƯƠNG TRÌNH CHÍNH ===
    class Program
    {
        static List<string[]> dsHocVien = new List<string[]>();
        static FileService fs = new FileService();
        static string filePath = @"..\..\student_list.csv";

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            dsHocVien = fs.DocFileCSV(filePath);

            int chon;
            do
            {
                Console.Clear();
                Console.WriteLine("===== MENU CHINH =====");
                Console.WriteLine("1. Xem danh sach hoc vien");
                Console.WriteLine("2. Them hoc vien");
                Console.WriteLine("3. Sua hoc vien");
                Console.WriteLine("4. Xoa hoc vien");
                Console.WriteLine("5. Luu vao file CSV");
                Console.WriteLine("6. Tinh luong giang vien");
                Console.WriteLine("7. Tinh diem chuyen can sinh vien");
                Console.WriteLine("0. Thoat");
                Console.WriteLine("======================");
                Console.Write("Chon: ");

                if (!int.TryParse(Console.ReadLine(), out chon))
                    chon = -1;

                switch (chon)
                {
                    case 1: XemDanhSach(); break;
                    case 2: ThemHocVien(); break;
                    case 3: SuaHocVien(); break;
                    case 4: XoaHocVien(); break;
                    case 5: LuuFile(); break;
                    case 6: TinhLuong(); break;
                    case 7: TinhDiemChuyenCan(); break;
                    case 0: LuuFile(); Console.WriteLine("\nTam biet!"); break;
                    default: Console.WriteLine("\nChon sai! Nhan Enter..."); Console.ReadLine(); break;
                }
            } while (chon != 0);
        }

        // ==================== FILE SERVICE ====================
        static void XemDanhSach()
        {
            Console.Clear();
            Console.WriteLine("=== DANH SACH HOC VIEN ===");
            if (dsHocVien.Count == 0)
                Console.WriteLine("Danh sach trong!");
            else
                foreach (var hv in dsHocVien)
                    Console.WriteLine($"ID: {hv[0]} | Ten: {hv[1]} | Tuoi: {hv[2]} | Email: {hv[3]} | Khoa: {hv[4]}");
            Console.WriteLine("\nNhan Enter..."); Console.ReadLine();
        }

        static void ThemHocVien()
        {
            Console.Clear();
            Console.Write("StudentID: "); string id = Console.ReadLine();
            Console.Write("FullName: "); string ten = Console.ReadLine();
            Console.Write("Age: "); string tuoi = Console.ReadLine();
            Console.Write("Email: "); string email = Console.ReadLine();
            Console.Write("Course: "); string khoa = Console.ReadLine();

            dsHocVien.Add(new string[] { id, ten, tuoi, email, khoa });
            Console.WriteLine("\nThem thanh cong! Nhan Enter..."); Console.ReadLine();
        }

        static void SuaHocVien()
        {
            Console.Clear();
            Console.Write("Nhap StudentID can sua: ");
            string id = Console.ReadLine();
            int index = dsHocVien.FindIndex(x => x[0] == id);

            if (index == -1) Console.WriteLine("Khong tim thay!");
            else
            {
                Console.Write("FullName moi (Enter = giu): "); string t = Console.ReadLine(); if (t != "") dsHocVien[index][1] = t;
                Console.Write("Age moi (Enter = giu): "); string a = Console.ReadLine(); if (a != "") dsHocVien[index][2] = a;
                Console.Write("Email moi (Enter = giu): "); string e = Console.ReadLine(); if (e != "") dsHocVien[index][3] = e;
                Console.Write("Course moi (Enter = giu): "); string c = Console.ReadLine(); if (c != "") dsHocVien[index][4] = c;
                Console.WriteLine("Sua thanh cong!");
            }
            Console.WriteLine("Nhan Enter..."); Console.ReadLine();
        }

        static void XoaHocVien()
        {
            Console.Clear();
            Console.Write("Nhap StudentID can xoa: ");
            string id = Console.ReadLine();
            int index = dsHocVien.FindIndex(x => x[0] == id);

            if (index == -1) Console.WriteLine("Khong tim thay!");
            else
            {
                Console.Write($"Xoa {dsHocVien[index][1]}? (y/n): ");
                if (Console.ReadLine().ToLower() == "y") { dsHocVien.RemoveAt(index); Console.WriteLine("Xoa thanh cong!"); }
                else Console.WriteLine("Da huy!");
            }
            Console.WriteLine("Nhan Enter..."); Console.ReadLine();
        }

        static void LuuFile()
        {
            fs.GhiFileCSV(filePath, dsHocVien);
            Console.WriteLine("\nLuu file CSV thanh cong!");
            Console.WriteLine("Nhan Enter..."); Console.ReadLine();
        }

        // ==================== TASK 2: TÍNH LƯƠNG ====================
        static void TinhLuong()
        {
            Console.Clear();
            Console.WriteLine("=== TINH LUONG GIANG VIEN ===");
            Console.Write("Nhap ma giang vien: ");
            string maGV = Console.ReadLine();
            Console.Write("Nhap ho ten giang vien: ");
            string hoTen = Console.ReadLine();
            Console.Write("Nhap so gio day: ");
            int soGioDay = int.Parse(Console.ReadLine());
            Console.Write("Nhap luong theo gio: ");
            decimal luongTheoGio = decimal.Parse(Console.ReadLine());
            Console.Write("Nhap tong gio tich luy (TotalHours): ");
            int totalHours = int.Parse(Console.ReadLine());

            decimal luong = soGioDay * luongTheoGio;

            Console.WriteLine("\n===== KET QUA =====");
            Console.WriteLine($"Ma GV: {maGV}");
            Console.WriteLine($"Ho ten: {hoTen}");
            Console.WriteLine($"So gio day: {soGioDay}");
            Console.WriteLine($"Luong theo gio: {luongTheoGio:N0} VND");
            Console.WriteLine($"=> Luong thang: {luong:N0} VND");
            Console.WriteLine($"=> Tong gio tich luy (TotalHours): {totalHours}");
            Console.WriteLine("\nNhan Enter..."); Console.ReadLine();
        }

        // ==================== TASK 2: TÍNH ĐIỂM CHUYÊN CẦN ====================
        static void TinhDiemChuyenCan()
        {
            Console.Clear();
            Console.WriteLine("=== TINH DIEM CHUYEN CAN ===");
            Console.Write("Nhap ma sinh vien: ");
            string maSV = Console.ReadLine();
            Console.Write("Nhap ho ten sinh vien: ");
            string hoTen = Console.ReadLine();
            Console.Write("Nhap so buoi di hoc (AttendanceCount): ");
            int attendanceCount = int.Parse(Console.ReadLine());
            Console.Write("Nhap tong so buoi hoc (TotalSessions): ");
            int totalSessions = int.Parse(Console.ReadLine());

            double diem = (totalSessions == 0) ? 0 : (double)attendanceCount / totalSessions * 10;

            Console.WriteLine("\n===== KET QUA =====");
            Console.WriteLine($"Ma SV: {maSV}");
            Console.WriteLine($"Ho ten: {hoTen}");
            Console.WriteLine($"So buoi di hoc (AttendanceCount): {attendanceCount}/{totalSessions}");
            Console.WriteLine($"=> Diem chuyen can: {diem:F1}/10");
            Console.WriteLine("\nNhan Enter..."); Console.ReadLine();
        }
    }
}