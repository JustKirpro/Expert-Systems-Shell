using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using ExpertSystemsShell.Components;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Forms;

public partial class ExplanationForm : Form
{
    private readonly WorkingMemory _workingMemory;

    public ExplanationForm(WorkingMemory workingMemory)
    {
        InitializeComponent();

        _workingMemory= workingMemory;
        InitializeVariablesListView();
        InitializeRulesTreeView();
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

            if (variable == _workingMemory.GoalVariable)
            {
                var font = new Font(DefaultFont, FontStyle.Bold);
                listViewItem.Font= font;
            }
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

    private void InitializeVariablesListView()
    {
        foreach (var (variable, domainValue) in _workingMemory.VariableValues)
        {
            var item = VariablesListView.Items.Add(variable.Name);
            item.SubItems.Add(domainValue.Value);
            item.Tag = variable;
        }

        VariablesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        VariablesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
    }

    private void ResetVariablesListViewColor()
    {
        foreach (var row in VariablesListView.Items)
        {
            var listViewItem = (ListViewItem)row;
            var variable = (Variable)listViewItem.Tag;
            listViewItem.BackColor = Color.White;

            if (variable == _workingMemory.GoalVariable)
            {
                var font = new Font(DefaultFont, FontStyle.Bold);
                listViewItem.Font = font;
            }
        }
    }

    private void InitializeRulesTreeView()
    {
        var root = FillTreeView(_workingMemory.GoalVariable, _workingMemory.FiredRules);
        RulesTreeView.Nodes.Add(root);
    }

    private static TreeNode FillTreeView(Variable variable, IReadOnlyCollection<Rule> rules)
    {
        var rule = FindRule(variable, rules)!;
        var treeNode = new TreeNode($"[{rule.Name}] {rule.FormattedRule}")
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
                    Tag = new List<Variable> { currentVariable }
                };

            treeNode.Nodes.Add(childNode);
        }

        return treeNode;
    }

    private static Rule? FindRule(Variable variable, IEnumerable<Rule> rules) => rules.FirstOrDefault(r => r.ActionPart.Select(f => f.Variable).Contains(variable));

    private bool IsCollapsed() => RulesButton.Text == "Раскрыть всё";
}