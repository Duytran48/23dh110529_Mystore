using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using PagedList.Mvc;

namespace _23dh110529_Mystore.Models.ViewModel
{
    public class ProductDetailVM
    {
        public Product Product { get; set; }
        public int quantity { get; set; }
        //tinh gia tri tam thoi
        public decimal estimatedValue => quantity * Product.ProductPrice;
        //cac thuoc tinh ho tro phan trang
        public int Pagenumber { get; set; }//trang hien tai
        public int PageSize { get; set; } = 3;//so spham moi trang
        //danh sach 8 sphamcung danh muc
        //public PagedList.IPagedList<Product> RelatedProducts { get; set; }
        public List<Product> RelatedProducts { get; set; }
        //dsach 8 spham ban chay nhat cung danh muc
        public PagedList.IPagedList<Product> TopProducts { get; set; }
    }
}