using System.Windows.Forms;

namespace ExpertSystemsShell.Forms
{
    partial class FactForm
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
            this.VariableComboBox = new System.Windows.Forms.ComboBox();
            this.ValueComboBox = new System.Windows.Forms.ComboBox();
            this.VariableAddButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.EqualSignLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // VariableComboBox
            // 
            this.VariableComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VariableComboBox.FormattingEnabled = true;
            this.VariableComboBox.Location = new System.Drawing.Point(12, 22);
            this.VariableComboBox.Name = "VariableComboBox";
            this.VariableComboBox.Size = new System.Drawing.Size(665, 40);
            this.VariableComboBox.TabIndex = 0;
            this.VariableComboBox.SelectedIndexChanged += new System.EventHandler(this.VariableComboBox_SelectedIndexChanged);
            // 
            // ValueComboBox
            // 
            this.ValueComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ValueComboBox.FormattingEnabled = true;
            this.ValueComboBox.Location = new System.Drawing.Point(12, 137);
            this.ValueComboBox.Name = "ValueComboBox";
            this.ValueComboBox.Size = new System.Drawing.Size(735, 40);
            this.ValueComboBox.TabIndex = 1;
            this.ValueComboBox.SelectedIndexChanged += new System.EventHandler(this.ValueComboBox_SelectedIndexChanged);
            // 
            // VariableAddButton
            // 
            this.VariableAddButton.Location = new System.Drawing.Point(683, 22);
            this.VariableAddButton.Name = "VariableAddButton";
            this.VariableAddButton.Size = new System.Drawing.Size(64, 40);
            this.VariableAddButton.TabIndex = 2;
            this.VariableAddButton.Text = "+";
            this.VariableAddButton.UseVisualStyleBackColor = true;
            this.VariableAddButton.Click += new System.EventHandler(this.VariableAddButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(597, 197);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(150, 46);
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(441, 197);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(150, 46);
            this.OkButton.TabIndex = 6;
            this.OkButton.Text = "ОК";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // EqualSignLabel
            // 
            this.EqualSignLabel.AutoSize = true;
            this.EqualSignLabel.Location = new System.Drawing.Point(365, 89);
            this.EqualSignLabel.Name = "EqualSignLabel";
            this.EqualSignLabel.Size = new System.Drawing.Size(30, 32);
            this.EqualSignLabel.TabIndex = 8;
            this.EqualSignLabel.Text = "=";
            // 
            // FactForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 250);
            this.Controls.Add(this.EqualSignLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.VariableAddButton);
            this.Controls.Add(this.ValueComboBox);
            this.Controls.Add(this.VariableComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FactForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Форма факта";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox VariableComboBox;
        private ComboBox ValueComboBox;
        private Button VariableAddButton;
        private new Button CancelButton;
        private Button OkButton;
        private Label EqualSignLabel;
    }
}