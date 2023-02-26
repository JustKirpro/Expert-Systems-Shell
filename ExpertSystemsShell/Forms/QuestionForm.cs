using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Forms;

public partial class QuestionForm : Form
{
    private readonly List<DomainValue> _values;

    public DomainValue? Value { get; private set; }

    public QuestionForm(Variable variable)
    {
        InitializeComponent();
        _values = variable.Domain.Values;

        InitializeOptionsComboBox(variable);
        questionLabel.Text = variable.Question;
    }

    private void SelectButton_Click(object sender, EventArgs e)
    {
        var option = GetSelectedOption();
        Value = _values.First(v => v.Value == option);

        DialogResult = DialogResult.OK;
    }

    private void OptionsComboBox_SelectedIndexChanged(object sender, EventArgs e) => SelectButton.Enabled = OptionsComboBox.SelectedIndex > -1;

    private void QuestionForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && SelectButton.Enabled)
        {
            SelectButton.PerformClick();
        }
    }

    private void InitializeOptionsComboBox(Variable variable)
    {
        var values = variable.Domain.Values;

        foreach (var value in values)
        {
            OptionsComboBox.Items.Add(value.Value);
        }

        OptionsComboBox.SelectedItem = values[0];
        OptionsComboBox.SelectedIndex = 0;
    }

    private string? GetSelectedOption() => OptionsComboBox.SelectedItem as string;
}