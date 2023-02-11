using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExpertSystemsShell.Forms;

public partial class ValueForm : Form
{
    private readonly List<string> _usedValues;

    public string Value { get; private set; }

    public ValueForm(List<string> usedValues, string domainValue)
    {
        InitializeComponent();
        ValueTextBox.Text = domainValue;

        _usedValues = usedValues;
        Value = domainValue;
    }

    private void ValueTextBox_TextChanged(object sender, EventArgs e)
    {
        OkButton.Enabled = !string.IsNullOrWhiteSpace(ValueTextBox.Text);
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        var value = ValueTextBox.Text;

        if (_usedValues.Contains(value) && value != Value)
        {
            MessageBox.Show("Данное значение уже есть в домене", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Value = value;
        DialogResult = DialogResult.OK;
    }
}