namespace ExpertSystemsShell.Forms
{
    partial class VariableForm
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
            this.VariableNameGroupBox = new System.Windows.Forms.GroupBox();
            this.VariableNameTextBox = new System.Windows.Forms.TextBox();
            this.DomainGroupBox = new System.Windows.Forms.GroupBox();
            this.AddDomainButton = new System.Windows.Forms.Button();
            this.DomainComboBox = new System.Windows.Forms.ComboBox();
            this.TypeGroupBox = new System.Windows.Forms.GroupBox();
            this.InferredOption = new System.Windows.Forms.RadioButton();
            this.RequestedInferredOption = new System.Windows.Forms.RadioButton();
            this.RequestedOption = new System.Windows.Forms.RadioButton();
            this.QuestionGroupBox = new System.Windows.Forms.GroupBox();
            this.QuestionTextBox = new System.Windows.Forms.TextBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.VariableNameGroupBox.SuspendLayout();
            this.DomainGroupBox.SuspendLayout();
            this.TypeGroupBox.SuspendLayout();
            this.QuestionGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // VariableNameGroupBox
            // 
            this.VariableNameGroupBox.Controls.Add(this.VariableNameTextBox);
            this.VariableNameGroupBox.Location = new System.Drawing.Point(12, 12);
            this.VariableNameGroupBox.Name = "VariableNameGroupBox";
            this.VariableNameGroupBox.Size = new System.Drawing.Size(735, 88);
            this.VariableNameGroupBox.TabIndex = 2;
            this.VariableNameGroupBox.TabStop = false;
            this.VariableNameGroupBox.Text = "Имя переменной:";
            // 
            // VariableNameTextBox
            // 
            this.VariableNameTextBox.Location = new System.Drawing.Point(6, 38);
            this.VariableNameTextBox.Name = "VariableNameTextBox";
            this.VariableNameTextBox.Size = new System.Drawing.Size(723, 39);
            this.VariableNameTextBox.TabIndex = 0;
            this.VariableNameTextBox.TextChanged += new System.EventHandler(this.VariableNameTextBox_TextChanged);
            // 
            // DomainGroupBox
            // 
            this.DomainGroupBox.Controls.Add(this.AddDomainButton);
            this.DomainGroupBox.Controls.Add(this.DomainComboBox);
            this.DomainGroupBox.Location = new System.Drawing.Point(12, 106);
            this.DomainGroupBox.Name = "DomainGroupBox";
            this.DomainGroupBox.Size = new System.Drawing.Size(735, 88);
            this.DomainGroupBox.TabIndex = 3;
            this.DomainGroupBox.TabStop = false;
            this.DomainGroupBox.Text = "Домен:";
            // 
            // AddDomainButton
            // 
            this.AddDomainButton.Location = new System.Drawing.Point(665, 38);
            this.AddDomainButton.Name = "AddDomainButton";
            this.AddDomainButton.Size = new System.Drawing.Size(64, 40);
            this.AddDomainButton.TabIndex = 4;
            this.AddDomainButton.Text = "+";
            this.AddDomainButton.UseVisualStyleBackColor = true;
            this.AddDomainButton.Click += new System.EventHandler(this.AddDomainButton_Click);
            // 
            // DomainComboBox
            // 
            this.DomainComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DomainComboBox.FormattingEnabled = true;
            this.DomainComboBox.Location = new System.Drawing.Point(6, 38);
            this.DomainComboBox.Name = "DomainComboBox";
            this.DomainComboBox.Size = new System.Drawing.Size(653, 40);
            this.DomainComboBox.TabIndex = 0;
            this.DomainComboBox.SelectedIndexChanged += new System.EventHandler(this.DomainComboBox_SelectedIndexChanged);
            // 
            // TypeGroupBox
            // 
            this.TypeGroupBox.Controls.Add(this.InferredOption);
            this.TypeGroupBox.Controls.Add(this.RequestedInferredOption);
            this.TypeGroupBox.Controls.Add(this.RequestedOption);
            this.TypeGroupBox.Location = new System.Drawing.Point(12, 200);
            this.TypeGroupBox.Name = "TypeGroupBox";
            this.TypeGroupBox.Size = new System.Drawing.Size(735, 163);
            this.TypeGroupBox.TabIndex = 3;
            this.TypeGroupBox.TabStop = false;
            this.TypeGroupBox.Text = "Тип переменной:";
            // 
            // InferredOption
            // 
            this.InferredOption.AutoSize = true;
            this.InferredOption.Location = new System.Drawing.Point(6, 80);
            this.InferredOption.Name = "InferredOption";
            this.InferredOption.Size = new System.Drawing.Size(171, 36);
            this.InferredOption.TabIndex = 2;
            this.InferredOption.TabStop = true;
            this.InferredOption.Text = "Выводимая";
            this.InferredOption.UseVisualStyleBackColor = true;
            this.InferredOption.CheckedChanged += new System.EventHandler(this.InferredOption_CheckedChanged);
            // 
            // RequestedInferredOption
            // 
            this.RequestedInferredOption.AutoSize = true;
            this.RequestedInferredOption.Location = new System.Drawing.Point(6, 122);
            this.RequestedInferredOption.Name = "RequestedInferredOption";
            this.RequestedInferredOption.Size = new System.Drawing.Size(347, 36);
            this.RequestedInferredOption.TabIndex = 1;
            this.RequestedInferredOption.TabStop = true;
            this.RequestedInferredOption.Text = "Запрашиваемо-выводимая";
            this.RequestedInferredOption.UseVisualStyleBackColor = true;
            this.RequestedInferredOption.CheckedChanged += new System.EventHandler(this.RequestedInferredOption_CheckedChanged);
            // 
            // RequestedOption
            // 
            this.RequestedOption.AutoSize = true;
            this.RequestedOption.Location = new System.Drawing.Point(6, 38);
            this.RequestedOption.Name = "RequestedOption";
            this.RequestedOption.Size = new System.Drawing.Size(222, 36);
            this.RequestedOption.TabIndex = 0;
            this.RequestedOption.TabStop = true;
            this.RequestedOption.Text = "Запрашиваемая";
            this.RequestedOption.UseVisualStyleBackColor = true;
            this.RequestedOption.CheckedChanged += new System.EventHandler(this.RequestedOption_CheckedChanged);
            // 
            // QuestionGroupBox
            // 
            this.QuestionGroupBox.Controls.Add(this.QuestionTextBox);
            this.QuestionGroupBox.Location = new System.Drawing.Point(12, 364);
            this.QuestionGroupBox.Name = "QuestionGroupBox";
            this.QuestionGroupBox.Size = new System.Drawing.Size(735, 148);
            this.QuestionGroupBox.TabIndex = 5;
            this.QuestionGroupBox.TabStop = false;
            this.QuestionGroupBox.Text = "Текст вопроса:";
            // 
            // QuestionTextBox
            // 
            this.QuestionTextBox.Location = new System.Drawing.Point(6, 33);
            this.QuestionTextBox.Multiline = true;
            this.QuestionTextBox.Name = "QuestionTextBox";
            this.QuestionTextBox.Size = new System.Drawing.Size(723, 110);
            this.QuestionTextBox.TabIndex = 1;
            this.QuestionTextBox.TextChanged += new System.EventHandler(this.QuestionTextBox_TextChanged);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(597, 513);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(150, 46);
            this.CancelButton.TabIndex = 11;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(441, 513);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(150, 46);
            this.OkButton.TabIndex = 10;
            this.OkButton.Text = "ОК";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // VariableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 564);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.QuestionGroupBox);
            this.Controls.Add(this.TypeGroupBox);
            this.Controls.Add(this.DomainGroupBox);
            this.Controls.Add(this.VariableNameGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "VariableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Форма переменной";
            this.VariableNameGroupBox.ResumeLayout(false);
            this.VariableNameGroupBox.PerformLayout();
            this.DomainGroupBox.ResumeLayout(false);
            this.TypeGroupBox.ResumeLayout(false);
            this.TypeGroupBox.PerformLayout();
            this.QuestionGroupBox.ResumeLayout(false);
            this.QuestionGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox VariableNameGroupBox;
        private System.Windows.Forms.TextBox VariableNameTextBox;
        private System.Windows.Forms.GroupBox DomainGroupBox;
        private System.Windows.Forms.Button AddDomainButton;
        private System.Windows.Forms.ComboBox DomainComboBox;
        private System.Windows.Forms.GroupBox TypeGroupBox;
        private System.Windows.Forms.RadioButton InferredOption;
        private System.Windows.Forms.RadioButton RequestedInferredOption;
        private System.Windows.Forms.RadioButton RequestedOption;
        private System.Windows.Forms.GroupBox QuestionGroupBox;
        private System.Windows.Forms.TextBox QuestionTextBox;
        private new System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OkButton;
    }
}