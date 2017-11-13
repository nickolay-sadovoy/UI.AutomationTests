using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Windows;
using System.Windows.Automation;
using UI.AutomationTests.UnitTests.Core;

namespace UI.AutomationTests.UnitTests
{
    public static class ContextMenuHelper
    {
        public static void TryToOpenContextMenu(Point clickPoint)
        {
            clickPoint.X += 25;
            clickPoint.Y += 5;
            Mouse.RightClick(clickPoint);
        }

        public static void SelectContextMenuItem( string itemName, Point? clickPoint = null)
        {
            if(clickPoint != null && clickPoint.HasValue)
                TryToOpenContextMenu(clickPoint.Value);

            //wait popup necessary
            Thread.Sleep(300);

            var popup = AutomationElement.RootElement.GetControlElement("Popup", AutomationElement.ClassNameProperty, treeScope: TreeScope.Children);
            Assert.IsNotNull(popup);

            var contexMenu = popup.GetControlElement("ContextMenu", AutomationElement.ClassNameProperty, treeScope: TreeScope.Children);
            Assert.IsNotNull(contexMenu);

            var selectItem = contexMenu.GetControlElement(itemName, AutomationElement.NameProperty, treeScope: TreeScope.Children);
            Assert.IsNotNull(selectItem);

            selectItem.InvokeAsInvokePattern();

        }
    }
}
