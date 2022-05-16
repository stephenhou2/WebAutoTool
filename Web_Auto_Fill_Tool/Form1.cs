using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.Diagnostics;
using System.Xml;
using System.Security;

namespace Web_Auto_Fill_Tool
{

    // 1.读取配置文件，生成对应的python文件
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            InitializeCfg();
        }

        private bool mShowConsole = false;
        private string mSelectExplorerPath = string.Empty;
        private string mSelectExplorerName = string.Empty;

        private List<string> mExcelRecords = new List<string>();

        private void InitializeCfg()
        {
            ReadSettings();
            ReadExcelRecords();
        }

        private void BtnGenerateScript_Click(object sender, EventArgs e)
        {
            foreach (string excelPath in mExcelRecords)
            {
                GeneratePyFile(excelPath);
            }
        }

        private void GeneratePyFile(string excelFullPath)
        {
            this.label_excute_result.Text = String.Empty;

            int ret = FilePathCheck(excelFullPath);
            if (ret != Def.RET_SUCCESS)
            {
                ShowBottomHint(ret);
                return;
            }


            CustomTable cfgTb;
            Dictionary<string, string> userData;
            (cfgTb, userData, ret) = ReadExcel(excelFullPath);
            if (ret != Def.RET_SUCCESS)
            {
                return;
            }

            string fileName = Path.GetFileName(excelFullPath);
            PyFileHelper.GeneratePyFile(cfgTb, userData, fileName);
            this.label_excute_result.Text = "自动化脚本生成成功!";
        }

        private void BtnAddExcelClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Title = "请选择要导入的表格文件";
            ofd.Multiselect = false;
            ofd.Filter = "excel文件（03）|*.xls|excel文件（07）|*.xlsx";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                //获取用户选择的文件完整路径
                if(!mExcelRecords.Contains(ofd.FileName))
                {
                    this.mExcelRecords.Add(ofd.FileName);
                    this.ExcelSelectView.Items.Add(ofd.FileName);
                }
            }
            WriteExcelRecords();
        }


        private void BtnRemoveExcelClick(object sender, EventArgs e)
        {
            int count = ExcelSelectView.Items.Count;
            for(int i=count-1;i>=0;i--)
            {
                bool check = ExcelSelectView.GetItemChecked(i);
                string path = ExcelSelectView.Items[i].ToString();
                if (check && mExcelRecords.Contains(path))
                {
                    this.ExcelSelectView.Items.RemoveAt(i);
                    mExcelRecords.Remove(path);
                }
            }

            WriteExcelRecords();
        }

        private void BtnSelectAllClick(object sender, EventArgs e)
        {
            bool allSelect = true;
            for(int i=0;i<this.ExcelSelectView.Items.Count;i++)
            {
                if(!ExcelSelectView.GetItemChecked(i))
                {
                    allSelect = false;
                    break;
                }
            }

            for (int i = 0; i < this.ExcelSelectView.Items.Count; i++)
            {
                this.ExcelSelectView.SetItemChecked(i,!allSelect);
            }
        }


        private int FilePathCheck(string filePath)
        {
            if (filePath.Equals(string.Empty))
            {
                return Def.FILE_CHECK_RET_EMPTY_PATH;
            }

            if (!File.Exists(filePath))
            {
                return Def.FILE_CHECK_RET_INVALID_PATH;
            }

            return Def.RET_SUCCESS;
        }

        private int ExcelCheck(IWorkbook workbook)
        {
            ISheet sheet = workbook.GetSheet("cfg");
            if (sheet == null)
            {
                return Def.EXCEL_READ_RET_MISSING_CFG_SHEET;
            }
            int rowNum = sheet.LastRowNum;
            if (rowNum == 0)
            {
                return Def.EXCEL_READ_RET_EMPTY_CFG_SHEET;
            }
            IRow fields = sheet.GetRow(0);
            int colNum = fields.LastCellNum;
            if (colNum == 0)
            {
                return Def.EXCEL_READ_RET_EMPTY_CFG_SHEET;
            }

            ISheet userDataSheet = workbook.GetSheetAt(1);
            if (userDataSheet == null)
            {
                return Def.EXCEL_READ_RET_MISSING_USERDATA_SHEET;
            }
            rowNum = userDataSheet.LastRowNum;
            if (rowNum == 0)
            {
                return Def.EXCEL_READ_RET_EMPTY_USERDATA_SHEET;
            }
            fields = sheet.GetRow(0);
            colNum = fields.LastCellNum;
            if (colNum == 0)
            {
                return Def.EXCEL_READ_RET_EMPTY_USERDATA_SHEET;
            }

            return Def.RET_SUCCESS;
        }

        private string GetCellValue(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.Unknown:
                case CellType.Blank:
                case CellType.Formula:
                case CellType.Error:
                    return string.Empty;
                case CellType.Numeric:
                    return cell.NumericCellValue.ToString();
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.String:
                    return cell.StringCellValue;
                default:
                    return string.Empty;
            }
        }

        private (CustomTable,Dictionary<string,string>,int) ReadExcel(string filePath)
        {
            FileStream fs = File.Open(filePath, FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
            
            if(fs == null)
            {
                return (null, null, Def.FILE_READ_RET_FAILED);
            }

            IWorkbook workbook;
            if(filePath.EndsWith(".xlsx"))
            {
                workbook = new XSSFWorkbook(fs);
            }
            else if(filePath.EndsWith(".xls"))
            {
                workbook = new HSSFWorkbook(fs);
            }
            else
            {
                return (null, null, Def.EXCEL_INVALID_TYPE);
            }

            fs.Dispose();
            fs.Close();

            int ret = ExcelCheck(workbook);
            if(ret != Def.RET_SUCCESS)
            {
                return (null, null, ret);
            }

            ISheet sheet = workbook.GetSheet("cfg");
            ISheet userDataSheet = workbook.GetSheetAt(1);

            CustomTable cfgTb = new CustomTable();
            int cfgRowNum = sheet.LastRowNum;
            IRow cfgFields = sheet.GetRow(0);
            int cfgColNum = cfgFields.LastCellNum;
            for (int i=0;i<cfgColNum;i++)
            {
                ICell fieldCell = cfgFields.GetCell(i);
                if(fieldCell == null)
                {
                    continue;
                }

                // 字段名
                string fieldName = GetCellValue(fieldCell);
                List<string> datas = new List<string>();
                for(int j=1;j<=cfgRowNum;j++)
                {
                    ICell dataCell = sheet.GetRow(j).GetCell(i);
                    if (dataCell == null)
                    {
                        datas.Add("");
                        continue;
                    }

                    string data = GetCellValue(dataCell);
                    datas.Add(data);
                }
                cfgTb.Add(fieldName,datas);
            }

            Dictionary<string, string> userData = new Dictionary<string, string>();
            int userRowNum = userDataSheet.LastRowNum;
            for(int i=0;i<=userRowNum;i++)
            {
                IRow userRow = userDataSheet.GetRow(i);

                ICell aliasCell = userRow.GetCell(0);
                ICell userValueCell = userRow.GetCell(1);
                
                if(aliasCell == null || userValueCell == null)
                {
                    continue;
                }

                string aliasKey = GetCellValue(aliasCell);
                string userValue = GetCellValue(userValueCell);
                userData.Add(aliasKey, userValue);
            }

            return (cfgTb, userData, Def.RET_SUCCESS);
        }

        private void ShowBottomHint(int ret)
        {
            if (ret == Def.FILE_CHECK_RET_EMPTY_PATH)
            {
                this.label_excute_result.Text = "当前未选中任何文件！！！";
            }
            else if(ret == Def.FILE_CHECK_RET_INVALID_PATH)
            {
                this.label_excute_result.Text = "文件路径不存在！！！";
            }
            else if(ret == Def.FILE_READ_RET_FAILED)
            {
                this.label_excute_result.Text = "文件读取失败！！！";
            }
            else if(ret == Def.EXCEL_INVALID_TYPE)
            {
                this.label_excute_result.Text = "文件类型错误！！！";
            }
            else if(ret == Def.EXCEL_READ_RET_MISSING_CFG_SHEET)
            {
                this.label_excute_result.Text = "缺少配置页！！！";
            }
            else if(ret == Def.EXCEL_READ_RET_MISSING_USERDATA_SHEET)
            {
                this.label_excute_result.Text = "缺少用户数据页！！！";
            }
            else if(ret == Def.EXCEL_READ_RET_EMPTY_CFG_SHEET)
            {
                this.label_excute_result.Text = "空的配置页！！！";
            }
            else if(ret == Def.EXCEL_READ_RET_EMPTY_USERDATA_SHEET)
            {
                this.label_excute_result.Text = "空的用户数据页！！！";
            }

        }

        private void RunSingleAutoFill(string python_exe,string filePath)
        {
            Process process = new Process();
            if (File.Exists(filePath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(python_exe, filePath);
                if (!mShowConsole)
                {
                    startInfo.UseShellExecute = false;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.CreateNoWindow = true;
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit(2000);
                    string output = process.StandardOutput.ReadToEnd();
                    process.Close();
                }
                else
                {
                    process.StartInfo = startInfo;
                    process.Start();
                }
            }
            else
            {
                this.label_excute_result.Text = string.Format("未找到自动化填充脚本，请重新生成【{0}】", filePath);
            }
        }

        private void RunAutoFill(string explorer)
        {
            string run = string.Empty;
            switch(explorer)
            {
                case "chrome":
                    run = "auto_chrome.py";
                    break;
                case "firefox":
                    run = "auto_firefox.py";
                    break;
                default:
                    break;
            }

            if (run.Equals(string.Empty))
            {
                this.label_excute_result.Text = string.Format("未找到对应的浏览器驱动，请检查文件完整性-{0}", explorer);
                return;
            }

            string python_exe = Path.GetFullPath(Def.PYTHON_EXE).Replace("\\", "/");

            if (File.Exists(python_exe))
            {
                for (int i = 0; i < ExcelSelectView.Items.Count; i++)
                {
                    if (ExcelSelectView.GetItemChecked(i))
                    {
                        string excelPath = ExcelSelectView.Items[i].ToString();
                        GeneratePyFile(excelPath);
                        string dir = Path.GetFileName(excelPath);
                        string pyPath = string.Format("{0}/{1}/{2}", Def.PY_FILE_ROOT,dir, run);
                        RunSingleAutoFill(python_exe, pyPath);
                    }
                }

            }
            else
            {
                this.label_excute_result.Text = "未找到Python解释器";
            }
        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            if(mSelectExplorerName.Equals(string.Empty))
            {
                this.label_excute_result.Text = "请先选择一个浏览器！！！";
                return;
            }

            if(mSelectExplorerName.Contains("chrome"))
            {
                RunAutoFill("chrome");
            }
            else if(mSelectExplorerName.Contains("firefox"))
            {
                RunAutoFill("firefox");
            }
            else
            {
                this.label_excute_result.Text = "只支持使用Chrome，FireFox，IE，Edge浏览器！！！";
            }
        }


        private void SetExplorer_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Title = "请选择浏览器";
            ofd.Multiselect = false;
            ofd.Filter = "exe文件|*.exe";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                //获取用户选择的文件完整路径
                mSelectExplorerPath = ofd.FileName;
                this.ExplorerPath.Text = mSelectExplorerPath;
                mSelectExplorerName = ofd.SafeFileName.ToLower();

                WriteSettings(mSelectExplorerPath, mShowConsole);
            }
        }

        private void ShowConsole_CheckedChanged(object sender, EventArgs e)
        {
            this.mShowConsole = this.ShowConsole.Checked;
            WriteSettings(mSelectExplorerPath, mShowConsole);
        }


        private void ReadSettings()
        {
            if (!File.Exists(Def.TOOL_SETTING_PATH))
            {
                this.label_excute_result.Text = "未找到配置文件，无法进行初始化";
                return;
            }

            XmlDocument xml = new XmlDocument();
            xml.Load(Def.TOOL_SETTING_PATH);
            XmlNode root = xml.SelectSingleNode("root");

            if(root != null)
            {
                XmlElement setting = root.SelectSingleNode("setting") as XmlElement;
                mSelectExplorerPath = setting.GetAttribute("driver_path");
                mSelectExplorerName = Path.GetFileName(mSelectExplorerPath);
                mShowConsole = bool.Parse(setting.GetAttribute("show_console"));

                this.ShowConsole.Checked = mShowConsole;
                this.ExplorerPath.Text = mSelectExplorerPath;
            }
        }

        private void WriteSettings(string driver, bool showConsole)
        {
            XmlDocument xml = new XmlDocument();

            XmlElement root = xml.CreateElement("root");
            xml.AppendChild(root);

            XmlElement setting = xml.CreateElement("setting");
            setting.SetAttribute("driver_path", mSelectExplorerPath);
            setting.SetAttribute("show_console", mShowConsole ? "True" : "False");
            root.AppendChild(setting);

            xml.Save(Def.TOOL_SETTING_PATH);
        }


        private void ReadExcelRecords()
        {
            if (!File.Exists(Def.EXCEL_RECORD_PATH))
            {
                this.label_excute_result.Text = "未找到表格清单文件，无法进行初始化自动化清单";
                return;
            }

            XmlDocument xml = new XmlDocument();
            xml.Load(Def.EXCEL_RECORD_PATH);
            XmlNode root = xml.SelectSingleNode("root");


            if (root != null)
            {
                foreach(XmlNode excel in root.ChildNodes)
                {
                    XmlElement record = excel as XmlElement;
                    string path = record.InnerText;
                    mExcelRecords.Add(path);
                    this.ExcelSelectView.Items.Add(path);
                }
            }
        }

        private void WriteExcelRecords()
        {
            XmlDocument xml = new XmlDocument();
            XmlElement root = xml.CreateElement("root");
            xml.AppendChild(root);
            foreach(string path in mExcelRecords)
            {
                XmlElement record = xml.CreateElement("excel");
                record.InnerText = path;
                root.AppendChild(record);
            }
            xml.Save(Def.EXCEL_RECORD_PATH);
        }
    }
}
