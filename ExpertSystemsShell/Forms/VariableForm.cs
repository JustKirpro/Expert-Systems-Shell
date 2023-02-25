using System;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;
using ExpertSystemsShell.Components;

namespace ExpertSystemsShell.Forms;

public partial class VariableForm : Form
{
    private readonly KnowledgeBase _knowledgeBase;

    private string _questionText = string.Empty;

    public Variable? Variable { get; private set; }

    #region Constructors

    public VariableForm(KnowledgeBase knowledgeBase)
    {
        InitializeComponent();
        Text = "Создание переменной";
        OkButton.Enabled = false;
        RequestedOption.Checked = true;
        VariableNameTextBox.Text = knowledgeBase.GetNextVariableName();

        _knowledgeBase = knowledgeBase;

        InitializeDomainsComboBox(knowledgeBase);
    }

    public VariableForm(KnowledgeBase knowledgeBase, Variable variable)
    {
        InitializeComponent();
        Text = "Редактирование переменной";

        _knowledgeBase = knowledgeBase;
        Variable = variable;

                InitializeControls(knowledgeBase, variable);
    }

    #endregion

    #region Button events

    private void OkButton_Click(object sender, EventArgs e)
    {
        var name = GetName();

        if (IsNameUsed(name))
        {
            ShowErrorMessageBox($"Переменная с именем \"{name}\" уже существует");
            return;
        }

        var question = GetQuestion();

        if (IsQuestionSkipped(question) && !IsDefaultQuestion(name))
        {
            question = string.Empty;
        }
        else
        {
            question = GetFinalQuestion(question);
        }

        var domain = GetDomain();
        var variableType = GetVariableType();

        SetVariable(name, question, domain, variableType);
        DialogResult = DialogResult.OK;
    }

    private void AddDomainButton_Click(object sender, EventArgs e)
    {
        using var domainForm = new DomainForm(_knowledgeBase);
        var result = domainForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var domain = domainForm.Domain!;
            _knowledgeBase.Domains.Add(domain);

            AddDomainToComboBox(domain);
        }
    }

    #endregion

    #region TextBoxes, RadioButtons and ComboBox events

    private void VariableNameTextBox_TextChanged(object sender, EventArgs e) => UpdateOkButtonAvailability();

    private void QuestionTextBox_TextChanged(object sender, EventArgs e)
    {
        var question = GetQuestion();

        if (IsQuestionValid(question))
        {
            _questionText = question;
        }
    }

    private void DomainComboBox_SelectedIndexChanged(object sender, EventArgs e) => UpdateOkButtonAvailability();

    private void RequestedOption_CheckedChanged(object sender, EventArgs e) => UpdateRadioButtons();

    private void InferredOption_CheckedChanged(object sender, EventArgs e) => UpdateRadioButtons();

    private void RequestedInferredOption_CheckedChanged(object sender, EventArgs e) => UpdateRadioButtons();

    #endregion

    #region Utility methods

    private void SetVariable(string name, string question, Domain domain, VariableType variableType)
    {
        if (Variable is null)
        {
            Variable = new Variable(name, question, domain, variableType);
            return;
        }

        Variable.Name = name;
        Variable.Question = question;
        Variable.Domain = domain;
        Variable.Type = variableType;
    }

    private string GetName() => VariableNameTextBox.Text.Trim();

    private bool IsNameUsed(string name) => _knowledgeBase.IsVariableNameUsed(name) && name != Variable?.Name;

    private string GetQuestion() => QuestionTextBox.Text.Trim();

    private static bool IsQuestionValid(string question) => !string.IsNullOrWhiteSpace(question);

    private string GetFinalQuestion(string question)
    {
        var variableType = GetVariableType();

        if (variableType is VariableType.Inferred)
        {
            return string.Empty;
        }

        var name = GetName();

        return IsQuestionValid(question) ? question : $"{name}?";
    }

    private bool IsQuestionSkipped(string question) => IsQuestionAvailable() && !IsQuestionValid(question);

    private bool IsQuestionAvailable() => RequestedOption.Checked || InferredRequestedOption.Checked;

    private static bool IsDefaultQuestion(string name) => MessageBox.Show($"Вы не ввели вопрос для переменной. Использовать вопрос по умолчанию: \"{name}?\"", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

    private Domain GetDomain()
    {
        var name = DomainComboBox.SelectedItem.ToString()!;
        return _knowledgeBase.GetDomainByName(name);
    }

    private VariableType GetVariableType()
    {
        if (RequestedOption.Checked)
        {
            return VariableType.Requested;
        }
        
        return InferredOption.Checked ? VariableType.Inferred : VariableType.InferredRequested;
    }

    private void InitializeControls(KnowledgeBase knowledgeBase, Variable variable)
    {
        VariableNameTextBox.Text = variable.Name;

        InitializeDomainsComboBox(knowledgeBase);
        InitializeRadioButtons(variable);

        QuestionTextBox.Text = variable.Question;
    }

    private void InitializeDomainsComboBox(KnowledgeBase knowledgeBase)
    {
        var domains = knowledgeBase.Domains;

        foreach (var domain in domains)
        {
            DomainComboBox.Items.Add(domain.Name);

            if (domain == Variable?.Domain)
            {
                DomainComboBox.SelectedItem = domain.Name;
            }
        }
    }

    private void InitializeRadioButtons(Variable variable)
    {
        switch (variable.Type)
        {
            case VariableType.Requested:
                RequestedOption.Checked = true;
                return;
            case VariableType.Inferred:
                InferredOption.Checked = true;
                return;
            case VariableType.InferredRequested:
                InferredRequestedOption.Checked = true;
                return;
        }
    }

    private bool IsDomainSelected() => DomainComboBox.SelectedIndex > -1;

    private void AddDomainToComboBox(Domain domain)
    {
        DomainComboBox.Items.Add(domain.Name);
        DomainComboBox.SelectedItem = domain.Name;
    }

    private void UpdateQuestionBoxAvailability() => QuestionTextBox.Enabled = !InferredOption.Checked;

    private void UpdateOkButtonAvailability()
    {
        var name = GetName();
        OkButton.Enabled = !string.IsNullOrWhiteSpace(name) && IsDomainSelected();
    }

    private void UpdateRadioButtons()
    {
        QuestionTextBox.Text = IsQuestionAvailable() ? _questionText : string.Empty;

        UpdateQuestionBoxAvailability();
        UpdateOkButtonAvailability();
    }

    private static void ShowErrorMessageBox(string message) => MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    #endregion
}