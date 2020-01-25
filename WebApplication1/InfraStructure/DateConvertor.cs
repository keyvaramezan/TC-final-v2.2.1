using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace TenderComp.InfraStructure
{
    public class DateConvertor
    {
        public string MiladiToShamsi(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            StringBuilder sb = new StringBuilder();
            sb.Append(pc.GetYear(date).ToString("0000"));
            sb.Append("/");
            sb.Append(pc.GetMonth(date).ToString("00"));
            sb.Append("/");
            sb.Append(pc.GetDayOfMonth(date).ToString("00"));
            return sb.ToString();

        }

        private string _dat, _sal, _mah, _roz;
        public DateTime Shamsitomiladi(string s)
        {
            _dat = s;
            _sal = _dat.Substring(0, 4);
            _mah = _dat.Substring(5, 2);
            _roz = _dat.Substring(8, 2);
            PersianCalendar pc = new PersianCalendar();
            //_ret = pc.ToDateTime(Convert.ToInt32(_sal), Convert.ToInt32(_mah), Convert.ToInt32(_roz), 0, 0, 0, 0).ToString(CultureInfo.CurrentCulture);
            var miladi = pc.ToDateTime(Convert.ToInt32(_sal), Convert.ToInt32(_mah), Convert.ToInt32(_roz), 0, 0, 0, 0);
            return miladi;
        }
    }
     
      
}