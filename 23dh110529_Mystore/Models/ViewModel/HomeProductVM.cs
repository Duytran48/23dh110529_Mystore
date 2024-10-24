using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _23dh110529_Mystore.Models.ViewModel
{
    public class HomeProductVM
    {
        public string SearchTerm { get; set; }
        public int PagenNumber { get; set; }//trang hiện tại
        public int Pagesize { get; set; } = 10;//số sản phẩm mỗi trang
        public List<Product> FeaturedProducts { get; set; }
        public PagedList.IPagedList<Product> NewProducts { get; set; }

    }
}