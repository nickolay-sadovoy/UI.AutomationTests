using System.Windows.Automation;

namespace UI.AutomationTests.UnitTests.Core
{
    public static class AutomationElementExtensions
    {
        public static void SetValueAsValuePattern(this AutomationElement automationElement, string value)
        {
            ValuePattern valuePattern = automationElement.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
            valuePattern.SetValue(value);
        }

        public static string GetValueAsValuePattern(this AutomationElement automationElement)
        {
            ValuePattern valuePattern = automationElement.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
            return valuePattern.Current.Value;
        }

        public static void InvokeAsInvokePattern(this AutomationElement automationElement)
        {
            var invokePattern = automationElement.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            invokePattern.Invoke();
        }

        public static void ChangeStateExpandCollapsePattern(this AutomationElement automationElement)
        {
            var invokePattern = automationElement.GetCurrentPattern(ExpandCollapsePattern.Pattern) as ExpandCollapsePattern;
            
            switch (invokePattern.Current.ExpandCollapseState)
            {
                case ExpandCollapseState.LeafNode:
                case ExpandCollapseState.Collapsed:
                    invokePattern.Expand();
                    break;
                case ExpandCollapseState.PartiallyExpanded:
                case ExpandCollapseState.Expanded:
                    invokePattern.Collapse();
                    break;
            }
        }

        public static void SelectAsSelectionItemPattern(this AutomationElement automationElement)
        {
            var selectionItemPattern = automationElement.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
            selectionItemPattern.Select();
        }

        public static AutomationElement GetControlElement(this AutomationElement parentElement, object value, AutomationProperty property = null, PropertyConditionFlags flags = PropertyConditionFlags.IgnoreCase, TreeScope treeScope = TreeScope.Descendants)
        {
            if (property == null)
                property = AutomationElement.AutomationIdProperty;

            Condition condition = new PropertyCondition(property, value, flags);
            AutomationElement controlElement = parentElement.FindFirst(treeScope, condition);
            return controlElement;
        }
    }
}
