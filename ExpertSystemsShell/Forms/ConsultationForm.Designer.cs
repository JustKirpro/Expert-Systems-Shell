namespace ExpertSystemsShell.Forms
{
    partial class ConsultationForm
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
            this.SelectButton = new System.Windows.Forms.Button();
            this.OptionsComboBox = new System.Windows.Forms.ComboBox();
            this.GoalVariableLabel = new System.Windows.Forms.Label();
            this.OptionGroupBox = new System.Windows.Forms.GroupBox();
            this.ButtonsGroupBox = new System.Windows.Forms.GroupBox();
            this.ShowExplanationButton = new System.Windows.Forms.Button();
            this.NewConsultationButton = new System.Windows.Forms.Button();
            this.OptionGroupBox.SuspendLayout();
            this.ButtonsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SelectButton
            // 
            this.SelectButton.Location = new System.Drawing.Point(488, 25);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(150, 40);
            this.SelectButton.TabIndex = 4;
            this.SelectButton.Text = "Выбрать";
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // OptionsComboBox
            // 
            this.OptionsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OptionsComboBox.FormattingEnabled = true;
            this.OptionsComboBox.Location = new System.Drawing.Point(6, 25);
            this.OptionsComboBox.Name = "OptionsComboBox";
            this.OptionsComboBox.Size = new System.Drawing.Size(482, 40);
            this.OptionsComboBox.TabIndex = 3;
            this.OptionsComboBox.SelectedIndexChanged += new System.EventHandler(this.OptionsComboBox_SelectedIndexChanged);
            // 
            // GoalVariableLabel
            // 
            this.GoalVariableLabel.Location = new System.Drawing.Point(145, 21);
            this.GoalVariableLabel.Name = "GoalVariableLabel";
            this.GoalVariableLabel.Size = new System.Drawing.Size(349, 44);
            this.GoalVariableLabel.TabIndex = 5;
            this.GoalVariableLabel.Text = "Выберите цель консультации:";
            // 
            // OptionGroupBox
            // 
            this.OptionGroupBox.Controls.Add(this.OptionsComboBox);
            this.OptionGroupBox.Controls.Add(this.SelectButton);
            this.OptionGroupBox.Location = new System.Drawing.Point(12, 52);
            this.OptionGroupBox.Name = "OptionGroupBox";
            this.OptionGroupBox.Size = new System.Drawing.Size(644, 65);
            this.OptionGroupBox.TabIndex = 6;
            this.OptionGroupBox.TabStop = false;
            // 
            // ButtonsGroupBox
            // 
            this.ButtonsGroupBox.Controls.Add(this.ShowExplanationButton);
            this.ButtonsGroupBox.Controls.Add(this.NewConsultationButton);
            this.ButtonsGroupBox.Location = new System.Drawing.Point(12, 52);
            this.ButtonsGroupBox.Name = "ButtonsGroupBox";
            this.ButtonsGroupBox.Size = new System.Drawing.Size(644, 65);
            this.ButtonsGroupBox.TabIndex = 7;
            this.ButtonsGroupBox.TabStop = false;
            this.ButtonsGroupBox.Visible = false;
            // 
            // ShowExplanationButton
            // 
            this.ShowExplanationButton.Location = new System.Drawing.Point(352, 25);
            this.ShowExplanationButton.Name = "ShowExplanationButton";
            this.ShowExplanationButton.Size = new System.Drawing.Size(286, 40);
            this.ShowExplanationButton.TabIndex = 5;
            this.ShowExplanationButton.Text = "Показать объяснения";
            this.ShowExplanationButton.UseVisualStyleBackColor = true;
            this.ShowExplanationButton.Click += new System.EventHandler(this.ShowExplanationButton_Click);
            // 
            // NewConsultationButton
            // 
            this.NewConsultationButton.Location = new System.Drawing.Point(6, 25);
            this.NewConsultationButton.Name = "NewConsultationButton";
            this.NewConsultationButton.Size = new System.Drawing.Size(286, 40);
            this.NewConsultationButton.TabIndex = 4;
            this.NewConsultationButton.Text = "Новая консультация";
            this.NewConsultationButton.UseVisualStyleBackColor = true;
            this.NewConsultationButton.Click += new System.EventHandler(this.NewConsultationButton_Click);
            // 
            // ConsultationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 129);
            this.Controls.Add(this.ButtonsGroupBox);
            this.Controls.Add(this.OptionGroupBox);
            this.Controls.Add(this.GoalVariableLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ConsultationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Консультация";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConsultationForm_KeyDown);
            this.OptionGroupBox.ResumeLayout(false);
            this.ButtonsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SelectButton;
        private System.Windows.Forms.ComboBox OptionsComboBox;
        private System.Windows.Forms.Label GoalVariableLabel;
        private System.Windows.Forms.GroupBox OptionGroupBox;
        private System.Windows.Forms.GroupBox ButtonsGroupBox;
        private System.Windows.Forms.Button ShowExplanationButton;
        private System.Windows.Forms.Button NewConsultationButton;
    }
}