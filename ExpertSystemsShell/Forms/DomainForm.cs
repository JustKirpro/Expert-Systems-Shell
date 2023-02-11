using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Forms;

public partial class DomainForm : Form
{
    private readonly List<string> _usedNames;

    private readonly List<string> _values = new();

    public Domain Domain { get; private set; }

    public DomainForm(List<string> usedNames)
    {
        InitializeComponent();
        Text = "Создание домена";
        OkButton.Enabled = false;

        _usedNames = usedNames;
        Domain = new Domain();
    }

    public DomainForm(List<string> usedNames, Domain domain)
    {
        InitializeComponent();
        Text = "Редактирование домена";

        _usedNames = usedNames;
        _values = domain.Values;
        Domain = domain;
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        DomainNameTextBox.Text = Domain.Name;

        foreach (var value in Domain.Values)
        {
            ValuesListView.Items.Add(value);
        }
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        var name = DomainNameTextBox.Text;

        if (_usedNames.Contains(name) && name != Domain.Name)
        {
            MessageBox.Show("Домен с данным именем уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Domain = new Domain
        {
            Name = name,
            Values = _values
        };
        
        DialogResult = DialogResult.OK;
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        var value = ValueTextBox.Text;

        if (_values.Contains(value))
        {
            MessageBox.Show("Данное значение уже есть в домене", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        _values.Add(value);
        ValuesListView.Items.Add(value);
        
        ValueTextBox.Text = "Введите значение:";
        UpdateOkButtonAvailability();
    }

    private void EditButton_Click(object sender, EventArgs e)
    {
        var selectedItem = ValuesListView.SelectedItems[0];
        var currentValue = selectedItem.Text;
        var usedValues = new List<string>();

        foreach (var listViewItem in ValuesListView.Items)
        {
            var value = (listViewItem as ListViewItem)!.Text;
            usedValues.Add(value!);
        }

        using var valueForm = new ValueForm(usedValues, currentValue);
        var result = valueForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var index = _values.IndexOf(currentValue);
            var value = valueForm.Value;

            _values.RemoveAt(index);
            _values.Insert(index, value);
            selectedItem.Text = value;

            UpdateOkButtonAvailability();
        }
    }

    private void DeleteButton_Click(object sender, EventArgs e)
    {
        var selectedItem = ValuesListView.SelectedItems[0];
        var value = selectedItem.Text;

        _values.Remove(value);
        ValuesListView.Items.Remove(selectedItem);
        UpdateOkButtonAvailability();
    }

    private void ValueTextBox_TextChanged(object sender, EventArgs e) => AddButton.Enabled = !string.IsNullOrWhiteSpace(ValueTextBox.Text);

    private void ValuesListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        var isAnyValueSelected = ValuesListView.SelectedItems.Count > 0;
        EditButton.Enabled = DeleteButton.Enabled = isAnyValueSelected;
    }

    private void DomainNameTextBox_TextChanged(object sender, EventArgs e) => UpdateOkButtonAvailability();

    private void UpdateOkButtonAvailability() => OkButton.Enabled = !string.IsNullOrWhiteSpace(DomainNameTextBox.Text) && ValuesListView.Items.Count > 0;

    private void ValueTextBox_Enter(object sender, EventArgs e)
    {
        if (ValueTextBox.Text == "Введите значение:")
        {
            ValueTextBox.Text = string.Empty;
        }
    }

    private void ValueTextBox_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ValueTextBox.Text))
        {
            ValueTextBox.Text = "Введите значение:";
        }
    }

    private void ValuesListView_ItemDrag(object sender, ItemDragEventArgs e) => DoDragDrop(e.Item!, DragDropEffects.Move);

    private void ValuesListView_DragEnter(object sender, DragEventArgs e) => e.Effect = DragDropEffects.Move;

    private void ValuesListView_DragDrop(object sender, DragEventArgs e)
    {
        var fromItemIndex = ValuesListView.SelectedIndices[0];

        var point = ValuesListView.PointToClient(new Point(e.X, e.Y));
        var toItemIndex = ValuesListView.GetItemAt(point.X, point.Y).Index;
        
        if (fromItemIndex == toItemIndex)
        {
            return;
        }

        var fromItem = ValuesListView.Items[fromItemIndex];

        _values.RemoveAt(fromItemIndex);
        _values.Insert(toItemIndex, fromItem.Text);

        ValuesListView.Items.RemoveAt(fromItemIndex);
        ValuesListView.Items.Insert(toItemIndex, fromItem);

        UpdateOkButtonAvailability();
    }
}