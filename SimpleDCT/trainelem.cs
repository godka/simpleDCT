using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using drizzle.Algorithm;
namespace SimpleDCT
{
    public class trainelem
    {
        private Matrix _mat;
        private int _result;
        private string _str;
        public trainelem(Matrix mat, int result)
        {
            _mat = mat;
            _result = result;
            _str = string.Empty;
        }
        public void Start()
        {
            var _tmp = Matrix.DCT(_mat);
            _mat = _tmp.Normalize();
            _str = _ToString();
        }
        public override string ToString()
        {
            return _str;
            //return base.ToString();
        }
        private string _ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var t in _mat.GetData())
            {
                sb.Append(t);
                sb.Append(",");
            }
            for (int i = 0; i < 10; i++)
            {
                if (i == _result)
                {
                    sb.Append("1");
                }
                else
                {
                    sb.Append("0");
                }
                sb.Append(",");
            }
            return sb.ToString();
        }
    }
}
