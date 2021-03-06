using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Web_Auto_Fill_Tool
{
    internal class PyFileHelper
    {
        private static string pre_tab = "";

        /// <summary>
        /// 生成自动化脚本
        /// </summary>
        /// <param name="ct"></param>
        /// <param name="path"></param>
        public static void GeneratePyFile(CustomTable cfgTb,Dictionary<string,string> userData, string selectFileName)
        {
            int cnt = cfgTb.GetDataCount();

            // 目标网站地址
            string userKey = cfgTb.GetValue("ALIAS_KEY", 0);
            string website = cfgTb.GetValue("KEY", 0).TrimEnd('\n');
            string openWebsiteAction = string.Empty;
            if (userData.TryGetValue(userKey,out string url))
            {
                openWebsiteAction = string.Format(PyModule.OPEN_WEBSITE, url);
            }
            else
            {
                openWebsiteAction = string.Format(PyModule.OPEN_WEBSITE, website);
            }

            StringBuilder actions = new StringBuilder();
            // 从第二行开始是行为定义行
            for(int i=1;i<cnt;i++)
            {
                string key = cfgTb.GetValue("KEY", i);
                string key2 = cfgTb.GetValue("KEY2", i);
                string by = cfgTb.GetValue("BY", i);
                string action = cfgTb.GetValue("ACTION", i);
                string aliasKey = cfgTb.GetValue("ALIAS_KEY", i);

                GenerateAction(actions, key, key2, action, by, aliasKey, userData);
            }
            string fillActions = actions.ToString();
            string finalActions = openWebsiteAction + fillActions;

            string directoryPath = string.Format(Def.PY_FILE_ROOT + "/" + selectFileName);
            FileHelper.CreateDirectory(directoryPath);

            string pyChrome = string.Format(PyModule.FILE_MODULE, "chrome", Path.GetFullPath(Def.PY_DRIVER_DIR+"/chromedriver").Replace("\\", "/"), "Chrome", finalActions);
            string pyFirefox = string.Format(PyModule.FILE_MODULE, "firefox", Path.GetFullPath(Def.PY_DRIVER_DIR+"/geckodriver").Replace("\\", "/"), "Firefox", finalActions);
            string pyEdge = string.Format(PyModule.FILE_MODULE, "edge", Path.GetFullPath(Def.PY_DRIVER_DIR+"/msedgedriver").Replace("\\", "/"), "Edge", finalActions);

            FileHelper.SavePyFile(pyChrome, directoryPath + "/auto_chrome.py");
            FileHelper.SavePyFile(pyFirefox, directoryPath + "/auto_firefox.py");
            FileHelper.SavePyFile(pyEdge, directoryPath + "/auto_edge.py");
        }

        private static void GenerateAction(StringBuilder bd, string key, string key2, string action,
            string by, string aliasKey, Dictionary<string, string> userData)
        {
            if(action == "global_wait")
            {
                GenerateGlobalWait(bd, key, key2, action, by, aliasKey, userData);
            }
            if (action == "global_wait_at")
            {
                GenerateGlobalWaitAt(bd, key, key2, action, by, aliasKey, userData);
            }
            if (action == "local_wait")
            {
                GenerateLocalWait(bd, key, key2, action, by, aliasKey, userData);
            }
            if (action == "local_wait_at")
            {
                GenerateLocalWaitAt(bd, key, key2, action, by, aliasKey, userData);
            }
            else if(action == "switch_frame")
            {
                GenerateSwitchToFrame(bd, key, key2, action, by, aliasKey, userData);
            }
            else if(action == "switch_default")
            {
                GenerateSwitchToDefault(bd, key, key2, action, by, aliasKey, userData);
            }
            else if(action == "global_find")
            {
                GenerateGlobalFind(bd, key, key2, action, by, aliasKey, userData);
            }
            else if(action == "global_find_at")
            {
                GenerateGlobalFindAt(bd, key, key2, action, by, aliasKey, userData);
            }
            else if (action == "local_find")
            {
                GenerateLocalFind(bd, key, key2, action, by, aliasKey, userData);
            }
            else if (action == "local_find_at")
            {
                GenerateLocalFindAt(bd, key, key2, action, by, aliasKey, userData);
            }
            else if(action == "click")
            {
                GenerateElementClick(bd, key, key2, action, by, aliasKey, userData);
            }
            else if(action == "send_keys")
            {
                GenerateElementSendKeys(bd, key, key2, action, by, aliasKey, userData);
            }
            else if(action == "sleep")
            {
                GenerateSleep(bd, key, key2, action, by, aliasKey, userData);
            }
            else if (action == "clear")
            {
                GenerateClear(bd, key, key2, action, by, aliasKey, userData);
            }
            else if (action == "get_text")
            {
                GenerateGetText(bd, key, key2, action, by, aliasKey, userData);
            }
            else if(action == "get_attr")
            {
                GenerateGetAttr(bd, key, key2, action, by, aliasKey, userData);
            }
            else if(action == "output")
            {
                GenerateOutput(bd, key, key2, action, by, aliasKey, userData);
            }
            else if(action == "wait_window")
            {
                GenerateWaitWindow(bd, key, key2, action, by, aliasKey, userData);
            }
            else if(action == "switch_window")
            {
                GenerateSwitchToWindow(bd, key, key2, action, by, aliasKey, userData);
            }
            else if(action == "local_find_all")
            {
                GenerateLocalFindAll(bd, key, key2, action, by, aliasKey, userData);
            }
            else if(action == "local_for_each")
            {
                GenerateLocalForeach(bd, key, key2, action, by, aliasKey, userData);
            }
            else if(action == "begin_func")
            {
                GenerateBeginFunc(bd, key, key2, action, by, aliasKey, userData);
            }
            else if(action == "end_func")
            {
                GenerateEndFunc(bd, key, key2, action, by, aliasKey, userData);
            }
        }

        private static void AppendScript(StringBuilder bd,string module)
        {
            if(pre_tab.Length > 0)
            {
                string append = module.Replace("\n", "\n    ");
                bd.Append(append);
            }
            else
            {
                bd.Append(module);
            }
        }

        private static void GenerateClear(StringBuilder bd, string key, string key2, string action,
string by, string aliasKey, Dictionary<string, string> userData)
        {
            string s = PyModule.CLEAR;
            AppendScript(bd, s);
        }

        private static void GenerateSleep(StringBuilder bd, string key, string key2, string action,
string by, string aliasKey, Dictionary<string, string> userData)
        {
            string s = string.Format(PyModule.SLEEP, key2);
            AppendScript(bd, s);
            //bd.AppendFormat(PyModule.SLEEP,key2);
        }

        private static void GenerateGlobalFind(StringBuilder bd, string key, string key2, string action,
string by, string aliasKey, Dictionary<string, string> userData)
        {
            string s = string.Format(PyModule.GLOBAL_FIND_ELEMENT, by, key);
            AppendScript(bd, s);
            //bd.AppendFormat(PyModule.GLOBAL_FIND_ELEMENT, by, key);
        }

        private static void GenerateGlobalFindAt(StringBuilder bd, string key, string key2, string action,
 string by, string aliasKey, Dictionary<string, string> userData)
        {
            string s = string.Format(PyModule.GLOBAL_FIND_ELEMENT_AT_INDEX, by, key, key2);
            AppendScript(bd, s);
            //bd.AppendFormat(PyModule.GLOBAL_FIND_ELEMENT_AT_INDEX, by, key, key2);
        }

    private static void GenerateLocalFind(StringBuilder bd, string key, string key2, string action,
string by, string aliasKey, Dictionary<string, string> userData)
        {
            string s = string.Format(PyModule.LOCAL_FIND_ELEMENT, by, key);
            AppendScript(bd, s);
            //bd.AppendFormat(PyModule.LOCAL_FIND_ELEMENT, by, key);
        }

        private static void GenerateLocalFindAt(StringBuilder bd, string key, string key2, string action,
            string by, string aliasKey, Dictionary<string, string> userData)
        {
            string s = string.Format(PyModule.LOCAL_FIND_ELEMENT_AT_INDEX, by, key, key2);
            AppendScript(bd, s);
            //bd.AppendFormat(PyModule.LOCAL_FIND_ELEMENT_AT_INDEX, by, key, key2);
        }

        private static void GenerateElementClick(StringBuilder bd, string key, string key2, string action,
string by, string aliasKey,Dictionary<string,string> userData)
        {
            string s = PyModule.ELEMENT_CLICK;
            AppendScript(bd, s);
            //bd.Append(PyModule.ELEMENT_CLICK);
        }

        private static void GenerateElementSendKeys(StringBuilder bd, string key, string key2, string action,
string by, string aliasKey, Dictionary<string, string> userData)
        {
            if (userData.TryGetValue(aliasKey, out string send_key))
            {
                string s = string.Format(PyModule.ELEMENT_SENDKEYS, send_key);
                AppendScript(bd, s);
                //bd.AppendFormat(PyModule.ELEMENT_SENDKEYS, send_key);
            }
            else
            {
                string s = string.Format(PyModule.ELEMENT_SENDKEYS, key);
                AppendScript(bd, s);
                //bd.AppendFormat(PyModule.ELEMENT_SENDKEYS, key);
            }
        }

        private static void GenerateGlobalWait(StringBuilder bd, string key, string key2, string action,
 string by, string aliasKey, Dictionary<string, string> userData)
        {
            float time = key2.Equals(string.Empty) ? 2 : float.Parse(key2);
            string s = string.Format(PyModule.GLOBAL_WAIT_ELEMENT, by, key, time);
            AppendScript(bd, s);
            //bd.AppendFormat(PyModule.GLOBAL_WAIT_ELEMENT, by, key, time);
        }
        private static void GenerateGlobalWaitAt(StringBuilder bd, string key, string key2, string action,
 string by, string aliasKey, Dictionary<string, string> userData)
        {
            float time = key2.Equals(string.Empty) ? 2 : float.Parse(key2);
            string s = string.Format(PyModule.GLOBAL_WAIT_ELEMENT_AT_INDEX, by, key, time, key2);
            AppendScript(bd, s);
            //bd.AppendFormat(PyModule.GLOBAL_WAIT_ELEMENT_AT_INDEX, by, key, time, key2);
        }

        private static void GenerateLocalWait(StringBuilder bd, string key, string key2, string action,
    string by, string aliasKey, Dictionary<string, string> userData)
        {
            float time = key2.Equals(string.Empty) ? 2 : float.Parse(key2);
            string s = string.Format(PyModule.LOCAL_WAIT_ELEMENT, by, key, time);
            AppendScript(bd, s);
            //bd.AppendFormat(PyModule.LOCAL_WAIT_ELEMENT, by, key, time);
        }

        private static void GenerateLocalWaitAt(StringBuilder bd, string key, string key2, string action,
string by, string aliasKey, Dictionary<string, string> userData)
        {
            float time = key2.Equals(string.Empty) ? 2 : float.Parse(key2);
            string s = string.Format(PyModule.LOCAL_WAIT_ELEMENT_AT_INDEX, by, key, time, key2);
            AppendScript(bd, s);
            //bd.AppendFormat(PyModule.LOCAL_WAIT_ELEMENT_AT_INDEX, by, key, time, key2);
        }

        private static void GenerateSwitchToFrame(StringBuilder bd, string key, string key2, string action,
string by, string aliasKey, Dictionary<string, string> userData)
        {
            string s = PyModule.SWITCH_TO_FRAME;
            AppendScript(bd, s);
            //bd.Append(PyModule.SWITCH_TO_FRAME);
        }
        private static void GenerateWaitWindow(StringBuilder bd, string key, string key2, string action,
string by, string aliasKey, Dictionary<string, string> userData)
        {
            string s = string.Format(PyModule.WAIT_WINDOW, key, key2);
            AppendScript(bd, s);
            //bd.AppendFormat(PyModule.WAIT_WINDOW, key, key2);
        }

        private static void GenerateSwitchToWindow(StringBuilder bd, string key, string key2, string action,
string by, string aliasKey, Dictionary<string, string> userData)
        {
            string s = string.Format(PyModule.SWITCH_TO_WINDOW, key);
            AppendScript(bd, s);
            //bd.AppendFormat(PyModule.SWITCH_TO_WINDOW, key);
        }

        private static void GenerateSwitchToDefault(StringBuilder bd, string key, string key2, string action,
string by, string aliasKey, Dictionary<string, string> userData)
        {
            string s = PyModule.SWITCH_TO_DEFAULT;
            AppendScript(bd, s);
            //bd.Append(PyModule.SWITCH_TO_DEFAULT);
        }

        private static void GenerateGetText(StringBuilder bd, string key, string key2, string action,
string by, string aliasKey, Dictionary<string, string> userData)
        {
            string s = PyModule.ELEMENT_GET_TEXT;
            AppendScript(bd, s);
            //bd.AppendFormat(PyModule.ELEMENT_GET_ATTR,key,key2);
        }

        private static void GenerateGetAttr(StringBuilder bd, string key, string key2, string action,
string by, string aliasKey, Dictionary<string, string> userData)
        {
            string s = string.Format(PyModule.ELEMENT_GET_ATTR, key, key2);
            AppendScript(bd, s);
            //bd.AppendFormat(PyModule.ELEMENT_GET_ATTR,key,key2);
        }

        private static void GenerateOutput(StringBuilder bd, string key, string key2, string action,
string by, string aliasKey, Dictionary<string, string> userData)
        {
            string s = string.Format(PyModule.OUT_PUT_SEARCH_RESULT, key);
            AppendScript(bd, s);
            //bd.AppendFormat(PyModule.OUT_PUT_SEARCH_RESULT,key);
        }


        private static void GenerateLocalFindAll(StringBuilder bd, string key, string key2, string action,
string by, string aliasKey, Dictionary<string, string> userData)
        {
            string s = string.Format(PyModule.LOCAL_FIND_ELEMENT_ALL, key);
            AppendScript(bd, s);
            //bd.AppendFormat(PyModule.LOCAL_FIND_ELEMENT_ALL, key);
        }

        private static void GenerateLocalForeach(StringBuilder bd, string key, string key2, string action,
string by, string aliasKey, Dictionary<string, string> userData)
        {
            string s = string.Format(PyModule.LOCAL_FOR_EACH, key);
            AppendScript(bd, s);
            //bd.AppendFormat(PyModule.LOCAL_FOR_EACH, key);
        }

        private static void GenerateBeginFunc(StringBuilder bd, string key, string key2, string action,
string by, string aliasKey, Dictionary<string, string> userData)
        {
            string s = PyModule.BEGIN_FUNC;
            AppendScript(bd, s);
            //bd.AppendFormat(PyModule.BEGIN_FUNC);
            pre_tab = pre_tab + "    ";
        }

        private static void GenerateEndFunc(StringBuilder bd, string key, string key2, string action,
string by, string aliasKey, Dictionary<string, string> userData)
        {
            if (pre_tab.Length > 4)
                pre_tab.Remove(pre_tab.Length - 4, 4);
        }

    }
}
