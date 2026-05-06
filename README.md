# 🎮 Game Testing Automation Framework (C#)

A computer vision–based automation framework for testing video games and graphical applications.

This project demonstrates how to automate user flows in non-DOM environments (such as games) using image recognition and simulated input.

## 🚀 Features

- 🧠 Computer Vision (OpenCV)
- 🖱️ Input Automation (Mouse simulation)
- 🧪 Test Framework (NUnit)
- ⏱️ Smart waits for non-deterministic systems
- 📸 Screenshot-based validation
- 📊 Test reporting (logs + screenshots)
- 🔄 CI-ready

## 🏗️ Architecture

- **Core** → Orchestration logic
- **Vision** → Detection via template matching
- **Input** → Mouse automation
- **Tests** → Test cases (NUnit)
- **Reporting** → Logs and execution artifacts


## 🧩 Tech Stack

- C# (.NET 8)
- OpenCvSharp
- NUnit
- WinAPI (mouse input)

## 📦 Setup

1. Clone repo:

```bash
git clone https://github.com/your-username/GameQAAutomation.git
cd GameQAAutomation
```

2. Restore dependencies:

```bash
dotnet restore
```

3. Run tests:

```bash
dotnet test
```

## 🧪 Example Test

```C#
[Test]
public void Should_Open_Settings_Menu()
{
    var main = new MainMenu();
    var settings = new SettingsMenu();

    main.OpenSettings();

    Assert.IsTrue(settings.IsVisible());
}
```

## 🧠 How It Works

1. Captures screen
2. Detects UI elements via template matching
3. Simulates mouse interaction
4. Validates expected visual state
5. Generates logs and screenshots

## 📊 Output

Each test generates:

- Logs
- Before/after screenshots
- Pass/Fail result

## 🔄 CI Integration

Example GitHub Actions:

```YAML
- name: Run UI Tests
  run: dotnet test
```

## ⚠️ Disclaimer

This project is intended for QA automation and testing environments only.
It does not interact with game memory or bypass protections.

## 📈 Why This Matters

Modern QA roles increasingly require testing beyond web apps.
This project demonstrates:

- Automation of non-DOM systems
- Visual validation strategies
- Handling non-deterministic environments

## 👨‍💻 Author

Kevin Rivera
