using System;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Forms
{
    public partial class RuleForm : Form
    {
        public Rule Rule { get; private set; } = null!;

        private Variables _variables;

        private Domains _domains;

        public RuleForm(Variables variables, Domains domains)
        {
            InitializeComponent();
            Text = "Создание нового правила";
            _variables= variables;
            _domains = domains;
        }

        public RuleForm(Variables variables, Domains domains, Rule currentRule)
        {
            InitializeComponent();
            Rule = currentRule;
            Text = "Редактирование правила";

            _variables = variables;
            _domains = domains;
        }

        /*private void InitializeListBoxes()
        {
            foreach (var statement in Rule.CondtionPart)
            {
                var listBoxItem = new ListViewItem();

            }
        } */

        private void CondtionPartAddButton_Click(object sender, EventArgs e)
        {
            using var factForm = new FactForm(_variables, _domains);
            var result = factForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                var fact = factForm.Fact;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {

        }

        private void CondtionPartEditButton_Click(object sender, EventArgs e)
        {
            //using var factForm = new FactForm(_variables, _domains, new Fact(_variables.g, "100");
           // var result = factForm.ShowDialog();
        }
    }
}