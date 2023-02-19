using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Forms;

public partial class MainForm : Form
{
    private readonly ExpertSystemShell _expertSystemShell = new();

    public MainForm()
    {
        InitializeComponent();
        PopulateLists();
        InitializeListViews();
    }

    #region MenuFile

    private void MenuFileNew_Click(object sender, EventArgs e)
    {

    }

    private void MenuFileOpen_Click(object sender, EventArgs e)
    {

    }

    private void MenuFileSave_Click(object sender, EventArgs e)
    {

    }

    private void MenuFileSaveAs_Click(object sender, EventArgs e)
    {

    }

    private void MenuFileExit_Click(object sender, EventArgs e)
    {
   
    }

    #endregion

    #region MenuConsultation

    private void MenuConsultationStart_Click(object sender, EventArgs e)
    {

    }

    private void MenuConsultationExplain_Click(object sender, EventArgs e)
    {

    }

    #endregion

    #region RulesTab

    private void AddRuleButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;

        using var ruleForm = new RuleForm(knowledgeBase);
        var result = ruleForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var rule = ruleForm.Rule!;
            var selectedIndex = GetSelectedIndex(RulesListView);

            if (selectedIndex > - 1)
            {
                knowledgeBase.InsertRule(selectedIndex, rule);
            }
            else
            {
                knowledgeBase.AddRule(rule);
            }

