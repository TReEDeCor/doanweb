using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
namespace TReEDeCor.Models
{
    public class TiemKiem
    {
        DatabaseDataContext data = null;
        public TiemKiem()
        {
            data = new DatabaseDataContext();
        }

        public IEnumerable<SANPHAM>TimKiemSanPham(string timkiem)
        {
            IOrderedQueryable<SANPHAM> model = data.SANPHAMs.OrderByDescending(x => x.MaSP);

            if (!string.IsNullOrEmpty(timkiem))
            {
                model = model.Where(x => x.TenSP.Contains(timkiem) || x.TenSP.Contains(timkiem)).OrderByDescending(x => x.MaSP);

            }

            return model;
        }
    }
}