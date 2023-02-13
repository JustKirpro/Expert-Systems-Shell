using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Forms;

public partial class DomainForm : Form
{
    private readonly List<string> _usedNames;

    private readonly List<DomainValue> _values = new();

    public Domain Domain { get; private set; } = null!;

    public DomainForm(List<string> usedNames)
    {
        InitializeComponent();
        Text = "Создание домена";
        OkButton.Enabled = false;

        _usedNames = usedNames;
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

    private void OkButton_Click(object sender, EventArgs e)
    {
        var name = GetName();

        if (IsNameUsed(name))
        {
            ShowErrorMessageBox($"Домен с именем {name} уже существует");
            return;
        }

        SetDomain(name, _values);
        DialogResult = DialogResult.OK;
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        var value = GetValue();

        if (IsValueUsed(value))
        {
            ShowErrorMessageBox("Данное значение уже есть в домене");
            return;
        }

        _values.Add(value);
        ValuesListView.Items.Add(value);

        SetValuePlaceholder();
        UpdateOkButtonAvailability();
    }

    private void EditButton_Click(object sender, EventArgs e)
    {
        var selectedItem = ValuesListView.SelectedItems[0];
        var currentValue = selectedItem.Text;
        var usedValues = GetValues();

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

    private void ValuesListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        var isAnyValueSelected = ValuesListView.SelectedItems.Count > 0;
        EditButton.Enabled = DeleteButton.Enabled = isAnyValueSelected;
    }

    private void ValueTextBox_Enter(object sender, EventArgs e)
    {
        var value = GetValue();

        if (value == "Введите значение:")
        {
            ResetValuePlaceholder();
        }
    }

    private void ValueTextBox_Leave(object sender, EventArgs e)
    {
        var value = GetValue();

        if (string.IsNullOrWhiteSpace(value))
        {
            SetValuePlaceholder();
        }
    }

    private void ValueTextBox_TextChanged(object sender, EventArgs e)
    {
        var value = GetValue();
        AddButton.Enabled = !string.IsNullOrWhiteSpace(value);
    }

    private void DomainNameTextBox_TextChanged(object sender, EventArgs e) => UpdateOkButtonAvailability();

    private void ValuesListView_ItemDrag(object sender, ItemDragEventArgs e) => DoDragDrop(e.Item!, DragDropEffects.Move);

    private void ValuesListView_DragEnter(object sender, DragEventArgs e) => e.Effect = DragDropEffects.Move;

    private void ValuesListView_DragDrop(object sender, DragEventArgs e)
    {
        var fromItemIndex = ValuesListView.SelectedIndices[0];

        var point = ValuesListView.PointToClient(new Point(e.X, e.Y));
        var toItemIndex = ValuesListView.GetItemAt(point.X, point.Y)!.Index;

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

    private void SetDomain(string name, List<string> values)
    {
        if (Domain is null)
        {
            Domain = new Domain(name, values);
            return;
        }

        Domain.Name = name;
        Domain.Values = values;
    }

    private string GetName() => DomainNameTextBox.Text.Trim();

    private bool IsNameUsed(string name) => _usedNames.Contains(name) && name != Domain.Name;

    private string GetValue() => ValueTextBox.Text.Trim();

    private List<string> GetValues()
    {
        var values = new List<string>();

        foreach (var listViewItem in ValuesListView.Items)
        {
            var value = (listViewItem as ListViewItem)!.Text;
            values.Add(value);
        }

        return values;
    }

    private bool IsValueUsed(string value) => _values.Contains(value);

    private void InitializeComponents()
    {
        DomainNameTextBox.Text = Domain.Name;

        foreach (var value in Domain.Values)
        {
            ValuesListView.Items.Add(value);
        }
    }

    private static void ShowErrorMessageBox(string message) => MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    private void UpdateOkButtonAvailability()
    {
        var name = GetName();
        OkButton.Enabled = !string.IsNullOrWhiteSpace(name) && ValuesListView.Items.Count > 0;
    }

    private void SetValuePlaceholder() => ValueTextBox.Text = "Введите значение:";

    private void ResetValuePlaceholder() => ValueTextBox.Text = string.Empty;
}