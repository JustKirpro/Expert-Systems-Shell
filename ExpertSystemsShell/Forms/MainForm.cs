using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Forms;

public partial class MainForm : Form
{
    private ExpertSystemShell _expertSystemShell = new();

    public MainForm()
    {
        InitializeComponent();
        InitializeListViews();
    }

    #region MenuFile

    private void MenuFileNew_Click(object sender, EventArgs e)
    {
        _expertSystemShell = new ExpertSystemShell();

        Text = "Новая экспертная система";
        MenuFileSave.Enabled = false;
        RefreshListViews();
    }

    private void MenuFileOpen_Click(object sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog()
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Filter = "Knowledge Base files|*.kb"
        };

        var result = openFileDialog.ShowDialog();

        if (result != DialogResult.OK)
        {
            return;
        }

        try
        {
            _expertSystemShell.LoadKnowledgeBase(openFileDialog.FileName);
        }
        catch (IOException)
        {
            ShowErrorMessageBox("При загрузке базы знаний произошла ошибка.");
            return;
        }

        Text = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
        MenuFileSave.Enabled = true;
        RefreshListViews();
    }

    private void MenuFileSave_Click(object sender, EventArgs e)
    {
        try
        {
            _expertSystemShell.SaveKnowledgeBase();
        }
        catch (IOException)
        {
            ShowErrorMessageBox("При сохранение базы знаний произошла ошибка.");
        }
    }

    private void MenuFileSaveAs_Click(object sender, EventArgs e)
    {
        using var saveFileDialog = new SaveFileDialog()
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Filter = "Knowledge Base files|*.kb"
        };

        var result = saveFileDialog.ShowDialog();

        if (result != DialogResult.OK)
        {
            return;
        }

        try
        {
            _expertSystemShell.SaveKnowledgeBase(saveFileDialog.FileName);
        }
        catch (IOException)
        {
            ShowErrorMessageBox("При сохранение базы знаний произошла ошибка.");
            return;
        }

        Text = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
        MenuFileSave.Enabled = true;
    }

    private void MenuFileExit_Click(object sender, EventArgs e)
    {
        var result = MessageBox.Show("Заверить работу приложения?", "Завершение работы", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

        if (result == DialogResult.OK)
        {
            Application.Exit();
        }
    }

    #endregion

    #region MenuConsultation

    private void MenuConsultationStart_Click(object sender, EventArgs e)
    {
        var consultationForms = new ConsultationForm(_expertSystemShell);
        consultationForms.ShowDialog();

        MenuConsultationExplain.Enabled = _expertSystemShell.IsExplanationAvailable;
    }

    private void MenuConsultationExplain_Click(object sender, EventArgs e) => _expertSystemShell.ShowExplanation();

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
                knowledgeBase.Rules.Insert(selectedIndex, rule);
            }
            else
            {
                knowledgeBase.Rules.Add(rule);
            }

            AddRuleToListView(rule, selectedIndex);
            ResizeListView(RulesListView);
            DisplayVariables();
            DisplayDomains();
        }
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
            DisplayVariables();
            DisplayDomains();
        }
    }

    private void DeleteRuleButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var item = GetSelectedItem(RulesListView);
        var rule = (Rule)item.Tag;

        knowledgeBase.Rules.Remove(rule);
        RulesListView.Items.Remove(item);
        ResizeListView(RulesListView);
    }

    private void RulesListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedItemsNumber = RulesListView.SelectedItems.Count;
        EditRuleButton.Enabled = DeleteRuleButton.Enabled = selectedItemsNumber == 1;

        if (selectedItemsNumber == 0)
        {
            ClearRulesListBoxes();
            return;
        }

        UpdateRulesListBoxes();
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

    private void AddRuleToListView(Rule rule, int index = -1)
    {
        var item = index == -1 ? RulesListView.Items.Add(rule.Name) : RulesListView.Items.Insert(index + 1, rule.Name);
        item.SubItems.Add(rule.FormattedRule);
        item.Tag = rule;
    }

    private void UpdateRuleInListView(Rule rule, ListViewItem listViewItem)
    {
        listViewItem.SubItems[0].Text = rule.Name;
        listViewItem.SubItems[1].Text = rule.FormattedRule;

        ResizeListView(RulesListView);
    }

    private void ClearRulesListBoxes()
    {
        ConditionPartListBox.Items.Clear();
        ActionPartListBox.Items.Clear();
    }

    private void UpdateRulesListBoxes()
    {
        ClearRulesListBoxes();

        var selectedItem = GetSelectedItem(RulesListView);
        var rule = (Rule)selectedItem.Tag;

        foreach (var fact in rule.ConditionPart)
        {
            ConditionPartListBox.Items.Add(fact.FormattedFact);
        }

        foreach (var fact in rule.ActionPart)
        {
            ActionPartListBox.Items.Add(fact.FormattedFact);
        }
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
            knowledgeBase.Variables.Add(variable);
            
            AddVariableToListView(variable);
            ResizeListView(VariablesListView);
            DisplayDomains();
        }
    }

    private void EditVariableButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var selectedItem = GetSelectedItem(VariablesListView);
        var variable = (Variable)selectedItem.Tag;

        if (knowledgeBase.IsVariableUsed(variable))
        {
            ShowErrorMessageBox($"Переменная \"{variable.Name}\" используется, поэтому её нельзя изменить.");
            return;
        }

        using var variableForm = new VariableForm(knowledgeBase, variable);
        var result = variableForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            variable = variableForm.Variable!;
            UpdateVariableInListView(variable, selectedItem);
            UpdateVariableListBoxes();
            DisplayDomains();
        }
    }

    private void DeleteVariableButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var item = GetSelectedItem(VariablesListView);
        var variable = (Variable)item.Tag;

        if (knowledgeBase.IsVariableUsed(variable)) 
        {
            ShowErrorMessageBox($"Переменная \"{variable.Name}\" используется, поэтому её нельзя удалить.");
            return;
        }

        knowledgeBase.Variables.Remove(variable);
        VariablesListView.Items.Remove(item);
        ResizeListView(VariablesListView);
    }

    private void VariablesListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedItemsNumber = VariablesListView.SelectedItems.Count;
        EditVariableButton.Enabled = DeleteVariableButton.Enabled = selectedItemsNumber == 1;

        if (selectedItemsNumber == 0)
        {
            ClearVariablesListBoxes();
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
    }

    private void UpdateVariableInListView(Variable variable, ListViewItem listViewItem)
    {
        listViewItem.SubItems[0].Text = variable.Name;
        listViewItem.SubItems[1].Text = variable.FormattedType;
        listViewItem.SubItems[2].Text = variable.Domain.Name;

        ResizeListView(VariablesListView);
    }

    private void ClearVariablesListBoxes()
    {
        DomainValuesListBox.Items.Clear();
        VariableRuleListBox.Items.Clear();
        QuestionListBox.Items.Clear();
    }

    private void UpdateVariableListBoxes()
    {
        ClearVariablesListBoxes();

        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var selectedItem = GetSelectedItem(VariablesListView);
        var variable = (Variable)selectedItem.Tag;

        foreach (var value in variable.Domain.Values)
        {
            DomainValuesListBox.Items.Add(value.Value);
        }

        var rules = knowledgeBase.GetRulesByVariable(variable);

        foreach (var rule in rules)
        {
            VariableRuleListBox.Items.Add(rule.Name);
        }

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
            knowledgeBase.Domains.Add(domain);
            
            AddDomainToListView(domain);
            ResizeListView(DomainsListView);
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
            UpdateDomainListBoxes();
        }
    }

    private void DeleteDomainButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var item = GetSelectedItem(DomainsListView);
        var domain = (Domain)item.Tag;

        if (knowledgeBase.IsDomainUsed(domain)) 
        {
            ShowErrorMessageBox($"Домен \"{domain.Name}\" используется, поэтому его нельзя удалить.");
            return;
        }

        knowledgeBase.Domains.Remove(domain);
        DomainsListView.Items.Remove(item);
        ResizeListView(DomainsListView);
    }

    private void DomainsListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedItemsNumber = DomainsListView.SelectedItems.Count;
        EditDomainButton.Enabled = DeleteDomainButton.Enabled = selectedItemsNumber == 1;

        if (selectedItemsNumber == 0)
        {
            ClearDomainListBoxes();
            return;
        }

        UpdateDomainListBoxes();
    }

    private void AddDomainToListView(Domain domain)
    {
        var item = DomainsListView.Items.Add(domain.Name);
        item.SubItems.Add(domain.FormattedValues);
        item.Tag = domain;
    }

    private void UpdateDomainInListView(Domain domain, ListViewItem listViewItem)
    {
        listViewItem.SubItems[0].Text = domain.Name;
        listViewItem.SubItems[1].Text = domain.FormattedValues;

        ResizeListView(DomainsListView);
    }

    private void ClearDomainListBoxes()
    {
        ValuesListBox.Items.Clear();
        DomainVariableListBox.Items.Clear();
    }

    private void UpdateDomainListBoxes()
    {
        ClearDomainListBoxes();

        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var selectedItem = GetSelectedItem(DomainsListView);
        var domain = (Domain)selectedItem.Tag;

        foreach (var value in domain.Values)
        {
            ValuesListBox.Items.Add(value.Value);
        }

        var variables = knowledgeBase.GetVariablesByDomain(domain);

        foreach (var variable in variables)
        {
            DomainVariableListBox.Items.Add(variable.Name);
        }
    }

    #endregion

    private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (var listView in new[] { RulesListView, VariablesListView, DomainsListView })
        {
            listView.SelectedItems.Clear();
        }
    }

    #region UtilityMethods

    private void InitializeListViews()
    {
        InitializeListView(RulesListView, new List<string> { "Имя", "Описание" });
        DisplayRules();

        InitializeListView(VariablesListView, new List<string> { "Имя", "Тип", "Домен" });
        DisplayVariables();

        InitializeListView(DomainsListView, new List<string> { "Имя", "Значения" });
        DisplayDomains();
    }

    private static void InitializeListView(ListView listView, List<string> columns)
    {
        foreach (var column in columns)
        {
            listView.Columns.Add(column);
        }

        listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
    }

    private void DisplayRules()
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        RulesListView.Items.Clear();

        foreach (var rule in knowledgeBase.Rules)
        {
            AddRuleToListView(rule);
        }

        ResizeListView(RulesListView);
    }

    private void DisplayVariables()
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        VariablesListView.Items.Clear();

        foreach (var variable in knowledgeBase.Variables)
        {
            AddVariableToListView(variable);
        }

        ResizeListView(VariablesListView);
    }

    private void DisplayDomains()
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        DomainsListView.Items.Clear();

        foreach (var domain in knowledgeBase.Domains)
        {
            AddDomainToListView(domain);
        }

        ResizeListView(DomainsListView);
    }

    private void RefreshListViews()
    {
        ClearRulesListBoxes();
        DisplayRules();

        ClearVariablesListBoxes();
        DisplayVariables();

        ClearDomainListBoxes();
        DisplayDomains();
    }

    private static ListViewItem GetSelectedItem(ListView listView) => listView.SelectedItems[0];

    private static int GetSelectedIndex(ListView listView) => listView.SelectedIndices.Count > 0 ? listView.SelectedIndices[0] : -1;

    private static void ResizeListView(ListView listView) => listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

    private static void ShowErrorMessageBox(string message) => MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    #endregion
}