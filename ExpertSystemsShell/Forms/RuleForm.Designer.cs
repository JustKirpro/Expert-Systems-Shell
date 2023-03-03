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
            this.ConditionPartDeleteButton = new System.Windows.Forms.Button();
            this.ConditionPartEditButton = new System.Windows.Forms.Button();
            this.ConditionPartAddButton = new System.Windows.Forms.Button();
            this.ExplanationGroupBox = new System.Windows.Forms.GroupBox();
            this.ReasonTextBox = new System.Windows.Forms.TextBox();
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
            this.RuleNameTextBox.TextChanged += new System.EventHandler(this.RuleNameTextBox_TextChanged);
            // 
            // ConditionPartGroupBox
            // 
            this.ConditionPartGroupBox.Controls.Add(this.ConditionPartListView);
            this.ConditionPartGroupBox.Controls.Add(this.ConditionPartDeleteButton);
            this.ConditionPartGroupBox.Controls.Add(this.ConditionPartEditButton);
            this.ConditionPartGroupBox.Controls.Add(this.ConditionPartAddButton);
            this.ConditionPartGroupBox.Location = new System.Drawing.Point(12, 106);
            this.ConditionPartGroupBox.Name = "ConditionPartGroupBox";
            this.ConditionPartGroupBox.Size = new System.Drawing.Size(446, 330);
            this.ConditionPartGroupBox.TabIndex = 1;
            this.ConditionPartGroupBox.TabStop = false;
            this.ConditionPartGroupBox.Text = "Посылка";
            // 
            // ConditionPartListView
            // 
            this.ConditionPartListView.AllowDrop = true;
            this.ConditionPartListView.Location = new System.Drawing.Point(6, 35);
            this.ConditionPartListView.Name = "ConditionPartListView";
            this.ConditionPartListView.Size = new System.Drawing.Size(434, 252);
            this.ConditionPartListView.TabIndex = 6;
            this.ConditionPartListView.UseCompatibleStateImageBehavior = false;
            this.ConditionPartListView.View = System.Windows.Forms.View.List;
            this.ConditionPartListView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ConditionPartListView_ItemDrag);
            this.ConditionPartListView.SelectedIndexChanged += new System.EventHandler(this.ConditionPartListView_SelectedIndexChanged);
            this.ConditionPartListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.ConditionPartListView_DragDrop);
            this.ConditionPartListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.ConditionPartListView_DragEnter);
            // 
            // ConditionPartDeleteButton
            // 
            this.ConditionPartDeleteButton.Enabled = false;
            this.ConditionPartDeleteButton.Location = new System.Drawing.Point(302, 293);
            this.ConditionPartDeleteButton.Name = "ConditionPartDeleteButton";
            this.ConditionPartDeleteButton.Size = new System.Drawing.Size(138, 37);
            this.ConditionPartDeleteButton.TabIndex = 5;
            this.ConditionPartDeleteButton.Text = "Удалить";
            this.ConditionPartDeleteButton.UseVisualStyleBackColor = true;
            this.ConditionPartDeleteButton.Click += new System.EventHandler(this.ConditionPartDeleteButton_Click);
            // 
            // ConditionPartEditButton
            // 
            this.ConditionPartEditButton.Enabled = false;
            this.ConditionPartEditButton.Location = new System.Drawing.Point(151, 293);
            this.ConditionPartEditButton.Name = "ConditionPartEditButton";
            this.ConditionPartEditButton.Size = new System.Drawing.Size(145, 37);
            this.ConditionPartEditButton.TabIndex = 4;
            this.ConditionPartEditButton.Text = "Изменить";
            this.ConditionPartEditButton.UseVisualStyleBackColor = true;
            this.ConditionPartEditButton.Click += new System.EventHandler(this.ConditionPartEditButton_Click);
            // 
            // ConditionPartAddButton
            // 
            this.ConditionPartAddButton.Location = new System.Drawing.Point(6, 293);
            this.ConditionPartAddButton.Name = "ConditionPartAddButton";
            this.ConditionPartAddButton.Size = new System.Drawing.Size(138, 37);
            this.ConditionPartAddButton.TabIndex = 3;
            this.ConditionPartAddButton.Text = "Добавить";
            this.ConditionPartAddButton.UseVisualStyleBackColor = true;
            this.ConditionPartAddButton.Click += new System.EventHandler(this.ConditionPartAddButton_Click);
            // 
            // ExplanationGroupBox
            // 
            this.ExplanationGroupBox.Controls.Add(this.ReasonTextBox);
            this.ExplanationGroupBox.Location = new System.Drawing.Point(12, 442);
            this.ExplanationGroupBox.Name = "ExplanationGroupBox";
            this.ExplanationGroupBox.Size = new System.Drawing.Size(898, 118);
            this.ExplanationGroupBox.TabIndex = 3;
            this.ExplanationGroupBox.TabStop = false;
            this.ExplanationGroupBox.Text = "Пояснение";
            // 
            // ReasonTextBox
            // 
            this.ReasonTextBox.Location = new System.Drawing.Point(6, 38);
            this.ReasonTextBox.Multiline = true;
            this.ReasonTextBox.Name = "ReasonTextBox";
            this.ReasonTextBox.Size = new System.Drawing.Size(886, 74);
            this.ReasonTextBox.TabIndex = 0;
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
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(760, 566);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(150, 46);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
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
            this.ActionPartListView.View = System.Windows.Forms.View.List;
            this.ActionPartListView.SelectedIndexChanged += new System.EventHandler(this.ActionPartListView_SelectedIndexChanged);
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
            this.ActionPartDeleteButton.Click += new System.EventHandler(this.ActionPartDeleteButton_Click);
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
            this.ActionPartEditButton.Click += new System.EventHandler(this.ActionPartEditButton_Click);
            // 
            // ActionPartAddButton
            // 
            this.ActionPartAddButton.Location = new System.Drawing.Point(6, 293);
            this.ActionPartAddButton.Name = "ActionPartAddButton";
            this.ActionPartAddButton.Size = new System.Drawing.Size(137, 37);
            this.ActionPartAddButton.TabIndex = 6;
            this.ActionPartAddButton.Text = "Добавить";
            this.ActionPartAddButton.UseVisualStyleBackColor = true;
            this.ActionPartAddButton.Click += new System.EventHandler(this.ActionPartAddButton_Click);
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
        private TextBox ReasonTextBox;
        private Button OkButton;
        private new Button CancelButton;
        private GroupBox ActionPartGroupBox;
        private Button ConditionPartAddButton;
        private Button ConditionPartDeleteButton;
        private Button ConditionPartEditButton;
        private Button ActionPartDeleteButton;
        private Button ActionPartEditButton;
        private Button ActionPartAddButton;
        private ListView ConditionPartListView;
        private ListView ActionPartListView;
    }
}