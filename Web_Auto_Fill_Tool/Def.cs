using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Auto_Fill_Tool
{
    public static class Def
    {
        // 文件路径检查结果
        public static int RET_SUCCESS = 0;
        public static int FILE_CHECK_RET_EMPTY_PATH = -1;
        public static int FILE_CHECK_RET_INVALID_PATH = -2;
        public static int FILE_READ_RET_FAILED = -3;

        // excel读取结果
        public static int EXCEL_INVALID_TYPE = -4;
        public static int EXCEL_READ_RET_MISSING_CFG_SHEET = -5;
        public static int EXCEL_READ_RET_MISSING_USERDATA_SHEET = -6;
        public static int EXCEL_READ_RET_EMPTY_CFG_SHEET = -7;
        public static int EXCEL_READ_RET_EMPTY_USERDATA_SHEET = -8;

        public static string PY_FILE_ROOT = "./PyFiles";
        public static string PY_DRIVER_DIR = "./Drivers";
        public static string PYTHON_EXE = "./Python/Python38/python.exe";

        public static string EXCEL_RECORD_PATH = "./excel_record.xml";
        public static string TOOL_SETTING_PATH = "./settings.xml";
    }
}
