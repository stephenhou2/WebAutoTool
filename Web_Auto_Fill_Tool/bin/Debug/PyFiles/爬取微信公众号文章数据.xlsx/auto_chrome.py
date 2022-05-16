
from selenium import webdriver
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.common.by import By
from selenium.webdriver.common.keys import Keys
from selenium.common import exceptions
from selenium.webdriver.support.ui import WebDriverWait
from time import sleep
from selenium.webdriver.support import expected_conditions as EC

s = Service('C:/Users/Administrator/Desktop/Web_Auto_Fill_Tool/Web_Auto_Fill_Tool/bin/Debug/Drivers/chromedriver')
#打开浏览器
dr = webdriver.Chrome(service=s)
web_search_result = ''

result = 0
result_str = '++++++++++++运行结果++++++++++++'
err_str = ''


#执行操作

#登录网址
dr.get('https://mp.weixin.qq.com')

sleep(1)

if result == 0:
    try:
        wait = WebDriverWait(dr, 10)
        wait.until(lambda x: dr.find_element(By.XPATH, '//*[@id="list_container"]/div[1]/div[2]/div/button'))
    except exceptions.TimeoutException:
        err_str += '\nWAIT--没有找到控件  {0}={1}'.format(By.XPATH, '//*[@id="list_container"]/div[1]/div[2]/div/button')
        result -= 1

if result == 0:
    try:
        element = dr.find_element(By.XPATH, '//*[@id="list_container"]/div[1]/div[2]/div/button')
    except exceptions.NoSuchElementException:
        err_str += '\nFIND--没有找到控件  {0}={1}'.format(By.XPATH, '//*[@id="list_container"]/div[1]/div[2]/div/button')
        result -= 1  

if result == 0:
    element.click()

if result == 0:
    try:
        wait = WebDriverWait(dr, 5)
        all_windows = dr.window_handles
        wait.until(lambda _: len(all_windows)-1 == 1)
    except(exceptions.TimeoutException,IndexError):
        err_str += '\nWAIT--没有等到窗口 index=1'
        result -= 1

if result == 0:
    all_windows = dr.window_handles
    dr.switch_to.window(all_windows[1])

for page in range(2,22):
    if result == 0:
        try:
            wait = WebDriverWait(dr, 5)
            wait.until(lambda x: dr.find_element(By.XPATH, '//*[@id="app"]/div/div[3]/span[2]/input'))
        except exceptions.TimeoutException:
            err_str += '\nWAIT--没有找到控件  {0}={1}'.format(By.XPATH, '//*[@id="app"]/div/div[3]/span[2]/input')
            result -= 1

        if result == 0:
            try:
                element = dr.find_element(By.XPATH, '//*[@id="app"]/div/div[3]/span[2]/input')
            except exceptions.NoSuchElementException:
                err_str += '\nFIND--没有找到控件  {0}={1}'.format(By.XPATH, '//*[@id="app"]/div/div[3]/span[2]/input')
                result -= 1  
        

        if result == 0:
            element.click()
            element.send_keys(Keys.BACKSPACE)
            element.send_keys(Keys.BACKSPACE)
            element.send_keys(Keys.BACKSPACE)

        if result == 0:
            element.send_keys(str(page))

        if result == 0:
            try:
                element = dr.find_element(By.XPATH, '//*[@id="app"]/div/div[3]/span[2]/a')
            except exceptions.NoSuchElementException:
                err_str += '\nFIND--没有找到控件  {0}={1}'.format(By.XPATH, '//*[@id="app"]/div/div[3]/span[2]/a')
                result -= 1  
        
        if result == 0:
            element.click()
            
        sleep(2)
        
        if result == 0:
            try:
                elements = dr.find_elements(By.CLASS_NAME, 'weui-desktop-mass-appmsg__title')
            except (exceptions.NoSuchElementException,IndexError):
                err_str += '\nFINDs--没有找到控件--没有找到控件  {0}={1}'.format(By.CLASS_NAME, 'weui-desktop-mass-appmsg__title')
                result -= 1    
                
            if result == 0:
                for element in elements:
                    attr = element.get_attribute('href')
                    web_search_result += '{attr}\r\n'.format(attr=attr)


if result == 0:
    file = open('./全部链接.txt', 'w+')
    file.write(web_search_result)
    file.close()
    
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

