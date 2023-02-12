using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExpertSystemsShell.Forms;

public partial class ValueForm : Form
{
    private readonly List<string> _usedValues;

    public string Value { get; private set; }

    public ValueForm(List<string> usedValues, string value)
    {
        InitializeComponent();
        ValueTextBox.Text = value;

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
        OkButton.Enabled = !string.IsNullOrWhiteSpace(value);
    }

    private string GetValue() => ValueTextBox.Text.Trim();

    private bool IsValueUsed(string value) => _usedValues.Contains(value) && value != Value;

    private static void ShowErrorMessageBox(string message) => MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
}