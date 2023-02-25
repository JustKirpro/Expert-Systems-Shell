using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;
using ExpertSystemsShell.Components;

namespace ExpertSystemsShell.Forms;

public partial class DomainForm : Form
{
    private readonly KnowledgeBase _knowledgeBase;

    private readonly List<DomainValue> _values = new();

    public Domain? Domain { get; private set; }

    #region Constructors

    public DomainForm(KnowledgeBase knowledgeBase)
    {
        InitializeComponent();
        Text = "Создание домена";
        OkButton.Enabled = false;
        DomainNameTextBox.Text = knowledgeBase.GetNextDomainName();

        _knowledgeBase = knowledgeBase;
    }

    public DomainForm(KnowledgeBase knowledgeBase, Domain domain)
    {
        InitializeComponent();
        InitializeControls(domain);
        Text = "Редактирование домена";

        _knowledgeBase = knowledgeBase;
        CopyValues(domain);
        Domain = domain;
    }

    #endregion

    #region Button events

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

        if (IsValueUsed(value))
        {
            ShowErrorMessageBox($"Значение \"{value}\" уже есть в домене");
            return;
        }

        var domainValue = new DomainValue(value);
        _values.Add(domainValue);

        AddItemToListView(domainValue);
        ResetValueTextBox();
        UpdateOkButtonAvailability();
    }

    private void EditButton_Click(object sender, EventArgs e)
    {
        var selectedItem = GetSelectedItem();
        var domainValue = (DomainValue)selectedItem.Tag;

        if (IsDomainValueUsed(domainValue))
        {
            ShowErrorMessageBox("Данное значение домена используется, поэтому его нельзя изменить");
            return;
        }

        var value = GetValue();

        if (IsValueUsed(value) && domainValue.Value != value)
        {
            ShowErrorMessageBox($"Значение \"{value}\" уже есть в домене");
            return;
        }

        domainValue.Value = value;
        selectedItem.Text = value;

        ResetValueTextBox();
        UpdateOkButtonAvailability();
    }

    private void DeleteButton_Click(object sender, EventArgs e)
    {
        var selectedItem = GetSelectedItem();
        var domainValue = (DomainValue)selectedItem.Tag;

        if (IsDomainValueUsed(domainValue))
        {
            ShowErrorMessageBox("Данное значение домена используется, поэтому его нельзя удалить");
            return;
        }

        _values.Remove(domainValue);

        ValuesListView.Items.Remove(selectedItem);
        UpdateOkButtonAvailability();
    }

    #endregion

    #region ListView and TextBoxes events

    private void ValuesListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        var isAnyValueSelected = IsAnyValueSelected();
        var isValueValid = IsValueValid();

        DeleteButton.Enabled = isAnyValueSelected;
        EditButton.Enabled = isAnyValueSelected && isValueValid;
    }

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
        var domainValue = (DomainValue)item.Tag;

        _values.RemoveAt(startIndex);
        _values.Insert(endIndex, domainValue);

        ValuesListView.Items.RemoveAt(startIndex);
        ValuesListView.Items.Insert(endIndex, item);

        UpdateOkButtonAvailability();
    }

    private void ValueTextBox_TextChanged(object sender, EventArgs e)
    {
        var isAnyValueSelected = IsAnyValueSelected();
        var isValueValid = IsValueValid();

        AddButton.Enabled = isValueValid;
        EditButton.Enabled = isAnyValueSelected && isValueValid;
    }

    private void DomainNameTextBox_TextChanged(object sender, EventArgs e) => UpdateOkButtonAvailability();

    #endregion

    #region Utility methods

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

    private bool IsNameUsed(string name) => _knowledgeBase.IsDomainNameUsed(name) && name != Domain?.Name;

    private bool IsNameValid() => !string.IsNullOrWhiteSpace(GetName());

    private string GetValue() => ValueTextBox.Text.Trim();

    private bool IsValueUsed(string value) => _values.Any(v => v.Value == value);

    private bool IsValueValid() => !string.IsNullOrWhiteSpace(GetValue());

    private bool IsAnyValueSelected() => ValuesListView.SelectedItems.Count > 0;

    private bool IsAnyValueAdded() => ValuesListView.Items.Count > 0;

    private bool IsDomainValueUsed(DomainValue domainValue) => _knowledgeBase.IsDomainValueUsed(domainValue);

    private void InitializeControls(Domain domain)
    {
        DomainNameTextBox.Text = domain.Name;

        foreach (var value in domain.Values)
        {
            AddItemToListView(value);
        }
    }

    private void CopyValues(Domain domain)
    {
        foreach (var value in domain.Values)
        {
            _values.Add(value);
        }
    }

    private ListViewItem GetSelectedItem() => ValuesListView.SelectedItems[0];

    private int GetSelectedItemIndex() => ValuesListView.SelectedIndices[0];

    private void AddItemToListView(DomainValue domainValue)
    {
        var item = ValuesListView.Items.Add(domainValue.Value);
        item.Tag = domainValue;
    }

    private void UpdateOkButtonAvailability() => OkButton.Enabled = IsNameValid() && IsAnyValueAdded();

    private void ResetValueTextBox() => ValueTextBox.Text = string.Empty;

    private static void ShowErrorMessageBox(string message) => MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    #endregion
}