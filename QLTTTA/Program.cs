using System;
using System.IO;
using System.Collections.Generic;

namespace QLTTTA
{
    // === LỚP KHÓA HỌC (Task 1) ===
    public class KhoaHoc
    {
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public decimal HocPhi { get; set; }
        public string TrinhDo { get; set; }

        public override string ToString()
        {
            return $"Ma: {MaKH} | Ten: {TenKH} | Hoc phi: {HocPhi:N0} VND | Trinh do: {TrinhDo}";
        }
    }

    // === LỚP FILESERVICE (Task 1) ===
    public class FileService
    {
        // Đọc danh sách khóa học từ file .txt
        public List<KhoaHoc> DocFile(string duongDan)
        {
            List<KhoaHoc> ds = new List<KhoaHoc>();

            if (!File.Exists(duongDan))
            {
                Console.WriteLine("Khong tim thay file! Tao file moi.");
                File.Create(duongDan).Close();
                return ds;
            }

            using (StreamReader sr = new StreamReader(duongDan))
            {
                string dong;
                while ((dong = sr.ReadLine()) != null)
                {
                    string[] p = dong.Split('|');
                    if (p.Length >= 4)
                    {
                        ds.Add(new KhoaHoc
                        {
                            MaKH = p[0],
                            TenKH = p[1],
                            HocPhi = decimal.Parse(p[2]),
                            TrinhDo = p[3]
                        });
                    }
                }
            }
            return ds;
        }

        // Ghi danh sách khóa học vào file .txt
        public void GhiFile(string duongDan, List<KhoaHoc> ds)
        {
            using (StreamWriter sw = new StreamWriter(duongDan))
            {
                foreach (var kh in ds)
                {
                    sw.WriteLine($"{kh.MaKH}|{kh.TenKH}|{kh.HocPhi}|{kh.TrinhDo}");
                }
            }
        }
    }

    // === LỚP GIẢNG VIÊN (Task 2) ===
    public class GiangVien
    {
        public string MaGV { get; set; }
        public string HoTen { get; set; }
        public int SoGioDay { get; set; }
        public decimal LuongTheoGio { get; set; }
        public int TotalHours { get; set; }

        public decimal TinhLuong()
        {
            return SoGioDay * LuongTheoGio;
        }

        public override string ToString()
        {
            return $"MaGV: {MaGV} | Ten: {HoTen} | Gio day: {SoGioDay} | Luong/gio: {LuongTheoGio:N0} | Tong gio: {TotalHours}";
        }
    }

    // === LỚP SINH VIÊN (Task 2) ===
    public class SinhVien
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public int AttendanceCount { get; set; }
        public int TotalSessions { get; set; }

        public double TinhDiemChuyenCan()
        {
            if (TotalSessions == 0) return 0;
            return (double)AttendanceCount / TotalSessions * 10;
        }

