namespace ExpertSystemsShell.Forms
{
    partial class QuestionForm
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
            this.OptionsComboBox = new System.Windows.Forms.ComboBox();
            this.SelectButton = new System.Windows.Forms.Button();
            this.questionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OptionsComboBox
            // 
            this.OptionsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OptionsComboBox.FormattingEnabled = true;
            this.OptionsComboBox.Location = new System.Drawing.Point(12, 114);
            this.OptionsComboBox.Name = "OptionsComboBox";
            this.OptionsComboBox.Size = new System.Drawing.Size(482, 40);
            this.OptionsComboBox.TabIndex = 1;
            this.OptionsComboBox.SelectedIndexChanged += new System.EventHandler(this.OptionsComboBox_SelectedIndexChanged);
            // 
            // SelectButton
            // 
            this.SelectButton.Location = new System.Drawing.Point(500, 114);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(150, 40);
            this.SelectButton.TabIndex = 2;
            this.SelectButton.Text = "Выбрать";
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // questionLabel
            // 
            this.questionLabel.Location = new System.Drawing.Point(12, 9);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(638, 102);
            this.questionLabel.TabIndex = 3;
            this.questionLabel.Text = "QuestionLabel";
            this.questionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // QuestionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 161);
            this.Controls.Add(this.questionLabel);
            this.Controls.Add(this.SelectButton);
            this.Controls.Add(this.OptionsComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "QuestionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вопрос";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QuestionForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox OptionsComboBox;
        private System.Windows.Forms.Button SelectButton;
        private System.Windows.Forms.Label questionLabel;
    }
}