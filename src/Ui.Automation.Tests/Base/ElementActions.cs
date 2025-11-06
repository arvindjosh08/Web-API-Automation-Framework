using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NLog;


namespace Ui.Automation.Tests.Base
{
    public class ElementActions
    {
        private IWebDriver driver;
        private readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(20);
        private const int MaxRetryCount = 3;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ElementActions(IWebDriver driver )
        {
            this.driver = driver;
        }
        public void Click(IWebElement element, string elementName = "", int timeoutInSeconds = -1)
        {
            ExecuteWithRetry(() =>
            {
                WaitForElementClickable(element, timeoutInSeconds);
                element.Click();
                logger.Info($"Clicked on element: {elementName}");
            }, $"Click failed on {elementName}");
        }

        public void SendKeys(IWebElement element, string text, string elementName = "", int timeoutInSeconds = -1)
        {
            ExecuteWithRetry(() =>
            {
                WaitForElementVisible(element, timeoutInSeconds);
                element.Clear();
                element.SendKeys(text);
                logger.Info($"Entered text '{text}' into element: {elementName}");
            }, $"SendKeys failed on {elementName}");
        }

        public string GetText(IWebElement element, string elementName = "", int timeoutInSeconds = -1)
        {
            string text = string.Empty;

            ExecuteWithRetry(() =>
            {
                WaitForElementVisible(element, timeoutInSeconds);
                text = element.Text?.Trim();
                logger.Info($"Fetched text from {elementName}: {text}");
            }, $"GetText failed on {elementName}");

            return text;
        }

        private IWebElement WaitForElementVisible(IWebElement element, int timeoutInSeconds = -1)
        {
            var waitTime = timeoutInSeconds > 0 ? timeoutInSeconds : (int)DefaultTimeout.TotalSeconds;

            var wait = new DefaultWait<IWebDriver>(driver)
            {
                Timeout = TimeSpan.FromSeconds(waitTime),
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));

            var elem = wait.Until(_ =>
            {
                try
                {
                    return element.Displayed ? element : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;

                }
            });
            if (elem == null)
                throw new NoSuchElementException("Element not visible after wait");

            return elem;
        }

        private IWebElement WaitForElementClickable(IWebElement element, int timeoutInSeconds = -1)
        {
            var wait = new DefaultWait<IWebDriver>(driver)
            {
                Timeout = TimeSpan.FromSeconds(timeoutInSeconds),
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));

            return wait.Until(_ =>
            {
                try
                {
                    return (element.Displayed && element.Enabled) ? element : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });
        }

        private void ExecuteWithRetry(Action action, string errorMessage)
        {
            int attempts = 0;
            Exception lastException = null;

            while (attempts < MaxRetryCount)
            {
                try
                {
                    action.Invoke();
                    return; // success
                }
                catch (NoSuchElementException ex)
                {
                    attempts++;
                    logger.Warn($"NoSuchElementException on attempt {attempts}: {ex.Message}");
                    lastException = ex;
                }
                catch (StaleElementReferenceException ex)
                {
                    attempts++;
                    logger.Warn($"StaleElementReferenceException on attempt {attempts}: {ex.Message}");
                    lastException = ex;
                }
                catch (ElementClickInterceptedException ex)
                {
                    attempts++;
                    logger.Warn($"ElementClickInterceptedException on attempt {attempts}: {ex.Message}");
                    lastException = ex;
                }
                catch (WebDriverException ex)
                {
                    attempts++;
                    logger.Warn($"WebDriverException on attempt {attempts}: {ex.Message}");
                    lastException = ex;
                }
            }
            logger.Error(lastException, $"{errorMessage} after {MaxRetryCount} attempts.");
            throw lastException;
        }

        public string GetText(By locator, string elementName = "", int timeoutInSeconds = -1)
        {
            string text = string.Empty;

            ExecuteWithRetry(() =>
            {
                var element = WaitForElement(locator, timeoutInSeconds);
                text = element.Text.Trim();
                logger.Info($"Fetched text from {elementName}: {text}");
            }, $"GetText failed on {elementName}");

            return text;
        }

        public void SendKeys(By locator, string text, string elementName = "", int timeoutInSeconds = -1)
        {
            ExecuteWithRetry(() =>
            {
                var element = WaitForElement(locator, timeoutInSeconds);
                element.Clear();
                element.SendKeys(text);
                logger.Info($"Entered text '{text}' into element: {elementName}");
            }, $"SendKeys failed on {elementName}");
        }

        private IWebElement WaitForElement(By locator, int timeoutInSeconds = -1)
        {
            int waitTime = timeoutInSeconds > 0 ? timeoutInSeconds : (int)DefaultTimeout.TotalSeconds;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime));

            return wait.Until(drv =>
            {
                try
                {
                    var el = drv.FindElement(locator);
                    return el.Displayed ? el : null; // only return if visible
                }
                catch (NoSuchElementException)
                {
                    return null; // keep waiting
                }
                catch (StaleElementReferenceException)
                {
                    return null; // retry if element is stale
                }
            });
        }

    }
}