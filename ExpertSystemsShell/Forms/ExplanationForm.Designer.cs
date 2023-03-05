namespace ExpertSystemsShell.Forms
{
    partial class ExplanationForm
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
            this.RulesTreeView = new System.Windows.Forms.TreeView();
            this.VariablesListView = new System.Windows.Forms.ListView();
            this.name = new System.Windows.Forms.ColumnHeader();
            this.value = new System.Windows.Forms.ColumnHeader();
            this.VariablesLabel = new System.Windows.Forms.Label();
            this.RulesLabel = new System.Windows.Forms.Label();
            this.RulesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RulesTreeView
            // 
            this.RulesTreeView.Location = new System.Drawing.Point(6, 44);
            this.RulesTreeView.Name = "RulesTreeView";
            this.RulesTreeView.Size = new System.Drawing.Size(1152, 960);
            this.RulesTreeView.TabIndex = 0;
            this.RulesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.RulesTreeView_AfterSelect);
            // 
            // VariablesListView
            // 
            this.VariablesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.value});
            this.VariablesListView.FullRowSelect = true;
            this.VariablesListView.Location = new System.Drawing.Point(1164, 44);
            this.VariablesListView.MultiSelect = false;
            this.VariablesListView.Name = "VariablesListView";
            this.VariablesListView.Size = new System.Drawing.Size(554, 960);
            this.VariablesListView.TabIndex = 1;
            this.VariablesListView.UseCompatibleStateImageBehavior = false;
            this.VariablesListView.View = System.Windows.Forms.View.Details;
            this.VariablesListView.SelectedIndexChanged += new System.EventHandler(this.VariablesListView_SelectedIndexChanged);
            // 
            // name
            // 
            this.name.Text = "Имя";
            this.name.Width = 200;
            // 
            // value
            // 
            this.value.Text = "Значение";
            this.value.Width = 273;
            // 
            // VariablesLabel
            // 
            this.VariablesLabel.AutoSize = true;
            this.VariablesLabel.Location = new System.Drawing.Point(1164, 9);
            this.VariablesLabel.Name = "VariablesLabel";
            this.VariablesLabel.Size = new System.Drawing.Size(159, 32);
            this.VariablesLabel.TabIndex = 2;
            this.VariablesLabel.Text = "Переменные";
            // 
            // RulesLabel
            // 
            this.RulesLabel.AutoSize = true;
            this.RulesLabel.Location = new System.Drawing.Point(6, 9);
            this.RulesLabel.Name = "RulesLabel";
            this.RulesLabel.Size = new System.Drawing.Size(109, 32);
            this.RulesLabel.TabIndex = 3;
            this.RulesLabel.Text = "Правила";
            // 
            // RulesButton
            // 
            this.RulesButton.Location = new System.Drawing.Point(121, 1);
            this.RulesButton.Name = "RulesButton";
            this.RulesButton.Size = new System.Drawing.Size(212, 40);
            this.RulesButton.TabIndex = 4;
            this.RulesButton.Text = "Раскрыть всё";
            this.RulesButton.UseVisualStyleBackColor = true;
            this.RulesButton.Click += new System.EventHandler(this.RulesButton_Click);
            // 
            // ExplanationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1720, 1008);
            this.Controls.Add(this.RulesButton);
            this.Controls.Add(this.RulesLabel);
            this.Controls.Add(this.VariablesLabel);
            this.Controls.Add(this.VariablesListView);
            this.Controls.Add(this.RulesTreeView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ExplanationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Объяснение";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ExplanationForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView RulesTreeView;
        private System.Windows.Forms.ListView VariablesListView;
        private System.Windows.Forms.Label VariablesLabel;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader value;
        private System.Windows.Forms.Label RulesLabel;
        private System.Windows.Forms.Button RulesButton;
    }
}