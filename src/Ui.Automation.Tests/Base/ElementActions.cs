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
                Timeout = TimeSpan.FromSeconds(waitTime),
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


        /// <summary>
        /// Scrolls to the bottom of the page robustly (retries until height stabilizes).
        /// </summary>
        public void ScrollToEndOfPage(int maxRetries = 5, int waitMsBetween = 400)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                long previousHeight = -1;
                for (int attempt = 0; attempt < maxRetries; attempt++)
                {
                    // Try both documentElement and body for cross-browser compatibility
                    long bodyHeight = Convert.ToInt64(js.ExecuteScript("return document.body.scrollHeight;"));
                    long docHeight = Convert.ToInt64(js.ExecuteScript("return document.documentElement.scrollHeight;"));
                    long targetHeight = Math.Max(bodyHeight, docHeight);

                    // Scroll to bottom
                    js.ExecuteScript("window.scrollTo(0, Math.max(document.body.scrollHeight, document.documentElement.scrollHeight));");
                    logger.Info($"Scroll attempt {attempt + 1}/{maxRetries} - requested scroll to {targetHeight}");

                    System.Threading.Thread.Sleep(waitMsBetween); // short wait to allow lazy load

                    // Get new height after wait
                    long newBodyHeight = Convert.ToInt64(js.ExecuteScript("return document.body.scrollHeight;"));
                    long newDocHeight = Convert.ToInt64(js.ExecuteScript("return document.documentElement.scrollHeight;"));
                    long newHeight = Math.Max(newBodyHeight, newDocHeight);

                    logger.Info($"Heights (before -> after): {targetHeight} -> {newHeight}");

                    // if height didn't change (or decreased), consider stable
                    if (newHeight == previousHeight || newHeight == targetHeight)
                    {
                        logger.Info("Page height stabilized - assume reached end of page.");
                        return;
                    }

                    previousHeight = newHeight;
                }

                logger.Warn("ScrollToEndOfPage: finished retries; page may still be loading new content.");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Failed to scroll to the end of the page");
                throw;
            }
        }

    }
}