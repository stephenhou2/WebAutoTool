
from selenium import webdriver
from selenium.webdriver.edge.service import Service
from selenium.webdriver.common.by import By
from selenium.webdriver.common.keys import Keys
from selenium.common import exceptions
from selenium.webdriver.support.ui import WebDriverWait
from time import sleep
from selenium.webdriver.support import expected_conditions as EC

s = Service('C:/Users/Administrator/Desktop/WebAutoTool/Web_Auto_Fill_Tool/bin/Debug/Drivers/msedgedriver')
#打开浏览器
dr = webdriver.Edge(service=s)
web_search_result = ''

result = 0
result_str = '++++++++++++运行结果++++++++++++'
err_str = ''


#执行操作

#登录网址
dr.get('https://dictionary.cambridge.org/dictionary/english-chinese-simplified/about')

sleep()

if result == 0:
    try:
        wait = WebDriverWait(dr, 10)
        wait.until(lambda x: dr.find_element(By.CLASS_NAME, 'di-body'))
    except exceptions.TimeoutException:
        err_str += '\nWAIT--没有找到控件  {0}={1}'.format(By.CLASS_NAME, 'di-body')
        result -= 1

if result == 0:
    try:
        element = dr.find_element(By.CLASS_NAME, 'di-body')
    except exceptions.NoSuchElementException:
        err_str += '\nFIND--没有找到控件  {0}={1}'.format(By.CLASS_NAME, 'di-body')
        result -= 1  

if result == 0:
    try:
        element = element.find_element(By.CLASS_NAME, 'hw dhw')
    except exceptions.NoSuchElementException:
        err_str += '\nFIND--没有找到控件  {0}={1}'.format(By.CLASS_NAME, 'hw dhw')
        result -= 1  
  
if result == 0:
    text = element.text
    web_search_result += '{1}'.format(str=text)

if result == 0:
    try:
        element = dr.find_element(By.CLASS_NAME, 'di-body')
    except exceptions.NoSuchElementException:
        err_str += '\nFIND--没有找到控件  {0}={1}'.format(By.CLASS_NAME, 'di-body')
        result -= 1  

if result == 0:
    try:
        element = element.find_element(By.CLASS_NAME, 'posgram dpos-g hdib lmr-5')
    except exceptions.NoSuchElementException:
        err_str += '\nFIND--没有找到控件  {0}={1}'.format(By.CLASS_NAME, 'posgram dpos-g hdib lmr-5')
        result -= 1  

if result == 0:
    try:
        element = element.find_elements(By.CLASS_NAME, 'pos dpos')[0]
    except (exceptions.NoSuchElementException,IndexError):
        err_str += '\nFIND--没有找到控件  {0}={1},index={2}'.format(By.CLASS_NAME, 'pos dpos',0)
        result -= 1 
  
if result == 0:
    text = element.text
    web_search_result += '{1}'.format(str=text)

if result == 0:
    try:
        element = dr.find_element(By.CLASS_NAME, 'uk dpron-i')
    except exceptions.NoSuchElementException:
        err_str += '\nFIND--没有找到控件  {0}={1}'.format(By.CLASS_NAME, 'uk dpron-i')
        result -= 1  

if result == 0:
    try:
        element = element.find_element(By.CLASS_NAME, 'ipa dipa lpr-2 lpl-1')
    except exceptions.NoSuchElementException:
        err_str += '\nFIND--没有找到控件  {0}={1}'.format(By.CLASS_NAME, 'ipa dipa lpr-2 lpl-1')
        result -= 1  
  
if result == 0:
    text = element.text
    web_search_result += '{1}'.format(str=text)

if result == 0:
    try:
        element = dr.find_element(By.CLASS_NAME, 'us dpron-i ')
    except exceptions.NoSuchElementException:
        err_str += '\nFIND--没有找到控件  {0}={1}'.format(By.CLASS_NAME, 'us dpron-i ')
        result -= 1  

if result == 0:
    try:
        element = element.find_element(By.CLASS_NAME, 'ipa dipa lpr-2 lpl-1')
    except exceptions.NoSuchElementException:
        err_str += '\nFIND--没有找到控件  {0}={1}'.format(By.CLASS_NAME, 'ipa dipa lpr-2 lpl-1')
        result -= 1  
  
if result == 0:
    text = element.text
    web_search_result += '{1}'.format(str=text)

def func(element):
    global result
    global err_str

    if result == 0:
        try:
            element = element.find_element(By.CLASS_NAME, 'di-body')
        except exceptions.NoSuchElementException:
            err_str += '\nFIND--没有找到控件  {0}={1}'.format(By.CLASS_NAME, 'di-body')
            result -= 1  
    

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
    ----------------------     {0}
+++++++++++++++++++++++++++++++
'''.format(err_str)

print(result_str)

