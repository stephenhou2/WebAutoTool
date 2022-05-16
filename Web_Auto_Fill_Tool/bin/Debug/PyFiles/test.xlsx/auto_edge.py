
from selenium import webdriver
from selenium.webdriver.edge.service import Service
from selenium.webdriver.common.by import By
from selenium.webdriver.common.keys import Keys
from selenium.common import exceptions
from selenium.webdriver.support.ui import WebDriverWait

s = Service('C:/Users/Administrator/Desktop/Web_Auto_Fill_Tool/Web_Auto_Fill_Tool/bin/Debug/Drivers/msedgedriver')
#打开浏览器
dr = webdriver.Edge(service=s)

result = 0
result_str = '++++++++++++运行结果++++++++++++'
err_str = ''


#执行操作

#登录网址
dr.get('https://passport.wanmei.com/reg/')

if result == 0:
    try:
        element = dr.find_element(By.ID, 'public_hg_btn')
        element.click()
    except exceptions.TimeoutException:
        err_str += '\n没有找到控件  {0}={1}'.format(By.ID, 'public_hg_btn')
        result -= 1

if result == 0:
    try:
        element = dr.find_element(By.NAME, 'username')
        element.send_keys('18019019585')
    except exceptions.TimeoutException:
        err_str += '\n没有找到控件  {0}={1}'.format(By.NAME, 'username')
        result -= 1

if result == 0:
    try:
        element = dr.find_element(By.XPATH, '//*[@id="tabCh"]/div[2]/form/div[2]/label')
        element.click()
    except exceptions.TimeoutException:
        err_str += '\n没有找到控件  {0}={1}'.format(By.XPATH, '//*[@id="tabCh"]/div[2]/form/div[2]/label')
        result -= 1

if result == 0:
    try:
        element = dr.find_element(By.XPATH, '//*[@id="tabCh"]/div[2]/form/div[2]/input')
        element.send_keys('123456')
    except exceptions.TimeoutException:
        err_str += '\n没有找到控件  {0}={1}'.format(By.XPATH, '//*[@id="tabCh"]/div[2]/form/div[2]/input')
        result -= 1

if result == 0:
    try:
        element = dr.find_element(By.XPATH, '//*[@id="tabCh"]/div[2]/form/div[4]/label')
        element.click()
    except exceptions.TimeoutException:
        err_str += '\n没有找到控件  {0}={1}'.format(By.XPATH, '//*[@id="tabCh"]/div[2]/form/div[4]/label')
        result -= 1

if result == 0:
    try:
        element = dr.find_element(By.XPATH, '//*[@id="tabCh"]/div[2]/form/div[4]/input')
        element.send_keys('123456')
    except exceptions.TimeoutException:
        err_str += '\n没有找到控件  {0}={1}'.format(By.XPATH, '//*[@id="tabCh"]/div[2]/form/div[4]/input')
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
    ----------------------     edge
+++++++++++++++++++++++++++++++
'''.format(err_str)

print(result_str)

