using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Automation;
using UI.AutomationTests.UnitTests.Core;

namespace UI.AutomationTests.UnitTests
{
    [TestClass]
    public class MainVindowUITests
    {
        [TestMethod]
        public void MainWindow_TextBoxes_Check_Changing_Text()
        {
            AutomationElement rootElement = AutomationElement.RootElement;
            Assert.IsNotNull(rootElement);

            Condition condition = new PropertyCondition(AutomationElement.NameProperty, "UI Automation Test Window");

            AutomationElement appElement = rootElement.FindFirst(TreeScope.Children, condition);
            Assert.IsNotNull(appElement);

            AutomationElement txtElementA = appElement.GetControlElement("txtA");
            Assert.IsNotNull(txtElementA);
            txtElementA.SetValueAsValuePattern("10");

            AutomationElement txtElementB = appElement.GetControlElement("txtB");
            Assert.IsNotNull(txtElementB);
            txtElementB.SetValueAsValuePattern("5");

            AutomationElement btnElementClick = appElement.GetControlElement("btnClick");
            Assert.IsNotNull(btnElementClick);
            btnElementClick.InvokeAsInvokePattern();

            AutomationElement txtElementC = appElement.GetControlElement("txtC");
            Assert.IsNotNull(txtElementC);
            var result = txtElementC.GetValueAsValuePattern();
            Assert.AreEqual("15", result);
        }

        

        
    }
}
