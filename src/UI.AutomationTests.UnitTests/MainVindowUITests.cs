﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Windows.Automation;
using UI.AutomationTests.UnitTests.Core;

namespace UI.AutomationTests.UnitTests
{
    [TestClass]
    public class MainVindowUITests
    {
        static MainVindowUITests()
        {
        }

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

        [TestMethod]
        public void MainWindow_RadioButtons_Check_Changing_Text()
        {
            AutomationElement rootElement = AutomationElement.RootElement;
            Assert.IsNotNull(rootElement);

            AutomationElement appElement = rootElement.GetControlElement(Properties.Resources.MainWindow_MainWindow_Id, treeScope: TreeScope.Children);
            Assert.IsNotNull(appElement);

            AutomationElement radioButtonA = appElement.GetControlElement(Properties.Resources.MainWindow_RadioButtonA_Id);
            Assert.IsNotNull(radioButtonA);

            AutomationElement radioButtonB = appElement.GetControlElement(Properties.Resources.MainWindow_RadioButtonB_Id);
            Assert.IsNotNull(radioButtonB);

            AutomationElement radioButtonC = appElement.GetControlElement(Properties.Resources.MainWindow_RadioButtonC_Id);
            Assert.IsNotNull(radioButtonC);

            radioButtonA.SelectAsSelectionItemPattern();
        }

        [TestMethod]
        public void MainWindow_ListView_Check_Changing_Text()
        {
            AutomationElement rootElement = AutomationElement.RootElement;
            Assert.IsNotNull(rootElement);

            AutomationElement appElement = rootElement.GetControlElement(Properties.Resources.MainWindow_MainWindow_Id, treeScope: TreeScope.Children);
            Assert.IsNotNull(appElement);

            AutomationElement listView = appElement.GetControlElement(Properties.Resources.MainWindow_ListView_Id);
            Assert.IsNotNull(listView);

            AutomationElement listItem = FindChildAtB(listView, 0);
            listItem.SelectAsSelectionItemPattern();
        }

        [TestMethod]
        public void MainWindow_Menu_Check_Executing()
        {
            AutomationElement rootElement = AutomationElement.RootElement;
            Assert.IsNotNull(rootElement);

            AutomationElement appElements = rootElement.GetControlElement(Properties.Resources.MainWindow_MainWindow_Id, treeScope: TreeScope.Children);
            Assert.IsNotNull(appElements);

            var menus = appElements.GetControlElement("Menu", AutomationElement.ClassNameProperty, treeScope: TreeScope.Children);
            Assert.IsNotNull(menus);

            var tools = menus.GetControlElement(Properties.Resources.MainWindow_Menu_Tools_Header, AutomationElement.NameProperty, treeScope: TreeScope.Children);
            Assert.IsNotNull(tools);
            tools.ChangeStateExpandCollapsePattern();

            var android = tools.GetControlElement(Properties.Resources.MainWindow_Menu_Android_Header, AutomationElement.NameProperty, treeScope: TreeScope.Children);
            Assert.IsNotNull(android);
            android.ChangeStateExpandCollapsePattern();

            var emulator = android.GetControlElement(Properties.Resources.MainWindow_Menu_Emulator_Header, AutomationElement.NameProperty, treeScope: TreeScope.Children);
            Assert.IsNotNull(emulator);
            emulator.InvokeAsInvokePattern();

            Assert.IsTrue(MainWindow_Menu_Check_Status(Properties.Resources.MainWindow_Menu_Emulator_Header));
        }

        [TestMethod]
        public void StressTestAdd100Elements()
        {
            var n = 100;
            AutomationElement rootElement = AutomationElement.RootElement;
            Assert.IsNotNull(rootElement);

            AutomationElement appElement = rootElement.GetControlElement(Properties.Resources.MainWindow_MainWindow_Id, treeScope: TreeScope.Children);
            Assert.IsNotNull(appElement);

            AutomationElement btnAddItemToCollectionClick = appElement.GetControlElement(Properties.Resources.MainWindow_ButtonAddItemToCollection_Id);
            Assert.IsNotNull(btnAddItemToCollectionClick);
            for (var i = 0; i < n; i++)
            {
                btnAddItemToCollectionClick.InvokeAsInvokePattern();
            }

        }

        private bool MainWindow_Menu_Check_Status(string expectedText)
        {
            AutomationElement rootElement = AutomationElement.RootElement;
            Assert.IsNotNull(rootElement);

            AutomationElement appElements = rootElement.GetControlElement(Properties.Resources.MainWindow_MainWindow_Id, treeScope: TreeScope.Children);
            Assert.IsNotNull(appElements);

            var statusTextBlock = appElements.GetControlElement(Properties.Resources.MainWindow_Status_Id, treeScope: TreeScope.Children);
            Assert.IsNotNull(statusTextBlock);

            var actualText = statusTextBlock.GetValueAsValuePattern();
            Assert.IsNotNull(actualText);

            return actualText.Equals(expectedText);
        }

        /// <summary>
        /// Retrieves an element in a list, using TreeWalker.
        /// </summary>
        /// <param name="parent">The list element.</param>
        /// <param name="index">The index of the element to find.</param>
        /// <returns>The list item.</returns>
        AutomationElement FindChildAt(AutomationElement parent, int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            TreeWalker walker = TreeWalker.ControlViewWalker;
            AutomationElement child = walker.GetFirstChild(parent);
            for (int x = 1; x <= index; x++)
            {
                child = walker.GetNextSibling(child);
                if (child == null)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            return child;
        }

        /// <summary>
        /// Retrieves an element in a list, using FindAll.
        /// </summary>
        /// <param name="parent">The list element.</param>
        /// <param name="index">The index of the element to find.</param>
        /// <returns>The list item.</returns>
        AutomationElement FindChildAtB(AutomationElement parent, int index)
        {
            Condition findCondition = new PropertyCondition(AutomationElement.IsSelectionItemPatternAvailableProperty, true);
            AutomationElementCollection found = parent.FindAll(TreeScope.Children, findCondition);
            if ((index < 0) || (index >= found.Count))
            {
                throw new ArgumentOutOfRangeException();
            }
            return found[index];
        }
    }
}
