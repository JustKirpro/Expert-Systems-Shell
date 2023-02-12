using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;
using ExpertSystemsShell.Forms;

namespace ExpertSystemsShell;

public partial class MainForm : Form
{
    private readonly List<Domain> _domains2 = new();

    private readonly Domains _domains = new();

    private readonly Variables _variables = new();

    private readonly List<Variable> _variables2 = new();
    private readonly List<Rule> _rules = new();

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

        _domains2.Add(domain1);
        _domains2.Add(domain2);

        _domains.Add(domain1);
        _domains.Add(domain2);

        var variable1 = new Variable { Name = "Курение", Domain = domain1, Question = null, Type = VariableType.Inferred };
        var variable2 = new Variable { Name = "Рост", Domain = domain2, Question = "Мяу", Type = VariableType.Requested };
        var variable3 = new Variable { Name = "Вес", Domain = domain2, Question = null, Type = VariableType.Requested };

        _variables.Add(variable1);
        _variables.Add(variable2);
        _variables.Add(variable3);

        _variables2.Add(variable1);
        _variables2.Add(variable2);
        _variables2.Add(variable3);

        var rule1 = new Rule
        {
            Name = "R1",
            Reason = "Meow",
            CondtionPart =
            new List<Fact> { new Fact { Variable = variable1, Value = "Да" }, new Fact { Variable = variable2, Value = "Средний" } },
            ActionPart = new List<Fact> { new Fact { Variable = variable3, Value = "Средний" } }
        };

        var rule2 = new Rule
        {
            Name = "R2",
            Reason = "Woof",
            CondtionPart =
            new List<Fact> { new Fact { Variable = variable1, Value = "Нет" }, new Fact { Variable = variable2, Value = "Средний" } },
            ActionPart = new List<Fact> { new Fact { Variable = variable3, Value = "Высокий" } }
        };
           
        _rules.Add(rule1);
        _rules.Add(rule2);
    }

    private void InitializeListViews()
    {
        foreach (var rule in _rules)
        {
            var listViewItem = new ListViewItem(rule.Name)
            {
                Tag = rule
            };

            listViewItem.SubItems.Add(rule.ToString());

            RulesListView.Items.Add(listViewItem);
        }

        foreach (var variable in _variables2)
        {
            var listViewItem = new ListViewItem(variable.Name)
            {
                Tag = variable
            };

            listViewItem.SubItems.Add(variable.FormattedType);
            listViewItem.SubItems.Add(variable.Domain.Name);

            VariablesListView.Items.Add(listViewItem);
        }


        foreach (var domain in _domains2)
        {
            var listViewItem = new ListViewItem(domain.Name)
            {
                Tag = domain
            };

            listViewItem.SubItems.Add(domain.FormattedValues);

            DomainsListView.Items.Add(listViewItem);
        }
    }

    #region Menu

    private void MenuFileOpen_Click(object sender, EventArgs e)
    {

    }

    private void MenuFileExit_Click(object sender, EventArgs e)
    {
        MessageBox.Show("Exit!");
    }

    #endregion

    #region RulesTab

    private void AddRuleButton_Click(object sender, EventArgs e)
    {
        using var ruleForm = new RuleForm();
        var result = ruleForm.ShowDialog();

        if (result == DialogResult.OK)
        {

        }
    }

    #endregion

    #region VariablesTab

    private void AddVariableButton_Click(object sender, EventArgs e)
    {
        var usedNames = _variables.GetNames();

        using var variableForm = new VariableForm(usedNames, _domains);
        var result = variableForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var newVariable = variableForm.Variable;
            _variables.Add(newVariable);
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
        var usedNames = _variables.GetNames();

        using var variableForm = new VariableForm(usedNames, variable!, _domains);
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

        _variables.Remove(variable!);
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
        var usedNames = _domains.GetNames();

        using var domainForm = new DomainForm(usedNames);
        var result = domainForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var newDomain = domainForm.Domain;
            _domains.Add(newDomain);
            AddDomainToListView(newDomain);
        }
    }

    private void EditDomainButton_Click(object sender, EventArgs e)
    {
        var selectedItem = DomainsListView.SelectedItems[0];
        var domain = selectedItem.Tag as Domain;
        var usedNames = _domains.GetNames();

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

        _domains.Remove(domain!);
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