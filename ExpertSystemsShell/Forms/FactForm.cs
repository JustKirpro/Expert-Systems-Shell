using System;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Forms;

public partial class FactForm : Form
{
    private readonly Variables _variables;

    private readonly Domains _domains;

    private Variable? _previousSelectedVariable;

    public Fact Fact { get; private set; } = null!;

    public FactForm(Variables variables, Domains domains)
    {
        InitializeComponent();
        Text = "Создание факта";
        OkButton.Enabled = false;

        _variables = variables;
        _domains = domains;

        InitializeVariableComboBox();
    }

    public FactForm(Variables variables, Domains domains, Fact fact)
    {
        InitializeComponent();
        Text = "Редактирование факта";

        _variables = variables;
        _domains = domains;
        Fact = fact;

        InitializeComponents();
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        var variable = _previousSelectedVariable;
        var value = GetValue();

        SetFact(variable!, value);
        DialogResult = DialogResult.OK;
    }

    private void VariableAddButton_Click(object sender, EventArgs e)
    {
        var usedNames = _variables.GetNames();

        using var variableForm = new VariableForm(usedNames, _domains);
        var result = variableForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var variable = variableForm.Variable;

            _variables.Add(variable);

            AddVariableToComboBox(variable);
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

    private void SetFact(Variable variable, string value)
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
        return _variables.GetByName(name)!;
    }

    private string GetValue() => ValueComboBox.Text;

    private void InitializeComponents()
    {
        InitializeVariableComboBox();
        InitializeValueComboBox();
    }

    private void InitializeVariableComboBox()
    {
        foreach (var variable in _variables)
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
        var values = Fact.Variable.Domain.Values;

        foreach (var value in values)
        {
            ValueComboBox.Items.Add(value);

            if (value == Fact.Value)
            {
                VariableComboBox.SelectedItem = value;
            }
        }
    }

    private void RefreshValues(Variable variable)
    {
        ValueComboBox.Items.Clear();
        ValueComboBox.SelectedIndex = -1;

        var values = variable!.Domain.Values;

        foreach (var value in values)
        {
            ValueComboBox.Items.Add(value);
        }
    }

    private void UpdateOkButtonAvailability() => OkButton.Enabled = IsAnyVariableSelected() && IsAnyValueSelected();

    private bool IsAnyVariableSelected() => VariableComboBox.SelectedIndex > -1;

    private bool IsAnyValueSelected() => ValueComboBox.SelectedIndex > -1;

    private void AddVariableToComboBox(Variable variable)
    {
        VariableComboBox.Items.Add(variable.Name);
        VariableComboBox.SelectedItem = variable.Name;
    }
}