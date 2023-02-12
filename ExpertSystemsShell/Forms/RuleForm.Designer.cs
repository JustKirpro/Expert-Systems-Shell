using System.Windows.Forms;

namespace ExpertSystemsShell.Forms
{
    partial class RuleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RuleNameGroupBox = new System.Windows.Forms.GroupBox();
            this.RuleNameTextBox = new System.Windows.Forms.TextBox();
            this.ConditionPartGroupBox = new System.Windows.Forms.GroupBox();
            this.ConditionPartListView = new System.Windows.Forms.ListView();
            this.CondtionPartDeleteButton = new System.Windows.Forms.Button();
            this.CondtionPartEditButton = new System.Windows.Forms.Button();
            this.CondtionPartAddButton = new System.Windows.Forms.Button();
            this.ExplanationGroupBox = new System.Windows.Forms.GroupBox();
            this.ExplanationTextBox = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ActionPartGroupBox = new System.Windows.Forms.GroupBox();
            this.ActionPartListView = new System.Windows.Forms.ListView();
            this.ActionPartDeleteButton = new System.Windows.Forms.Button();
            this.ActionPartEditButton = new System.Windows.Forms.Button();
            this.ActionPartAddButton = new System.Windows.Forms.Button();
            this.RuleNameGroupBox.SuspendLayout();
            this.ConditionPartGroupBox.SuspendLayout();
            this.ExplanationGroupBox.SuspendLayout();
            this.ActionPartGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // RuleNameGroupBox
            // 
            this.RuleNameGroupBox.Controls.Add(this.RuleNameTextBox);
            this.RuleNameGroupBox.Location = new System.Drawing.Point(12, 12);
            this.RuleNameGroupBox.Name = "RuleNameGroupBox";
            this.RuleNameGroupBox.Size = new System.Drawing.Size(898, 88);
            this.RuleNameGroupBox.TabIndex = 0;
            this.RuleNameGroupBox.TabStop = false;
            this.RuleNameGroupBox.Text = "Имя правила:";
            // 
            // RuleNameTextBox
            // 
            this.RuleNameTextBox.Location = new System.Drawing.Point(6, 38);
            this.RuleNameTextBox.Name = "RuleNameTextBox";
            this.RuleNameTextBox.Size = new System.Drawing.Size(886, 39);
            this.RuleNameTextBox.TabIndex = 0;
            // 
            // ConditionPartGroupBox
            // 
            this.ConditionPartGroupBox.Controls.Add(this.ConditionPartListView);
            this.ConditionPartGroupBox.Controls.Add(this.CondtionPartDeleteButton);
            this.ConditionPartGroupBox.Controls.Add(this.CondtionPartEditButton);
            this.ConditionPartGroupBox.Controls.Add(this.CondtionPartAddButton);
            this.ConditionPartGroupBox.Location = new System.Drawing.Point(12, 106);
            this.ConditionPartGroupBox.Name = "ConditionPartGroupBox";
            this.ConditionPartGroupBox.Size = new System.Drawing.Size(446, 330);
            this.ConditionPartGroupBox.TabIndex = 1;
            this.ConditionPartGroupBox.TabStop = false;
            this.ConditionPartGroupBox.Text = "Посылка";
            // 
            // ConditionPartListView
            // 
            this.ConditionPartListView.Location = new System.Drawing.Point(6, 35);
            this.ConditionPartListView.Name = "ConditionPartListView";
            this.ConditionPartListView.Size = new System.Drawing.Size(434, 252);
            this.ConditionPartListView.TabIndex = 6;
            this.ConditionPartListView.UseCompatibleStateImageBehavior = false;
            // 
            // CondtionPartDeleteButton
            // 
            this.CondtionPartDeleteButton.Enabled = false;
            this.CondtionPartDeleteButton.Location = new System.Drawing.Point(302, 293);
            this.CondtionPartDeleteButton.Name = "CondtionPartDeleteButton";
            this.CondtionPartDeleteButton.Size = new System.Drawing.Size(138, 37);
            this.CondtionPartDeleteButton.TabIndex = 5;
            this.CondtionPartDeleteButton.Text = "Удалить";
            this.CondtionPartDeleteButton.UseVisualStyleBackColor = true;
            // 
            // CondtionPartEditButton
            // 
            this.CondtionPartEditButton.Enabled = false;
            this.CondtionPartEditButton.Location = new System.Drawing.Point(151, 293);
            this.CondtionPartEditButton.Name = "CondtionPartEditButton";
            this.CondtionPartEditButton.Size = new System.Drawing.Size(145, 37);
            this.CondtionPartEditButton.TabIndex = 4;
            this.CondtionPartEditButton.Text = "Изменить";
            this.CondtionPartEditButton.UseVisualStyleBackColor = true;
            this.CondtionPartEditButton.Click += new System.EventHandler(this.CondtionPartEditButton_Click);
            // 
            // CondtionPartAddButton
            // 
            this.CondtionPartAddButton.Location = new System.Drawing.Point(6, 293);
            this.CondtionPartAddButton.Name = "CondtionPartAddButton";
            this.CondtionPartAddButton.Size = new System.Drawing.Size(138, 37);
            this.CondtionPartAddButton.TabIndex = 3;
            this.CondtionPartAddButton.Text = "Добавить";
            this.CondtionPartAddButton.UseVisualStyleBackColor = true;
            this.CondtionPartAddButton.Click += new System.EventHandler(this.CondtionPartAddButton_Click);
            // 
            // ExplanationGroupBox
            // 
            this.ExplanationGroupBox.Controls.Add(this.ExplanationTextBox);
            this.ExplanationGroupBox.Location = new System.Drawing.Point(12, 442);
            this.ExplanationGroupBox.Name = "ExplanationGroupBox";
            this.ExplanationGroupBox.Size = new System.Drawing.Size(898, 118);
            this.ExplanationGroupBox.TabIndex = 3;
            this.ExplanationGroupBox.TabStop = false;
            this.ExplanationGroupBox.Text = "Пояснение";
            // 
            // ExplanationTextBox
            // 
            this.ExplanationTextBox.Location = new System.Drawing.Point(6, 38);
            this.ExplanationTextBox.Multiline = true;
            this.ExplanationTextBox.Name = "ExplanationTextBox";
            this.ExplanationTextBox.Size = new System.Drawing.Size(886, 74);
            this.ExplanationTextBox.TabIndex = 0;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(604, 566);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(150, 46);
            this.OkButton.TabIndex = 4;
            this.OkButton.Text = "ОК";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(760, 566);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(150, 46);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ActionPartGroupBox
            // 
            this.ActionPartGroupBox.Controls.Add(this.ActionPartListView);
            this.ActionPartGroupBox.Controls.Add(this.ActionPartDeleteButton);
            this.ActionPartGroupBox.Controls.Add(this.ActionPartEditButton);
            this.ActionPartGroupBox.Controls.Add(this.ActionPartAddButton);
            this.ActionPartGroupBox.Location = new System.Drawing.Point(464, 106);
            this.ActionPartGroupBox.Name = "ActionPartGroupBox";
            this.ActionPartGroupBox.Size = new System.Drawing.Size(446, 330);
            this.ActionPartGroupBox.TabIndex = 6;
            this.ActionPartGroupBox.TabStop = false;
            this.ActionPartGroupBox.Text = "Заключение";
            // 
            // ActionPartListView
            // 
            this.ActionPartListView.Location = new System.Drawing.Point(6, 35);
            this.ActionPartListView.Name = "ActionPartListView";
            this.ActionPartListView.Size = new System.Drawing.Size(434, 252);
            this.ActionPartListView.TabIndex = 7;
            this.ActionPartListView.UseCompatibleStateImageBehavior = false;
            // 
            // ActionPartDeleteButton
            // 
            this.ActionPartDeleteButton.Enabled = false;
            this.ActionPartDeleteButton.Location = new System.Drawing.Point(303, 293);
            this.ActionPartDeleteButton.Name = "ActionPartDeleteButton";
            this.ActionPartDeleteButton.Size = new System.Drawing.Size(137, 37);
            this.ActionPartDeleteButton.TabIndex = 8;
            this.ActionPartDeleteButton.Text = "Удалить";
            this.ActionPartDeleteButton.UseVisualStyleBackColor = true;
            // 
            // ActionPartEditButton
            // 
            this.ActionPartEditButton.Enabled = false;
            this.ActionPartEditButton.Location = new System.Drawing.Point(152, 293);
            this.ActionPartEditButton.Name = "ActionPartEditButton";
            this.ActionPartEditButton.Size = new System.Drawing.Size(145, 37);
            this.ActionPartEditButton.TabIndex = 7;
            this.ActionPartEditButton.Text = "Изменить";
            this.ActionPartEditButton.UseVisualStyleBackColor = true;
            // 
            // ActionPartAddButton
            // 
            this.ActionPartAddButton.Location = new System.Drawing.Point(6, 293);
            this.ActionPartAddButton.Name = "ActionPartAddButton";
            this.ActionPartAddButton.Size = new System.Drawing.Size(137, 37);
            this.ActionPartAddButton.TabIndex = 6;
            this.ActionPartAddButton.Text = "Добавить";
            this.ActionPartAddButton.UseVisualStyleBackColor = true;
            // 
            // RuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 619);
            this.Controls.Add(this.ActionPartGroupBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.ExplanationGroupBox);
            this.Controls.Add(this.ConditionPartGroupBox);
            this.Controls.Add(this.RuleNameGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RuleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Форма правила";
            this.RuleNameGroupBox.ResumeLayout(false);
            this.RuleNameGroupBox.PerformLayout();
            this.ConditionPartGroupBox.ResumeLayout(false);
            this.ExplanationGroupBox.ResumeLayout(false);
            this.ExplanationGroupBox.PerformLayout();
            this.ActionPartGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox RuleNameGroupBox;
        private TextBox RuleNameTextBox;
        private GroupBox ConditionPartGroupBox;
        private GroupBox ExplanationGroupBox;
        private TextBox ExplanationTextBox;
        private Button OkButton;
        private new Button CancelButton;
        private GroupBox ActionPartGroupBox;
        private Button CondtionPartAddButton;
        private Button CondtionPartDeleteButton;
        private Button CondtionPartEditButton;
        private Button ActionPartDeleteButton;
        private Button ActionPartEditButton;
        private Button ActionPartAddButton;
        private ListView ConditionPartListView;
        private ListView ActionPartListView;
    }
}