        public override string ToString()
        {
            return $"MaSV: {MaSV} | Ten: {HoTen} | Di hoc: {AttendanceCount}/{TotalSessions} | Diem CC: {TinhDiemChuyenCan():F1}";
        }
    }

    // === CHƯƠNG TRÌNH CHÍNH ===
    class Program
    {
        static List<KhoaHoc> dsKhoaHoc = new List<KhoaHoc>();
        static List<GiangVien> dsGiangVien = new List<GiangVien>();
        static List<SinhVien> dsSinhVien = new List<SinhVien>();
        static FileService fs = new FileService();
        static string filePath = "dulieu.txt";

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Load dữ liệu khóa học từ file
            dsKhoaHoc = fs.DocFile(filePath);

            // Tạo sẵn dữ liệu mẫu cho Task 2
            dsGiangVien.Add(new GiangVien { MaGV = "GV001", HoTen = "Nguyen Van A", SoGioDay = 40, LuongTheoGio = 200000, TotalHours = 120 });
            dsGiangVien.Add(new GiangVien { MaGV = "GV002", HoTen = "Tran Thi C", SoGioDay = 30, LuongTheoGio = 250000, TotalHours = 80 });
            dsSinhVien.Add(new SinhVien { MaSV = "SV001", HoTen = "Le Van B", AttendanceCount = 18, TotalSessions = 20 });
            dsSinhVien.Add(new SinhVien { MaSV = "SV002", HoTen = "Pham Thi D", AttendanceCount = 15, TotalSessions = 20 });

            int chon;
            do
            {
                Console.Clear();
                Console.WriteLine("========== MENU CHINH ==========");
                Console.WriteLine("1. Quan ly Khoa hoc");
                Console.WriteLine("2. Quan ly Giang vien");
                Console.WriteLine("3. Quan ly Sinh vien");
                Console.WriteLine("0. Thoat");
                Console.WriteLine("================================");
                Console.Write("Chon: ");

                if (!int.TryParse(Console.ReadLine(), out chon))
                    chon = -1;

                switch (chon)
                {
                    case 1: MenuKhoaHoc(); break;
                    case 2: MenuGiangVien(); break;
                    case 3: MenuSinhVien(); break;
                    case 0:
                        fs.GhiFile(filePath, dsKhoaHoc);
                        Console.WriteLine("\nDa luu du lieu! Tam biet!");
                        break;
                    default:
                        Console.WriteLine("\nChon sai! Nhan Enter...");
                        Console.ReadLine();
                        break;
                }
            } while (chon != 0);
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
                Console.WriteLine("5. Luu vao file");
                Console.WriteLine("0. Quay lai");
                Console.WriteLine("============================");
                Console.Write("Chon: ");

                if (!int.TryParse(Console.ReadLine(), out chon))
                    chon = -1;

                switch (chon)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("=== DANH SACH KHOA HOC ===");
                        if (dsKhoaHoc.Count == 0)
                            Console.WriteLine("Danh sach trong!");
                        else
                            foreach (var kh in dsKhoaHoc)
                                Console.WriteLine(kh);
                        Console.WriteLine("\nNhan Enter...");
                        Console.ReadLine();
                        break;
                    case 2: ThemKhoaHoc(); break;
                    case 3: SuaKhoaHoc(); break;
                    case 4: XoaKhoaHoc(); break;
                    case 5:
                        fs.GhiFile(filePath, dsKhoaHoc);
                        Console.WriteLine("\nLuu file thanh cong! Nhan Enter...");
                        Console.ReadLine();
                        break;
                }
            } while (chon != 0);
        }

        static void ThemKhoaHoc()
        {
            Console.Clear();
            Console.WriteLine("=== THEM KHOA HOC ===");
            KhoaHoc kh = new KhoaHoc();
            Console.Write("Nhap Ma khoa hoc: ");
            kh.MaKH = Console.ReadLine();
            Console.Write("Nhap Ten khoa hoc: ");
            kh.TenKH = Console.ReadLine();
            Console.Write("Nhap Hoc phi: ");
            kh.HocPhi = decimal.Parse(Console.ReadLine());
            Console.Write("Nhap Trinh do: ");
            kh.TrinhDo = Console.ReadLine();

            dsKhoaHoc.Add(kh);
            fs.GhiFile(filePath, dsKhoaHoc);
            Console.WriteLine("\nThem thanh cong! Nhan Enter...");
            Console.ReadLine();
        }

        static void SuaKhoaHoc()
        {
            Console.Clear();
            Console.WriteLine("=== SUA KHOA HOC ===");
            Console.Write("Nhap Ma khoa hoc can sua: ");
            string ma = Console.ReadLine();
            var kh = dsKhoaHoc.Find(x => x.MaKH == ma);

            if (kh == null)
            {
                Console.WriteLine("Khong tim thay!");
            }
            else
            {
                Console.WriteLine($"Dang sua: {kh}");
                Console.Write("Nhap Ten moi (Enter = giu nguyen): ");
                string ten = Console.ReadLine();
                if (!string.IsNullOrEmpty(ten)) kh.TenKH = ten;

                Console.Write("Nhap Hoc phi moi (Enter = giu nguyen): ");
                string hp = Console.ReadLine();
                if (!string.IsNullOrEmpty(hp)) kh.HocPhi = decimal.Parse(hp);

                Console.Write("Nhap Trinh do moi (Enter = giu nguyen): ");
                string td = Console.ReadLine();
                if (!string.IsNullOrEmpty(td)) kh.TrinhDo = td;

                fs.GhiFile(filePath, dsKhoaHoc);
                Console.WriteLine("Sua thanh cong!");
            }
            Console.WriteLine("Nhan Enter...");
            Console.ReadLine();
        }

        static void XoaKhoaHoc()
        {
            Console.Clear();
            Console.WriteLine("=== XOA KHOA HOC ===");
            Console.Write("Nhap Ma khoa hoc can xoa: ");
            string ma = Console.ReadLine();
            var kh = dsKhoaHoc.Find(x => x.MaKH == ma);

            if (kh == null)
            {
                Console.WriteLine("Khong tim thay!");
            }
            else
            {
                Console.WriteLine($"Ban muon xoa: {kh}");
                Console.Write("Xac nhan xoa? (y/n): ");
                if (Console.ReadLine().ToLower() == "y")
                {
                    dsKhoaHoc.Remove(kh);
                    fs.GhiFile(filePath, dsKhoaHoc);
                    Console.WriteLine("Xoa thanh cong!");
                }
                else Console.WriteLine("Da huy!");
            }
            Console.WriteLine("Nhan Enter...");
            Console.ReadLine();
        }

        // ==================== MENU GIẢNG VIÊN ====================
        static void MenuGiangVien()
        {
            int chon;
            do
            {
                Console.Clear();
                Console.WriteLine("===== QUAN LY GIANG VIEN =====");
                Console.WriteLine("1. Xem danh sach");
                Console.WriteLine("2. Them giang vien");
                Console.WriteLine("3. Sua giang vien");
                Console.WriteLine("4. Xoa giang vien");
                Console.WriteLine("5. Tinh luong");
                Console.WriteLine("0. Quay lai");
                Console.WriteLine("==============================");
                Console.Write("Chon: ");

                if (!int.TryParse(Console.ReadLine(), out chon))
                    chon = -1;

                switch (chon)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("=== DANH SACH GIANG VIEN ===");
                        if (dsGiangVien.Count == 0) Console.WriteLine("Danh sach trong!");
                        else foreach (var gv in dsGiangVien) Console.WriteLine(gv);
                        Console.WriteLine("\nNhan Enter..."); Console.ReadLine();
                        break;
                    case 2: ThemGiangVien(); break;
                    case 3: SuaGiangVien(); break;
                    case 4: XoaGiangVien(); break;
                    case 5: TinhLuongGiangVien(); break;
                }
            } while (chon != 0);
        }

        static void ThemGiangVien()
        {
            Console.Clear();
            Console.WriteLine("=== THEM GIANG VIEN ===");
            GiangVien gv = new GiangVien();
            Console.Write("Ma GV: "); gv.MaGV = Console.ReadLine();
            Console.Write("Ho ten: "); gv.HoTen = Console.ReadLine();
            Console.Write("So gio day: "); gv.SoGioDay = int.Parse(Console.ReadLine());
            Console.Write("Luong/gio: "); gv.LuongTheoGio = decimal.Parse(Console.ReadLine());
            Console.Write("Tong gio tich luy (TotalHours): "); gv.TotalHours = int.Parse(Console.ReadLine());
            dsGiangVien.Add(gv);
            Console.WriteLine("\nThem thanh cong! Nhan Enter..."); Console.ReadLine();
        }

        static void SuaGiangVien()
        {
            Console.Clear();
            Console.Write("Nhap Ma GV can sua: ");
            var gv = dsGiangVien.Find(x => x.MaGV == Console.ReadLine());
            if (gv == null) { Console.WriteLine("Khong tim thay!"); }
            else
            {
                Console.Write("Ho ten moi (Enter = giu): "); string t = Console.ReadLine(); if (t != "") gv.HoTen = t;
                Console.Write("So gio day moi (Enter = giu): "); string g = Console.ReadLine(); if (g != "") gv.SoGioDay = int.Parse(g);
                Console.Write("Luong/gio moi (Enter = giu): "); string l = Console.ReadLine(); if (l != "") gv.LuongTheoGio = decimal.Parse(l);
                Console.Write("TotalHours moi (Enter = giu): "); string th = Console.ReadLine(); if (th != "") gv.TotalHours = int.Parse(th);
                Console.WriteLine("Sua thanh cong!");
            }
            Console.WriteLine("Nhan Enter..."); Console.ReadLine();
        }

        static void XoaGiangVien()
        {
            Console.Clear();
            Console.Write("Nhap Ma GV can xoa: ");
            var gv = dsGiangVien.Find(x => x.MaGV == Console.ReadLine());
            if (gv == null) { Console.WriteLine("Khong tim thay!"); }
            else
            {
                Console.Write($"Xoa {gv.HoTen}? (y/n): ");
                if (Console.ReadLine().ToLower() == "y") { dsGiangVien.Remove(gv); Console.WriteLine("Xoa thanh cong!"); }
                else Console.WriteLine("Da huy!");
            }
            Console.WriteLine("Nhan Enter..."); Console.ReadLine();
        }

        static void TinhLuongGiangVien()
        {
            Console.Clear();
            Console.Write("Nhap Ma GV: ");
            var gv = dsGiangVien.Find(x => x.MaGV == Console.ReadLine());
            if (gv == null) { Console.WriteLine("Khong tim thay!"); }
            else
            {
                Console.WriteLine($"\nGV: {gv.HoTen} | Gio day: {gv.SoGioDay} | Luong/gio: {gv.LuongTheoGio:N0}");
                Console.WriteLine($"=> Luong: {gv.TinhLuong():N0} VND | TotalHours: {gv.TotalHours}");
            }
            Console.WriteLine("\nNhan Enter..."); Console.ReadLine();
        }

        // ==================== MENU SINH VIÊN ====================
        static void MenuSinhVien()
        {
            int chon;
            do
            {
                Console.Clear();
                Console.WriteLine("===== QUAN LY SINH VIEN =====");
                Console.WriteLine("1. Xem danh sach");
                Console.WriteLine("2. Them sinh vien");
                Console.WriteLine("3. Sua sinh vien");
                Console.WriteLine("4. Xoa sinh vien");
                Console.WriteLine("5. Tinh diem chuyen can");
                Console.WriteLine("0. Quay lai");
                Console.WriteLine("=============================");
                Console.Write("Chon: ");

                if (!int.TryParse(Console.ReadLine(), out chon))
                    chon = -1;

                switch (chon)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("=== DANH SACH SINH VIEN ===");
                        if (dsSinhVien.Count == 0) Console.WriteLine("Danh sach trong!");
                        else foreach (var sv in dsSinhVien) Console.WriteLine(sv);
                        Console.WriteLine("\nNhan Enter..."); Console.ReadLine();
                        break;
                    case 2: ThemSinhVien(); break;
                    case 3: SuaSinhVien(); break;
                    case 4: XoaSinhVien(); break;
                    case 5: TinhDiemSinhVien(); break;
                }
            } while (chon != 0);
        }

        static void ThemSinhVien()
        {
            Console.Clear();
            SinhVien sv = new SinhVien();
            Console.Write("Ma SV: "); sv.MaSV = Console.ReadLine();
            Console.Write("Ho ten: "); sv.HoTen = Console.ReadLine();
            Console.Write("So buoi di hoc (AttendanceCount): "); sv.AttendanceCount = int.Parse(Console.ReadLine());
            Console.Write("Tong so buoi (TotalSessions): "); sv.TotalSessions = int.Parse(Console.ReadLine());
            dsSinhVien.Add(sv);
            Console.WriteLine("\nThem thanh cong! Nhan Enter..."); Console.ReadLine();
        }

        static void SuaSinhVien()
        {
            Console.Clear();
            Console.Write("Nhap Ma SV can sua: ");
            var sv = dsSinhVien.Find(x => x.MaSV == Console.ReadLine());
            if (sv == null) { Console.WriteLine("Khong tim thay!"); }
            else
            {
                Console.Write("Ho ten moi (Enter = giu): "); string t = Console.ReadLine(); if (t != "") sv.HoTen = t;
                Console.Write("AttendanceCount moi (Enter = giu): "); string a = Console.ReadLine(); if (a != "") sv.AttendanceCount = int.Parse(a);
                Console.Write("TotalSessions moi (Enter = giu): "); string ts = Console.ReadLine(); if (ts != "") sv.TotalSessions = int.Parse(ts);
                Console.WriteLine("Sua thanh cong!");
            }
            Console.WriteLine("Nhan Enter..."); Console.ReadLine();
        }

        static void XoaSinhVien()
        {
            Console.Clear();
            Console.Write("Nhap Ma SV can xoa: ");
            var sv = dsSinhVien.Find(x => x.MaSV == Console.ReadLine());
            if (sv == null) { Console.WriteLine("Khong tim thay!"); }
            else
            {
                Console.Write($"Xoa {sv.HoTen}? (y/n): ");
                if (Console.ReadLine().ToLower() == "y") { dsSinhVien.Remove(sv); Console.WriteLine("Xoa thanh cong!"); }
                else Console.WriteLine("Da huy!");
            }
            Console.WriteLine("Nhan Enter..."); Console.ReadLine();
        }

        static void TinhDiemSinhVien()
        {
            Console.Clear();
            Console.Write("Nhap Ma SV: ");
            var sv = dsSinhVien.Find(x => x.MaSV == Console.ReadLine());
            if (sv == null) { Console.WriteLine("Khong tim thay!"); }
            else
            {
                Console.WriteLine($"\nSV: {sv.HoTen} | Di hoc: {sv.AttendanceCount}/{sv.TotalSessions}");
                Console.WriteLine($"=> Diem chuyen can: {sv.TinhDiemChuyenCan():F1}/10");
            }
            Console.WriteLine("\nNhan Enter..."); Console.ReadLine();
        }
    }
}