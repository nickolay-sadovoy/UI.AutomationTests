using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Windows;
using System.Windows.Automation;
using UI.AutomationTests.UnitTests.Core;

namespace UI.AutomationTests.UnitTests
{
    [TestClass]
    public class VSInstanceWindowsTests
    {
        static AutomationElement rootElement = AutomationElement.RootElement;
        static AutomationElement vsInstanse = rootElement.GetControlElement("App9 - Microsoft Visual Studio ", property: AutomationElement.NameProperty, treeScope: TreeScope.Children);
        static string projectName = "App9.iOS";
        static string contextMenuCommand = "Build";

        [TestMethod]
        public void IsVsInstanseNotNul()
        {
            Assert.IsNotNull(vsInstanse);
        }

        [TestMethod]
        public void IsSolutionExplorerExist()
        {

            var App9_Android = vsInstanse.GetControlElement(projectName, AutomationElement.NameProperty, treeScope: TreeScope.Descendants);
            Assert.IsNotNull(App9_Android);

            var supportedPaterns = App9_Android.GetSupportedPatterns();
            App9_Android.SetFocus();
            var rect = App9_Android.Current.BoundingRectangle;

            if (rect != null)
            {
                var clickPoint = new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
                ContextMenuHelper.SelectContextMenuItem(contextMenuCommand, clickPoint);
            }
        }
    }
}
