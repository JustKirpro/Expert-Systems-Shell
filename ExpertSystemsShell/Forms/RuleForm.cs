using System;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Forms
{
    public partial class RuleForm : Form
    {
        public Rule Rule { get; private set; }

        public RuleForm()
        {
            InitializeComponent();
            Rule = new Rule();
            Text = "Создание нового правила";
        }

        public RuleForm(Rule currentRule)
        {
            InitializeComponent();
            Rule = currentRule;
            Text = "Редактирование правила";
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
            using var factForm = new FactForm();
            var result = factForm.ShowDialog();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {

        }
    }
}