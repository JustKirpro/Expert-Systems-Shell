using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;
using ExpertSystemsShell.Modules;

namespace ExpertSystemsShell.Forms;

public partial class FactForm : Form
{
    private readonly KnowledgeBase _knowledgeBase;

    private readonly List<string> _usedVariables;

    private readonly bool _isRequested;

    private Variable _previousSelectedVariable = null!;

    public Fact? Fact { get; private set; }

    public FactForm(KnowledgeBase knowledgeBase, List<string> usedVariables, bool isRequested)
    {
        InitializeComponent();
        Text = "Создание факта";
        OkButton.Enabled = false;

        _knowledgeBase = knowledgeBase;
        _usedVariables = usedVariables;
        _isRequested = isRequested;

        InitializeVariableComboBox();
    }

    public FactForm(KnowledgeBase knowledgeBase, List<string> usedVariables, Fact fact, bool isRequested)
    {
        InitializeComponent();
        Text = "Редактирование факта";

        _knowledgeBase = knowledgeBase;
        _usedVariables = usedVariables;
        _isRequested = isRequested;
        Fact = fact;

        InitializeComponents();
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        var variable = _previousSelectedVariable;

        if (IsVariableUsed(variable))
        {
            ShowErrorMessageBox($"В правиле уже содержится факт с переменной \"{variable.Name}\"");
            return;
        }

        var value = GetValue(variable);

        SetFact(variable, value);
        DialogResult = DialogResult.OK;
    }

    private void VariableAddButton_Click(object sender, EventArgs e)
    {
        using var variableForm = new VariableForm(_knowledgeBase);
        var result = variableForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var variable = variableForm.Variable!;

            _knowledgeBase.AddVariable(variable);

            if (IsVariableAvailable(variable)) 
            {
                AddVariableToComboBox(variable);
            }
        }
    }

    private void VariableComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        var variable = GetVariable();

        if (variable == _previousSelectedVariable)
        {
            return;
        }

        _previousSelectedVariable = variable;

        RefreshValues(variable);
    }

    private void ValueComboBox_SelectedIndexChanged(object sender, EventArgs e) => UpdateOkButtonAvailability();

    private void SetFact(Variable variable, DomainValue value)
    {
        if (Fact is null)
        {
            Fact = new Fact(variable, value);
            return;
        }

        Fact.Variable = variable;
        Fact.Value = value;
    }

    private Variable GetVariable()
    {
        var name = VariableComboBox.Text;
        return _knowledgeBase.GetVariableByName(name);
    }

    private bool IsVariableUsed(Variable variable) => _usedVariables.Contains(variable.Name) && variable != Fact?.Variable;

    private DomainValue GetValue(Variable variable)
    {
        var value = ValueComboBox.Text;
        var domain = variable.Domain;

        return domain.Values.First(v => v.Value == value);
    }
    
    private bool IsVariableAvailable(Variable variable)
    {
        return (IsVariableRequestedInferred() || IsVariableRequested() && _isRequested || IsVariableInferred() && !_isRequested) && !IsVariableUsed(variable);
        
        bool IsVariableRequestedInferred() => variable.Type is VariableType.RequestedInferred;
        bool IsVariableRequested() => variable.Type is VariableType.Requested;
        bool IsVariableInferred() => variable.Type is VariableType.Inferred;
    }

    private void InitializeComponents()
    {
        InitializeVariableComboBox();
        InitializeValueComboBox();
    }

    private void InitializeVariableComboBox()
    {
        var variables = _knowledgeBase.Variables;

        foreach (var variable in variables.Where(IsVariableAvailable))
        {
            VariableComboBox.Items.Add(variable.Name);

            if (variable == Fact?.Variable)
            {
                VariableComboBox.SelectedItem = variable.Name;
            }
        }
    }

    private void InitializeValueComboBox()
    {
        var values = Fact!.Variable.Domain.Values;

        ValueComboBox.Items.Clear();

        foreach (var value in values)
        {
            ValueComboBox.Items.Add(value.Value);

            if (value == Fact.Value)
            {
                ValueComboBox.SelectedItem = value.Value;
            }
        }
    }

    private void RefreshValues(Variable variable)
    {
        ValueComboBox.Items.Clear();
        ValueComboBox.SelectedIndex = -1;

        var values = variable.Domain.Values;

        foreach (var value in values)
        {
            ValueComboBox.Items.Add(value.Value);
        }
    }

    private void AddVariableToComboBox(Variable variable)
    {
        VariableComboBox.Items.Add(variable.Name);
        VariableComboBox.SelectedItem = variable.Name;
    }

    private void UpdateOkButtonAvailability() => OkButton.Enabled = IsAnyVariableSelected() && IsAnyValueSelected();

    private bool IsAnyVariableSelected() => VariableComboBox.SelectedIndex > -1;

    private bool IsAnyValueSelected() => ValueComboBox.SelectedIndex > -1;

    private static void ShowErrorMessageBox(string message) => MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
}