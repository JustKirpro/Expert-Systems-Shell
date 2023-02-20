using ExpertSystemsShell.Components;
using System;
using System.Windows.Forms;

namespace ExpertSystemsShell.Forms
{
    public partial class ConsultationForm : Form
    {
        private readonly KnowledgeBase _knowledgeBase;

        private readonly InferenceEngine _inferenceEngine;

        public WorkingMemory? WorkingMemory { get; set; }

        public ConsultationForm(KnowledgeBase knowledgeBase)
        {
            InitializeComponent();

            _knowledgeBase = knowledgeBase;
            _inferenceEngine = new(knowledgeBase);

            SetGoalVariables();
            Write("Выберите цель консультации");
        }

        private void AnswerButton_Click(object sender, EventArgs e)
        {
            if (_inferenceEngine.GoalVariabe is null)
            {
                var name = GetOption();
                var variable = _knowledgeBase.GetVariableByName(name);
                _inferenceEngine.GoalVariabe = variable;

                SetOptions();
            }
        }

        private void SetGoalVariables()
        {
            var variables = _knowledgeBase.GetGoalVariables();

            foreach (var variable in variables)
            {
                OptionsComboBox.Items.Add(variable.Name);
            }
        }

        private void SetOptions()
        {
            var variable = _inferenceEngine.GoalVariabe;
            var options = variable!.Domain.Values;

            OptionsComboBox.Items.Clear();

            foreach (var option in options)
            {
                OptionsComboBox.Items.Add(option.Value);
            }
        }

        private string GetOption() => OptionsComboBox.Text;

        private void Write(string message) => ConsultationListBox.Items.Add(message);
    }
}