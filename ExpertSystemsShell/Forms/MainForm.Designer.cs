using System.Windows.Forms;

namespace ExpertSystemsShell.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuConsultation = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuConsultationStart = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuConsultationExplain = new System.Windows.Forms.ToolStripMenuItem();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.RulesTab = new System.Windows.Forms.TabPage();
            this.RulesListView = new System.Windows.Forms.ListView();
            this.ActionPartGroupBox = new System.Windows.Forms.GroupBox();
            this.ActionPartListBox = new System.Windows.Forms.ListBox();
            this.ConditionPartGroupBox = new System.Windows.Forms.GroupBox();
            this.ConditionPartListBox = new System.Windows.Forms.ListBox();
            this.EditRuleGroupBox = new System.Windows.Forms.GroupBox();
            this.DeleteRuleButton = new System.Windows.Forms.Button();
            this.EditRuleButton = new System.Windows.Forms.Button();
            this.AddRuleButton = new System.Windows.Forms.Button();
            this.VariablesTab = new System.Windows.Forms.TabPage();
            this.VariablesListView = new System.Windows.Forms.ListView();
            this.QuestionTextGroupBox = new System.Windows.Forms.GroupBox();
            this.QuestionListBox = new System.Windows.Forms.ListBox();
            this.DomainValuesGroupBox = new System.Windows.Forms.GroupBox();
            this.DomainValuesListBox = new System.Windows.Forms.ListBox();
            this.EditVariableGroupBox = new System.Windows.Forms.GroupBox();
            this.DeleteVariableButton = new System.Windows.Forms.Button();
            this.EditVariableButton = new System.Windows.Forms.Button();
            this.AddVariableButton = new System.Windows.Forms.Button();
            this.DomainsTab = new System.Windows.Forms.TabPage();
            this.DomainsListView = new System.Windows.Forms.ListView();
            this.ValuesGroupBox = new System.Windows.Forms.GroupBox();
            this.ValuesListBox = new System.Windows.Forms.ListBox();
            this.EditDomainGroupBox = new System.Windows.Forms.GroupBox();
            this.DeleteDomainButton = new System.Windows.Forms.Button();
            this.EditDomainButton = new System.Windows.Forms.Button();
            this.AddDomainButton = new System.Windows.Forms.Button();
            this.MenuStrip.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.RulesTab.SuspendLayout();
            this.ActionPartGroupBox.SuspendLayout();
            this.ConditionPartGroupBox.SuspendLayout();
            this.EditRuleGroupBox.SuspendLayout();
            this.VariablesTab.SuspendLayout();
            this.QuestionTextGroupBox.SuspendLayout();
            this.DomainValuesGroupBox.SuspendLayout();
            this.EditVariableGroupBox.SuspendLayout();
            this.DomainsTab.SuspendLayout();
            this.ValuesGroupBox.SuspendLayout();
            this.EditDomainGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuConsultation});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(1492, 40);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // MenuFile
            // 
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFileNew,
            this.MenuFileOpen,
            this.MenuFileSave,
            this.MenuFileSaveAs,
            this.MenuFileExit});
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(90, 38);
            this.MenuFile.Text = "Файл";
            // 
            // MenuFileNew
            // 
            this.MenuFileNew.Name = "MenuFileNew";
            this.MenuFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.MenuFileNew.Size = new System.Drawing.Size(343, 44);
            this.MenuFileNew.Text = "Новый...";
            this.MenuFileNew.Click += new System.EventHandler(this.MenuFileNew_Click);
            // 
            // MenuFileOpen
            // 
            this.MenuFileOpen.Name = "MenuFileOpen";
            this.MenuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.MenuFileOpen.Size = new System.Drawing.Size(343, 44);
            this.MenuFileOpen.Text = "Открыть...";
            this.MenuFileOpen.Click += new System.EventHandler(this.MenuFileOpen_Click);
            // 
            // MenuFileSave
            // 
            this.MenuFileSave.Name = "MenuFileSave";
            this.MenuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.MenuFileSave.Size = new System.Drawing.Size(343, 44);
            this.MenuFileSave.Text = "Сохранить";
            this.MenuFileSave.Click += new System.EventHandler(this.MenuFileSave_Click);
            // 
            // MenuFileSaveAs
            // 
            this.MenuFileSaveAs.Name = "MenuFileSaveAs";
            this.MenuFileSaveAs.Size = new System.Drawing.Size(343, 44);
            this.MenuFileSaveAs.Text = "Сохранить как...";
            this.MenuFileSaveAs.Click += new System.EventHandler(this.MenuFileSaveAs_Click);
            // 
            // MenuFileExit
            // 
            this.MenuFileExit.Name = "MenuFileExit";
            this.MenuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.MenuFileExit.Size = new System.Drawing.Size(343, 44);
            this.MenuFileExit.Text = "Выход";
            this.MenuFileExit.Click += new System.EventHandler(this.MenuFileExit_Click);
            // 
            // MenuConsultation
            // 
            this.MenuConsultation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuConsultationStart,
            this.MenuConsultationExplain});
            this.MenuConsultation.Name = "MenuConsultation";
            this.MenuConsultation.Size = new System.Drawing.Size(186, 36);
            this.MenuConsultation.Text = "Консультация";
            // 
            // MenuConsultationStart
            // 
            this.MenuConsultationStart.Name = "MenuConsultationStart";
            this.MenuConsultationStart.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.MenuConsultationStart.Size = new System.Drawing.Size(428, 44);
            this.MenuConsultationStart.Text = "Начать консультацию";
            this.MenuConsultationStart.Click += new System.EventHandler(this.MenuConsultationStart_Click);
            // 
            // MenuConsultationExplain
            // 
            this.MenuConsultationExplain.Enabled = false;
            this.MenuConsultationExplain.Name = "MenuConsultationExplain";
            this.MenuConsultationExplain.Size = new System.Drawing.Size(428, 44);
            this.MenuConsultationExplain.Text = "Показать объяснение";
            this.MenuConsultationExplain.Click += new System.EventHandler(this.MenuConsultationExplain_Click);
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.RulesTab);
            this.TabControl.Controls.Add(this.VariablesTab);
            this.TabControl.Controls.Add(this.DomainsTab);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 40);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(1492, 882);
            this.TabControl.TabIndex = 1;
            // 
            // RulesTab
            // 
            this.RulesTab.Controls.Add(this.RulesListView);
            this.RulesTab.Controls.Add(this.ActionPartGroupBox);
            this.RulesTab.Controls.Add(this.ConditionPartGroupBox);
            this.RulesTab.Controls.Add(this.EditRuleGroupBox);
            this.RulesTab.Location = new System.Drawing.Point(8, 46);
            this.RulesTab.Name = "RulesTab";
            this.RulesTab.Padding = new System.Windows.Forms.Padding(3);
            this.RulesTab.Size = new System.Drawing.Size(1476, 828);
            this.RulesTab.TabIndex = 0;
            this.RulesTab.Text = "Правила";
            this.RulesTab.UseVisualStyleBackColor = true;
            // 
            // RulesListView
            // 
            this.RulesListView.AllowDrop = true;
            this.RulesListView.FullRowSelect = true;
            this.RulesListView.Location = new System.Drawing.Point(0, 0);
            this.RulesListView.MultiSelect = false;
            this.RulesListView.Name = "RulesListView";
            this.RulesListView.Size = new System.Drawing.Size(889, 823);
            this.RulesListView.TabIndex = 8;
            this.RulesListView.UseCompatibleStateImageBehavior = false;
            this.RulesListView.View = System.Windows.Forms.View.Details;
            this.RulesListView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.RulesListView_ItemDrag);
            this.RulesListView.SelectedIndexChanged += new System.EventHandler(this.RulesListView_SelectedIndexChanged);
            this.RulesListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.RulesListView_DragDrop);
            this.RulesListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.RulesListView_DragEnter);
            // 
            // ActionPartGroupBox
            // 
            this.ActionPartGroupBox.Controls.Add(this.ActionPartListBox);
            this.ActionPartGroupBox.Location = new System.Drawing.Point(889, 589);
            this.ActionPartGroupBox.Name = "ActionPartGroupBox";
            this.ActionPartGroupBox.Size = new System.Drawing.Size(587, 239);
            this.ActionPartGroupBox.TabIndex = 5;
            this.ActionPartGroupBox.TabStop = false;
            this.ActionPartGroupBox.Text = "Заключение";
            // 
            // ActionPartListBox
            // 
            this.ActionPartListBox.FormattingEnabled = true;
            this.ActionPartListBox.HorizontalScrollbar = true;
            this.ActionPartListBox.ItemHeight = 32;
            this.ActionPartListBox.Location = new System.Drawing.Point(3, 38);
            this.ActionPartListBox.Name = "ActionPartListBox";
            this.ActionPartListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.ActionPartListBox.Size = new System.Drawing.Size(578, 196);
            this.ActionPartListBox.TabIndex = 1;
            // 
            // ConditionPartGroupBox
            // 
            this.ConditionPartGroupBox.Controls.Add(this.ConditionPartListBox);
            this.ConditionPartGroupBox.Location = new System.Drawing.Point(889, 253);
            this.ConditionPartGroupBox.Name = "ConditionPartGroupBox";
            this.ConditionPartGroupBox.Size = new System.Drawing.Size(587, 330);
            this.ConditionPartGroupBox.TabIndex = 4;
            this.ConditionPartGroupBox.TabStop = false;
            this.ConditionPartGroupBox.Text = "Посылка";
            // 
            // ConditionPartListBox
            // 
            this.ConditionPartListBox.FormattingEnabled = true;
            this.ConditionPartListBox.HorizontalScrollbar = true;
            this.ConditionPartListBox.ItemHeight = 32;
            this.ConditionPartListBox.Location = new System.Drawing.Point(6, 32);
            this.ConditionPartListBox.Name = "ConditionPartListBox";
            this.ConditionPartListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.ConditionPartListBox.Size = new System.Drawing.Size(575, 292);
            this.ConditionPartListBox.TabIndex = 0;
            // 
            // EditRuleGroupBox
            // 
            this.EditRuleGroupBox.Controls.Add(this.DeleteRuleButton);
            this.EditRuleGroupBox.Controls.Add(this.EditRuleButton);
            this.EditRuleGroupBox.Controls.Add(this.AddRuleButton);
            this.EditRuleGroupBox.Location = new System.Drawing.Point(889, 3);
            this.EditRuleGroupBox.Name = "EditRuleGroupBox";
            this.EditRuleGroupBox.Size = new System.Drawing.Size(587, 247);
            this.EditRuleGroupBox.TabIndex = 3;
            this.EditRuleGroupBox.TabStop = false;
            this.EditRuleGroupBox.Text = "Редактирование";
            // 
            // DeleteRuleButton
            // 
            this.DeleteRuleButton.BackColor = System.Drawing.Color.Transparent;
            this.DeleteRuleButton.Enabled = false;
            this.DeleteRuleButton.Location = new System.Drawing.Point(6, 182);
            this.DeleteRuleButton.Name = "DeleteRuleButton";
            this.DeleteRuleButton.Size = new System.Drawing.Size(575, 46);
            this.DeleteRuleButton.TabIndex = 2;
            this.DeleteRuleButton.Text = "Удалить";
            this.DeleteRuleButton.UseVisualStyleBackColor = false;
            this.DeleteRuleButton.Click += new System.EventHandler(this.DeleteRuleButton_Click);
            // 
            // EditRuleButton
            // 
            this.EditRuleButton.BackColor = System.Drawing.Color.Transparent;
            this.EditRuleButton.Enabled = false;
            this.EditRuleButton.Location = new System.Drawing.Point(6, 112);
            this.EditRuleButton.Name = "EditRuleButton";
            this.EditRuleButton.Size = new System.Drawing.Size(575, 46);
            this.EditRuleButton.TabIndex = 1;
            this.EditRuleButton.Text = "Изменить";
            this.EditRuleButton.UseVisualStyleBackColor = false;
            this.EditRuleButton.Click += new System.EventHandler(this.EditRuleButton_Click);
            // 
            // AddRuleButton
            // 
            this.AddRuleButton.BackColor = System.Drawing.Color.Transparent;
            this.AddRuleButton.Location = new System.Drawing.Point(6, 38);
            this.AddRuleButton.Name = "AddRuleButton";
            this.AddRuleButton.Size = new System.Drawing.Size(575, 46);
            this.AddRuleButton.TabIndex = 0;
            this.AddRuleButton.Text = "Добавить";
            this.AddRuleButton.UseVisualStyleBackColor = false;
            this.AddRuleButton.Click += new System.EventHandler(this.AddRuleButton_Click);
            // 
            // VariablesTab
            // 
            this.VariablesTab.Controls.Add(this.VariablesListView);
            this.VariablesTab.Controls.Add(this.QuestionTextGroupBox);
            this.VariablesTab.Controls.Add(this.DomainValuesGroupBox);
            this.VariablesTab.Controls.Add(this.EditVariableGroupBox);
            this.VariablesTab.Location = new System.Drawing.Point(8, 46);
            this.VariablesTab.Name = "VariablesTab";
            this.VariablesTab.Padding = new System.Windows.Forms.Padding(3);
            this.VariablesTab.Size = new System.Drawing.Size(1476, 828);
            this.VariablesTab.TabIndex = 1;
            this.VariablesTab.Text = "Переменные";
            this.VariablesTab.UseVisualStyleBackColor = true;
            // 
            // VariablesListView
            // 
            this.VariablesListView.FullRowSelect = true;
            this.VariablesListView.Location = new System.Drawing.Point(0, 0);
            this.VariablesListView.Name = "VariablesListView";
            this.VariablesListView.Size = new System.Drawing.Size(889, 822);
            this.VariablesListView.TabIndex = 9;
            this.VariablesListView.UseCompatibleStateImageBehavior = false;
            this.VariablesListView.View = System.Windows.Forms.View.Details;
            this.VariablesListView.SelectedIndexChanged += new System.EventHandler(this.VariablesListView_SelectedIndexChanged);
            // 
            // QuestionTextGroupBox
            // 
            this.QuestionTextGroupBox.Controls.Add(this.QuestionListBox);
            this.QuestionTextGroupBox.Location = new System.Drawing.Point(889, 589);
            this.QuestionTextGroupBox.Name = "QuestionTextGroupBox";
            this.QuestionTextGroupBox.Size = new System.Drawing.Size(587, 239);
            this.QuestionTextGroupBox.TabIndex = 6;
            this.QuestionTextGroupBox.TabStop = false;
            this.QuestionTextGroupBox.Text = "Текст вопроса:";
            // 
            // QuestionListBox
            // 
            this.QuestionListBox.FormattingEnabled = true;
            this.QuestionListBox.HorizontalScrollbar = true;
            this.QuestionListBox.ItemHeight = 32;
            this.QuestionListBox.Location = new System.Drawing.Point(3, 38);
            this.QuestionListBox.Name = "QuestionListBox";
            this.QuestionListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.QuestionListBox.Size = new System.Drawing.Size(578, 196);
            this.QuestionListBox.TabIndex = 1;
            // 
            // DomainValuesGroupBox
            // 
            this.DomainValuesGroupBox.Controls.Add(this.DomainValuesListBox);
            this.DomainValuesGroupBox.Location = new System.Drawing.Point(889, 253);
            this.DomainValuesGroupBox.Name = "DomainValuesGroupBox";
            this.DomainValuesGroupBox.Size = new System.Drawing.Size(587, 330);
            this.DomainValuesGroupBox.TabIndex = 5;
            this.DomainValuesGroupBox.TabStop = false;
            this.DomainValuesGroupBox.Text = "Значения домена:";
            // 
            // DomainValuesListBox
            // 
            this.DomainValuesListBox.FormattingEnabled = true;
            this.DomainValuesListBox.HorizontalScrollbar = true;
            this.DomainValuesListBox.ItemHeight = 32;
            this.DomainValuesListBox.Location = new System.Drawing.Point(6, 32);
            this.DomainValuesListBox.Name = "DomainValuesListBox";
            this.DomainValuesListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.DomainValuesListBox.Size = new System.Drawing.Size(575, 292);
            this.DomainValuesListBox.TabIndex = 0;
            // 
            // EditVariableGroupBox
            // 
            this.EditVariableGroupBox.Controls.Add(this.DeleteVariableButton);
            this.EditVariableGroupBox.Controls.Add(this.EditVariableButton);
            this.EditVariableGroupBox.Controls.Add(this.AddVariableButton);
            this.EditVariableGroupBox.Location = new System.Drawing.Point(889, 3);
            this.EditVariableGroupBox.Name = "EditVariableGroupBox";
            this.EditVariableGroupBox.Size = new System.Drawing.Size(587, 247);
            this.EditVariableGroupBox.TabIndex = 4;
            this.EditVariableGroupBox.TabStop = false;
            this.EditVariableGroupBox.Text = "Редактирование";
            // 
            // DeleteVariableButton
            // 
            this.DeleteVariableButton.BackColor = System.Drawing.Color.Transparent;
            this.DeleteVariableButton.Enabled = false;
            this.DeleteVariableButton.Location = new System.Drawing.Point(6, 182);
            this.DeleteVariableButton.Name = "DeleteVariableButton";
            this.DeleteVariableButton.Size = new System.Drawing.Size(575, 46);
            this.DeleteVariableButton.TabIndex = 2;
            this.DeleteVariableButton.Text = "Удалить";
            this.DeleteVariableButton.UseVisualStyleBackColor = false;
            this.DeleteVariableButton.Click += new System.EventHandler(this.DeleteVariableButton_Click);
            // 
            // EditVariableButton
            // 
            this.EditVariableButton.BackColor = System.Drawing.Color.Transparent;
            this.EditVariableButton.Enabled = false;
            this.EditVariableButton.Location = new System.Drawing.Point(6, 112);
            this.EditVariableButton.Name = "EditVariableButton";
            this.EditVariableButton.Size = new System.Drawing.Size(575, 46);
            this.EditVariableButton.TabIndex = 1;
            this.EditVariableButton.Text = "Изменить";
            this.EditVariableButton.UseVisualStyleBackColor = false;
            this.EditVariableButton.Click += new System.EventHandler(this.EditVariableButton_Click);
            // 
            // AddVariableButton
            // 
            this.AddVariableButton.BackColor = System.Drawing.Color.Transparent;
            this.AddVariableButton.Location = new System.Drawing.Point(6, 38);
            this.AddVariableButton.Name = "AddVariableButton";
            this.AddVariableButton.Size = new System.Drawing.Size(575, 46);
            this.AddVariableButton.TabIndex = 0;
            this.AddVariableButton.Text = "Добавить";
            this.AddVariableButton.UseVisualStyleBackColor = false;
            this.AddVariableButton.Click += new System.EventHandler(this.AddVariableButton_Click);
            // 
            // DomainsTab
            // 
            this.DomainsTab.Controls.Add(this.DomainsListView);
            this.DomainsTab.Controls.Add(this.ValuesGroupBox);
            this.DomainsTab.Controls.Add(this.EditDomainGroupBox);
            this.DomainsTab.Location = new System.Drawing.Point(8, 46);
            this.DomainsTab.Name = "DomainsTab";
            this.DomainsTab.Padding = new System.Windows.Forms.Padding(3);
            this.DomainsTab.Size = new System.Drawing.Size(1476, 828);
            this.DomainsTab.TabIndex = 2;
            this.DomainsTab.Text = "Домены";
            this.DomainsTab.UseVisualStyleBackColor = true;
            // 
            // DomainsListView
            // 
            this.DomainsListView.FullRowSelect = true;
            this.DomainsListView.Location = new System.Drawing.Point(-3, 0);
            this.DomainsListView.MultiSelect = false;
            this.DomainsListView.Name = "DomainsListView";
            this.DomainsListView.Size = new System.Drawing.Size(892, 824);
            this.DomainsListView.TabIndex = 13;
            this.DomainsListView.UseCompatibleStateImageBehavior = false;
            this.DomainsListView.View = System.Windows.Forms.View.Details;
            this.DomainsListView.SelectedIndexChanged += new System.EventHandler(this.DomainsListView_SelectedIndexChanged);
            // 
            // ValuesGroupBox
            // 
            this.ValuesGroupBox.Controls.Add(this.ValuesListBox);
            this.ValuesGroupBox.Location = new System.Drawing.Point(889, 268);
            this.ValuesGroupBox.Name = "ValuesGroupBox";
            this.ValuesGroupBox.Size = new System.Drawing.Size(587, 556);
            this.ValuesGroupBox.TabIndex = 11;
            this.ValuesGroupBox.TabStop = false;
            this.ValuesGroupBox.Text = "Значения домена:";
            // 
            // ValuesListBox
            // 
            this.ValuesListBox.FormattingEnabled = true;
            this.ValuesListBox.HorizontalScrollbar = true;
            this.ValuesListBox.ItemHeight = 32;
            this.ValuesListBox.Location = new System.Drawing.Point(6, 32);
            this.ValuesListBox.Name = "ValuesListBox";
            this.ValuesListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.ValuesListBox.Size = new System.Drawing.Size(575, 516);
            this.ValuesListBox.TabIndex = 0;
            // 
            // EditDomainGroupBox
            // 
            this.EditDomainGroupBox.Controls.Add(this.DeleteDomainButton);
            this.EditDomainGroupBox.Controls.Add(this.EditDomainButton);
            this.EditDomainGroupBox.Controls.Add(this.AddDomainButton);
            this.EditDomainGroupBox.Location = new System.Drawing.Point(889, 3);
            this.EditDomainGroupBox.Name = "EditDomainGroupBox";
            this.EditDomainGroupBox.Size = new System.Drawing.Size(587, 247);
            this.EditDomainGroupBox.TabIndex = 10;
            this.EditDomainGroupBox.TabStop = false;
            this.EditDomainGroupBox.Text = "Редактирование";
            // 
            // DeleteDomainButton
            // 
            this.DeleteDomainButton.BackColor = System.Drawing.Color.Transparent;
            this.DeleteDomainButton.Enabled = false;
            this.DeleteDomainButton.Location = new System.Drawing.Point(6, 182);
            this.DeleteDomainButton.Name = "DeleteDomainButton";
            this.DeleteDomainButton.Size = new System.Drawing.Size(575, 46);
            this.DeleteDomainButton.TabIndex = 2;
            this.DeleteDomainButton.Text = "Удалить";
            this.DeleteDomainButton.UseVisualStyleBackColor = false;
            this.DeleteDomainButton.Click += new System.EventHandler(this.DeleteDomainButton_Click);
            // 
            // EditDomainButton
            // 
            this.EditDomainButton.BackColor = System.Drawing.Color.Transparent;
            this.EditDomainButton.Enabled = false;
            this.EditDomainButton.Location = new System.Drawing.Point(6, 112);
            this.EditDomainButton.Name = "EditDomainButton";
            this.EditDomainButton.Size = new System.Drawing.Size(575, 46);
            this.EditDomainButton.TabIndex = 1;
            this.EditDomainButton.Text = "Изменить";
            this.EditDomainButton.UseVisualStyleBackColor = false;
            this.EditDomainButton.Click += new System.EventHandler(this.EditDomainButton_Click);
            // 
            // AddDomainButton
            // 
            this.AddDomainButton.BackColor = System.Drawing.Color.Transparent;
            this.AddDomainButton.Location = new System.Drawing.Point(6, 38);
            this.AddDomainButton.Name = "AddDomainButton";
            this.AddDomainButton.Size = new System.Drawing.Size(575, 46);
            this.AddDomainButton.TabIndex = 0;
            this.AddDomainButton.Text = "Добавить";
            this.AddDomainButton.UseVisualStyleBackColor = false;
            this.AddDomainButton.Click += new System.EventHandler(this.AddDomainButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1492, 922);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.MenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.MenuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Новая экспертная система";
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.TabControl.ResumeLayout(false);
            this.RulesTab.ResumeLayout(false);
            this.ActionPartGroupBox.ResumeLayout(false);
            this.ConditionPartGroupBox.ResumeLayout(false);
            this.EditRuleGroupBox.ResumeLayout(false);
            this.VariablesTab.ResumeLayout(false);
            this.QuestionTextGroupBox.ResumeLayout(false);
            this.DomainValuesGroupBox.ResumeLayout(false);
            this.EditVariableGroupBox.ResumeLayout(false);
            this.DomainsTab.ResumeLayout(false);
            this.ValuesGroupBox.ResumeLayout(false);
            this.EditDomainGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip MenuStrip;
        private ToolStripMenuItem MenuFile;
        private ToolStripMenuItem MenuFileNew;
        private ToolStripMenuItem MenuFileOpen;
        private ToolStripMenuItem MenuFileSave;
        private ToolStripMenuItem MenuFileSaveAs;
        private ToolStripMenuItem MenuConsultation;
        private ToolStripMenuItem MenuFileExit;
        private ToolStripMenuItem MenuConsultationStart;
        private ToolStripMenuItem MenuConsultationExplain;
        private TabControl TabControl;
        private TabPage RulesTab;
        private TabPage VariablesTab;
        private TabPage DomainsTab;
        private GroupBox EditRuleGroupBox;
        private GroupBox ConditionPartGroupBox;
        private GroupBox ActionPartGroupBox;
        private Button AddRuleButton;
        private Button EditRuleButton;
        private Button DeleteRuleButton;
        private ListBox ConditionPartListBox;
        private ListBox ActionPartListBox;
        private ListView RulesListView;
        private ListView VariablesListView;
        private GroupBox QuestionTextGroupBox;
        private ListBox QuestionListBox;
        private GroupBox DomainValuesGroupBox;
        private ListBox DomainValuesListBox;
        private GroupBox EditVariableGroupBox;
        private Button DeleteVariableButton;
        private Button EditVariableButton;
        private Button AddVariableButton;
        private ListView DomainsListView;
        private GroupBox ValuesGroupBox;
        private ListBox ValuesListBox;
        private GroupBox EditDomainGroupBox;
        private Button DeleteDomainButton;
        private Button EditDomainButton;
        private Button AddDomainButton;
    }
}