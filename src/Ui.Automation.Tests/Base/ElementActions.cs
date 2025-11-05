using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NLog;


namespace Ui.Automation.Tests.Base
{
    public class ElementActions
    {
        private IWebDriver driver;
        private readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(10);
        private const int MaxRetryCount = 3;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ElementActions()
        {
            this.driver = WebDriverFactory.GetDriver(); // Thread-safe driver from DriverFactory

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
                Timeout = TimeSpan.FromSeconds(timeoutInSeconds),
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            return wait.Until(_ =>
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
        }

        private IWebElement WaitForElementClickable(IWebElement element, int timeoutInSeconds = -1)
        {
            var wait = new DefaultWait<IWebDriver>(driver)
            {
                Timeout = TimeSpan.FromSeconds(timeoutInSeconds),
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

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

            while (attempts < MaxRetryCount)
            {
                try
                {
                    action.Invoke();
                    return; // success
                }
                catch (StaleElementReferenceException ex)
                {
                    attempts++;
                    logger.Warn($"StaleElementReferenceException on attempt {attempts}: {ex.Message}");
                }
                catch (ElementClickInterceptedException ex)
                {
                    attempts++;
                    logger.Warn($"ElementClickInterceptedException on attempt {attempts}: {ex.Message}");
                }
                catch (WebDriverException ex)
                {
                    attempts++;
                    logger.Warn($"WebDriverException on attempt {attempts}: {ex.Message}");
                }
            }

            throw new Exception($"{errorMessage} after {MaxRetryCount} attempts.");
        }

    }
}