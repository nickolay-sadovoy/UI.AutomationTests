using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Automation;
using System.Diagnostics;

namespace UI.AutomationTests.UnitTests
{
    [TestClass]
    public class MainVindowUITests
    {
        [TestMethod]
        public void MainWindow_TextBoxes_Check_Changing_Text()
        {
            LogMessage("Getting RootElement...");
            AutomationElement rootElement = AutomationElement.RootElement;
            if (rootElement != null)
            {
                LogMessage("OK." + Environment.NewLine);

                Condition condition = new PropertyCondition(AutomationElement.NameProperty, "UI Automation Test Window");

                LogMessage("Searching for Test Window...");
                AutomationElement appElement = rootElement.FindFirst(TreeScope.Children, condition);

                if (appElement != null)
                {
                    LogMessage("OK " + Environment.NewLine);
                    LogMessage("Searching for TextBox A control...");
                    AutomationElement txtElementA = GetControlElement(appElement, "txtA");
                    if (txtElementA != null)
                    {
                        LogMessage("OK " + Environment.NewLine);
                        LogMessage("Setting TextBox A value...");
                        try
                        {
                            ValuePattern valuePatternA = txtElementA.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                            valuePatternA.SetValue("10");
                            LogMessage("OK " + Environment.NewLine);
                        }
                        catch
                        {
                            WriteLogError();
                        }
                    }
                    else
                    {
                        WriteLogError();
                    }

                    LogMessage("Searching for TextBox B control...");
                    AutomationElement txtElementB = GetControlElement(appElement, "txtB");
                    if (txtElementB != null)
                    {
                        LogMessage("OK " + Environment.NewLine);
                        LogMessage("Setting TextBox B value...");
                        try
                        {
                            ValuePattern valuePatternB = txtElementB.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                            valuePatternB.SetValue("5");
                            LogMessage("OK " + Environment.NewLine);
                        }
                        catch
                        {
                            WriteLogError();
                        }
                    }
                    else
                    {
                        WriteLogError();
                    }

                    LogMessage("Searching for Button Click control...");
                    AutomationElement btnElementClick = GetControlElement(appElement, "btnClick");
                    if (btnElementClick != null)
                    {
                        //TODO: Click button
                    }
                    else
                    {
                        WriteLogError();
                    }

                    //Check TextBox C text == 5
                }
                else
                {
                    WriteLogError();
                }
            }
        }

        private AutomationElement GetControlElement(AutomationElement parentElement, string value)
        {
            Condition condition = new PropertyCondition(AutomationElement.AutomationIdProperty, value);
            AutomationElement txtElement = parentElement.FindFirst(TreeScope.Descendants, condition);
            return txtElement;
        }

        private void LogMessage(string message)
        {
            Trace.WriteLine(message);
        }

        private void WriteLogError()
        {
            LogMessage("ERROR." + Environment.NewLine);
        }
    }
}
