using System;
using System.Windows.Forms;

namespace ExpertSystemsShell.Forms;

public partial class ConsultationForm : Form
{
    private readonly ExpertSystemShell _expertSystemShell;

    public ConsultationForm(ExpertSystemShell expertSystemShell)
    {
        InitializeComponent();

        _expertSystemShell = expertSystemShell;
        SetOptions();
    }

    private void SelectButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var option = GetSelectedOption();
        var variable = knowledgeBase.GetVariableByName(option);

        var inferredVariable = _expertSystemShell.InferVariable(variable);

        if (inferredVariable is null)
        {
            MessageBox.Show("Цель консультации не была достигнута. Обратитесь к другой ЭС", "Результаты", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        MessageBox.Show($"Цель консультации достигнута!\n {variable.Name} - {inferredVariable.Value.Value}", "Результаты", MessageBoxButtons.OK, MessageBoxIcon.Information);

        OptionGroupBox.Visible = false;
        ButtonsGroupBox.Visible = true;
    }

    private void NewConsultationButton_Click(object sender, EventArgs e)
    {
        ButtonsGroupBox.Visible = false;
        OptionGroupBox.Visible = true;
    }

    private void ShowExplanationButton_Click(object sender, EventArgs e) => _expertSystemShell.ShowExplanation();

    private void OptionsComboBox_SelectedIndexChanged(object sender, EventArgs e) => SelectButton.Enabled = OptionsComboBox.SelectedIndex > -1;

    private void ConsultationForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && SelectButton.Enabled)
        {
            SelectButton.PerformClick();
        }
    }

    private void SetOptions()
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var goalVariables = knowledgeBase.GetGoalVariables();

        foreach (var goalVariable in goalVariables)
        {
            OptionsComboBox.Items.Add(goalVariable.Name);
        }

        if (goalVariables.Count > 0)
        {
            OptionsComboBox.SelectedItem = goalVariables[0];
            OptionsComboBox.SelectedIndex = 0;
        }
    }

    private string GetSelectedOption() => OptionsComboBox.Text;
}