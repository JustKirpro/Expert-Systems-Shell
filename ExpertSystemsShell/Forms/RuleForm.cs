using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using ExpertSystemsShell.Components;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Forms;

public partial class RuleForm : Form
{
    private readonly KnowledgeBase _knowledgeBase;

    private readonly List<Fact> _conditionPart = new();

    private readonly List<Fact> _actionPart = new();

    public Rule? Rule { get; private set; }

    #region Constructors

    public RuleForm(KnowledgeBase knowledgeBase)
    {
        InitializeComponent();
        Text = "Создание правила";
        RuleNameTextBox.Text = knowledgeBase.GenerateNextRuleName();

        _knowledgeBase = knowledgeBase;
    }

    public RuleForm(KnowledgeBase knowledgeBase, Rule rule)
    {
        InitializeComponent();
        Text = "Редактирование правила";

        _knowledgeBase= knowledgeBase;
        Rule = rule;

        CopyParts(rule.ConditionPart, rule.ActionPart);
        InitializeControls(rule);
        OkButton.Enabled = true;
    }

    #endregion

    #region Button events

    private void OkButton_Click(object sender, EventArgs e)
    {
        var name = GetName();

        if (IsNameUsed(name))
        {
            ShowErrorMessageBox($"Правило с именем \"{name}\" уже существует.");
            return;
        }

        if (!IsConditionPartUnique(_conditionPart))
        {
            ShowErrorMessageBox("Правило с такой посылкой уже существует.");
            return;
        }

        var reason = GetReason();

        SetRule(name, reason, _conditionPart, _actionPart);
        DialogResult = DialogResult.OK;
    }

