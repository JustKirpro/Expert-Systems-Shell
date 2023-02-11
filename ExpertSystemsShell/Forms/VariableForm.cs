using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Forms
{
    public partial class VariableForm : Form
    {
        private readonly List<string> _usedNames;

        private readonly Domains _domains;

        public Variable Variable { get; private set; }

        public VariableForm(List<string> usedNames, Domains domains)
        {
            InitializeComponent();
            Text = "Создание переменной";
            OkButton.Enabled = false;
            RequestedOption.Checked = true;

            _usedNames = usedNames;
            _domains = domains;
            Variable = new Variable();
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

                if (domain == Variable.Domain)
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
            else if (Variable.Type != VariableType.RequestedInferred)
            {
                RequestedInferredOption.Checked = true;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!ValidateName(out string name) || !ValidateQuestion(out string question))
            {
                return;
            }

            var domain = GetDomain();
            VariableType variableType = GetVariableType();

            Variable = new Variable
            {
                Name = name,
                Domain = domain,
                Type = variableType,
            };

            if (variableType is not VariableType.Inferred)
            {
                Variable.Question = question.Trim();
            }

            DialogResult = DialogResult.OK;
        }

        private bool ValidateName(out string name)
        {
            name = VariableNameTextBox.Text;

            if (_usedNames.Contains(name) && name != Variable.Name)
            {
                MessageBox.Show("Переменная с данным именем уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool ValidateQuestion(out string question)
        {
            question = QuestionTextBox.Text;

            if ((RequestedOption.Checked || RequestedInferredOption.Checked) && string.IsNullOrWhiteSpace(question))
            {
                var result = MessageBox.Show("Вы не ввели вопрос для переменной, продолжить без него?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No || result == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        private Domain GetDomain() => _domains.GetDomainByName(DomainComboBox.SelectedItem.ToString()!)!;

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

        private void DomainComboBox_SelectedIndexChanged(object sender, EventArgs e) => UpdateOkButtonAvailability();

        private void VariableNameTextBox_TextChanged(object sender, EventArgs e) => UpdateOkButtonAvailability();

        private void RequestedOption_CheckedChanged(object sender, EventArgs e) => UpdateRadioButton();

        private void InferredOption_CheckedChanged(object sender, EventArgs e) => UpdateRadioButton();

        private void RequestedInferredOption_CheckedChanged(object sender, EventArgs e) => UpdateRadioButton();

        private void UpdateRadioButton()
        {
            UpdateQuestionBoxAvailability();
            UpdateOkButtonAvailability();
        }

        private bool IsComboboxItemSelected() => DomainComboBox.SelectedIndex > -1;

        private void UpdateOkButtonAvailability() => OkButton.Enabled = !string.IsNullOrWhiteSpace(VariableNameTextBox.Text) && IsComboboxItemSelected();

        private void UpdateQuestionBoxAvailability() => QuestionTextBox.Enabled = !InferredOption.Checked;
    }
}