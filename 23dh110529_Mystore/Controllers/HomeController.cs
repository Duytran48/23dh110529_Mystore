using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public ActionResult Trangchu(string searchTerm, int? page)
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
                p.Category.CategoryName.Contains(searchTerm) );
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
        //Get Home/ProductDetails
            public ActionResult ProductDetail(int? id,int? quantity,int?page)
             {
                if (id==null)
                {
                   return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                        Product pro = db.Products.Find(id);
                        if (pro == null)
                        {
                                 return HttpNotFound();
                         }
                             //lay tat ca san pham cung danh muc
                            var products = db.Products.Where(p => p.CategoryID == pro.CategoryID && p.ProductID != pro.ProductID).AsQueryable();

                         ProductDetailVM model = new ProductDetailVM();

                    //Doan code lien quan toi phan trang
                    //lay so trang hien tai mac dinh(mac dinh la 1 neu ko co gia tri)
                            int pageNumber = page ?? 1;
                         int pageSize = model.PageSize; //so san pham moi trang
                             model.Product = pro;
                                model.RelatedProducts = products.OrderBy(p => p.ProductID).Take(8).ToList();
                                model.TopProducts = products.OrderByDescending(p => p.OrderDetails.Count()).Take(8).ToPagedList(pageNumber, pageSize);

                    if(quantity.HasValue)
                    {
                            model.quantity = quantity.Value;
                    }
                    return View(model); 
             }
    }
}