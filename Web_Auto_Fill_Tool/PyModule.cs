using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Auto_Fill_Tool
{
    internal static class PyModule
    {
        public static string FILE_MODULE = @"
from selenium import webdriver
from selenium.webdriver.{0}.service import Service
from selenium.webdriver.common.by import By
from selenium.webdriver.common.keys import Keys
from selenium.common import exceptions
from selenium.webdriver.support.ui import WebDriverWait
from time import sleep
from selenium.webdriver.support import expected_conditions as EC

s = Service('{1}')
#打开浏览器
dr = webdriver.{2}(service=s)
web_search_result = ''

result = 0
result_str = '++++++++++++运行结果++++++++++++'
err_str = ''


#执行操作
{3}

if result == 0:
    result_str += '''
    ----------------------
        √ 验证成功
    ----------------------
+++++++++++++++++++++++++++++++
'''
else:
    result_str += '''
    ----------------------
        x 验证失败
    ----------------------     {{0}}
+++++++++++++++++++++++++++++++
'''.format(err_str)

print(result_str)

";

        public static string OPEN_WEBSITE = @"
#登录网址
dr.get('{0}')
";
        public static string SLEEP = @"
sleep({0})
";
        public static string GLOBAL_WAIT_ELEMENT = @"
if result == 0:
    try:
        wait = WebDriverWait(dr, {2})
        wait.until(lambda x: dr.find_element(By.{0}, '{1}'))
    except exceptions.TimeoutException:
        err_str += '\nWAIT--没有找到控件  {{0}}={{1}}'.format(By.{0}, '{1}')
        result -= 1
";

        public static string GLOBAL_WAIT_ELEMENT_AT_INDEX = @"
if result == 0:
    try:
        wait = WebDriverWait(dr, {2})
        wait.until(lambda x: dr.find_elements(By.{0}, '{1}')[{3}])
    except(exceptions.TimeoutException,IndexError):
        err_str += '\nWAIT--没有找到控件  {{0}}={{1}},index={{2}}'.format(By.{0}, '{1}',{3})
        result -= 1
";

        public static string LOCAL_WAIT_ELEMENT = @"
if result == 0:
    try:
        wait = WebDriverWait(dr, {2})
        wait.until(lambda x: element.find_element(By.{0}, '{1}'))
    except exceptions.TimeoutException:
        err_str += '\nWAIT--没有找到控件  {{0}}={{1}}'.format(By.{0}, '{1}')
        result -= 1
";

        public static string LOCAL_WAIT_ELEMENT_AT_INDEX = @"
if result == 0:
    try:
        wait = WebDriverWait(dr, {2})
        wait.until(lambda x: element.find_elements(By.{0}, '{1}')[{3}])
    except(exceptions.TimeoutException,IndexError):
        err_str += '\nWAIT--没有找到控件  {{0}}={{1}},index={{2}}'.format(By.{0}, '{1}',{3})
        result -= 1
";

        public static string GLOBAL_FIND_ELEMENT = @"
if result == 0:
    try:
        element = dr.find_element(By.{0}, '{1}')
    except exceptions.NoSuchElementException:
        err_str += '\nFIND--没有找到控件  {{0}}={{1}}'.format(By.{0}, '{1}')
        result -= 1  
";

        public static string GLOBAL_FIND_ELEMENT_AT_INDEX = @"
if result == 0:
    try:
        element = dr.find_elements(By.{0}, '{1}')[{2}]
    except (exceptions.NoSuchElementException,IndexError):
        err_str += '\nFIND--没有找到控件  {{0}}={{1}},index={{2}}'.format(By.{0}, '{1}',{2})
        result -= 1 
";

        public static string LOCAL_FIND_ELEMENT = @"
if result == 0:
    try:
        element = element.find_element(By.{0}, '{1}')
    except exceptions.NoSuchElementException:
        err_str += '\nFIND--没有找到控件  {{0}}={{1}}'.format(By.{0}, '{1}')
        result -= 1  
";

        public static string LOCAL_FIND_ELEMENT_AT_INDEX = @"
if result == 0:
    try:
        element = element.find_elements(By.{0}, '{1}')[{2}]
    except (exceptions.NoSuchElementException,IndexError):
        err_str += '\nFIND--没有找到控件  {{0}}={{1}},index={{2}}'.format(By.{0}, '{1}',{2})
        result -= 1 
";

        public static string LOCAL_FIND_ELEMENT_ALL = @"
if result == 0:
    try:
        all_elements = element.find_elements(By.{0}, '{1}')
    except (exceptions.NoSuchElementException):
        err_str += '\nFIND--没有找到控件  {{0}}={{1}}'.format(By.{0}, '{1}')
        result -= 1 
";

        public static string LOCAL_FOR_EACH = @"
if result == 0:
    try:
        all_elements = element.find_elements(By.{0}, '{1}')
        for element in all_elements:
            func(element)
    except (exceptions.NoSuchElementException):
        err_str += '\nFIND--没有找到控件  {{0}}={{1}}'.format(By.{0}, '{1}')
        result -= 1 
";

        public static string BEGIN_FUNC = @"
def func(element):
    global result
    global err_str
";

        public static string SWITCH_TO_FRAME = @"
if result == 0:
    dr.switch_to.frame(element)     
";

        public static string WAIT_WINDOW = @"
if result == 0:
    try:
        wait = WebDriverWait(dr, {1})
        all_windows = dr.window_handles
        wait.until(lambda _: len(all_windows)-1 == {0})
    except(exceptions.TimeoutException,IndexError):
        err_str += '\nWAIT--没有等到窗口 index={0}'
        result -= 1
";

        public static string SWITCH_TO_WINDOW = @"
if result == 0:
    all_windows = dr.window_handles
    dr.switch_to.window(all_windows[{0}])
";

        public static string SWITCH_TO_DEFAULT = @"
if result == 0:
    dr.switch_to.default_content()
";

        public static string ELEMENT_CLICK = @"
if result == 0:
    element.click()
";

        public static string ELEMENT_SENDKEYS = @"
if result == 0:
    element.send_keys('{0}')
";

        public static string ELEMENT_GET_TEXT = @"  
if result == 0:
    text = element.text
    web_search_result += '{1}'.format(str=text)
";

        public static string ELEMENT_GET_ATTR = @"
if result == 0:
    attr = element.get_attribute('{0}')
    web_search_result += '{1}'.format(str=attr)
";

        public static string OUT_PUT_SEARCH_RESULT = @"
if result == 0:
        file = open('{0}', 'w+')
        file.write(web_search_result)
        file.close()
";

        public static string CLEAR = @"
if result == 0:
    element.clear()
";

    }
}
