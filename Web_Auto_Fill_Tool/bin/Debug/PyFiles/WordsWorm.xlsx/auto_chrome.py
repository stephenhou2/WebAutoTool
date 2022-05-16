from selenium import webdriver
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.common.by import By
from selenium.webdriver.common.keys import Keys
from selenium.common import exceptions
from selenium.webdriver.support.ui import WebDriverWait
from time import sleep
from selenium.webdriver.support import expected_conditions as EC

s = Service('C:/Users/Administrator/Desktop/WebAutoTool/Web_Auto_Fill_Tool/bin/Debug/Drivers/chromedriver')
options = webdriver.ChromeOptions.add_argument(self=,argument="headless")

# 打开浏览器
dr = webdriver.Chrome(service=s, chrome_options=options)
web_search_result = ''

result = 0
result_str = '++++++++++++运行结果++++++++++++'
err_str = ''

targets = [
    # "a.m.",
    # "abandon",
    # "abbreviate",
    # "abbreviation",
    # "abdomen",
    # "ability",
    # "able",
    # "abnormal",
    # "abnormality",
    # "aboard",
    "about"
]


def insert_empty_word_info():
    global web_search_result
    # 插入空的单词数据行
    web_search_result = web_search_result + word + ",,,,,"


# 执行操作
for word in targets:
    url = "https://dictionary.cambridge.org/dictionary/english-chinese-simplified/" + word
    # 登录网址
    dr.get(url)

    # 等待网页数据
    try:
        wait = WebDriverWait(dr, 10)
        wait.until(lambda x: dr.find_element(By.CLASS_NAME, 'di-body'))
    except exceptions.TimeoutException:
        # 插入单词本身的空行数据，接着抓下一个单词数据
        insert_empty_word_info()
        continue

    # root node
    root = dr.find_element(By.CLASS_NAME, 'di-body')

    # 单词拼写 不用抓

    # 英式发音，只抓一次
    ProTagUK = ""
    # 美式发音，只抓一次
    ProTagUS = ""

    # 抓英式发音
    try:
        element = root.find_element(By.CLASS_NAME, 'uk dpron-i ')
        element = element.find_element(By.CLASS_NAME, 'ipa dipa lpr-2 lpl-1')
        ProTagUK += element.text
    except exceptions.NoSuchElementException:
        pass

    # 抓美式发音
    try:
        element = root.find_element(By.CLASS_NAME, 'us dpron-i ')
        element = element.find_element(By.CLASS_NAME, 'ipa dipa lpr-2 lpl-1')
        ProTagUK += element.text
    except exceptions.NoSuchElementException:
        pass

    # 抓词条
    word_items = root.find_elements(By.CLASS_NAME, 'pos-body')

    # 词性
    Type = ""
    # 近义
    GuideWord = ""
    # 等级
    WordRate = "D"
    # 英文释义
    EnExplain = ""
    # 中文释义
    CnExplain = ""

    # 遍历词条，写入单词数据
    for item in word_items:
        try:
            Type = item.find_element(By.CLASS_NAME, 'pos dsense_pos').text
            element = item.find_element(By.CLASS_NAME, 'guideword dsense_gw')
            GuideWord = element.find_element(By.TAG_NAME, 'span').text
            element = item.find_element(By.CLASS_NAME, 'def-info ddef-info')
            WordRate = element.find_element(By.TAG_NAME, 'span').text
            element = item.find_element(By.CLASS_NAME, 'def ddef_d db')

            # subs = element.find_elements(By.)
            print(element.text)
        except exceptions.NoSuchElementException:
            pass


# if result == 0:
#     result_str += '''
#     ----------------------
#         √ 验证成功
#     ----------------------
# +++++++++++++++++++++++++++++++
# '''
# else:
#     result_str += '''
#     ----------------------
#         x 验证失败
#     ----------------------     {0}
# +++++++++++++++++++++++++++++++
# '''.format(err_str)

# print(result_str)
