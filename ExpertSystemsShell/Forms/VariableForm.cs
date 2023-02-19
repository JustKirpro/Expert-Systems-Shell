using System;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;
using ExpertSystemsShell.Modules;

namespace ExpertSystemsShell.Forms;

public partial class VariableForm : Form
{
    private readonly KnowledgeBase _knowledgeBase;

    private string _questionText = string.Empty;

    public Variable? Variable { get; private set; }

    public VariableForm(KnowledgeBase knowledgeBase)
    {
        InitializeComponent();
        Text = "Создание переменной";
        OkButton.Enabled = false;
        RequestedOption.Checked = true;

        _knowledgeBase = knowledgeBase;

        InitializeDomainsComboBox();
    }

    public VariableForm(KnowledgeBase knowledgeBase, Variable variable)
    {
        InitializeComponent();
        Text = "Редактирование переменной";

        _knowledgeBase = knowledgeBase;
        Variable = variable;

        InitializeComponents();
    }

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
            return;
        }

        question = GetFinalQuestion(question);
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
            _knowledgeBase.AddDomain(domain);

            AddDomainToComboBox(domain);
        }
    }

    private void VariableNameTextBox_TextChanged(object sender, EventArgs e) => UpdateOkButtonAvailability();

    private void QuestionTextBox_TextChanged(object sender, EventArgs e)
    {
        var question = GetQuestion();

        if (!string.IsNullOrWhiteSpace(question))
        {
            _questionText = QuestionTextBox.Text.Trim();
        }
    }

    private void DomainComboBox_SelectedIndexChanged(object sender, EventArgs e) => UpdateOkButtonAvailability();

    private void RequestedOption_CheckedChanged(object sender, EventArgs e) => UpdateRadioButtons();

    private void InferredOption_CheckedChanged(object sender, EventArgs e) => UpdateRadioButtons();

    private void RequestedInferredOption_CheckedChanged(object sender, EventArgs e) => UpdateRadioButtons();

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

    private string GetFinalQuestion(string question)
    {
        var variableType = GetVariableType();

        if (variableType is VariableType.Inferred)
        {
            return string.Empty;
        }

        var name = GetName();

        return string.IsNullOrWhiteSpace(question) ? $"{name}?" : question;
    }

    private bool IsQuestionSkipped(string question) => IsQuestionAvailable() && string.IsNullOrWhiteSpace(question);

    private static bool IsDefaultQuestion(string name)
    {
        var result = MessageBox.Show($"Вы не ввели вопрос для переменной. Использовать вопрос по умолчанию: \"{name}?\"",
            "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        return result == DialogResult.Yes;
    }

    private bool IsQuestionAvailable() => RequestedOption.Checked || RequestedInferredOption.Checked;

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
        
        return InferredOption.Checked ? VariableType.Inferred : VariableType.RequestedInferred;
    }

    private void InitializeComponents()
    {
        VariableNameTextBox.Text = Variable!.Name;

        InitializeDomainsComboBox();
        InitializeRadioButtons();

        QuestionTextBox.Text = Variable.Question;
    }

    private void InitializeDomainsComboBox()
    {
        var domains = _knowledgeBase.Domains;

        foreach (var domain in domains)
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
        switch (Variable!.Type)
        {
            case VariableType.Requested:
                RequestedOption.Checked = true;
                return;
            case VariableType.Inferred:
                InferredOption.Checked = true;
                return;
            case VariableType.RequestedInferred:
                RequestedInferredOption.Checked = true;
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
}