import allure
import pytest
from pages.welcome import WelcomePage
from appium.webdriver.common.appiumby import AppiumBy

@allure.description('Test Welcome Page')
class TestWelcomePage:
        
    @allure.title('Test that Welcome Page is displayed on startup')
    def test_welcome_page_is_displayed(self):
        self.driver.switch_to.context('UNITY')
        self.driver.find_element(AppiumBy.XPATH, '//PlayButton').click()
    
