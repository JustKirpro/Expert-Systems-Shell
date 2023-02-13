using ExpertSystemsShell.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExpertSystemsShell.Forms;

public partial class ValueForm : Form
{
    private readonly List<DomainValue> _usedValues;

    public DomainValue Value { get; private set; }

    public ValueForm(List<DomainValue> usedValues, DomainValue value)
    {
        InitializeComponent();
        ValueTextBox.Text = value.Value;

        _usedValues = usedValues;
        Value = value;
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        var value = GetValue();

        if (IsValueUsed(value))
        {
            ShowErrorMessageBox("Данное значение уже есть в домене");
            return;
        }

        Value = value;
        DialogResult = DialogResult.OK;
    }

    private void ValueTextBox_TextChanged(object sender, EventArgs e)
    {
        var value = GetValue();
        OkButton.Enabled = !string.IsNullOrWhiteSpace(value.Value);
    }

    private DomainValue GetValue() => new(ValueTextBox.Text.Trim());

    private bool IsValueUsed(DomainValue value) => _usedValues.Contains(value) && value != Value;

    private static void ShowErrorMessageBox(string message) => MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
}