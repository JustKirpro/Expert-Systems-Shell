using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;
using ExpertSystemsShell.Forms;

namespace ExpertSystemsShell;

public partial class MainForm : Form
{
    private readonly ExpertSystemShell _expertSystemShell = new();

    public MainForm()
    {
        InitializeComponent();
        PopulateLists();
        InitializeListViews();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        RulesListView.Columns.Add("Имя");
        RulesListView.Columns.Add("Описание");
        RulesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        RulesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        VariablesListView.Columns.Add("Имя");
        VariablesListView.Columns.Add("Тип");
        VariablesListView.Columns.Add("Домен");
        VariablesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        VariablesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        DomainsListView.Columns.Add("Имя");
        DomainsListView.Columns.Add("Значения");
        DomainsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        DomainsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
    }

    private void PopulateLists()
    {
        var domain1 = new Domain("Да / Нет1", new List<string>() { "Да", "Нет" });
        var domain2 = new Domain("Высокий / Средний / Низкий1", new List<string>() { "Высокий", "Средний", "Низкий" });

        _expertSystemShell.Domains.Add(domain1);
        _expertSystemShell.Domains.Add(domain2);

        var variable1 = new Variable("Курение", domain1, VariableType.Inferred, null);
        var variable2 = new Variable("Рост", domain2, VariableType.Requested, "Мяу");
        var variable3 = new Variable("Вес", domain2, VariableType.Requested);

        _expertSystemShell.Variables.Add(variable1);
        _expertSystemShell.Variables.Add(variable2);
        _expertSystemShell.Variables.Add(variable3);

        var rule1 = new Rule("R1", "Meow", new List<Fact> { new Fact { Variable = variable1, Value = "Да" }, new Fact { Variable = variable2, Value = "Средний" } }, new List<Fact> { new Fact { Variable = variable3, Value = "Средний" } });

        var rule2 = new Rule("R2", "Woof", new List<Fact> { new Fact { Variable = variable1, Value = "Нет" }, new Fact { Variable = variable2, Value = "Средний" } }, new List<Fact> { new Fact { Variable = variable3, Value = "Высокий" } });
           
        _expertSystemShell.Rules.Add(rule1);
        _expertSystemShell.Rules.Add(rule2);
    }

    private void InitializeListViews()
    {
        foreach (var rule in _expertSystemShell.Rules)
        {
            var listViewItem = new ListViewItem(rule.Name)
            {
                Tag = rule
            };

            listViewItem.SubItems.Add(rule.ToString());

            RulesListView.Items.Add(listViewItem);
        }

        foreach (var variable in _expertSystemShell.Variables)
        {
            var listViewItem = new ListViewItem(variable.Name)
            {
                Tag = variable
            };

            listViewItem.SubItems.Add(variable.FormattedType);
            listViewItem.SubItems.Add(variable.Domain.Name);

            VariablesListView.Items.Add(listViewItem);
        }


        foreach (var domain in _expertSystemShell.Domains)
        {
            var listViewItem = new ListViewItem(domain.Name)
            {
                Tag = domain
            };

            listViewItem.SubItems.Add(domain.FormattedValues);

            DomainsListView.Items.Add(listViewItem);
        }
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
        MessageBox.Show("Exit!");
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
        using var ruleForm = new RuleForm(_expertSystemShell.Variables, _expertSystemShell.Domains);
        var result = ruleForm.ShowDialog();

        if (result == DialogResult.OK)
        {

        }
    }

    private void EditRuleButton_Click(object sender, EventArgs e)
    {
        var selectedItem = RulesListView.SelectedItems[0];
        var variable = selectedItem.Tag as Rule;
        var usedNames = _expertSystemShell.Rules.GetNames();

        //using var ruleForm = new RuleForm(usedNames, variable!, _domains);
        //var result = variableForm.ShowDialog();

        //if (result == DialogResult.OK)
        {
            //var newDomain = domainForm.Domain;
            //_domains.Add(newDomain);
            //AddDomainToListView(newDomain);
        }
    }

