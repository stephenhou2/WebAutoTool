using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Auto_Fill_Tool
{
    internal class CustomTable
    {
        private Dictionary<string, List<string>> mTable = new Dictionary<string, List<string>>();
        private int mCount = 0;

        public string GetValue(string fieldName,int row)
        {
            return mTable[fieldName][row];
        }
        
        public void Add(string fieldName,List<string> datas)
        {
            mTable.Add(fieldName, datas);
            mCount = datas.Count;
        }

        public int GetDataCount()
        {
            return mCount;
        }

    }
}
