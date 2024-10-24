using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _23dh110529_Mystore.Models;
using _23dh110529_Mystore.Models.ViewModel;
using PagedList;

namespace _23dh110529_Mystore.Controllers
{
    public class HomeController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();
        //GET:  Admin/Products

        public ActionResult Index(string searchTerm, int? page)
        {
            var model = new HomeProductVM();
            var products = db.Products.AsQueryable();
            //tìm kiếm sản phẩm dựa trên từ khóa
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model.SearchTerm = searchTerm;
                products = products.Where(p =>
                p.ProductName.Contains(searchTerm) ||
                p.ProductDescription.Contains(searchTerm) ||
                p.Category.CategoryName.Contains(searchTerm));
            }
            //Đoạn code liên quan tới phân trang
            //Lấy số trang hiện tại (mặc địng là trang 1 nếu ko có giá trị)
            int pageNumber = page ?? 1;
            int pageSize = 6; //số sản phẩm mỗi trang

            //lấy top 10 sản phẩm bán chạy nhất
            model.FeaturedProducts = products.OrderByDescending(p=>p.OrderDetails.Count() ).Take(10).ToList();
            //lấy 20 sản phảm bán ế nhất và phân trang
            model.NewProducts=products.OrderBy(p => p.OrderDetails.Count()).Take(20).ToPagedList(pageNumber,pageSize);
            return View(model);
        }




        //    public ActionResult Index()
        //{
        //    return View();
        //}

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
    }
}