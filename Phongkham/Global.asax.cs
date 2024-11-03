using Phongkham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Phongkham
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            CreateAdminUser();
        }
        //PHÂN QUYỀN
        private void CreateAdminUser()
        {
            using (var db = new phongkham1Entities())
            {
                // Kiểm tra xem khoa Admin có tồn tại không
                if (!db.KHOAs.Any(k => k.TenKhoa == "Admin"))
                {
                    var khoaAdmin = new KHOA
                    {
                        TenKhoa = "Admin"
                    };
                    db.KHOAs.Add(khoaAdmin);
                    db.SaveChanges();
                }

                // Kiểm tra xem admin có tồn tại không
                if (!db.BACSIs.Any(b => b.Role == "Admin"))
                {
                    var admin = new BACSI
                    {
                        TenBS = "",
                        DiaChiBS = "Admin",
                        PhoneBS = "1234567890",
                        Email = "admin@example.com",
                        CCCDBS = "123456789",
                        TenKhoa = "Admin",
                        PasswordBS = "admin123", // Lưu ý: Mật khẩu nên được mã hóa
                        Role = "Admin"
                    };
                    db.BACSIs.Add(admin);
                    db.SaveChanges();
                }
            }
        }
    }

   
}