            AddRuleToListView(rule, selectedIndex);
        }

        DisplayVariables();
        DisplayDomains();
    }

    private void EditRuleButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var selectedItem = GetSelectedItem(RulesListView);
        var rule = (Rule)selectedItem.Tag;

        using var ruleForm = new RuleForm(knowledgeBase, rule);
        var result = ruleForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            rule = ruleForm.Rule!;
            UpdateRuleInListView(rule, selectedItem);
            UpdateRulesListBoxes();
        }

        DisplayVariables();
        DisplayDomains();
    }

    private void DeleteRuleButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var selectedItem = GetSelectedItem(RulesListView);
        var rule = (Rule)selectedItem.Tag;

        knowledgeBase.RemoveRule(rule);
        RulesListView.Items.Remove(selectedItem);
    }

    private void RulesListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        var isAnyItemSelected = RulesListView.SelectedItems.Count > 0;
        EditRuleButton.Enabled = DeleteRuleButton.Enabled = isAnyItemSelected;

        if (!isAnyItemSelected)
        {
            ConditionPartListBox.Items.Clear();
            ActionPartListBox.Items.Clear();
            return;
        }

        UpdateRulesListBoxes();
    }

    private void AddRuleToListView(Rule rule, int index = -1)
    {
        var item = index == -1 ? RulesListView.Items.Add(rule.Name) : RulesListView.Items.Insert(index + 1, rule.Name);
        item.SubItems.Add(rule.FormattedRule);
        item.Tag = rule;

        ResizeListView(RulesListView);
    }

    private void UpdateRuleInListView(Rule rule, ListViewItem listViewItem)
    {
        listViewItem.SubItems[0].Text = rule.Name;
        listViewItem.SubItems[1].Text = rule.FormattedRule;

        ResizeListView(RulesListView);
    }

    private void UpdateRulesListBoxes()
    {
        var selectedItem = GetSelectedItem(RulesListView);
        var rule = (Rule)selectedItem.Tag;

        ConditionPartListBox.Items.Clear();

        foreach (var fact in rule.ConditionPart)
        {
            ConditionPartListBox.Items.Add(fact.FormattedFact);
        }

        ActionPartListBox.Items.Clear();

        foreach (var fact in rule.ActionPart)
        {
            ActionPartListBox.Items.Add(fact.FormattedFact);
        }
    }

    private void RulesListView_ItemDrag(object sender, ItemDragEventArgs e) => DoDragDrop(e.Item!, DragDropEffects.Move);

    private void RulesListView_DragEnter(object sender, DragEventArgs e) => e.Effect = DragDropEffects.Move;

    private void RulesListView_DragDrop(object sender, DragEventArgs e)
    {
        var startIndex = GetSelectedIndex(RulesListView);

        var point = RulesListView.PointToClient(new Point(e.X, e.Y));
        var item = RulesListView.GetItemAt(point.X, point.Y);

        if (item is null)
        {
            return;
        }

        var endIndex = item.Index;

        if (startIndex == endIndex)
        {
            return;
        }

        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        item = RulesListView.Items[startIndex];
        var rule = (Rule)item.Tag;

        knowledgeBase.Rules.RemoveAt(startIndex);
        knowledgeBase.Rules.Insert(endIndex, rule);

        RulesListView.Items.RemoveAt(startIndex);
        RulesListView.Items.Insert(endIndex, item);
    }

    #endregion

    #region VariablesTab

    private void AddVariableButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;

        using var variableForm = new VariableForm(knowledgeBase);
        var result = variableForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var variable = variableForm.Variable!;
            knowledgeBase.AddVariable(variable);
            
            AddVariableToListView(variable);
        }

        DisplayDomains();
    }

    private void EditVariableButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var selectedItem = GetSelectedItem(VariablesListView);
        var variable = (Variable)selectedItem.Tag;

        if (knowledgeBase.IsVariableUsed(variable))
        {
            ShowErrorMessageBox("Данная переменная используется, поэтому её нельзя изменить");
            return;
        }

        using var variableForm = new VariableForm(knowledgeBase, variable);
        var result = variableForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            variable = variableForm.Variable!;
            UpdateVariableInListView(variable, selectedItem);
            UpdateVariableListBoxes();
        }

        DisplayDomains();
    }

    private void DeleteVariableButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var selectedItem = GetSelectedItem(VariablesListView);
        var variable = (Variable)selectedItem.Tag;

        if (knowledgeBase.IsVariableUsed(variable))
        {
            ShowErrorMessageBox("Данная переменная используется, поэтому её нельзя удалить");
            return;
        }

        knowledgeBase.RemoveVariable(variable);
        VariablesListView.Items.Remove(selectedItem);
    }

    private void VariablesListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        var isAnyItemSelected = VariablesListView.SelectedItems.Count > 0;
        EditVariableButton.Enabled = DeleteVariableButton.Enabled = isAnyItemSelected;

        if (!isAnyItemSelected)
        {
            DomainValuesListBox.Items.Clear();
            QuestionListBox.Items.Clear();
            return;
        }

        UpdateVariableListBoxes();
    }

    private void AddVariableToListView(Variable variable)
    {
        var item = VariablesListView.Items.Add(variable.Name);
        item.SubItems.Add(variable.FormattedType);
        item.SubItems.Add(variable.Domain.Name);
        item.Tag = variable;

        ResizeListView(VariablesListView);
    }

    private void UpdateVariableInListView(Variable variable, ListViewItem listViewItem)
    {
        listViewItem.SubItems[0].Text = variable.Name;
        listViewItem.SubItems[1].Text = variable.FormattedType;
        listViewItem.SubItems[2].Text = variable.Domain.Name;

        ResizeListView(VariablesListView);
    }

    private void UpdateVariableListBoxes()
    {
        var selectedItem = GetSelectedItem(VariablesListView);
        var variable = (Variable)selectedItem.Tag;

        DomainValuesListBox.Items.Clear();

        foreach (var value in variable.Domain.Values)
        {
            DomainValuesListBox.Items.Add(value.Value);
        }

        QuestionListBox.Items.Clear();
        QuestionListBox.Items.Add(variable.Question);
    }

    #endregion

    #region DomainsTab

    private void AddDomainButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;

        using var domainForm = new DomainForm(knowledgeBase);
        var result = domainForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var domain = domainForm.Domain!;
            knowledgeBase.AddDomain(domain);
            
            AddDomainToListView(domain);
        }
    }

    private void EditDomainButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var selectedItem = GetSelectedItem(DomainsListView);
        var domain = (Domain)selectedItem.Tag;

        using var domainForm = new DomainForm(knowledgeBase, domain);
        var result = domainForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            domain = domainForm.Domain!;
            UpdateDomainInListView(domain, selectedItem);
            UpdateDomainValuesListBox();
        }
    }

    private void DeleteDomainButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var selectedItem = GetSelectedItem(DomainsListView);
        var domain = (Domain)selectedItem.Tag;

        if (knowledgeBase.IsDomainUsed(domain))
        {
            ShowErrorMessageBox("Данный домен используется, поэтому его нельзя удалить");
            return;
        }

        knowledgeBase.RemoveDomain(domain);
        DomainsListView.Items.Remove(selectedItem);
    }

    private void DomainsListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        var isAnyItemSelected = DomainsListView.SelectedItems.Count > 0;
        EditDomainButton.Enabled = DeleteDomainButton.Enabled = isAnyItemSelected;

        if (!isAnyItemSelected)
        {
            ValuesListBox.Items.Clear();
            return;
        }

        UpdateDomainValuesListBox();
    }

    private void AddDomainToListView(Domain domain)
    {
        var item = DomainsListView.Items.Add(domain.Name);
        item.SubItems.Add(domain.FormattedValues);
        item.Tag = domain;

        ResizeListView(DomainsListView);
    }

    private void UpdateDomainInListView(Domain domain, ListViewItem listViewItem)
    {
        listViewItem.SubItems[0].Text = domain.Name;
        listViewItem.SubItems[1].Text = domain.FormattedValues;

        ResizeListView(DomainsListView);
    }

    private void UpdateDomainValuesListBox()
    {
        var selectedItem = GetSelectedItem(DomainsListView);
        var domain = (Domain)selectedItem.Tag;

        ValuesListBox.Items.Clear();

        foreach (var value in domain.Values)
        {
            ValuesListBox.Items.Add(value.Value);
        }
    }

    #endregion

    #region UtilityMethods

    private static void InitializeListView(ListView listView, List<string> columns)
    {
        foreach (var column in columns)
        {
            listView.Columns.Add(column);
        }

        listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
    }

    private void PopulateLists()
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;

        var yesDomainValue = new DomainValue("Да");
        var noDomainValue = new DomainValue("Нет");

        var yesNoDomain = new Domain("Да / Нет", new List<DomainValue>() { yesDomainValue, noDomainValue });

        var highDomainValue = new DomainValue("Высокий");
        var mediumDomainValue = new DomainValue("Средний");
        var lowDomainValue = new DomainValue("Низкий");

        var highMediumLowDomain = new Domain("Высокий / Средний/ Низкий", new List<DomainValue>() { highDomainValue, mediumDomainValue, lowDomainValue });

        knowledgeBase.Domains.Add(yesNoDomain);
        knowledgeBase.Domains.Add(highMediumLowDomain);

        var smokingVariable = new Variable("Курение", string.Empty, yesNoDomain, VariableType.Inferred);
        var heightVariable = new Variable("Рост", "мяу", highMediumLowDomain, VariableType.Requested);
        var weightVariable = new Variable("Вес", string.Empty, highMediumLowDomain, VariableType.Requested);

        knowledgeBase.Variables.Add(smokingVariable);
        knowledgeBase.Variables.Add(heightVariable);
        knowledgeBase.Variables.Add(weightVariable);

        var rule1 = new Rule("R1", "Meow", new List<Fact> { new(weightVariable, mediumDomainValue), new(heightVariable, highDomainValue) }, new List<Fact> { new(smokingVariable, noDomainValue) });
        var rule2 = new Rule("R2", "Woof", new List<Fact> { new(weightVariable, highDomainValue), new(heightVariable, highDomainValue) }, new List<Fact> { new(smokingVariable, yesDomainValue) });

        knowledgeBase.Rules.Add(rule1);
        knowledgeBase.Rules.Add(rule2);
    }

    private void InitializeListViews()
    {
        InitializeListView(RulesListView, new List<string> { "Имя", "Описание" });
        DisplayRules();

        InitializeListView(VariablesListView, new List<string> { "Имя", "Тип", "Домен" });
        DisplayVariables();

        InitializeListView(DomainsListView, new List<string> { "Имя", "Значения" });
        DisplayDomains();
    }

    private void DisplayRules()
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        RulesListView.Items.Clear();

        foreach (var rule in knowledgeBase.Rules)
        {
            AddRuleToListView(rule);
        }
    }

    private void DisplayVariables()
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        VariablesListView.Items.Clear();

        foreach (var variable in knowledgeBase.Variables)
        {
            AddVariableToListView(variable);
        }
    }

    private void DisplayDomains()
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        DomainsListView.Items.Clear();

        foreach (var domain in knowledgeBase.Domains)
        {
            AddDomainToListView(domain);
        }
    }

    private static ListViewItem GetSelectedItem(ListView listView) => listView.SelectedItems[0];

    private static int GetSelectedIndex(ListView listView) => listView.SelectedIndices.Count > 0 ? listView.SelectedIndices[0] : -1;

    private static void ResizeListView(ListView listView) => listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

    private static void ShowErrorMessageBox(string message) => MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    #endregion
}