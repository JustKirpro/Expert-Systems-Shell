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
            this.AnswerGroupBox = new System.Windows.Forms.GroupBox();
            this.AnswerButton = new System.Windows.Forms.Button();
            this.OptionsComboBox = new System.Windows.Forms.ComboBox();
            this.ConsultationListBox = new System.Windows.Forms.ListBox();
            this.AnswerGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AnswerGroupBox
            // 
            this.AnswerGroupBox.Controls.Add(this.AnswerButton);
            this.AnswerGroupBox.Controls.Add(this.OptionsComboBox);
            this.AnswerGroupBox.Location = new System.Drawing.Point(1, 635);
            this.AnswerGroupBox.Name = "AnswerGroupBox";
            this.AnswerGroupBox.Size = new System.Drawing.Size(687, 75);
            this.AnswerGroupBox.TabIndex = 0;
            this.AnswerGroupBox.TabStop = false;
            // 
            // AnswerButton
            // 
            this.AnswerButton.Location = new System.Drawing.Point(531, 21);
            this.AnswerButton.Name = "AnswerButton";
            this.AnswerButton.Size = new System.Drawing.Size(150, 40);
            this.AnswerButton.TabIndex = 1;
            this.AnswerButton.Text = "Ответить";
            this.AnswerButton.UseVisualStyleBackColor = true;
            this.AnswerButton.Click += new System.EventHandler(this.AnswerButton_Click);
            // 
            // OptionsComboBox
            // 
            this.OptionsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OptionsComboBox.FormattingEnabled = true;
            this.OptionsComboBox.Location = new System.Drawing.Point(6, 21);
            this.OptionsComboBox.Name = "OptionsComboBox";
            this.OptionsComboBox.Size = new System.Drawing.Size(512, 40);
            this.OptionsComboBox.TabIndex = 0;
            // 
            // ConsultationListBox
            // 
            this.ConsultationListBox.FormattingEnabled = true;
            this.ConsultationListBox.ItemHeight = 32;
            this.ConsultationListBox.Location = new System.Drawing.Point(7, 9);
            this.ConsultationListBox.Name = "ConsultationListBox";
            this.ConsultationListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.ConsultationListBox.Size = new System.Drawing.Size(668, 644);
            this.ConsultationListBox.TabIndex = 1;
            // 
            // ConsultationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 705);
            this.Controls.Add(this.ConsultationListBox);
            this.Controls.Add(this.AnswerGroupBox);
            this.Name = "ConsultationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Консультация";
            this.AnswerGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox AnswerGroupBox;
        private System.Windows.Forms.Button AnswerButton;
        private System.Windows.Forms.ComboBox OptionsComboBox;
        private System.Windows.Forms.ListBox ConsultationListBox;
    }
}