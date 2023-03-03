using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using ExpertSystemsShell.Components;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Forms;

public partial class ExplanationForm : Form
{
    public ExplanationForm(WorkingMemory workingMemory)
    {
        InitializeComponent();
        InitializeVariablesListView(workingMemory.VariableValues);
        InitializeRulesTreeView(workingMemory.GoalVariable, workingMemory.FiredRules);
    }

    private void RulesButton_Click(object sender, System.EventArgs e)
    {
        var isCollapsed = IsCollapsed();

        if (isCollapsed)
        {
            RulesTreeView.ExpandAll();
            RulesButton.Text = "Скрыть всё";
        }
        else
        {
            RulesTreeView.CollapseAll();
            RulesButton.Text = "Раскрыть всё";
        }

        ResetVariablesListViewColor();
    }

    private void RulesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
        var selectedNode = RulesTreeView.SelectedNode;
        var variables = (List<Variable>)selectedNode.Tag;

        foreach (var row in VariablesListView.Items)
        {
            var listViewItem = (ListViewItem)row;
            var variable = (Variable)((listViewItem).Tag);
            listViewItem.BackColor = variables.Contains(variable) ? Color.IndianRed : Color.White;
        }
    }

    private void VariablesListView_SelectedIndexChanged(object sender, System.EventArgs e) => VariablesListView.SelectedItems.Clear();

    private void ExplanationForm_KeyDown(object sender, KeyEventArgs e)
    {
        var isCollapsed = IsCollapsed();

        if (e.Modifiers == Keys.Shift && (e.KeyCode == Keys.Oemplus && isCollapsed || e.KeyCode == Keys.OemMinus && !isCollapsed))
        {
            RulesButton.PerformClick();
        }
    }

    private void InitializeVariablesListView(List<Fact> facts)
    {
        foreach (var fact in facts)
        {
            var item = VariablesListView.Items.Add(fact.Variable.Name);
            item.SubItems.Add(fact.Value.Value);
            item.Tag = fact.Variable;
        }

        VariablesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        VariablesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
    }

    private void ResetVariablesListViewColor()
    {
        foreach (var row in VariablesListView.Items)
        {
            var listViewItem = (ListViewItem)row;
            listViewItem.BackColor = Color.White;
        }
    }

    private void InitializeRulesTreeView(Variable goalVariable, List<Rule> rules)
    {
        var root = FillTreeView(goalVariable, rules);
        RulesTreeView.Nodes.Add(root);
    }

    private static TreeNode FillTreeView(Variable variable, List<Rule> rules)
    {
        var rule = FindRule(variable, rules)!;
        var treeNode = new TreeNode(rule.FormattedRule)
        { 
            Tag = rule.ActionPart.Select(fact => fact.Variable).ToList()
        };

        foreach (var fact in rule.ConditionPart)
        {
            var currentVariable = fact.Variable;

            var childNode = currentVariable.Type is VariableType.Inferred or VariableType.InferredRequested && FindRule(currentVariable, rules) is not null
                ? FillTreeView(currentVariable, rules)
                : new TreeNode($"{currentVariable.Name} = {fact.Value.Value} (запрошена у пользователя)")
                {
                    Tag = new List<Variable>() { currentVariable }
                };

            treeNode.Nodes.Add(childNode);
        }

        return treeNode;
    }

    private static Rule? FindRule(Variable variable, IEnumerable<Rule> rules) => rules.FirstOrDefault(r => r.ActionPart.Select(f => f.Variable).Contains(variable));

    private bool IsCollapsed() => RulesButton.Text == "Раскрыть всё";
}