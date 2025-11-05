# Test Automation Framework

This is an enterprise-grade test automation framework built using **Selenium WebDriver**, **MSTest**, and **NLog** in **C#**. It supports both **UI** and **API** automation, parallel execution and logging

---

## Table of Contents
- [Project Structure](#project-structure)
- [Technologies Used](#technologies-used)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Setup](#setup)
- [Running Tests](#running-tests)
- [Logging](#logging)

---

## Project Structure
```
TestAutomationFramework/
├── src/
│   ├── UI.Automation.Tests/
│   │   ├── Pages/            # Page Object Models
│   │   ├── Tests/            # Test classes
│   │   ├── Utilities/        # Helper classes (e.g., ScreenshotUtils)
│   │   ├── Config/           # AppSettings.json for UI tests
│   │   ├── Base/             # Base classes, Action wrapper methods, WebDriverFactory
│   │   └── Data/             # Test data for UI tests
│   └── API.Automation.Tests/
│       ├── Services/         # API Service definitions (Service Object Model)
│       ├── Tests/            # API test classes
│       ├── Config/           # AppSettings.json for API tests
│       ├── Base/             # Base service, RestClient factory
│       ├── Data/             # API request and schema files
│       ├── Models/           # API request and response DTOs
│       └── Utilities/        # ConfigReader and JsonSchemaValidator
├── NLog.config               # Logging configuration
├── TestAutomationFramework.csproj
└── README.md
```

## Technologies Used
- **C#** (.NET 9.0)
- **Selenium WebDriver** for UI automation
- **MSTest** for test execution and test categorizations
- **NLog** for logging
- **RestSharp** for API automation
- **Newtonsoft.Json** for JSON handling


## Features
- Page Object Model (POM) design pattern for UI tests which significantly enhances the scalability of test automation frameworks
- Service Object Model (SOM) design pattern for API tests to support enterprise level microservices api's
- Base API service for building HTTP requests (RestSharp)
- Parallel test execution 
- Thread-safe actions during parallel execution
- Retry mechanism for UI interactions
- Handling of browser specific drivers using WebDriverManger
- Logging to **console** and **log files** using NLog
- Configurable using `AppSettings.json`
- JSON schema validation for API schema validation
- API request and response DTO's to handle complex json responses by deserializing the API response to DTO objects
- UI and API components are in separate package which makes the framework scalable and we can add another package in future for mobile testing using appium and keep shared code in common package.


## Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio Code](https://code.visualstudio.com/) or Visual Studio 2022
- Internet access for downloading NuGet packages
- No need to installed browser specific drivers as it is automatically handled through WebDriverManager


## Setup on mac
1. Clone the repository:
```bash
git clone https://github.com/arvindjosh08/Web-API-Automation-Framework.git
```
2. Restore NuGet packages:
```bash
dotnet restore
```
3. Build the project
```bash
dotnet build
```

## Running Tests on mac
Follow these steps to execute the tests on your Mac machine:
1. Run all tests (both UI and API) with console logging. By default the UI test will run on chrome browser
```bash
dotnet test --logger "console;verbosity=detailed"
```
2. Run only UI test with console logging
```bash
dotnet test --logger "console;verbosity=detailed" --filter "TestCategory=api"
```
3. Run only UI test in edge browser with console logging.
```bash
dotnet test --filter "TestCategory=ui" --logger "console;verbosity=detailed" -- "TestRunParameters.Parameter(name=\"Browser\", value=\"edge\")"
```
4. Run only UI test in chrome browser with console logging.
```bash
dotnet test --filter "TestCategory=ui" --logger "console;verbosity=detailed" -- "TestRunParameters.Parameter(name=\"Browser\", value=\"chrome\")"
```
5. Run only API test with console logging.
```bash
dotnet test --filter "TestCategory=api" --logger "console;verbosity=detailed"
```
6. Run only UI test without console logging.
```bash
dotnet test --filter "TestCategory=ui"
```
7. Run only API test without console logging.
```bash
dotnet test --filter "TestCategory=api"
```

## Parallel Execution
To run test in parallel. Go to MSTestSettings.cs file available in the root of the project. Uncomment the assemble attribute and then run one of the commands in above section

## Logging
- Logging is handled by NLog.
- Logs are written to: bin/Debug/net9.0/Logs
- File: Logs/test-log-{date}.log
- Configure log levels and targets in NLog.config.
- Thread ID is logged for parallel execution.