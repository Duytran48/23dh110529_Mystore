using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;

namespace _23dh110529_Mystore.Models.ViewModel
{
    public class ProductSearchVM
    {
        //tiêu chí để search theo mô tả tiêu chí sản phẩm
        public string SearchTerm { get; set; }
        //các tiêu chí để search theo đơn giá
        public decimal? Minprice { get; set; }
        public decimal? Maxprice { get; set; }

        //Thứ tự sắp xếp
        public string SortOrder { get; set; }
        //danh sách sản phẩm thỏa theo điều kiện tìm kiếm
        
        //các thuộc tính hỗ trợ phân trag
        public int PagenNumber { get; set; }//trang hiện tại
        public int Pagesize { get; set; } = 10;//số sản phẩm mỗi trang

        public PagedList.IPagedList<Product> Products { get; set;}
    }

}