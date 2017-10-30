using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Windows.Automation;
using UI.AutomationTests.UnitTests.Core;

namespace UI.AutomationTests.UnitTests
{
    [TestClass]
    public class XamarinFormsWindowTest
    {
        string dialogFormat = "New Cross Platform App - {0}";
        string newAppName = "App1";
        [TestMethod]
        public void New_Cross_Platform_App_Dialog_Configure()
        {
            AutomationElement rootElement = AutomationElement.RootElement;
            Assert.IsNotNull(rootElement);

            #region Get VS Window
            
            var VSName = "Start Page - Microsoft Visual Studio ";
            AutomationElement vsInstanse = rootElement.GetControlElement(VSName, property: AutomationElement.NameProperty, treeScope: TreeScope.Children);
            Assert.IsNotNull(vsInstanse);

            #endregion
            
            #region Get Create XF dialog
            
            var dialogTitle = string.Format(dialogFormat, newAppName);
            
            AutomationElement createDialogElement = vsInstanse.GetControlElement(dialogTitle, property: AutomationElement.NameProperty, treeScope: TreeScope.Children);
            Assert.IsNotNull(createDialogElement);
            
            #endregion
            
            #region Select Blank App template
            
            AutomationElement listViewTemplates = createDialogElement.GetControlElement("lvTemplates");
            Assert.IsNotNull(listViewTemplates);
            
            Condition findCondition = new PropertyCondition(AutomationElement.IsSelectionItemPatternAvailableProperty, true);
            AutomationElementCollection found = listViewTemplates.FindAll(TreeScope.Children, findCondition);
            //blank app - 0; master detail - 1
            found[0].SelectAsSelectionItemPattern();
            
            #endregion
            
            #region Select Native UI Technology
            
            AutomationElement xamarinFormsRadioButton = createDialogElement.GetControlElement("Native", property: AutomationElement.NameProperty);
            Assert.IsNotNull(xamarinFormsRadioButton);
            xamarinFormsRadioButton.SelectAsSelectionItemPattern();
            
            #endregion
            
            #region Select Shared Project Strategy
            
            AutomationElement sharedProjectRadioButton = createDialogElement.GetControlElement("Shared Project", property: AutomationElement.NameProperty);
            Assert.IsNotNull(sharedProjectRadioButton);
            sharedProjectRadioButton.SelectAsSelectionItemPattern();
            
            #endregion
            
            #region Ok click
            
            AutomationElement btnOKElement = createDialogElement.GetControlElement("OK", property: AutomationElement.NameProperty);
            Assert.IsNotNull(btnOKElement);
            btnOKElement.InvokeAsInvokePattern();

            #endregion

          
            #region New Universal Windows Project

            #region Get VS Window

            VSName = "App1 - Microsoft Visual Studio ";
            vsInstanse = rootElement.GetControlElement(VSName, property: AutomationElement.NameProperty, treeScope: TreeScope.Children);
            Assert.IsNotNull(vsInstanse);

            #endregion

            #region errors
            Pause();
            //errors ?? we dont want get them. but in my case all works ok with this 2 errors
            AutomationElement microsoftVSDialog = vsInstanse.GetControlElement("Microsoft Visual Studio", property: AutomationElement.NameProperty, treeScope: TreeScope.Children);
            while (microsoftVSDialog != null)
            {
                var okButton = microsoftVSDialog.GetControlElement("OK", AutomationElement.NameProperty);
                okButton?.InvokeAsInvokePattern();
                Pause();
                microsoftVSDialog = vsInstanse.GetControlElement("Microsoft Visual Studio", property: AutomationElement.NameProperty, treeScope: TreeScope.Children);
            }

            #endregion

            Pause();
            var newUWPDialogTitle = "New Universal Windows Project";

            //if we want UWP have to chose
            //but now we don`t
            AutomationElement newUWPDialog = vsInstanse.GetControlElement(newUWPDialogTitle, property: AutomationElement.NameProperty, treeScope: TreeScope.Children);
            Assert.IsNotNull(newUWPDialog);

            AutomationElement flowLayoutPanel1 = newUWPDialog.GetControlElement("flowLayoutPanel1");
            Assert.IsNotNull(flowLayoutPanel1);

            AutomationElement tableLayoutPanel2 = newUWPDialog.GetControlElement("tableLayoutPanel2");
            Assert.IsNotNull(tableLayoutPanel2);

            AutomationElement cancelButton = tableLayoutPanel2.GetControlElement("Cancel", property: AutomationElement.NameProperty);
            Assert.IsNotNull(cancelButton);
            cancelButton.InvokeAsInvokePattern();
            #endregion
        }

        // in my case 2 sec is enough
        private void Pause(int delay = 2000)
        {
            var delayTask = Task.Delay(delay);
            Task.WaitAll(delayTask);
        }
    }
}
