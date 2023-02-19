using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;
using ExpertSystemsShell.Modules;

namespace ExpertSystemsShell.Forms;

public partial class RuleForm : Form
{
    private readonly KnowledgeBase _knowledgeBase;

    private readonly List<Fact> _conditionPart = new();

    private readonly List<Fact> _actionPart = new();

    public Rule? Rule { get; private set; }

    public RuleForm(KnowledgeBase knowledgeBase)
    {
        InitializeComponent();
        Text = "Создание правила";
        OkButton.Enabled = false;

        _knowledgeBase = knowledgeBase;
    }

    public RuleForm(KnowledgeBase knowledgeBase, Rule rule)
    {
        InitializeComponent();
        Text = "Редактирование правила";

        _knowledgeBase= knowledgeBase;
        InitializeParts(rule.ConditionPart, rule.ActionPart);
        Rule = rule;

        InitializeComponents();
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        var name = GetName();

        if (IsNameUsed(name))
        {
            ShowErrorMessageBox($"Правило с именем \"{name}\" уже существует");
            return;
        }

        if (!IsConditionPartUnique(_conditionPart))
        {
            ShowErrorMessageBox("Правило с такой посылкой уже существует");
            return;
        }

        var reason = GetReason();

        SetRule(name, reason, _conditionPart, _actionPart);
        DialogResult = DialogResult.OK;
    }

    private void ConditionPartAddButton_Click(object sender, EventArgs e)
    {
        var usedVariables = GetConditionPartUsedVariables();

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

        var usedVariables = GetConditionPartUsedVariables();
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
        var selectedItem = GetSelectedItem(ConditionPartListView);
        var fact = (Fact)selectedItem.Tag;

        _conditionPart.Remove(fact);

        ConditionPartListView.Items.Remove(selectedItem);
        UpdateOkButtonAvailability();
    }

    private void ConditionPartListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        var isAnyFactSelected = ConditionPartListView.SelectedItems.Count > 0;
        ConditionPartEditButton.Enabled = ConditionPartDeleteButton.Enabled = isAnyFactSelected;
    }

    private void ActionPartAddButton_Click(object sender, EventArgs e)
    {
        var usedVariables = GetActionPartUsedVariables();

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

        var usedVariables = GetActionPartUsedVariables();
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
        var selectedItem = GetSelectedItem(ActionPartListView);
        var fact = (Fact)selectedItem.Tag;

        _actionPart.Remove(fact);

        ActionPartListView.Items.Remove(selectedItem);
        UpdateOkButtonAvailability();
    }

    private void ActionPartListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        var isAnyFactSelected = ActionPartListView.SelectedItems.Count > 0;
        ActionPartEditButton.Enabled = ActionPartDeleteButton.Enabled = isAnyFactSelected;
    }

    private void RuleNameTextBox_TextChanged(object sender, EventArgs e) => UpdateOkButtonAvailability();

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

    private void InitializeParts(IEnumerable<Fact> conditionPart, IEnumerable<Fact> actionPart)
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

    private bool IsNameUsed(string name) => _knowledgeBase.Rules.Any(r => r.Name == name) && name != Rule?.Name;

    private string GetReason() => ReasonTextBox.Text.Trim();

    private bool IsConditionPartUnique(List<Fact> conditionPart)
    {
        foreach (var rule in _knowledgeBase.Rules)
        {
            if (!IsFactsNumberMatched(rule, conditionPart))
            {
                continue;
            }

            var isConditionPartMatched = conditionPart
                .Select(nf => rule.ConditionPart.Any(f => nf.Variable == f.Variable && nf.Value == f.Value))
                .All(isFactExisted => isFactExisted);
            
            if (isConditionPartMatched)
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsFactsNumberMatched(Rule rule, ICollection conditionPart) => conditionPart.Count == rule.ConditionPart.Count;

    private List<string> GetConditionPartUsedVariables() => _conditionPart.Select(v => v.Variable.Name).ToList();

    private List<string> GetActionPartUsedVariables() => _actionPart.Select(v => v.Variable.Name).ToList();

    private void InitializeComponents()
    {
        RuleNameTextBox.Text = Rule!.Name;
        ReasonTextBox.Text = Rule.Reason;

        InitializeConditionPartListView();
        InitializeActionPartListView();
    }

    private void InitializeConditionPartListView()
    {
        foreach (var fact in _conditionPart)
        {
            AddItemToListView(ConditionPartListView, fact);
        }
    }

    private void InitializeActionPartListView()
    {
        foreach (var fact in _actionPart)
        {
            AddItemToListView(ActionPartListView, fact);
        }
    }

    private void UpdateOkButtonAvailability()
    {
        var name = GetName();
        OkButton.Enabled = !string.IsNullOrWhiteSpace(name) && ConditionPartListView.Items.Count > 0 && ActionPartListView.Items.Count > 0;
    }

    private static void AddItemToListView(ListView listView, Fact fact)
    {
        var item = listView.Items.Add(fact.FormattedFact);
        item.Tag = fact;
    }

    private static ListViewItem GetSelectedItem(ListView listView) => listView.SelectedItems[0];
    
    private static void ShowErrorMessageBox(string message) => MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
}