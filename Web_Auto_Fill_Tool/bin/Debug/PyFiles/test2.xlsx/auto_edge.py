
from selenium import webdriver
from selenium.webdriver.edge.service import Service
from selenium.webdriver.common.by import By
from selenium.webdriver.common.keys import Keys
from selenium.common import exceptions
from selenium.webdriver.support.ui import WebDriverWait
from time import sleep

s = Service('C:/Users/Administrator/Desktop/Web_Auto_Fill_Tool/Web_Auto_Fill_Tool/bin/Debug/Drivers/msedgedriver')
#打开浏览器
dr = webdriver.Edge(service=s)

result = 0
result_str = '++++++++++++运行结果++++++++++++'
err_str = ''


#执行操作

#登录网址
dr.get('https://email.163.com/')

sleep(1)

if result == 0:
    try:
        wait = WebDriverWait(dr, 2)
        wait.until(lambda x: dr.find_elements(By.TAG_NAME, 'iframe')[0])
    except(exceptions.TimeoutException,IndexError):
        err_str += '\nWAIT--没有找到控件  {0}={1},index={2}'.format(By.TAG_NAME, 'iframe',0)
        result -= 1

if result == 0:
    try:
        element = dr.find_elements(By.TAG_NAME, 'iframe')[0]
    except (exceptions.NoSuchElementException,IndexError):
        err_str += '\nFIND--没有找到控件  {0}={1},index={2}'.format(By.TAG_NAME, 'iframe',0)
        result -= 1 

if result == 0:
    dr.switch_to.frame(element)     

if result == 0:
    try:
        element = dr.find_element(By.NAME, 'email')
    except exceptions.NoSuchElementException:
        err_str += '\nFIND--没有找到控件  {0}={1}'.format(By.NAME, 'email')
        result -= 1  

if result == 0:
    element.send_keys('stephenhou2')

if result == 0:
    try:
        element = dr.find_element(By.NAME, 'password')
    except exceptions.NoSuchElementException:
        err_str += '\nFIND--没有找到控件  {0}={1}'.format(By.NAME, 'password')
        result -= 1  

if result == 0:
    element.send_keys('199123hlh')

if result == 0:
    try:
        element = dr.find_element(By.ID, 'dologin')
    except exceptions.NoSuchElementException:
        err_str += '\nFIND--没有找到控件  {0}={1}'.format(By.ID, 'dologin')
        result -= 1  

if result == 0:
    element.click()

sleep(1)

if result == 0:
    try:
        wait = WebDriverWait(dr, 2)
        wait.until(lambda x: dr.find_element(By.ID, '_mail_component_149_149'))
    except exceptions.TimeoutException:
        err_str += '\nWAIT--没有找到控件  {0}={1}'.format(By.ID, '_mail_component_149_149')
        result -= 1

if result == 0:
    try:
        element = dr.find_element(By.ID, '_mail_component_149_149')
    except exceptions.NoSuchElementException:
        err_str += '\nFIND--没有找到控件  {0}={1}'.format(By.ID, '_mail_component_149_149')
        result -= 1  

if result == 0:
    element.click()

sleep(1)

if result == 0:
    try:
        wait = WebDriverWait(dr, 2)
        wait.until(lambda x: dr.find_elements(By.CLASS_NAME, 'nui-editableAddr-ipt')[1])
    except(exceptions.TimeoutException,IndexError):
        err_str += '\nWAIT--没有找到控件  {0}={1},index={2}'.format(By.CLASS_NAME, 'nui-editableAddr-ipt',1)
        result -= 1

if result == 0:
    try:
        element = dr.find_elements(By.CLASS_NAME, 'nui-editableAddr-ipt')[1]
    except (exceptions.NoSuchElementException,IndexError):
        err_str += '\nFIND--没有找到控件  {0}={1},index={2}'.format(By.CLASS_NAME, 'nui-editableAddr-ipt',1)
        result -= 1 

if result == 0:
    try:
        wait = WebDriverWait(dr, 2)
        wait.until(lambda x: dr.find_elements(By.CLASS_NAME, 'APP-editor-iframe')[0])
    except(exceptions.TimeoutException,IndexError):
        err_str += '\nWAIT--没有找到控件  {0}={1},index={2}'.format(By.CLASS_NAME, 'APP-editor-iframe',0)
        result -= 1

if result == 0:
    try:
        element = dr.find_elements(By.CLASS_NAME, 'APP-editor-iframe')[0]
    except (exceptions.NoSuchElementException,IndexError):
        err_str += '\nFIND--没有找到控件  {0}={1},index={2}'.format(By.CLASS_NAME, 'APP-editor-iframe',0)
        result -= 1 

if result == 0:
    dr.switch_to.frame(element)     

if result == 0:
    try:
        element = dr.find_element(By.CLASS_NAME, 'nui-scroll')
    except exceptions.NoSuchElementException:
        err_str += '\nFIND--没有找到控件  {0}={1}'.format(By.CLASS_NAME, 'nui-scroll')
        result -= 1  

if result == 0:
    try:
        element = element.find_element(By.TAG_NAME, 'p')
    except exceptions.NoSuchElementException:
        err_str += '\nFIND--没有找到控件  {0}={1}'.format(By.TAG_NAME, 'p')
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

