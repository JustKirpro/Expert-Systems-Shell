using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Forms;
public partial class VariableForm : Form
{
    private readonly List<string> _usedNames;

    private readonly Domains _domains;

    private string? _questionText;

    public Variable Variable { get; private set; } = null!;

    public VariableForm(List<string> usedNames, Domains domains)
    {
        InitializeComponent();
        Text = "Создание переменной";
        OkButton.Enabled = false;
        RequestedOption.Checked = true;

        _usedNames = usedNames;
        _domains = domains;

        InitializeDomainsComboBox();
    }

    public VariableForm(List<string> usedNames, Variable variable, Domains domains)
    {
        InitializeComponent();
        Text = "Редактирование переменной";

        _usedNames = usedNames;
        _domains = domains;
        Variable = variable;

        InitializeComponents();
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        var name = GetName();

        if (IsNameUsed(name))
        {
            ShowErrorMessageBox("Переменная с данным именем уже существует");
            return;
        }

        var question = GetQuestion();

        if (IsQuestionSkipped(question) && !IsQuestionValid())
        {
            return;
        }

        var domain = GetDomain();
        VariableType variableType = GetVariableType();

        SetVariable(name, question, domain, variableType);
        DialogResult = DialogResult.OK;
    }

    private void AddDomainButton_Click(object sender, EventArgs e)
    {
        var usedNames = _domains.GetNames();

        using var domainForm = new DomainForm(usedNames);
        var result = domainForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var newDomain = domainForm.Domain;
            _domains.Add(newDomain);

            DomainComboBox.SelectedItem = newDomain;
        }
    }

    private void VariableNameTextBox_TextChanged(object sender, EventArgs e) => UpdateOkButtonAvailability();

    private void DomainComboBox_SelectedIndexChanged(object sender, EventArgs e) => UpdateOkButtonAvailability();

    private void RequestedOption_CheckedChanged(object sender, EventArgs e) => UpdateRadioButtons();

    private void InferredOption_CheckedChanged(object sender, EventArgs e) => UpdateRadioButtons();

    private void RequestedInferredOption_CheckedChanged(object sender, EventArgs e) => UpdateRadioButtons();

    private void SetVariable(string name, string? question, Domain domain, VariableType variableType)
    {
        if (Variable is null)
        {
            Variable = question is null ? new(name, domain, variableType) : new(name, question, domain, variableType);
            return;
        }

        Variable.Name = name;
        Variable.Domain = domain;
        Variable.Type = variableType;

        if (question is not null)
        {
            Variable.Question = question;
        }
    }

    private string GetName() => VariableNameTextBox.Text.Trim();

    private bool IsNameUsed(string name) => _usedNames.Contains(name) && name != Variable?.Name;

    private string? GetQuestion() => QuestionTextBox.Text.Trim();

    private bool IsQuestionSkipped(string? question) => IsQuestionAvailable() && string.IsNullOrWhiteSpace(question);

    private static bool IsQuestionValid()
    {
        var result = MessageBox.Show("Вы не ввели вопрос для переменной, продолжить без него?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        return result == DialogResult.Yes;
    }

    private bool IsQuestionAvailable() => RequestedOption.Checked || RequestedInferredOption.Checked;

    private Domain GetDomain()
    {
        var name = DomainComboBox.SelectedItem.ToString()!;
        return _domains.GetDomainByName(name)!;
    }

    private VariableType GetVariableType()
    {
        if (RequestedInferredOption.Checked)
        {
            return VariableType.Requested;
        }
        else if (InferredOption.Checked)
        {
            return VariableType.RequestedInferred;
        }
        else
        {
            return VariableType.RequestedInferred;
        }
    }

    private void InitializeComponents()
    {
        VariableNameTextBox.Text = Variable.Name;

        InitializeDomainsComboBox();
        InitializeRadioButtons();

        if (Variable.Question is not null)
        {
            QuestionTextBox.Text = Variable.Question;
        }
    }

    private void InitializeDomainsComboBox()
    {
        foreach (var domain in _domains)
        {
            DomainComboBox.Items.Add(domain.Name);

            if (domain == Variable?.Domain)
            {
                DomainComboBox.SelectedItem = domain.Name;
            }
        }
    }

    private void InitializeRadioButtons()
    {
        if (Variable.Type == VariableType.Requested)
        {
            RequestedOption.Checked = true;
        }
        else if (Variable.Type == VariableType.Inferred)
        {
            InferredOption.Checked = true;
        }
        else
        {
            RequestedInferredOption.Checked = true;
        }
    }

    private static void ShowErrorMessageBox(string message) => MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    private bool IsComboboxItemSelected() => DomainComboBox.SelectedIndex > -1;

    private void UpdateQuestionBoxAvailability() => QuestionTextBox.Enabled = !InferredOption.Checked;

    private void UpdateOkButtonAvailability()
    {
        var name = GetName();
        OkButton.Enabled = !string.IsNullOrWhiteSpace(name) && IsComboboxItemSelected();
    }

    private void UpdateRadioButtons()
    {
        if (IsQuestionAvailable())
        {
            QuestionTextBox.Text = _questionText;
        }
        else
        {
            QuestionTextBox.Text = string.Empty;
        }

        UpdateQuestionBoxAvailability();
        UpdateOkButtonAvailability();
    }

    private void QuestionTextBox_TextChanged(object sender, EventArgs e)
    {
        var question = GetQuestion();

        if (!string.IsNullOrWhiteSpace(question))
        {
            _questionText = QuestionTextBox.Text.Trim();
        }
    }
}