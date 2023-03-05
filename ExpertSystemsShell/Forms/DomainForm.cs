using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using ExpertSystemsShell.Components;
using ExpertSystemsShell.Entities;
using System.Text;

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
        DomainNameTextBox.Text = knowledgeBase.GenerateNextDomainName();

        _knowledgeBase = knowledgeBase;
    }

    public DomainForm(KnowledgeBase knowledgeBase, Domain domain)
    {
        InitializeComponent();
        InitializeControls(domain);
        Text = "Редактирование домена";

        _knowledgeBase = knowledgeBase;
        Domain = domain;

        CopyValues(domain);
    }

    #endregion

    #region Button events

    private void OkButton_Click(object sender, EventArgs e)
    {
        var name = GetName();

        if (IsNameUsed(name))
        {
            ShowErrorMessageBox($"Домен с именем \"{name}\" уже существует.");
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
            ShowErrorMessageBox($"Значение \"{value}\" уже есть в домене.");
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
            var rulesList = GenerateRulesList(domainValue);
            ShowErrorMessageBox($"Значение домена \"{domainValue.Value}\" используется в правилах {rulesList}, поэтому его нельзя изменить.");
            return;
        }

        var value = GetValue();

        if (IsValueUsed(value) && value != domainValue.Value)
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
        foreach (var row in ValuesListView.SelectedItems)
        {
            var item = (ListViewItem)row;
            var domainValue = (DomainValue)item.Tag;

            if (IsDomainValueUsed(domainValue))
            {
                var rulesList = GenerateRulesList(domainValue);
                ShowErrorMessageBox($"Значение домена \"{domainValue.Value}\" используется в правилах {rulesList}, поэтому его нельзя удалить.");
                continue;
            }

            _values.Remove(domainValue);
            ValuesListView.Items.Remove(item);
        }

        UpdateOkButtonAvailability();
    }

    #endregion

    #region ListView and TextBoxes events

    private void ValuesListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        var isAnyValueSelected = IsAnyValueSelected();

        DeleteButton.Enabled = isAnyValueSelected;
        EditButton.Enabled = isAnyValueSelected && IsValueValid() && IsOnlyOneValueSelected();
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
        var isValueValid = IsValueValid();

        AddButton.Enabled = isValueValid;
        EditButton.Enabled = isValueValid && IsAnyValueSelected();
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

    private bool IsOnlyOneValueSelected() => ValuesListView.SelectedItems.Count == 1;

    private bool IsAnyValueAdded() => ValuesListView.Items.Count > 0;

    private bool IsDomainValueUsed(DomainValue domainValue) => _knowledgeBase.GetRulesByDomainValue(domainValue).Count > 0;

    private string GenerateRulesList(DomainValue domainValue)
    {
        var stringBuilder = new StringBuilder();
        var rules = _knowledgeBase.GetRulesByDomainValue(domainValue);

        foreach (var rule in rules)
        {
            stringBuilder.Append($"{rule.Name}, ");
        }

        return stringBuilder.ToString()[..^2];
    }

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