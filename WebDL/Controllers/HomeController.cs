using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebDL.Context;


namespace WebDL.Controllers
{
    public class HomeController : Controller
    {
        WebDLEntities objWebDLEntities = new WebDLEntities();
        public ActionResult Index()
        {
            var lstDiemDen = objWebDLEntities.DiemDens.ToList();
            var lstReservation = objWebDLEntities.Reservations.ToList();
            ViewBag.DiemDen = lstDiemDen;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
     

        [HttpPost]
        public ActionResult SubmitForm(Reservation model)
        {
            if (ModelState.IsValid)
            {
                // Tạo một thực thể mới từ model
                var entity = new Reservation
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    StartDate = (DateTime)model.StartDate,
                    EndDate = (DateTime)model.EndDate,
                    Destination =(string) model.Destination,
                    Participants = (int) model.Participants
                };

                // Thêm thực thể vào cơ sở dữ liệu
                objWebDLEntities.Reservations.Add(entity);
                objWebDLEntities.SaveChanges();

                return RedirectToAction("Success"); // Chuyển hướng tới một trang thành công hoặc trả về một view
            }

            return View(model); // Trả về cùng view với thông báo lỗi nếu có
        }
        public ActionResult Success()
        {
            var lstDiemDen = objWebDLEntities.DiemDens.ToList();
            ViewBag.DiemDen = lstDiemDen;
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Mã hóa mật khẩu trước khi lưu vào cơ sở dữ liệu
                    nguoiDung.MatKhau = GetMD5(nguoiDung.MatKhau);
                    nguoiDung.NgayTao = DateTime.Now;
                    objWebDLEntities.NguoiDungs.Add(nguoiDung);
                    objWebDLEntities.SaveChanges();

                    TempData["SuccessMessage"] = "Đăng ký thành công!";
                    //return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Tài khoản hoặc email đã được sử dụng";
                    ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin");
                    return View(nguoiDung);
                }
            }

            return View(nguoiDung);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(NguoiDung model, QuanTriVien model1)
        {
            WebDLEntities objWebDLEntities = new WebDLEntities();
            try
            {
                if (ModelState.IsValid)
                {
                    // Kiểm tra xem các trường bắt buộc đã được điền đầy đủ
                    if (string.IsNullOrEmpty(model.TenDangNhap) || string.IsNullOrEmpty(model.MatKhau))
                    {
                        ModelState.AddModelError("", "Tên đăng nhập và mật khẩu không được để trống");
                        return View(model);
                    }

                    // Mã hóa mật khẩu để so sánh với cơ sở dữ liệu
                    var encryptedPassword = GetMD5(model.MatKhau);

                    //  Kiểm tra xem người dùng có tồn tại trong bảng NguoiDung không
                    var user = objWebDLEntities.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == model.TenDangNhap && u.MatKhau == encryptedPassword);

                    if (user != null)
                    {
                        //  Kiểm tra xem người dùng có phải là quản trị viên trong bảng QuanTriVien không
                        var isAdmin = objWebDLEntities.QuanTriViens.Any(q => q.TenDangNhap == model1.TenDangNhap && q.MatKhau == encryptedPassword);

                        if (isAdmin)
                        {
                            // Nếu là quản trị viên, lưu thông tin vào Session và chuyển hướng đến trang admin
                            Session["Email"] = user.Email;
                            Session["UserId"] = user.MaNguoiDung;
                            return Redirect("/Admin/HomeAdmin/Admin#");
                        }
                        else
                        {
                            //  Nếu không phải là quản trị viên, lưu thông tin vào Session và chuyển hướng đến trang chủ
                            Session["Email"] = user.Email;
                            Session["UserId"] = user.MaNguoiDung;
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        // Nếu không tìm thấy người dùng trong bảng NguoiDung, kiểm tra trong bảng QuanTriVien
                        var adminUser = objWebDLEntities.QuanTriViens.FirstOrDefault(q => q.TenDangNhap == model1.TenDangNhap && q.MatKhau == model1.MatKhau);

                        if (adminUser != null)
                        {
                            //  Nếu tìm thấy người dùng là quản trị viên trong bảng QuanTriVien, lưu Session và chuyển hướng đến trang admin
                            Session["Email"] = adminUser.Email;
                            Session["UserId"] = adminUser.MaQuanTriVien;
                            return Redirect("/Admin/HomeAdmin/Admin#");
                        }
                        else
                        {
                            // Nếu không tìm thấy người dùng trong cả hai bảng, hiển thị thông báo lỗi
                            TempData["Error"] = "Tên người dùng hoặc mật khẩu không đúng";
                            System.Diagnostics.Debug.WriteLine("User not found with TenDangNhap: " + model.TenDangNhap + " and MatKhau: " + encryptedPassword);
                            ModelState.AddModelError("", "Đăng nhập thất bại. Vui lòng kiểm tra lại tên đăng nhập và mật khẩu.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có lỗi xảy ra trong quá trình đăng nhập
                ViewBag.Error = "Đã xảy ra lỗi khi đăng nhập: " + ex.Message;
                System.Diagnostics.Debug.WriteLine("Đã xảy ra lỗi khi đăng nhập: " + ex.Message);
            }

            //  Trả về view Login với model để hiển thị lại form đăng nhập
            return View(model);
        }



        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public static string GetMD5(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException(nameof(str), "Input string cannot be null or empty.");
            }

            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(str);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public string MenuDestination ()
        {
            List<string> lst = new List<string>();
            string htm = "";
            lst = objWebDLEntities.DiemDens.Select(s=>s.ViTri).Distinct().ToList();
            for(int i=0; i< lst.Count; i++)
            {
                htm += "<li><a href='#" + lst[i] + "'>" + lst[i] + "</a></li>";
            }
            return htm;

        }

    }
}