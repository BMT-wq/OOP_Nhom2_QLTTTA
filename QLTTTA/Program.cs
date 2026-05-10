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

        // Đọc file CSV bất kỳ (dùng chung cho giáo viên, lịch học, khóa học)
        public List<string[]> DocFileCSV_TongQuat(string duongDan, int soCot)
        {
            List<string[]> ds = new List<string[]>();

            if (!File.Exists(duongDan))
            {
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
                    if (p.Length >= soCot)
                    {
                        ds.Add(p);
                    }
                }
            }
            return ds;
        }

        // Ghi file CSV bất kỳ
        public void GhiFileCSV_TongQuat(string duongDan, string tieuDe, List<string[]> ds)
        {
            using (StreamWriter sw = new StreamWriter(duongDan))
            {
                sw.WriteLine(tieuDe);
                foreach (var item in ds)
                {
                    sw.WriteLine(string.Join(",", item));
                }
            }
        }
    }

    // === CHƯƠNG TRÌNH CHÍNH ===
    class Program
    {
        static List<string[]> dsHocVien = new List<string[]>();
        static List<string[]> dsGiaoVien = new List<string[]>();
        static List<string[]> dsLichHoc = new List<string[]>();
        static List<string[]> dsKhoaHoc = new List<string[]>();
        static FileService fs = new FileService();
        static string filePath = @"..\..\student_list.csv";
        static string fileGiaoVien = @"..\..\teacher_list.csv";
        static string fileLichHoc = @"..\..\schedule_list.csv";
        static string fileKhoaHoc = @"..\..\course_list.csv";

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Load dữ liệu
            dsHocVien = fs.DocFileCSV(filePath);
            dsGiaoVien = fs.DocFileCSV_TongQuat(fileGiaoVien, 4);
            dsLichHoc = fs.DocFileCSV_TongQuat(fileLichHoc, 5);
            dsKhoaHoc = fs.DocFileCSV_TongQuat(fileKhoaHoc, 4);

            int chon;
            do
            {
                Console.Clear();
                Console.WriteLine("========== MENU CHINH ==========");
                Console.WriteLine("1. Quan ly Hoc Vien");
                Console.WriteLine("2. Quan ly Giao Vien");
                Console.WriteLine("3. Quan ly Lich Hoc");
                Console.WriteLine("4. Quan ly Khoa Hoc");
                Console.WriteLine("5. Tinh luong giang vien");
                Console.WriteLine("6. Tinh diem chuyen can sinh vien");
                Console.WriteLine("0. Thoat");
                Console.WriteLine("================================");
                Console.Write("Chon: ");

                if (!int.TryParse(Console.ReadLine(), out chon))
                    chon = -1;

                switch (chon)
                {
                    case 1: MenuHocVien(); break;
                    case 2: MenuGiaoVien(); break;
                    case 3: MenuLichHoc(); break;
                    case 4: MenuKhoaHoc(); break;
                    case 5: TinhLuong(); break;
                    case 6: TinhDiemChuyenCan(); break;
                    case 0:
                        LuuFile();
                        LuuFileGiaoVien();
                        LuuFileLichHoc();
                        LuuFileKhoaHoc();
                        Console.WriteLine("\nTam biet!");
                        break;
                    default:
                        Console.WriteLine("\nChon sai! Nhan Enter...");
                        Console.ReadLine();
                        break;
                }
            } while (chon != 0);
        }

        // ==================== MENU HỌC VIÊN ====================
        static void MenuHocVien()
        {
            int chon;
            do
            {
                Console.Clear();
                Console.WriteLine("===== QUAN LY HOC VIEN =====");
                Console.WriteLine("1. Xem danh sach hoc vien");
                Console.WriteLine("2. Them hoc vien");
                Console.WriteLine("3. Sua hoc vien");
                Console.WriteLine("4. Xoa hoc vien");
                Console.WriteLine("5. Luu vao file CSV");
                Console.WriteLine("0. Quay lai");
                Console.WriteLine("============================");
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
                }
            } while (chon != 0);
        }

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

        // ==================== MENU GIÁO VIÊN ====================
        static void MenuGiaoVien()
        {
            int chon;
            do
            {
                Console.Clear();
                Console.WriteLine("===== QUAN LY GIAO VIEN =====");
                Console.WriteLine("1. Xem danh sach giao vien");
                Console.WriteLine("2. Them giao vien");
                Console.WriteLine("3. Sua giao vien");
                Console.WriteLine("4. Xoa giao vien");
                Console.WriteLine("5. Luu vao file CSV");
                Console.WriteLine("0. Quay lai");
                Console.WriteLine("=============================");
                Console.Write("Chon: ");

                if (!int.TryParse(Console.ReadLine(), out chon))
                    chon = -1;

                switch (chon)
                {
                    case 1: XemDSGiaoVien(); break;
                    case 2: ThemGiaoVien(); break;
                    case 3: SuaGiaoVien(); break;
                    case 4: XoaGiaoVien(); break;
                    case 5: LuuFileGiaoVien(); break;
                }
            } while (chon != 0);
        }

        static void XemDSGiaoVien()
        {
            Console.Clear();
            Console.WriteLine("=== DANH SACH GIAO VIEN ===");
            if (dsGiaoVien.Count == 0)
                Console.WriteLine("Danh sach trong!");
            else
                foreach (var gv in dsGiaoVien)
                    Console.WriteLine($"Ma: {gv[0]} | Ten: {gv[1]} | Chuyen mon: {gv[2]} | Email: {gv[3]}");
            Console.WriteLine("\nNhan Enter..."); Console.ReadLine();
        }

        static void ThemGiaoVien()
        {
            Console.Clear();
            Console.Write("Ma GV: "); string ma = Console.ReadLine();
            Console.Write("Ho ten: "); string ten = Console.ReadLine();
            Console.Write("Chuyen mon: "); string chuyenMon = Console.ReadLine();
            Console.Write("Email: "); string email = Console.ReadLine();
            dsGiaoVien.Add(new string[] { ma, ten, chuyenMon, email });
            Console.WriteLine("\nThem thanh cong! Nhan Enter..."); Console.ReadLine();
        }

        static void SuaGiaoVien()
        {
            Console.Clear();
            Console.Write("Nhap Ma GV can sua: ");
            string ma = Console.ReadLine();
            int index = dsGiaoVien.FindIndex(x => x[0] == ma);
            if (index == -1) Console.WriteLine("Khong tim thay!");
            else
            {
                Console.Write("Ho ten moi (Enter = giu): "); string t = Console.ReadLine(); if (t != "") dsGiaoVien[index][1] = t;
                Console.Write("Chuyen mon moi (Enter = giu): "); string cm = Console.ReadLine(); if (cm != "") dsGiaoVien[index][2] = cm;
                Console.Write("Email moi (Enter = giu): "); string e = Console.ReadLine(); if (e != "") dsGiaoVien[index][3] = e;
                Console.WriteLine("Sua thanh cong!");
            }
            Console.WriteLine("Nhan Enter..."); Console.ReadLine();
        }

        static void XoaGiaoVien()
        {
            Console.Clear();
            Console.Write("Nhap Ma GV can xoa: ");
            string ma = Console.ReadLine();
            int index = dsGiaoVien.FindIndex(x => x[0] == ma);
            if (index == -1) Console.WriteLine("Khong tim thay!");
            else
            {
                Console.Write($"Xoa {dsGiaoVien[index][1]}? (y/n): ");
                if (Console.ReadLine().ToLower() == "y") { dsGiaoVien.RemoveAt(index); Console.WriteLine("Xoa thanh cong!"); }
                else Console.WriteLine("Da huy!");
            }
            Console.WriteLine("Nhan Enter..."); Console.ReadLine();
        }

        static void LuuFileGiaoVien()
        {
            fs.GhiFileCSV_TongQuat(fileGiaoVien, "TeacherID,FullName,Specialization,Email", dsGiaoVien);
            Console.WriteLine("\nLuu file giao vien thanh cong!");
            Console.WriteLine("Nhan Enter..."); Console.ReadLine();
        }

        // ==================== MENU LỊCH HỌC ====================
        static void MenuLichHoc()
        {
            int chon;
            do
            {
                Console.Clear();
                Console.WriteLine("===== QUAN LY LICH HOC =====");
                Console.WriteLine("1. Xem danh sach lich hoc");
                Console.WriteLine("2. Them lich hoc");
                Console.WriteLine("3. Sua lich hoc");
                Console.WriteLine("4. Xoa lich hoc");
                Console.WriteLine("5. Luu vao file CSV");
                Console.WriteLine("0. Quay lai");
                Console.WriteLine("============================");
                Console.Write("Chon: ");

                if (!int.TryParse(Console.ReadLine(), out chon))
                    chon = -1;

                switch (chon)
                {
                    case 1: XemDSLichHoc(); break;
                    case 2: ThemLichHoc(); break;
                    case 3: SuaLichHoc(); break;
                    case 4: XoaLichHoc(); break;
                    case 5: LuuFileLichHoc(); break;
                }
            } while (chon != 0);
        }

        static void XemDSLichHoc()
        {
            Console.Clear();
            Console.WriteLine("=== DANH SACH LICH HOC ===");
            if (dsLichHoc.Count == 0)
                Console.WriteLine("Danh sach trong!");
            else
                foreach (var lh in dsLichHoc)
                    Console.WriteLine($"Ma: {lh[0]} | Khoa hoc: {lh[1]} | GV: {lh[2]} | Thu: {lh[3]} | Gio: {lh[4]}");
            Console.WriteLine("\nNhan Enter..."); Console.ReadLine();
        }

        static void ThemLichHoc()
        {
            Console.Clear();
            Console.Write("Ma lich hoc: "); string ma = Console.ReadLine();
            Console.Write("Ma khoa hoc: "); string maKH = Console.ReadLine();
            Console.Write("Ma giao vien: "); string maGV = Console.ReadLine();
            Console.Write("Thu (VD: Thu 2): "); string thu = Console.ReadLine();
            Console.Write("Gio hoc (VD: 7h30-9h00): "); string gio = Console.ReadLine();
            dsLichHoc.Add(new string[] { ma, maKH, maGV, thu, gio });
            Console.WriteLine("\nThem thanh cong! Nhan Enter..."); Console.ReadLine();
        }

        static void SuaLichHoc()
        {
            Console.Clear();
            Console.Write("Nhap Ma lich hoc can sua: ");
            string ma = Console.ReadLine();
            int index = dsLichHoc.FindIndex(x => x[0] == ma);
            if (index == -1) Console.WriteLine("Khong tim thay!");
            else
            {
                Console.Write("Ma khoa hoc moi (Enter = giu): "); string kh = Console.ReadLine(); if (kh != "") dsLichHoc[index][1] = kh;
                Console.Write("Ma giao vien moi (Enter = giu): "); string gv = Console.ReadLine(); if (gv != "") dsLichHoc[index][2] = gv;
                Console.Write("Thu moi (Enter = giu): "); string thu = Console.ReadLine(); if (thu != "") dsLichHoc[index][3] = thu;
                Console.Write("Gio hoc moi (Enter = giu): "); string gio = Console.ReadLine(); if (gio != "") dsLichHoc[index][4] = gio;
                Console.WriteLine("Sua thanh cong!");
            }
            Console.WriteLine("Nhan Enter..."); Console.ReadLine();
        }

        static void XoaLichHoc()
        {
            Console.Clear();
            Console.Write("Nhap Ma lich hoc can xoa: ");
            string ma = Console.ReadLine();
            int index = dsLichHoc.FindIndex(x => x[0] == ma);
            if (index == -1) Console.WriteLine("Khong tim thay!");
            else
            {
                Console.Write($"Xoa lich hoc {dsLichHoc[index][0]}? (y/n): ");
                if (Console.ReadLine().ToLower() == "y") { dsLichHoc.RemoveAt(index); Console.WriteLine("Xoa thanh cong!"); }
                else Console.WriteLine("Da huy!");
            }
            Console.WriteLine("Nhan Enter..."); Console.ReadLine();
        }

        static void LuuFileLichHoc()
        {
            fs.GhiFileCSV_TongQuat(fileLichHoc, "ScheduleID,CourseID,TeacherID,Day,Time", dsLichHoc);
            Console.WriteLine("\nLuu file lich hoc thanh cong!");
            Console.WriteLine("Nhan Enter..."); Console.ReadLine();
        }

        // ==================== MENU KHÓA HỌC ====================
        static void MenuKhoaHoc()
        {
            int chon;
            do
            {
                Console.Clear();
                Console.WriteLine("===== QUAN LY KHOA HOC =====");
                Console.WriteLine("1. Xem danh sach khoa hoc");
                Console.WriteLine("2. Them khoa hoc");
                Console.WriteLine("3. Sua khoa hoc");
                Console.WriteLine("4. Xoa khoa hoc");
                Console.WriteLine("5. Luu vao file CSV");
                Console.WriteLine("0. Quay lai");
                Console.WriteLine("============================");
                Console.Write("Chon: ");

                if (!int.TryParse(Console.ReadLine(), out chon))
                    chon = -1;

                switch (chon)
                {
                    case 1: XemDSKhoaHoc(); break;
                    case 2: ThemKhoaHoc(); break;
                    case 3: SuaKhoaHoc(); break;
                    case 4: XoaKhoaHoc(); break;
                    case 5: LuuFileKhoaHoc(); break;
                }
            } while (chon != 0);
        }

        static void XemDSKhoaHoc()
        {
            Console.Clear();
            Console.WriteLine("=== DANH SACH KHOA HOC ===");
            if (dsKhoaHoc.Count == 0)
                Console.WriteLine("Danh sach trong!");
            else
                foreach (var kh in dsKhoaHoc)
                    Console.WriteLine($"Ma: {kh[0]} | Ten: {kh[1]} | Hoc phi: {kh[2]} | Trinh do: {kh[3]}");
            Console.WriteLine("\nNhan Enter..."); Console.ReadLine();
        }

        static void ThemKhoaHoc()
        {
            Console.Clear();
            Console.Write("Ma khoa hoc: "); string ma = Console.ReadLine();
            Console.Write("Ten khoa hoc: "); string ten = Console.ReadLine();
            Console.Write("Hoc phi: "); string hocPhi = Console.ReadLine();
            Console.Write("Trinh do: "); string trinhDo = Console.ReadLine();
            dsKhoaHoc.Add(new string[] { ma, ten, hocPhi, trinhDo });
            Console.WriteLine("\nThem thanh cong! Nhan Enter..."); Console.ReadLine();
        }

        static void SuaKhoaHoc()
        {
            Console.Clear();
            Console.Write("Nhap Ma khoa hoc can sua: ");
            string ma = Console.ReadLine();
            int index = dsKhoaHoc.FindIndex(x => x[0] == ma);
            if (index == -1) Console.WriteLine("Khong tim thay!");
            else
            {
                Console.Write("Ten moi (Enter = giu): "); string t = Console.ReadLine(); if (t != "") dsKhoaHoc[index][1] = t;
                Console.Write("Hoc phi moi (Enter = giu): "); string hp = Console.ReadLine(); if (hp != "") dsKhoaHoc[index][2] = hp;
                Console.Write("Trinh do moi (Enter = giu): "); string td = Console.ReadLine(); if (td != "") dsKhoaHoc[index][3] = td;
                Console.WriteLine("Sua thanh cong!");
            }
            Console.WriteLine("Nhan Enter..."); Console.ReadLine();
        }

        static void XoaKhoaHoc()
        {
            Console.Clear();
            Console.Write("Nhap Ma khoa hoc can xoa: ");
            string ma = Console.ReadLine();
            int index = dsKhoaHoc.FindIndex(x => x[0] == ma);
            if (index == -1) Console.WriteLine("Khong tim thay!");
            else
            {
                Console.Write($"Xoa {dsKhoaHoc[index][1]}? (y/n): ");
                if (Console.ReadLine().ToLower() == "y") { dsKhoaHoc.RemoveAt(index); Console.WriteLine("Xoa thanh cong!"); }
                else Console.WriteLine("Da huy!");
            }
            Console.WriteLine("Nhan Enter..."); Console.ReadLine();
        }

        static void LuuFileKhoaHoc()
        {
            fs.GhiFileCSV_TongQuat(fileKhoaHoc, "CourseID,CourseName,Fee,Level", dsKhoaHoc);
            Console.WriteLine("\nLuu file khoa hoc thanh cong!");
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