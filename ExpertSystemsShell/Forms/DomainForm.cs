using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Forms;

public partial class DomainForm : Form
{
    private readonly List<string> _usedNames;

    private readonly List<DomainValue> _values = new();

    public Domain? Domain { get; private set; }

    public DomainForm(List<string> usedNames)
    {
        InitializeComponent();
        Text = "Создание домена";
        OkButton.Enabled = false;

        _usedNames= usedNames;
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
            ShowErrorMessageBox($"Домен с именем \"{name}\" уже существует");
            return;
        }

        SetDomain(name, _values);
        DialogResult = DialogResult.OK;
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        var value = GetValue();

        if (IsValueAlreadyExists(value))
        {
            ShowErrorMessageBox($"Значение \"{value}\" уже есть в домене");
            return;
        }

        var domainValue = new DomainValue(value);
        _values.Add(domainValue);

        AddItemToListView(domainValue);
        ValueTextBox.Text = string.Empty;
        UpdateOkButtonAvailability();
    }

    private void EditButton_Click(object sender, EventArgs e)
    {
        var selectedItem = GetSelectedItem();
        var domainValue = selectedItem.Tag as DomainValue;

        if (domainValue!.IsUsed)
        {
            ShowErrorMessageBox($"Данное значение домена используется, поэтому его нельзя изменить");
            return;
        }

        var value = GetValue();

        if (IsValueAlreadyExists(value) && domainValue.Value != value)
        {
            ShowErrorMessageBox($"Значение \"{value}\" уже есть в домене");
            return;
        }

        domainValue.Value = value;
        selectedItem.Text = value;

        ValueTextBox.Text = string.Empty;
    }

    private void DeleteButton_Click(object sender, EventArgs e)
    {
        var selectedItem = GetSelectedItem();
        var domainValue = selectedItem.Tag as DomainValue;

        if (domainValue!.IsUsed)
        {
            ShowErrorMessageBox($"Данное значение домена используется, поэтому его нельзя удалить");
            return;
        }

        _values.Remove(domainValue);

        RemoveItemFromListView(selectedItem);
        UpdateOkButtonAvailability();
    }

    private void ValuesListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        var isAnyValueSelected = ValuesListView.SelectedItems.Count > 0;
        DeleteButton.Enabled = isAnyValueSelected;

        var value = GetValue();
        var isValueValid = !string.IsNullOrWhiteSpace(value);
        EditButton.Enabled = isAnyValueSelected && isValueValid;
    }

    private void ValueTextBox_TextChanged(object sender, EventArgs e)
    {
        var value = GetValue();
        var isValueValid = !string.IsNullOrWhiteSpace(value);
        AddButton.Enabled = isValueValid;

        var isAnyValueSelected = ValuesListView.SelectedItems.Count > 0;
        EditButton.Enabled = isValueValid && isAnyValueSelected;
    }
    
    private void DomainNameTextBox_TextChanged(object sender, EventArgs e) => UpdateOkButtonAvailability();

    private void ValuesListView_ItemDrag(object sender, ItemDragEventArgs e) => DoDragDrop(e.Item!, DragDropEffects.Move);

    private void ValuesListView_DragEnter(object sender, DragEventArgs e) => e.Effect = DragDropEffects.Move;

    private void ValuesListView_DragDrop(object sender, DragEventArgs e)
    {
        var startIndex = GetSelectedItemIndex();

        var point = ValuesListView.PointToClient(new Point(e.X, e.Y));
        var item = ValuesListView.GetItemAt(point.X, point.Y);

        if (item is null)
        {
            return;
        }

        var endIndex = item.Index;

        if (startIndex == endIndex)
        {
            return;
        }

        item = ValuesListView.Items[startIndex];
        var domainValue = item.Tag as DomainValue;

        _values.RemoveAt(startIndex);
        _values.Insert(endIndex, domainValue!);

        ValuesListView.Items.RemoveAt(startIndex);
        ValuesListView.Items.Insert(endIndex, item);

        UpdateOkButtonAvailability();
    }

    private void SetDomain(string name, List<DomainValue> values)
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

    private bool IsNameUsed(string name) => _usedNames.Contains(name) && name != Domain?.Name;

    private string GetValue() => ValueTextBox.Text.Trim();

    private bool IsValueAlreadyExists(string value) => _values.Any(v => v.Value == value);

    private bool IsAnyValueAdded() => ValuesListView.Items.Count > 0;

    private void InitializeComponents()
    {
        DomainNameTextBox.Text = Domain!.Name;

        foreach (var value in Domain.Values)
        {
            var item = ValuesListView.Items.Add(value.Value);
            item.Tag = value;
        }
    }

    private ListViewItem GetSelectedItem() => ValuesListView.SelectedItems[0];

    private int GetSelectedItemIndex() => ValuesListView.SelectedIndices[0];

    private void AddItemToListView(DomainValue domainValue)
    {
        var item = ValuesListView.Items.Add(domainValue.Value);
        item.Tag = domainValue;
    }

    private void RemoveItemFromListView(ListViewItem item) => ValuesListView.Items.Remove(item);

    private void UpdateOkButtonAvailability()
    {
        var name = GetName();
        OkButton.Enabled = !string.IsNullOrWhiteSpace(name) && IsAnyValueAdded();
    }

    private static void ShowErrorMessageBox(string message) => MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
}