    private void ConditionPartAddButton_Click(object sender, EventArgs e)
    {
        var usedVariables = GetUsedVariables();

        using var factForm = new FactForm(_knowledgeBase, usedVariables, true);
        var result = factForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var fact = factForm.Fact!;

            _conditionPart.Add(fact);

            AddItemToListView(ConditionPartListView, fact);
            UpdateOkButtonAvailability();
        }
    }

    private void ConditionPartEditButton_Click(object sender, EventArgs e)
    {
        var selectedItem = GetSelectedItem(ConditionPartListView);
        var fact = (Fact)selectedItem.Tag;

        var usedVariables = GetUsedVariables();
        usedVariables.Remove(fact.Variable.Name);

        using var factForm = new FactForm(_knowledgeBase, usedVariables, fact, true);
        var result = factForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var index = _conditionPart.IndexOf(fact);
            fact = factForm.Fact!;

            _conditionPart[index].Variable = fact.Variable;
            _conditionPart[index].Value = fact.Value;

            selectedItem.Text = fact.FormattedFact;
            UpdateOkButtonAvailability();
        }
    }

    private void ConditionPartDeleteButton_Click(object sender, EventArgs e)
    {
        foreach (var row in ConditionPartListView.SelectedItems)
        {
            var item = (ListViewItem)row;
            var fact = (Fact)item.Tag;

            _conditionPart.Remove(fact);
            ConditionPartListView.Items.Remove(item);
        }

        UpdateOkButtonAvailability();
    }

    private void ActionPartAddButton_Click(object sender, EventArgs e)
    {
        var usedVariables = GetUsedVariables();

        using var factForm = new FactForm(_knowledgeBase, usedVariables, false);
        var result = factForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var fact = factForm.Fact!;

            _actionPart.Add(fact);

            AddItemToListView(ActionPartListView, fact);
            UpdateOkButtonAvailability();
        }
    }

    private void ActionPartEditButton_Click(object sender, EventArgs e)
    {
        var selectedItem = GetSelectedItem(ActionPartListView);
        var fact = (Fact)selectedItem.Tag;

        var usedVariables = GetUsedVariables();
        usedVariables.Remove(fact.Variable.Name);

        using var factForm = new FactForm(_knowledgeBase, usedVariables, fact, false);
        var result = factForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var index = _actionPart.IndexOf(fact);
            fact = factForm.Fact!;

            _actionPart[index].Variable = fact.Variable;
            _actionPart[index].Value = fact.Value;

            selectedItem.Text = fact.FormattedFact;
            UpdateOkButtonAvailability();
        }
    }

    private void ActionPartDeleteButton_Click(object sender, EventArgs e)
    {
        foreach (var row in ActionPartListView.SelectedItems)
        {
            var item = (ListViewItem)row;
            var fact = (Fact)item.Tag;

            _actionPart.Remove(fact);
            ActionPartListView.Items.Remove(item);
        }

        UpdateOkButtonAvailability();
    }

    #endregion

    #region ListViews and TextBox events

    private void ConditionPartListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedItemsNumber = ConditionPartListView.SelectedItems.Count;
        ConditionPartEditButton.Enabled = selectedItemsNumber == 1;
        ConditionPartDeleteButton.Enabled = selectedItemsNumber > 0;
    }

    private void ConditionPartListView_ItemDrag(object sender, ItemDragEventArgs e) => DoDragDrop(e.Item!, DragDropEffects.Move);

    private void ConditionPartListView_DragEnter(object sender, DragEventArgs e) => e.Effect = DragDropEffects.Move;

    private void ConditionPartListView_DragDrop(object sender, DragEventArgs e)
    {
        var startIndex = GetSelectedItemIndex(ConditionPartListView);

        var point = ConditionPartListView.PointToClient(new Point(e.X, e.Y));
        var item = ConditionPartListView.GetItemAt(point.X, point.Y);

        if (item is null)
        {
            return;
        }

        var endIndex = item.Index;

        if (startIndex == endIndex)
        {
            return;
        }

        item = ConditionPartListView.Items[startIndex];
        var fact = (Fact)item.Tag;

        _conditionPart.RemoveAt(startIndex);
        _conditionPart.Insert(endIndex, fact);

        ConditionPartListView.Items.RemoveAt(startIndex);
        ConditionPartListView.Items.Insert(endIndex, item);

        UpdateOkButtonAvailability();
    }

    private void ActionPartListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedItemsNumber = ActionPartListView.SelectedItems.Count;
        ActionPartEditButton.Enabled = selectedItemsNumber == 1;
        ActionPartDeleteButton.Enabled = selectedItemsNumber > 0;
    }

    private void RuleNameTextBox_TextChanged(object sender, EventArgs e) => UpdateOkButtonAvailability();

    private void ReasonTextBox_TextChanged(object sender, EventArgs e) => UpdateOkButtonAvailability();

    #endregion

    #region Utility methods

    private void SetRule(string name, string reason, List<Fact> conditionPart, List<Fact> actionPart)
    {
        if (Rule is null)
        {
            Rule = new Rule(name, reason, conditionPart, actionPart);
            return;
        }

        Rule.Name = name;
        Rule.Reason = reason;
        Rule.ConditionPart = conditionPart;
        Rule.ActionPart = actionPart;
    }

    private void CopyParts(IEnumerable<Fact> conditionPart, IEnumerable<Fact> actionPart)
    {
        foreach (var newFact in conditionPart.Select(fact => new Fact(fact.Variable, fact.Value)))
        {
            _conditionPart.Add(newFact);
        }

        foreach (var newFact in actionPart.Select(fact => new Fact(fact.Variable, fact.Value)))
        {
            _actionPart.Add(newFact);
        }
    }

    private string GetName() => RuleNameTextBox.Text.Trim();

    private bool IsNameValid() => !string.IsNullOrWhiteSpace(GetName());

    private bool IsNameUsed(string name) => _knowledgeBase.IsRuleNameUsed(name) && name != Rule?.Name;

    private string GetReason() => ReasonTextBox.Text.Trim();

    private bool IsConditionPartUnique(List<Fact> conditionPart)
    {
        foreach (var rule in _knowledgeBase.Rules)
        {
            if (!IsFactsNumberMatched(rule, conditionPart))
            {
                continue;
            }

            var isConditionPartMatched = conditionPart.Select(nf => rule.ConditionPart.Any(f => nf.Variable == f.Variable && nf.Value == f.Value)).All(f => f);
            
            if (isConditionPartMatched && rule != Rule)
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsFactsNumberMatched(Rule rule, ICollection conditionPart) => conditionPart.Count == rule.ConditionPart.Count;

    private List<string> GetUsedVariables() => _conditionPart.Union(_actionPart).Select(v => v.Variable.Name).ToList();

    private void InitializeControls(Rule rule)
    {
        RuleNameTextBox.Text = rule.Name;
        ReasonTextBox.Text = rule.Reason;

        InitializeListView(_conditionPart, ConditionPartListView);
        InitializeListView(_actionPart, ActionPartListView);
    }

    private static void InitializeListView(List<Fact> part, ListView listView)
    {
        foreach (var fact in part)
        {
            AddItemToListView(listView, fact);
        }
    }

    private static int GetSelectedItemIndex(ListView listView) => listView.SelectedIndices[0];

    private void UpdateOkButtonAvailability() => OkButton.Enabled = IsNameValid() && ActionPartListView.Items.Count > 0;

    private static void AddItemToListView(ListView listView, Fact fact)
    {
        var item = listView.Items.Add(fact.FormattedFact);
        item.Tag = fact;
    }

    private static ListViewItem GetSelectedItem(ListView listView) => listView.SelectedItems[0];

    private static void ShowErrorMessageBox(string message) => MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    #endregion
}