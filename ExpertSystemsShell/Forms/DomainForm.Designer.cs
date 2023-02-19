namespace ExpertSystemsShell.Forms
{
    partial class DomainForm
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
            this.DomainNameTextBox = new System.Windows.Forms.TextBox();
            this.DomainNameGroupBox = new System.Windows.Forms.GroupBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.DomainValuesGroupBox = new System.Windows.Forms.GroupBox();
            this.EditButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.ValueTextBox = new System.Windows.Forms.TextBox();
            this.ValuesListView = new System.Windows.Forms.ListView();
            this.DomainNameGroupBox.SuspendLayout();
            this.DomainValuesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // DomainNameTextBox
            // 
            this.DomainNameTextBox.Location = new System.Drawing.Point(6, 38);
            this.DomainNameTextBox.Name = "DomainNameTextBox";
            this.DomainNameTextBox.Size = new System.Drawing.Size(723, 39);
            this.DomainNameTextBox.TabIndex = 0;
            this.DomainNameTextBox.TextChanged += new System.EventHandler(this.DomainNameTextBox_TextChanged);
            // 
            // DomainNameGroupBox
            // 
            this.DomainNameGroupBox.Controls.Add(this.DomainNameTextBox);
            this.DomainNameGroupBox.Location = new System.Drawing.Point(12, 12);
            this.DomainNameGroupBox.Name = "DomainNameGroupBox";
            this.DomainNameGroupBox.Size = new System.Drawing.Size(735, 88);
            this.DomainNameGroupBox.TabIndex = 1;
            this.DomainNameGroupBox.TabStop = false;
            this.DomainNameGroupBox.Text = "Имя домена:";
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(597, 515);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(150, 46);
            this.CancelButton.TabIndex = 9;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(441, 516);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(150, 45);
            this.OkButton.TabIndex = 8;
            this.OkButton.Text = "ОК";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // DomainValuesGroupBox
            // 
            this.DomainValuesGroupBox.Controls.Add(this.EditButton);
            this.DomainValuesGroupBox.Controls.Add(this.DeleteButton);
            this.DomainValuesGroupBox.Controls.Add(this.AddButton);
            this.DomainValuesGroupBox.Controls.Add(this.ValueTextBox);
            this.DomainValuesGroupBox.Controls.Add(this.ValuesListView);
            this.DomainValuesGroupBox.Location = new System.Drawing.Point(12, 106);
            this.DomainValuesGroupBox.Name = "DomainValuesGroupBox";
            this.DomainValuesGroupBox.Size = new System.Drawing.Size(735, 410);
            this.DomainValuesGroupBox.TabIndex = 10;
            this.DomainValuesGroupBox.TabStop = false;
            this.DomainValuesGroupBox.Text = "Допустимые значения домена:";
            // 
            // EditButton
            // 
            this.EditButton.Enabled = false;
            this.EditButton.Location = new System.Drawing.Point(252, 358);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(234, 46);
            this.EditButton.TabIndex = 6;
            this.EditButton.Text = "Изменить";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Enabled = false;
            this.DeleteButton.Location = new System.Drawing.Point(495, 358);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(234, 46);
            this.DeleteButton.TabIndex = 5;
            this.DeleteButton.Text = "Удалить";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Enabled = false;
            this.AddButton.Location = new System.Drawing.Point(6, 357);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(234, 46);
            this.AddButton.TabIndex = 3;
            this.AddButton.Text = "Добавить";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // ValueTextBox
            // 
            this.ValueTextBox.Location = new System.Drawing.Point(6, 312);
            this.ValueTextBox.Name = "ValueTextBox";
            this.ValueTextBox.Size = new System.Drawing.Size(723, 39);
            this.ValueTextBox.TabIndex = 1;
            this.ValueTextBox.TextChanged += new System.EventHandler(this.ValueTextBox_TextChanged);
            // 
            // ValuesListView
            // 
            this.ValuesListView.AllowDrop = true;
            this.ValuesListView.FullRowSelect = true;
            this.ValuesListView.Location = new System.Drawing.Point(6, 38);
            this.ValuesListView.MultiSelect = false;
            this.ValuesListView.Name = "ValuesListView";
            this.ValuesListView.Size = new System.Drawing.Size(723, 264);
            this.ValuesListView.TabIndex = 0;
            this.ValuesListView.UseCompatibleStateImageBehavior = false;
            this.ValuesListView.View = System.Windows.Forms.View.List;
            this.ValuesListView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ValuesListView_ItemDrag);
            this.ValuesListView.SelectedIndexChanged += new System.EventHandler(this.ValuesListView_SelectedIndexChanged);
            this.ValuesListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.ValuesListView_DragDrop);
            this.ValuesListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.ValuesListView_DragEnter);
            // 
            // DomainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 564);
            this.Controls.Add(this.DomainValuesGroupBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.DomainNameGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DomainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Форма домена";
            this.DomainNameGroupBox.ResumeLayout(false);
            this.DomainNameGroupBox.PerformLayout();
            this.DomainValuesGroupBox.ResumeLayout(false);
            this.DomainValuesGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox DomainNameTextBox;
        private System.Windows.Forms.GroupBox DomainNameGroupBox;
        private new System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.GroupBox DomainValuesGroupBox;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.TextBox ValueTextBox;
        private System.Windows.Forms.ListView ValuesListView;
        private System.Windows.Forms.Button EditButton;
    }
}