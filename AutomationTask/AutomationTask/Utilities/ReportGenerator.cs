using System;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;

[Binding]
public sealed class ReportGenerator
{
    private static ExtentTest featureName;
    private static ExtentTest scenario;
    private static ExtentReports extent;

    [BeforeTestRun]
    public static void BeforeTestRun()
    {

        string path1 = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
        string path = path1 + "Report\\index.html";
        ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(path);
        htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
        extent = new ExtentReports();
        extent.AttachReporter(htmlReporter);
    }

    [BeforeFeature]
    public static void BeforeFeature()
    {
        featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        Console.WriteLine("BeforeFeature");
    }
    [BeforeScenario]
    public void BeforeScenario()
    {
        Console.WriteLine("BeforeScenario");
        scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
    }
    [AfterStep]
    public void InsertReportingSteps()
    {
        var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
        if (ScenarioContext.Current.TestError == null)
        {
            if (stepType == "Given")
                scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
            else if (stepType == "When")
                scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
            else if (stepType == "Then")
                scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
            else if (stepType == "And")
                scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
        }
        else if (ScenarioContext.Current.TestError != null)
        {
            if (stepType == "Given")
            {
                scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            }
            else if (stepType == "When")
            {
                scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            }
            else if (stepType == "Then")
            {
                scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            }
            else if (stepType == "And")
            {
                scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            }
        }
    }
    [AfterScenario]
    public void AfterScenario()
    {
        Console.WriteLine("AfterScenario");
    }
    [AfterTestRun]
    public static void AfterTestRun()
    {
        extent.Flush();
    }
}
