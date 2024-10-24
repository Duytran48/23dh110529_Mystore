using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using _23dh110529_Mystore.Models;
using _23dh110529_Mystore.Models.ViewModel;
using PagedList;
namespace _23dh110529_Mystore.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        // GET: Admin/Products
        public ActionResult Index(string searchTerm,decimal? minprice, decimal? maxprice,string sortOder,int? page )
        {
            var model = new ProductSearchVM();
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
            //tím kiếm dựa trên giá tối thiểu
            if (minprice.HasValue)
            {
                model.Minprice = minprice.Value;
                products = products.Where(p => p.ProductPrice >= minprice.Value);
            }
            //tím kiếm dựa trên giá tối đa
            if (maxprice.HasValue)
            {
                model.Maxprice = maxprice.Value;
                products = products.Where(p => p.ProductPrice >= maxprice.Value);
            }
            //Áp dụng sắp xếp dựa trên lựa chọn của người dùng
            switch(sortOder)
            {
                case "name_asc": products = products.OrderBy(p=>p.ProductName); break;
                case "name_desc": products = products.OrderByDescending(p =>p.ProductName); break;
                case "price_asc": products = products.OrderBy(p =>p.ProductPrice); break;
                case "price_dsc": products = products.OrderByDescending(p =>p.ProductName); break;
                default:
                    products = products.OrderBy(p => p.ProductName); break;
            }
            model.SortOrder = sortOder;

            //Đoạn code liên quan tới phân trang
            //Lấy số trang hiện tại (mặc địng là trang 1 nếu ko có giá trị)
            int pageNumber = page?? 1;
            int pageSize = 2; //số sản phẩm mỗi trang

            //đóng câu lệnh này , sử dụng ToPagedlist để lấy danh sách đã phân trang
            //model.Products = products.ToList();
            model.Products = products.ToPagedList(pageNumber,pageSize);        
            return View(model); 
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,CategoryID,ProductName,ProductPrice,ProductImage,ProductDescription,Sold_Quantity")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,CategoryID,ProductName,ProductPrice,ProductImage,ProductDescription,Sold_Quantity")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
