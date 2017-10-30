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

            AutomationElement appElement = rootElement.GetControlElement(Properties.Resources.MainWindow_MainWindow_Id, treeScope:TreeScope.Children);
            Assert.IsNotNull(appElement);

            AutomationElement txtElementA = appElement.GetControlElement(Properties.Resources.MainWindow_TextBoxA_Id);
            Assert.IsNotNull(txtElementA);
            txtElementA.SetValueAsValuePattern("10");

            AutomationElement txtElementB = appElement.GetControlElement(Properties.Resources.MainWindow_TextBoxB_Id);
            Assert.IsNotNull(txtElementB);
            txtElementB.SetValueAsValuePattern("5");

            AutomationElement btnElementClick = appElement.GetControlElement(Properties.Resources.MainWindow_ButtonClick_Id);
            Assert.IsNotNull(btnElementClick);
            btnElementClick.InvokeAsInvokePattern();

            AutomationElement txtElementC = appElement.GetControlElement(Properties.Resources.MainWindow_TextBoxC_Id);
            Assert.IsNotNull(txtElementC);
            var result = txtElementC.GetValueAsValuePattern();
            Assert.AreEqual("15", result);
        }
    }
}
