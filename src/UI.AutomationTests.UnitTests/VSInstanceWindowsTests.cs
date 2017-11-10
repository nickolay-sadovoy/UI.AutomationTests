using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Windows.Automation;
using UI.AutomationTests.UnitTests.Core;

namespace UI.AutomationTests.UnitTests
{
    [TestClass]
    public class VSInstanceWindowsTests
    {
        static AutomationElement rootElement = AutomationElement.RootElement;
        static AutomationElement vsInstanse = rootElement.GetControlElement("App9 - Microsoft Visual Studio ", property: AutomationElement.NameProperty, treeScope: TreeScope.Children);

        [TestMethod]
        public void IsVsInstanseNotNul()
        {
            Assert.IsNotNull(vsInstanse);
        }

        [TestMethod]
        public void IsSolutionExplorerExist()
        {
            var solutionExplorerFloatWindow = vsInstanse.GetControlElement("Solution Explorer", AutomationElement.NameProperty, treeScope: TreeScope.Children);
            Assert.IsNotNull(solutionExplorerFloatWindow);

            var solutionExplorer = solutionExplorerFloatWindow.GetControlElement("Solution Explorer", AutomationElement.NameProperty, treeScope: TreeScope.Children);
            Assert.IsNotNull(solutionExplorer);

            var toolWindowTabGroup = solutionExplorer.GetControlElement("GenericPane", AutomationElement.ClassNameProperty, treeScope: TreeScope.Children);
            Assert.IsNotNull(toolWindowTabGroup);

            var treeView = toolWindowTabGroup.GetControlElement("TreeView", AutomationElement.ClassNameProperty, treeScope: TreeScope.Children);
            Assert.IsNotNull(treeView);

            var solutionTreeViewItem = treeView.GetControlElement("TreeViewItem", AutomationElement.ClassNameProperty, treeScope: TreeScope.Children);
            Assert.IsNotNull(solutionTreeViewItem);

            var App9_Android = solutionTreeViewItem.GetControlElement("App9.Android", AutomationElement.NameProperty, treeScope: TreeScope.Children);
            Assert.IsNotNull(App9_Android);

            var supportedPaterns = App9_Android.GetSupportedPatterns();
            //App9_Android.ChangeStateExpandCollapsePattern();
            App9_Android.SetFocus();
            var synchronizedInputPattern = App9_Android.GetCurrentPattern(SynchronizedInputPattern.Pattern) as SynchronizedInputPattern;
            synchronizedInputPattern.StartListening(SynchronizedInputType.MouseRightButtonUp);
            Thread.Sleep(3000);
            synchronizedInputPattern.Cancel();
        }
    }
}
