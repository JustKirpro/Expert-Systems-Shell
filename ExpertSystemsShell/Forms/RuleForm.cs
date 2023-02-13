using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Forms
{
    public partial class RuleForm : Form
    {
        private readonly Variables _variables;

        private readonly Domains _domains;

        private readonly List<Fact> _conditionPart = new();

        private readonly List<Fact> _actionPart = new();

        public Rule Rule { get; private set; } = null!;

        public RuleForm(Variables variables, Domains domains)
        {
            InitializeComponent();
            Text = "Создание правила";

            _variables= variables;
            _domains = domains;
        }

        public RuleForm(Variables variables, Domains domains, Rule rule)
        {
            InitializeComponent();
            Text = "Редактирование правила";

            _variables = variables;
            _domains = domains;
            Rule = rule;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {

        }

        private void CondtionPartAddButton_Click(object sender, EventArgs e)
        {
            using var factForm = new FactForm(_variables, _domains);
            var result = factForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                var fact = factForm.Fact;

                _conditionPart.Add(fact);


            }
        }

        private void CondtionPartEditButton_Click(object sender, EventArgs e)
        {
            //using var factForm = new FactForm(_variables, _domains, new Fact(_variables.g, "100");
           // var result = factForm.ShowDialog();
        }

        private void CondtionPartDeleteButton_Click(object sender, EventArgs e)
        {

        }

        private void ConditionPartListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ActionPartAddButton_Click(object sender, EventArgs e)
        {

        }

        private void ActionPartEditButton_Click(object sender, EventArgs e)
        {

        }

        private void ActionPartDeleteButton_Click(object sender, EventArgs e)
        {

        }

        private void ActionPartListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RuleNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ReasonTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}