    private void RulesListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        var isAnyItemSelected = RulesListView.SelectedItems.Count > 0;
        EditRuleButton.Enabled = DeleteRuleButton.Enabled = isAnyItemSelected;
    }

    #endregion

    #region VariablesTab

    private void AddVariableButton_Click(object sender, EventArgs e)
    {
        var usedNames = _expertSystemShell.Variables.GetNames();

        using var variableForm = new VariableForm(usedNames, _expertSystemShell.Domains);
        var result = variableForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var newVariable = variableForm.Variable;
            _expertSystemShell.Variables.Add(newVariable);
            AddVariableToListView(newVariable);
        }
    }

    private void AddVariableToListView(Variable variable)
    {
        var listViewItem = new ListViewItem(variable.Name)
        {
            Tag = variable
        };

        listViewItem.SubItems.Add(variable.FormattedType);
        listViewItem.SubItems.Add(variable.Domain.Name);
        VariablesListView.Items.Add(listViewItem);
        VariablesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
    }

    private void EditVariableButton_Click(object sender, EventArgs e)
    {
        var selectedItem = VariablesListView.SelectedItems[0];
        var variable = selectedItem.Tag as Variable;
        var usedNames = _expertSystemShell.Variables.GetNames();

        using var variableForm = new VariableForm(usedNames, variable!, _expertSystemShell.Domains);
        var result = variableForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            //var newDomain = domainForm.Domain;
            //_domains.Add(newDomain);
            //AddDomainToListView(newDomain);
        }
    }

    private void DeleteVariableButton_Click(object sender, EventArgs e)
    {
        var selectedItem = VariablesListView.SelectedItems[0];
        var variable = selectedItem.Tag as Variable;

        _expertSystemShell.Variables.Remove(variable!);
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

    private void UpdateVariableListBoxes()
    {
        DomainValuesListBox.Items.Clear();

        var selectedItem = VariablesListView.SelectedItems[0];
        var variable = selectedItem.Tag as Variable;

        foreach (var value in variable!.Domain.Values)
        {
            DomainValuesListBox.Items.Add(value);
        }

        QuestionListBox.Items.Clear();

        QuestionListBox.Items.Add(variable!.Question ?? string.Empty);
    }

    #endregion

    #region DomainsTab

    private void AddDomainButton_Click(object sender, EventArgs e)
    {
        var usedNames = _expertSystemShell.Domains.GetNames();

        using var domainForm = new DomainForm(usedNames);
        var result = domainForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var newDomain = domainForm.Domain;
            _expertSystemShell.Domains.Add(newDomain);
            AddDomainToListView(newDomain);
        }
    }

    private void EditDomainButton_Click(object sender, EventArgs e)
    {
        var selectedItem = DomainsListView.SelectedItems[0];
        var domain = selectedItem.Tag as Domain;
        var usedNames = _expertSystemShell.Domains.GetNames();

        using var domainForm = new DomainForm(usedNames, domain!);
        var result = domainForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var updatedDomain = domainForm.Domain;
            UpdateDomainInListView(updatedDomain, selectedItem);
            UpdateDomainValuesListBox();
        }
    }

    private void DeleteDomainButton_Click(object sender, EventArgs e)
    {
        var selectedItem = DomainsListView.SelectedItems[0];
        var domain = selectedItem.Tag as Domain;

        _expertSystemShell.Domains.Remove(domain!);
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
        var listViewItem = new ListViewItem(domain.Name)
        {
            Tag = domain
        };
        
        listViewItem.SubItems.Add(domain.FormattedValues);
        DomainsListView.Items.Add(listViewItem);
        DomainsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
    }

    private void UpdateDomainInListView(Domain domain, ListViewItem listViewItem)
    {
        listViewItem.SubItems[0].Text = domain.Name;
        listViewItem.SubItems[1].Text = domain.FormattedValues;
        listViewItem.Tag = domain;
        DomainsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
    }

    private void UpdateDomainValuesListBox()
    {
        ValuesListBox.Items.Clear();

        var selectedItem = DomainsListView.SelectedItems[0];
        var domain = selectedItem.Tag as Domain;

        foreach (var value in domain!.Values)
        {
            ValuesListBox.Items.Add(value);
        }
    }

    #endregion
}