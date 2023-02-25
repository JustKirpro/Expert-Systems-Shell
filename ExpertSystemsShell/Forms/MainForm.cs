using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using ExpertSystemsShell.Entities;
using ExpertSystemsShell.Components;
using System.Threading.Tasks;

namespace ExpertSystemsShell.Forms;

public partial class MainForm : Form
{
    private ExpertSystemShell _expertSystemShell = new();

    public MainForm()
    {
        InitializeComponent();
        //PopulateLists();
        InitializeListViews();
    }

    #region MenuFile

    private void MenuFileNew_Click(object sender, EventArgs e)
    {
        //_expertSystemShell = new();
        var path = @"C:\Users\Justk\Desktop\poof.json";
        var m = JsonConvert.SerializeObject(_expertSystemShell.KnowledgeBase, Formatting.Indented);
        File.WriteAllText(path, m);
    }

    // Async void
    private async void MenuFileOpen_Click(object sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog()
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Filter = "json files|*.json"
        };

        var result = openFileDialog.ShowDialog();

        if (result == DialogResult.OK)
        {
            var path = openFileDialog.FileName;
            var json = await File.ReadAllTextAsync(path);
            var knowledgeBase = JsonConvert.DeserializeObject<KnowledgeBase>(json,
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            _expertSystemShell.KnowledgeBase = knowledgeBase!;

            DisplayRules();
            DisplayVariables();
            DisplayDomains();
        }
    }

    private void MenuFileSave_Click(object sender, EventArgs e)
    {
        var path = @"C:\Users\Justk\Desktop\poof.json";
        var m = JsonConvert.SerializeObject(_expertSystemShell.KnowledgeBase);
        File.WriteAllText(path, m); 
    }

    private async void MenuFileSaveAs_Click(object sender, EventArgs e)
    {
        using var saveFileDialog = new SaveFileDialog()
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Filter = "json files|*.json"
        };

        var result = saveFileDialog.ShowDialog();

        if (result == DialogResult.OK)
        {
            var path = saveFileDialog.FileName;
            var json = JsonConvert.SerializeObject(_expertSystemShell.KnowledgeBase, Formatting.Indented,
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            await File.WriteAllTextAsync(path, json);
        }
    }

    private void MenuFileExit_Click(object sender, EventArgs e)
    {
        var result = MessageBox.Show("Заверить работу приложения?", "Завершение работы", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

        if (result == DialogResult.OK)
        {
            Application.Exit();
        }
    }

    #endregion

    #region MenuConsultation

    private void MenuConsultationStart_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var consultationForms = new ConsultationForm(knowledgeBase);
        var result = consultationForms.ShowDialog();

        if (result == DialogResult.OK)
        {

        }
    }

    private void MenuConsultationExplain_Click(object sender, EventArgs e)
    {
        //_expertSystemShell.
    }

    #endregion

    #region RulesTab

    private void AddRuleButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;

        using var ruleForm = new RuleForm(knowledgeBase);
        var result = ruleForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var rule = ruleForm.Rule!;
            var selectedIndex = GetSelectedIndex(RulesListView);

            if (selectedIndex > - 1)
            {
                knowledgeBase.Rules.Insert(selectedIndex, rule);
            }
            else
            {
                knowledgeBase.Rules.Add(rule);
            }

            AddRuleToListView(rule, selectedIndex);
            ResizeListView(RulesListView);
        }

        DisplayVariables();
        DisplayDomains();
    }

    private void EditRuleButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var selectedItem = GetSelectedItem(RulesListView);
        var rule = (Rule)selectedItem.Tag;

        using var ruleForm = new RuleForm(knowledgeBase, rule);
        var result = ruleForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            rule = ruleForm.Rule!;
            UpdateRuleInListView(rule, selectedItem);
            ResizeListView(RulesListView);
            UpdateRulesListBoxes();
        }

        DisplayVariables();
        DisplayDomains();
    }

    private void DeleteRuleButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var selectedItem = GetSelectedItem(RulesListView);
        var rule = (Rule)selectedItem.Tag;

        knowledgeBase.Rules.Remove(rule);
        RulesListView.Items.Remove(selectedItem);
        ResizeListView(RulesListView);
    }

    private void RulesListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        var isAnyItemSelected = RulesListView.SelectedItems.Count > 0;
        EditRuleButton.Enabled = DeleteRuleButton.Enabled = isAnyItemSelected;

        if (!isAnyItemSelected)
        {
            ConditionPartListBox.Items.Clear();
            ActionPartListBox.Items.Clear();
            return;
        }

        UpdateRulesListBoxes();
    }

    private void AddRuleToListView(Rule rule, int index = -1)
    {
        var item = index == -1 ? RulesListView.Items.Add(rule.Name) : RulesListView.Items.Insert(index + 1, rule.Name);
        item.SubItems.Add(rule.FormattedRule);
        item.Tag = rule;
    }

    private void UpdateRuleInListView(Rule rule, ListViewItem listViewItem)
    {
        listViewItem.SubItems[0].Text = rule.Name;
        listViewItem.SubItems[1].Text = rule.FormattedRule;

        ResizeListView(RulesListView);
    }

    private void UpdateRulesListBoxes()
    {
        var selectedItem = GetSelectedItem(RulesListView);
        var rule = (Rule)selectedItem.Tag;

        ConditionPartListBox.Items.Clear();

        foreach (var fact in rule.ConditionPart)
        {
            ConditionPartListBox.Items.Add(fact.FormattedFact);
        }

        ActionPartListBox.Items.Clear();

        foreach (var fact in rule.ActionPart)
        {
            ActionPartListBox.Items.Add(fact.FormattedFact);
        }
    }

    private void RulesListView_ItemDrag(object sender, ItemDragEventArgs e) => DoDragDrop(e.Item!, DragDropEffects.Move);

    private void RulesListView_DragEnter(object sender, DragEventArgs e) => e.Effect = DragDropEffects.Move;

    private void RulesListView_DragDrop(object sender, DragEventArgs e)
    {
        var startIndex = GetSelectedIndex(RulesListView);

        var point = RulesListView.PointToClient(new Point(e.X, e.Y));
        var item = RulesListView.GetItemAt(point.X, point.Y);

        if (item is null)
        {
            return;
        }

        var endIndex = item.Index;

        if (startIndex == endIndex)
        {
            return;
        }

        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        item = RulesListView.Items[startIndex];
        var rule = (Rule)item.Tag;

        knowledgeBase.Rules.RemoveAt(startIndex);
        knowledgeBase.Rules.Insert(endIndex, rule);

        RulesListView.Items.RemoveAt(startIndex);
        RulesListView.Items.Insert(endIndex, item);
    }

    #endregion

    #region VariablesTab

    private void AddVariableButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;

        using var variableForm = new VariableForm(knowledgeBase);
        var result = variableForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var variable = variableForm.Variable!;
            knowledgeBase.Variables.Add(variable);
            
            AddVariableToListView(variable);
            ResizeListView(VariablesListView);
        }

        DisplayDomains();
    }

    private void EditVariableButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var selectedItem = GetSelectedItem(VariablesListView);
        var variable = (Variable)selectedItem.Tag;

        if (knowledgeBase.IsVariableUsed(variable))
        {
            ShowErrorMessageBox("Данная переменная используется, поэтому её нельзя изменить");
            return;
        }

        using var variableForm = new VariableForm(knowledgeBase, variable);
        var result = variableForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            variable = variableForm.Variable!;
            UpdateVariableInListView(variable, selectedItem);
            ResizeListView(VariablesListView);
            UpdateVariableListBoxes();
        }

        DisplayDomains();
    }

    private void DeleteVariableButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var selectedItem = GetSelectedItem(VariablesListView);
        var variable = (Variable)selectedItem.Tag;

        if (knowledgeBase.IsVariableUsed(variable))
        {
            ShowErrorMessageBox("Данная переменная используется, поэтому её нельзя удалить");
            return;
        }

        knowledgeBase.Variables.Remove(variable);
        VariablesListView.Items.Remove(selectedItem);
        ResizeListView(VariablesListView);
    }

    private void VariablesListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        var isAnyItemSelected = VariablesListView.SelectedItems.Count > 0;
        EditVariableButton.Enabled = DeleteVariableButton.Enabled = isAnyItemSelected;

        if (!isAnyItemSelected)
        {
            DomainValuesListBox.Items.Clear();
            VariableRuleListBox.Items.Clear();
            QuestionListBox.Items.Clear();
            return;
        }

        UpdateVariableListBoxes();
    }

    private void AddVariableToListView(Variable variable)
    {
        var item = VariablesListView.Items.Add(variable.Name);
        item.SubItems.Add(variable.FormattedType);
        item.SubItems.Add(variable.Domain.Name);
        item.Tag = variable;
    }

    private void UpdateVariableInListView(Variable variable, ListViewItem listViewItem)
    {
        listViewItem.SubItems[0].Text = variable.Name;
        listViewItem.SubItems[1].Text = variable.FormattedType;
        listViewItem.SubItems[2].Text = variable.Domain.Name;

        ResizeListView(VariablesListView);
    }

    private void UpdateVariableListBoxes()
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var selectedItem = GetSelectedItem(VariablesListView);
        var variable = (Variable)selectedItem.Tag;

        DomainValuesListBox.Items.Clear();

        foreach (var value in variable.Domain.Values)
        {
            DomainValuesListBox.Items.Add(value.Value);
        }

        VariableRuleListBox.Items.Clear();

        var rules = knowledgeBase.GetRulesByVariable(variable);

        foreach (var rule in rules)
        {
            VariableRuleListBox.Items.Add(rule.Name);
        }

        QuestionListBox.Items.Clear();
        QuestionListBox.Items.Add(variable.Question);
    }

    #endregion

    #region DomainsTab

    private void AddDomainButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;

        using var domainForm = new DomainForm(knowledgeBase);
        var result = domainForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            var domain = domainForm.Domain!;
            knowledgeBase.Domains.Add(domain);
            
            AddDomainToListView(domain);
            ResizeListView(DomainsListView);
        }
    }

    private void EditDomainButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var selectedItem = GetSelectedItem(DomainsListView);
        var domain = (Domain)selectedItem.Tag;

        using var domainForm = new DomainForm(knowledgeBase, domain);
        var result = domainForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            domain = domainForm.Domain!;
            UpdateDomainInListView(domain, selectedItem);
            ResizeListView(DomainsListView);
            UpdateDomainListBoxes();
        }
    }

    private void DeleteDomainButton_Click(object sender, EventArgs e)
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var selectedItem = GetSelectedItem(DomainsListView);
        var domain = (Domain)selectedItem.Tag;

        if (knowledgeBase.IsDomainUsed(domain))
        {
            ShowErrorMessageBox("Данный домен используется, поэтому его нельзя удалить");
            return;
        }

        knowledgeBase.Domains.Remove(domain);
        DomainsListView.Items.Remove(selectedItem);
        ResizeListView(DomainsListView);
    }

    private void DomainsListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        var isAnyItemSelected = DomainsListView.SelectedItems.Count > 0;
        EditDomainButton.Enabled = DeleteDomainButton.Enabled = isAnyItemSelected;

        if (!isAnyItemSelected)
        {
            ValuesListBox.Items.Clear();
            DomainVariableListBox.Items.Clear();
            return;
        }

        UpdateDomainListBoxes();
    }

    private void AddDomainToListView(Domain domain)
    {
        var item = DomainsListView.Items.Add(domain.Name);
        item.SubItems.Add(domain.FormattedValues);
        item.Tag = domain;
    }

    private void UpdateDomainInListView(Domain domain, ListViewItem listViewItem)
    {
        listViewItem.SubItems[0].Text = domain.Name;
        listViewItem.SubItems[1].Text = domain.FormattedValues;

        ResizeListView(DomainsListView);
    }

    private void UpdateDomainListBoxes()
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        var selectedItem = GetSelectedItem(DomainsListView);
        var domain = (Domain)selectedItem.Tag;

        ValuesListBox.Items.Clear();

        foreach (var value in domain.Values)
        {
            ValuesListBox.Items.Add(value.Value);
        }

        DomainVariableListBox.Items.Clear();

        var variables = knowledgeBase.GetVariablesByDomain(domain);

        foreach (var variable in variables)
        {
            DomainVariableListBox.Items.Add(variable.Name);
        }
    }

    #endregion

    #region UtilityMethods

    private static void InitializeListView(ListView listView, List<string> columns)
    {
        foreach (var column in columns)
        {
            listView.Columns.Add(column);
        }

        listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
    }

    private void PopulateLists()
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;

        var pug = new DomainValue("Мопс");
        var frenchBuldog = new DomainValue("Французский бульдог");
        var poodle = new DomainValue("Пудель");
        var dachshund = new DomainValue("Такса");
        var japaneseChin = new DomainValue("Японский хин");
        var chihuahua = new DomainValue("Чихуахуа");
        var rottweiler = new DomainValue("Ротвейлер");
        var chowChow = new DomainValue("Чау-чау");
        var labrador = new DomainValue("Лабрадор");
        var grif = new DomainValue("Брюссельский гриффон");
        var breedDomain = new Domain("Породы", new List<DomainValue>() { pug, frenchBuldog, poodle, dachshund, japaneseChin, chihuahua, rottweiler, chowChow, labrador, grif });
        knowledgeBase.Domains.Add(breedDomain);

        var choleric = new DomainValue("Холерик");
        var sanguine = new DomainValue("Сангвиник");
        var phlegmatic = new DomainValue("Флегматик");
        var melancholic = new DomainValue("Меланхолик");
        var temperament = new Domain("Темпераменты", new List<DomainValue>() { choleric, sanguine, phlegmatic, melancholic });
        knowledgeBase.Domains.Add(temperament);

        var low1 = new DomainValue("Низкая");
        var medium1 = new DomainValue("Средняя");
        var high1 = new DomainValue("Высокая");
        var lowMediumHigh1 = new Domain("Низкая / Средняя / Высокая", new List<DomainValue>() { low1, medium1, high1 });
        knowledgeBase.Domains.Add(lowMediumHigh1);

        var none = new DomainValue("Нет");
        var notMany = new DomainValue("Мало");
        var many = new DomainValue("Много");
        var noneNotManyMany = new Domain("Нет / Мало / Много", new List<DomainValue>() { none, notMany, many });
        knowledgeBase.Domains.Add(noneNotManyMany);

        var no = new DomainValue("Нет");
        var yes = new DomainValue("Да");
        var noYes = new Domain("Нет / Да", new List<DomainValue>() { no, yes });
        knowledgeBase.Domains.Add(noYes);

        var low2 = new DomainValue("Низкий");
        var medium2 = new DomainValue("Средний");
        var high2 = new DomainValue("Высокий");
        var lowMediumHigh2 = new Domain("Низкий / Средний / Высокий", new List<DomainValue>() { low2, medium2, high2 });
        knowledgeBase.Domains.Add(lowMediumHigh2);

        var unfavorable = new DomainValue("Неблагоприятные");
        var medium = new DomainValue("Средние");
        var favorable = new DomainValue("Благоприятные");
        var externalFactors = new Domain("Окружающие факторы", new List<DomainValue>() { unfavorable, medium, favorable });
        knowledgeBase.Domains.Add(externalFactors);

        var small = new DomainValue("Маленький");
        var medium3 = new DomainValue("Средний");
        var large = new DomainValue("Большой");
        var size = new Domain("Маленький / Средний / Большой", new List<DomainValue>() { small, medium3, large });
        knowledgeBase.Domains.Add(size);

        var small1 = new DomainValue("Маленькое");
        var medium4 = new DomainValue("Среднее");
        var large1 = new DomainValue("Большое");
        var attention = new Domain("Маленькое / Среднее / Большое", new List<DomainValue>() { small1, medium4, large1 });
        knowledgeBase.Domains.Add(attention);

        var bad = new DomainValue("Плохие");
        var medium5 = new DomainValue("Средние");
        var good = new DomainValue("Хорошие");
        var opportunities = new Domain("Плохие / Средние / Хорошие", new List<DomainValue>() { bad, medium5, good });
        knowledgeBase.Domains.Add(opportunities);

        var unhurried = new DomainValue("Неспешный");
        var active = new DomainValue("Активный");
        var VeryActive = new DomainValue("Очень активный");
        var pace = new Domain("Темпы прогулок", new List<DomainValue>() { unhurried, active, VeryActive });
        knowledgeBase.Domains.Add(pace);

        var lessThanHour = new DomainValue("Менее часа в день");
        var lessThanTwoHours = new DomainValue("От часу до двух в день");
        var moreThanTwoHours = new DomainValue("Более двух часов");
        var time = new Domain("Длительности прогулок", new List<DomainValue>() { lessThanHour, lessThanTwoHours, moreThanTwoHours });
        knowledgeBase.Domains.Add(time);

        var breed = new Variable("Порода", string.Empty, breedDomain, VariableType.Inferred);
        knowledgeBase.Variables.Add(breed);

        var temp = new Variable("Темперамент", string.Empty, temperament, VariableType.Inferred);
        var danger = new Variable("Потенциальная опасность", string.Empty, lowMediumHigh1, VariableType.Inferred);
        var others = new Variable("Другие обитатели", string.Empty, noneNotManyMany, VariableType.Inferred);
        var animals = new Variable("Другие животные", "Есть ли в семье другие животные?", noYes, VariableType.Requested);
        var children = new Variable("Маленькие дети", "Есть ли в семье маленькие дети?", noYes, VariableType.Requested);
        var alergy = new Variable("Алергия", "Есть ли в семье у кого-нибудь аллергия на собак?", noYes, VariableType.Requested);
        var defender = new Variable("Защитник", "Должна ли собака быть защитником?", noYes, VariableType.Requested);
        var noise = new Variable("Уровень шума", "Какой урвоень шума для вас приемлем?", lowMediumHigh2, VariableType.Requested);
        knowledgeBase.Variables.AddRange(new[] { temp, danger, others, animals, children, alergy, defender, noise });

        var exters = new Variable("Внешние факторы", string.Empty, externalFactors, VariableType.Inferred);
        var grooming = new Variable("Грумминг-салоны", "Есть ли поблизости грумминг-салоны?", noYes, VariableType.Requested);
        var heathRisk = new Variable("Опасность для здоровья", string.Empty, lowMediumHigh1, VariableType.Inferred);
        var drool = new Variable("Уровень слюноотделения", "Какой урвоень слюноотделения для вас приемлем?", lowMediumHigh2, VariableType.Requested);
        var climate = new Variable("Умеренный климат", "Является ли климат умеренным?", noYes, VariableType.Requested);
        var veter = new Variable("Ветеринарные клиники", "Есть ли поблизости ветеринарные клиники?", noYes, VariableType.Requested);
        var sze = new Variable("Размер", "Собаку какого размера вы хотите?", size, VariableType.Requested);
        knowledgeBase.Variables.AddRange(new[] { exters, grooming, heathRisk, drool, climate, veter, sze });

        var readiness = new Variable("Готовность стать хозяином", string.Empty, lowMediumHigh1, VariableType.Inferred);
        var expirience = new Variable("Опыт владения собаками", "Есть ли у вас опыт владения собаками?", noYes, VariableType.Requested);
        var closeness = new Variable("Близость с собакой", string.Empty, lowMediumHigh1, VariableType.Inferred);
        var atention = new Variable("Уделяемое внимание", string.Empty, attention, VariableType.Inferred);
        var missOften = new Variable("Частое отсутствие", "Вы часто отсутствуете дома", noYes, VariableType.Requested);
        var train = new Variable("Желание дрессировать", "Есть ли у вас желение заниматься дрессировкой", noYes, VariableType.Requested);
        var walks = new Variable("Возможности для прогулок", string.Empty, opportunities, VariableType.Inferred);
        var activity = new Variable("Активность хозяина", string.Empty, lowMediumHigh1, VariableType.Inferred);
        var walkDur = new Variable("Длительность прогулок", "Сколько времени в день вы готовы уделять прогулкам?", time, VariableType.Requested);
        var walkTemp = new Variable("Темп прогулок", "Какой темп прогулок вы предпочитаете?", pace, VariableType.Requested);
        var dacha = new Variable("Наличие дачи", "Есть ли у вас дача?", noYes, VariableType.InferredRequested);
        var house = new Variable("Частный дом", "Живёте ли в частном доме?", noYes, VariableType.Requested);
        knowledgeBase.Variables.AddRange(new[] { readiness, expirience, closeness, atention, missOften, train, walks, activity, walkDur, walkTemp, dacha, house });

        #region Breed
        var r1 = new Rule("R1", "Темперамент сангвиник, благоприятные внешние факторы, средний размер => Мопсик",
            new List<Fact>(new[] { new Fact(temp, sanguine), new Fact(exters, favorable), new Fact(sze, medium3) }),
            new List<Fact>(new[] { new Fact(breed, pug) }));
        var r2 = new Rule("R2", "Темперамент меланхолик, благоприятные внешние факторы, средний размер => Мопсик",
            new List<Fact>(new[] { new Fact(temp, melancholic), new Fact(exters, favorable), new Fact(sze, medium3) }),
            new List<Fact>(new[] { new Fact(breed, pug) }));
        knowledgeBase.Rules.AddRange(new[] { r1, r2 });

        var r3 = new Rule("R3", "Темперамент сангвиник, средние внешние факторы, маленький размер, готовность человека стать хозяином средняя => Чихуахуа",
            new List<Fact>(new[] { new Fact(temp, sanguine), new Fact(exters, medium), new Fact(sze, small), new Fact(readiness, medium1 ) }),
            new List<Fact>(new[] { new Fact(breed, chihuahua) }));
        var r4 = new Rule("R4", "Темперамент сангвиник, средние внешние факторы, маленький размер, готовность человека стать хозяином высокая => Чихуахуа",
            new List<Fact>(new[] { new Fact(temp, sanguine), new Fact(exters, medium), new Fact(sze, small), new Fact(readiness, high1) }),
            new List<Fact>(new[] { new Fact(breed, chihuahua) }));
        var r5 = new Rule("R5", "Темперамент сангвиник, благоприятные внешние факторы, маленький размер, готовность человека стать хозяином средняя => Чихуахуа",
            new List<Fact>(new[] { new Fact(temp, sanguine), new Fact(exters, favorable), new Fact(sze, small), new Fact(readiness, medium1) }),
            new List<Fact>(new[] { new Fact(breed, chihuahua) }));
        var r6 = new Rule("R6", "Темперамент сангвиник, благоприятные внешние факторы, маленький размер, готовность человека стать хозяином высокая => Чихуахуа",
            new List<Fact>(new[] { new Fact(temp, sanguine), new Fact(exters, favorable), new Fact(sze, small), new Fact(readiness, high1) }),
            new List<Fact>(new[] { new Fact(breed, chihuahua) }));
        var r7 = new Rule("R7", "Темперамент холерик, средние внешние факторы, маленький размер, готовность человека стать хозяином средняя => Чихуахуа",
           new List<Fact>(new[] { new Fact(temp, choleric), new Fact(exters, medium), new Fact(sze, small), new Fact(readiness, medium1) }),
           new List<Fact>(new[] { new Fact(breed, chihuahua) }));
        var r8 = new Rule("R8", "Темперамент холерик, средние внешние факторы, маленький размер, готовность человека стать хозяином высокая => Чихуахуа",
            new List<Fact>(new[] { new Fact(temp, choleric), new Fact(exters, medium), new Fact(sze, small), new Fact(readiness, high1) }),
            new List<Fact>(new[] { new Fact(breed, chihuahua) }));
        var r9 = new Rule("R9", "Темперамент холерик, благоприятные внешние факторы, маленький размер, готовность человека стать хозяином средняя => Чихуахуа",
            new List<Fact>(new[] { new Fact(temp, choleric), new Fact(exters, favorable), new Fact(sze, small), new Fact(readiness, medium1) }),
            new List<Fact>(new[] { new Fact(breed, chihuahua) }));
        var r10 = new Rule("R10", "Темперамент холерик, благоприятные внешние факторы, маленький размер, готовность человека стать хозяином высокая => Чихуахуа",
            new List<Fact>(new[] { new Fact(temp, choleric), new Fact(exters, favorable), new Fact(sze, small), new Fact(readiness, high1) }),
            new List<Fact>(new[] { new Fact(breed, chihuahua) }));
        knowledgeBase.Rules.AddRange(new[] { r3, r4, r5, r6, r7, r8, r9, r10 });

        var r11 = new Rule("R11", "Темперамент флегматик, средние внешние факторы, средний размер => Французский бульдог",
            new List<Fact>(new[] { new Fact(temp, phlegmatic), new Fact(exters, medium), new Fact(sze, medium3) }),
            new List<Fact>(new[] { new Fact(breed, frenchBuldog) }));
        var r12 = new Rule("R12", "Темперамент флегматик, благоприятные внешние факторы, средний размер => Французский бульдог",
            new List<Fact>(new[] { new Fact(temp, phlegmatic), new Fact(exters, favorable), new Fact(sze, medium3) }),
            new List<Fact>(new[] { new Fact(breed, frenchBuldog) }));
        knowledgeBase.Rules.AddRange(new[] { r11, r12 });

        var r13 = new Rule("R13", "Темперамент сангвиник, средние внешние факторы, большой размер, готовность человека стать хозяином средняя => Ротвейлер",
            new List<Fact>(new[] { new Fact(temp, sanguine), new Fact(exters, medium), new Fact(sze, large), new Fact(readiness, medium1) }),
            new List<Fact>(new[] { new Fact(breed, rottweiler) }));
        var r14 = new Rule("R14", "Темперамент сангвиник, средние внешние факторы, большой размер, готовность человека стать хозяином высокая => Ротвейлер",
            new List<Fact>(new[] { new Fact(temp, sanguine), new Fact(exters, medium), new Fact(sze, large), new Fact(readiness, high1) }),
            new List<Fact>(new[] { new Fact(breed, rottweiler) }));
        var r15 = new Rule("R15", "Темперамент сангвиник, благоприятные внешние факторы, большой размер, готовность человека стать хозяином средняя => Ротвейлер",
            new List<Fact>(new[] { new Fact(temp, sanguine), new Fact(exters, favorable), new Fact(sze, large), new Fact(readiness, medium1) }),
            new List<Fact>(new[] { new Fact(breed, rottweiler) }));
        var r16 = new Rule("R16", "Темперамент сангвиник, благоприятные внешние факторы, большой размер, готовность человека стать хозяином высокая => Ротвейлер",
            new List<Fact>(new[] { new Fact(temp, sanguine), new Fact(exters, favorable), new Fact(sze, large), new Fact(readiness, high1) }),
            new List<Fact>(new[] { new Fact(breed, rottweiler) }));
        var r17 = new Rule("R17", "Темперамент флегматик, средние внешние факторы, большой размер, готовность человека стать хозяином средняя => Ротвейлер",
            new List<Fact>(new[] { new Fact(temp, phlegmatic), new Fact(exters, medium), new Fact(sze, large), new Fact(readiness, medium1) }),
            new List<Fact>(new[] { new Fact(breed, rottweiler) }));
        var r18 = new Rule("R18", "Темперамент флегматик, средние внешние факторы, большой размер, готовность человека стать хозяином высокая => Ротвейлер",
            new List<Fact>(new[] { new Fact(temp, phlegmatic), new Fact(exters, medium), new Fact(sze, large), new Fact(readiness, high1) }),
            new List<Fact>(new[] { new Fact(breed, rottweiler) }));
        var r19 = new Rule("R19", "Темперамент флегматик, благоприятные внешние факторы, большой размер, готовность человека стать хозяином средняя => Ротвейлер",
            new List<Fact>(new[] { new Fact(temp, phlegmatic), new Fact(exters, favorable), new Fact(sze, large), new Fact(readiness, medium1) }),
            new List<Fact>(new[] { new Fact(breed, rottweiler) }));
        var r20 = new Rule("R20", "Темперамент флегматик, благоприятные внешние факторы, большой размер, готовность человека стать хозяином высокая => Ротвейлер",
            new List<Fact>(new[] { new Fact(temp, phlegmatic), new Fact(exters, favorable), new Fact(sze, large), new Fact(readiness, high1) }),
            new List<Fact>(new[] { new Fact(breed, rottweiler) }));
        knowledgeBase.Rules.AddRange(new[] { r13, r14, r15, r16, r17, r18, r19, r20 });

        var r21 = new Rule("R21", "Темперамент сангвиник, благоприятные внешние факторы, средний размер, готовность человека стать хозяином средняя => Пудель",
            new List<Fact>(new[] { new Fact(temp, sanguine), new Fact(exters, favorable), new Fact(sze, medium3), new Fact(readiness, medium1) }),
            new List<Fact>(new[] { new Fact(breed, poodle) }));
        var r22 = new Rule("R22", "Темперамент сангвиник, благоприятные внешние факторы, средний размер, готовность человека стать хозяином высокая => Пудель",
            new List<Fact>(new[] { new Fact(temp, sanguine), new Fact(exters, favorable), new Fact(sze, medium3), new Fact(readiness, high1) }),
            new List<Fact>(new[] { new Fact(breed, poodle) }));
        var r23 = new Rule("R23", "Темперамент флегматик, благоприятные внешние факторы, средний размер, готовность человека стать хозяином средняя => Пудель",
            new List<Fact>(new[] { new Fact(temp, phlegmatic), new Fact(exters, favorable), new Fact(sze, medium3), new Fact(readiness, medium1) }),
            new List<Fact>(new[] { new Fact(breed, poodle) }));
        var r24 = new Rule("R24", "Темперамент флегматик, благоприятные внешние факторы, средний размер, готовность человека стать хозяином высокая => Пудель",
            new List<Fact>(new[] { new Fact(temp, phlegmatic), new Fact(exters, favorable), new Fact(sze, medium3), new Fact(readiness, high1) }),
            new List<Fact>(new[] { new Fact(breed, poodle) }));
        knowledgeBase.Rules.AddRange(new[] { r21, r22, r23, r24 });

        var r25 = new Rule("R25", "Темперамент холерик, средние внешние факторы, большой размер, готовность человека стать хозяином высокая => Чау-чау",
            new List<Fact>(new[] { new Fact(temp, choleric), new Fact(exters, medium), new Fact(sze, large), new Fact(readiness, high1) }),
            new List<Fact>(new[] { new Fact(breed, chowChow) }));
        var r26 = new Rule("R26", "Темперамент холерик, благоприятные внешние факторы, большой размер, готовность человека стать хозяином высокая => Чау-чау",
            new List<Fact>(new[] { new Fact(temp, choleric), new Fact(exters, favorable), new Fact(sze, large), new Fact(readiness, high1) }),
            new List<Fact>(new[] { new Fact(breed, chowChow) }));
        var r27 = new Rule("R27", "Темперамент флегматик, средние внешние факторы, большой размер, готовность человека стать хозяином высокая => Чау-чау",
            new List<Fact>(new[] { new Fact(temp, phlegmatic), new Fact(exters, medium), new Fact(sze, large), new Fact(readiness, high1) }),
            new List<Fact>(new[] { new Fact(breed, chowChow) }));
        var r28 = new Rule("R28", "Темперамент флегматик, благоприятные внешние факторы, большой размер, готовность человека стать хозяином высокая => Чау-чау",
            new List<Fact>(new[] { new Fact(temp, phlegmatic), new Fact(exters, favorable), new Fact(sze, large), new Fact(readiness, high1) }),
            new List<Fact>(new[] { new Fact(breed, chowChow) }));
        knowledgeBase.Rules.AddRange(new[] { r25, r26, r27, r28 });

        var r29 = new Rule("R29", "Темперамент меланхолик, средний размер, готовность человека стать хозяином средняя => Такса",
            new List<Fact>(new[] { new Fact(temp, melancholic), new Fact(sze, medium3), new Fact(readiness, medium1) }),
            new List<Fact>(new[] { new Fact(breed, dachshund) }));
        var r30 = new Rule("R30", "Темперамент меланхолик, средний размер, готовность человека стать хозяином высокая => Такса",
            new List<Fact>(new[] { new Fact(temp, melancholic), new Fact(sze, medium3), new Fact(readiness, high1) }),
            new List<Fact>(new[] { new Fact(breed, dachshund) }));
        var r31 = new Rule("R31", "Темперамент флегматик, средний размер, готовность человека стать хозяином средняя => Такса",
            new List<Fact>(new[] { new Fact(temp, phlegmatic), new Fact(sze, medium3), new Fact(readiness, medium1) }),
            new List<Fact>(new[] { new Fact(breed, dachshund) }));
        var r32 = new Rule("R32", "Темперамент флегматик, средний размер, готовность человека стать хозяином высокая => Такса",
            new List<Fact>(new[] { new Fact(temp, phlegmatic), new Fact(sze, medium3), new Fact(readiness, high1) }),
            new List<Fact>(new[] { new Fact(breed, dachshund) }));
        knowledgeBase.Rules.AddRange(new[] { r29, r30, r31, r32 });

        var r33 = new Rule("R33", "Темперамент меланхолик, большой размер => Лабрадор",
            new List<Fact>(new[] { new Fact(temp, melancholic), new Fact(sze, large) }),
            new List<Fact>(new[] { new Fact(breed, labrador) }));
        var r34 = new Rule("R34", "Темперамент сангвиник, большой размер => Лабрадор",
            new List<Fact>(new[] { new Fact(temp, sanguine), new Fact(sze, large) }),
            new List<Fact>(new[] { new Fact(breed, labrador) }));
        knowledgeBase.Rules.AddRange(new[] { r33, r34 });

        var r35 = new Rule("R35", "Темперамент меланхолик, маленький размер => Японский хин",
            new List<Fact>(new[] { new Fact(temp, melancholic), new Fact(sze, small) }),
            new List<Fact>(new[] { new Fact(breed, japaneseChin) }));
        var r36 = new Rule("R36", "Темперамент флегматик, маленький размер => Японский хин",
            new List<Fact>(new[] { new Fact(temp, phlegmatic), new Fact(sze, small) }),
            new List<Fact>(new[] { new Fact(breed, japaneseChin) }));
        knowledgeBase.Rules.AddRange(new[] { r35, r36 });

        var r37 = new Rule("R37", "Темперамент сангвиник, средние внешние факторы, маленький размер => Брюссельский гриффон",
            new List<Fact>(new[] { new Fact(temp, sanguine), new(exters, medium), new Fact(sze, small) }),
            new List<Fact>(new[] { new Fact(breed, grif) }));
        var r38 = new Rule("R38", "Темперамент сангвиник, благоприятные внешние факторы, маленький размер => Брюссельский гриффон",
           new List<Fact>(new[] { new Fact(temp, sanguine), new(exters, favorable), new Fact(sze, small) }),
           new List<Fact>(new[] { new Fact(breed, grif) }));
        knowledgeBase.Rules.AddRange(new[] { r37, r38 });
        
        #endregion

        #region Temeprament

        var r39 = new Rule("R39", "Опасность для здоровья низкая, собака должна быть защитником, приемлемый уровень шума средний => Холерик",
            new List<Fact>(new[] { new Fact(heathRisk, low1), new(defender, yes), new(noise, medium2) }),
            new List<Fact>(new[] { new Fact(temp, choleric) }));
        var r40 = new Rule("R40", "Опасность для здоровья низкая, собака должна быть защитником, приемлемый уровень шума высокий => Холерик",
            new List<Fact>(new[] { new Fact(heathRisk, low1), new(defender, yes), new(noise, high2) }),
            new List<Fact>(new[] { new Fact(temp, choleric) }));
        var r41 = new Rule("R41", "Опасность для здоровья средняя, собака должна быть защитником, приемлемый уровень шума средний => Холерик",
           new List<Fact>(new[] { new Fact(heathRisk, medium1), new(defender, yes), new(noise, medium2) }),
           new List<Fact>(new[] { new Fact(temp, choleric) }));
        var r42 = new Rule("R42", "Опасность для здоровья средняя, собака должна быть защитником, приемлемый уровень шума высокий => Холерик",
            new List<Fact>(new[] { new Fact(heathRisk, medium1), new(defender, yes), new(noise, high2) }),
            new List<Fact>(new[] { new Fact(temp, choleric) }));
        knowledgeBase.Rules.AddRange(new[] { r39, r40, r41, r42 });

        var r43 = new Rule("R43", "Опасность для здоровья низкая, собака не должна быть защитником, приемлемый уровень шума средний => Сангвиник",
           new List<Fact>(new[] { new Fact(heathRisk, low1), new(defender, no), new(noise, medium2) }),
           new List<Fact>(new[] { new Fact(temp, sanguine) }));
        var r44 = new Rule("R44", "Опасность для здоровья низкая, собака не должна быть защитником, приемлемый уровень шума высокий => Сангвиник",
           new List<Fact>(new[] { new Fact(heathRisk, low1), new(defender, no), new(noise, high2) }),
           new List<Fact>(new[] { new Fact(temp, sanguine) }));
        var r45 = new Rule("R45", "Опасность для здоровья средняя, собака не должна быть защитником, приемлемый уровень шума средний => Сангвиник",
           new List<Fact>(new[] { new Fact(heathRisk, medium1), new(defender, no), new(noise, medium2) }),
           new List<Fact>(new[] { new Fact(temp, sanguine) }));
        var r46 = new Rule("R46", "Опасность для здоровья средняя, собака не должна быть защитником, приемлемый уровень шума высокий => Сангвиник",
           new List<Fact>(new[] { new Fact(heathRisk, medium1), new(defender, no), new(noise, high2) }),
           new List<Fact>(new[] { new Fact(temp, sanguine) }));
        knowledgeBase.Rules.AddRange(new[] { r43, r44, r45, r46 });

        var r47 = new Rule("R47", "Опасность для здоровья высокая, собака должна быть защитником, приемлемый уровень шума средний => Сангвиник",
           new List<Fact>(new[] { new Fact(heathRisk, high1), new(defender, yes), new(noise, medium2) }),
           new List<Fact>(new[] { new Fact(temp, sanguine) }));
        var r48 = new Rule("R48", "Опасность для здоровья высокая, собака должна быть защитником, приемлемый уровень шума высокий => Сангвиник",
           new List<Fact>(new[] { new Fact(heathRisk, high1), new(defender, yes), new(noise, high2) }),
           new List<Fact>(new[] { new Fact(temp, sanguine) }));
        knowledgeBase.Rules.AddRange(new[] { r47, r48 });

        var r49 = new Rule("R49", "Собака должна быть защитником, приемлемый уровень шума низкий => Флегматик",
           new List<Fact>(new[] { new Fact(defender, yes), new(noise, low2) }),
           new List<Fact>(new[] { new Fact(temp, phlegmatic) }));
        var r50 = new Rule("R50", "Опасность для здоровья низкая, собака не должна быть защитником, приемлемый уровень шума низкий => Флегматик",
           new List<Fact>(new[] { new Fact(heathRisk, low1), new Fact(defender, no), new(noise, low2) }),
           new List<Fact>(new[] { new Fact(temp, phlegmatic) }));
        knowledgeBase.Rules.AddRange(new[] { r49, r50 });

        var r51 = new Rule("R51", "Опасность для здоровья средняя, собака не должна быть защитником, приемлемый уровень шума низкий => Меланхолик",
          new List<Fact>(new[] { new Fact(heathRisk, medium1), new Fact(defender, no), new(noise, low2) }),
          new List<Fact>(new[] { new Fact(temp, melancholic) }));
        var r52 = new Rule("R52", "Опасность для здоровья высокая, собака не должна быть защитником => Меланхолик",
          new List<Fact>(new[] { new Fact(heathRisk, high1), new Fact(defender, no) }),
          new List<Fact>(new[] { new Fact(temp, melancholic) }));
        knowledgeBase.Rules.AddRange(new[] { r51, r52 });

        #endregion

        var r53 = new Rule("R53", "Других обитателей нет, ни у кого в семье нет аллергии на собак => Низкая",
            new List<Fact>(new[] { new Fact(others, none), new(alergy, no)}),
            new List<Fact>(new[] { new Fact(danger, low1) }));
        var r54 = new Rule("R54", "Других обитателей мало, ни у кого в семье нет аллергии на собак => Низкая",
           new List<Fact>(new[] { new Fact(others, notMany), new(alergy, no) }),
           new List<Fact>(new[] { new Fact(danger, low1) }));
        knowledgeBase.Rules.AddRange(new[] { r53, r54 });

        var r55 = new Rule("R55", "Других обитателей много, ни у кого в семье нет аллергии на собак => Средняя",
            new List<Fact>(new[] { new Fact(others, many), new(alergy, no) }),
            new List<Fact>(new[] { new Fact(danger, medium1) }));
        var r56 = new Rule("R56", "Других обитателей нет, у кого-то в семье есть аллергии на собак => Средняя",
            new List<Fact>(new[] { new Fact(others, none), new(alergy, yes) }),
            new List<Fact>(new[] { new Fact(danger, medium1) }));
        knowledgeBase.Rules.AddRange(new[] { r55, r56 });

        var r57 = new Rule("R57", "Других обитателей мало, у кого-то в семье есть аллергии на собак => Высокая",
            new List<Fact>(new[] { new Fact(others, notMany), new(alergy, yes) }),
            new List<Fact>(new[] { new Fact(danger, high1) }));
        var r58 = new Rule("R58", "Других обитателей много, у кого-то в семье есть аллергии на собак => Высокая",
           new List<Fact>(new[] { new Fact(others, many), new(alergy, yes) }),
           new List<Fact>(new[] { new Fact(danger, high1) }));
        knowledgeBase.Rules.AddRange(new[] { r57, r58 });

        var r59 = new Rule("R59", "Нет других домашних животных и маленьких детей => Нет",
           new List<Fact>(new[] { new Fact(animals, no), new(children, no) }),
           new List<Fact>(new[] { new Fact(others, none) }));
        var r60 = new Rule("R60", "Нет других домашних животных, но есть маленькие дети => Мало",
           new List<Fact>(new[] { new Fact(animals, no), new(children, yes) }),
           new List<Fact>(new[] { new Fact(others, notMany) }));
        var r61 = new Rule("R61", "Есть другие домашние животные, но нет маленьких детей => Мало",
           new List<Fact>(new[] { new Fact(animals, yes), new(children, no) }),
           new List<Fact>(new[] { new Fact(others, notMany) }));
        var r62 = new Rule("R62", "Есть другие домашние животные и маленькие дети => Много",
           new List<Fact>(new[] { new Fact(animals, yes), new(children, yes) }),
           new List<Fact>(new[] { new Fact(others, many) }));
        knowledgeBase.Rules.AddRange(new[] { r59, r60, r61, r62 });

        var r63 = new Rule("R63", "Поблизости нет грумминг-салонов, высокая опасность для здоровья => Неблагоприятные",
            new List<Fact>(new[] { new Fact(grooming, no), new(heathRisk, high1) }),
            new List<Fact>(new[] { new Fact(exters, unfavorable) }));
        var r64 = new Rule("R64", "Поблизости нет грумминг-салонов, низкая опасность для здоровья => Средние",
            new List<Fact>(new[] { new Fact(grooming, no), new(heathRisk, low1) }),
            new List<Fact>(new[] { new Fact(exters, medium) }));
        var r65 = new Rule("R65", "Поблизости нет грумминг-салонов, средняя опасность для здоровья => Средние",
            new List<Fact>(new[] { new Fact(grooming, no), new(heathRisk, medium1) }),
            new List<Fact>(new[] { new Fact(exters, medium) }));
        var r66 = new Rule("R66", "Поблизости есть грумминг-салоны, высокая опасность для здоровья => Средние",
            new List<Fact>(new[] { new Fact(grooming, yes), new(heathRisk, high1) }),
            new List<Fact>(new[] { new Fact(exters, medium) }));
        var r67 = new Rule("R67", "Поблизости есть грумминг-салоны, низкая опасность для здоровья => Благоприятные",
            new List<Fact>(new[] { new Fact(grooming, yes), new(heathRisk, low1) }),
            new List<Fact>(new[] { new Fact(exters, favorable) }));
        var r68 = new Rule("R68", "Поблизости есть грумминг-салоны, средняя опасность для здоровья => Благоприятные",
            new List<Fact>(new[] { new Fact(grooming, yes), new(heathRisk, medium1) }),
            new List<Fact>(new[] { new Fact(exters, favorable) }));
        knowledgeBase.Rules.AddRange(new[] { r63, r64, r65, r66, r67, r68 });

        var r69 = new Rule("R69", "Приемлемый уровень слюноотделения низкий, климат умеренный, поблизости есть ветеринарные клиники => Низкая",
            new List<Fact>(new[] { new Fact(drool, low2), new(climate, yes), new(veter, yes)}),
            new List<Fact>(new[] { new Fact(heathRisk, low1) }));
        var r70 = new Rule("R70", "Приемлемый уровень слюноотделения средний, климат умеренный, поблизости есть ветеринарные клиники => Низкая",
            new List<Fact>(new[] { new Fact(drool, medium2), new(climate, yes), new(veter, yes) }),
            new List<Fact>(new[] { new Fact(heathRisk, low1) }));
        var r71 = new Rule("R71", "Приемлемый уровень слюноотделения низкий, климат не умеренный, поблизости есть ветеринарные клиники => Низкая",
            new List<Fact>(new[] { new Fact(drool, low2), new(climate, no), new(veter, yes) }),
            new List<Fact>(new[] { new Fact(heathRisk, low1) }));
        var r72 = new Rule("R72", "Приемлемый уровень слюноотделения низкий, поблизости нет ветеринарных клиник => Средняя",
            new List<Fact>(new[] { new Fact(drool, low2), new(veter, no) }),
            new List<Fact>(new[] { new Fact(heathRisk, medium1) }));
        var r73 = new Rule("R73", "Приемлемый уровень слюноотделения средний, поблизости есть ветеринарные клиники => Средняя",
            new List<Fact>(new[] { new Fact(drool, medium2), new(veter, yes) }),
            new List<Fact>(new[] { new Fact(heathRisk, medium1) }));
        var r74 = new Rule("R74", "Приемлемый уровень слюноотделения средний, климат умеренный => Средняя",
            new List<Fact>(new[] { new Fact(drool, medium2), new(climate, yes) }),
            new List<Fact>(new[] { new Fact(heathRisk, medium1) }));
        var r75 = new Rule("R75", "Приемлемый уровень слюноотделения высокий, поблизости есть ветеринарные клиники => Средняя",
            new List<Fact>(new[] { new Fact(drool, high2), new(veter, yes) }),
            new List<Fact>(new[] { new Fact(heathRisk, medium1) }));
        var r76 = new Rule("R76", "Приемлемый уровень слюноотделения средний, климат не умеренный, поблизости нет ветеринарных клиник => Высокая",
            new List<Fact>(new[] { new Fact(drool, medium2), new(climate, no), new(veter, no) }),
            new List<Fact>(new[] { new Fact(heathRisk, high1) }));
        var r77 = new Rule("R77", "Приемлемый уровень слюноотделения высокий, поблизости нет ветеринарных клиник => Высокая",
            new List<Fact>(new[] { new Fact(drool, high2), new(veter, no) }),
            new List<Fact>(new[] { new Fact(heathRisk, high1) }));
        knowledgeBase.Rules.AddRange(new[] { r69, r70, r71, r72, r73, r74, r75, r76, r77 });

        var r78 = new Rule("R78", "Нет опыта владения собаками и низкая близость => Низкая",
            new List<Fact>(new[] { new Fact(expirience, no), new(closeness, low1) }),
            new List<Fact>(new[] { new Fact(readiness, low1) }));
        var r79 = new Rule("R79", "Нет опыта владения собаками и средняя близость => Средняя",
            new List<Fact>(new[] { new Fact(expirience, no), new(closeness, medium1) }),
            new List<Fact>(new[] { new Fact(readiness, medium1) }));
        var r80 = new Rule("R80", "Есть опыт владения собаками и низкая близость => Средняя",
            new List<Fact>(new[] { new Fact(expirience, yes), new(closeness, low1) }),
            new List<Fact>(new[] { new Fact(readiness, medium1) }));
        var r81 = new Rule("R81", "Есть опыт владения собаками и средняя близость => Средняя",
            new List<Fact>(new[] { new Fact(expirience, yes), new(closeness, medium1) }),
            new List<Fact>(new[] { new Fact(readiness, medium1) }));
        var r82 = new Rule("R82", "Высокая близость => Высокая",
            new List<Fact>(new[] { new Fact(closeness, high1) }),
            new List<Fact>(new[] { new Fact(readiness, high1) }));
        knowledgeBase.Rules.AddRange(new[] { r78, r79, r80, r81, r82 });

        var r83 = new Rule("R83", "Уделяется мало внимания, при этом возможности для прогулок плохие => Низкая",
           new List<Fact>(new[] { new Fact(atention, small1), new(walks, bad) }),
           new List<Fact>(new[] { new Fact(closeness, low1) }));
        var r84 = new Rule("R84", "Уделяется мало внимания, при этом возможности для прогулок средние => Низкая",
           new List<Fact>(new[] { new Fact(atention, small1), new(walks, medium5) }),
           new List<Fact>(new[] { new Fact(closeness, low1) }));
        var r85 = new Rule("R85", "Уделяется мало внимания, при этом возможности для прогулок хорошие => Средняя",
           new List<Fact>(new[] { new Fact(atention, small1), new(walks, good) }),
           new List<Fact>(new[] { new Fact(closeness, medium1) }));
        var r86 = new Rule("R86", "Уделяется среднее количество внимания, при этом возможности для прогулок плохие => Средняя",
           new List<Fact>(new[] { new Fact(atention, medium4), new(walks, bad) }),
           new List<Fact>(new[] { new Fact(closeness, medium1) }));
        var r87 = new Rule("R87", "Уделяется среднее количество внимания, при этом возможности для прогулок средние => Средняя",
           new List<Fact>(new[] { new Fact(atention, medium4), new(walks, medium5) }),
           new List<Fact>(new[] { new Fact(closeness, medium1) }));
        var r88 = new Rule("R88", "Уделяется большое количество внимания, при этом возможности для прогулок плохие => Средняя",
           new List<Fact>(new[] { new Fact(atention, large1), new(walks, bad) }),
           new List<Fact>(new[] { new Fact(closeness, medium1) }));
        var r89 = new Rule("R89", "Уделяется среднее количество внимания, при этом возможности для прогулок хорошие => Высокая",
           new List<Fact>(new[] { new Fact(atention, medium4), new(walks, good) }),
           new List<Fact>(new[] { new Fact(closeness, high1) }));
        var r90 = new Rule("R90", "Уделяется большое количество внимания, при этом возможности для прогулок средние => Высокая",
           new List<Fact>(new[] { new Fact(atention, large1), new(walks, medium5) }),
           new List<Fact>(new[] { new Fact(closeness, high1) }));
        var r91 = new Rule("R91", "Уделяется большое количество внимания, при этом возможности для прогулок хорошие => Высокая",
           new List<Fact>(new[] { new Fact(atention, large1), new(walks, good) }),
           new List<Fact>(new[] { new Fact(closeness, high1) }));
        knowledgeBase.Rules.AddRange(new[] { r83, r84, r85, r86, r87, r88, r89, r90, r91 });

        var r92 = new Rule("R92", "Хозяин часто отсутствует дома и не желает заниматься дрессировкой => Маленькое",
           new List<Fact>(new[] { new Fact(missOften, yes), new(train, no) }),
           new List<Fact>(new[] { new Fact(atention, small1) }));
        var r93 = new Rule("R93", "Хозяин часто отсутствует дома, но желает заниматься дрессировкой => Среднее",
           new List<Fact>(new[] { new Fact(missOften, yes), new(train, yes) }),
           new List<Fact>(new[] { new Fact(atention, medium4) }));
        var r94 = new Rule("R94", "Хозяин редко отсутствует дома, но не желает заниматься дрессировкой => Среднее",
           new List<Fact>(new[] { new Fact(missOften, no), new(train, no) }),
           new List<Fact>(new[] { new Fact(atention, medium4) }));
        var r95 = new Rule("R95", "Хозяин редко отсутствует дома и желает заниматься дрессировкой => Большое",
           new List<Fact>(new[] { new Fact(missOften, no), new(train, yes) }),
           new List<Fact>(new[] { new Fact(atention, large1) }));
        knowledgeBase.Rules.AddRange(new[] { r92, r93, r94, r95 });

        var r96 = new Rule("R96", "Активность хозяина низкая, при этом он живёт в квартире и не имеет дачи => Плохие",
           new List<Fact>(new[] { new Fact(activity, low1), new(house, no), new(dacha, no) }),
           new List<Fact>(new[] { new Fact(walks, bad) }));
        var r97 = new Rule("R97", "Активность хозяина средняя, при этом он живёт в квартире и не имеет дачи => Плохие",
           new List<Fact>(new[] { new Fact(activity, medium1), new(house, no), new(dacha, no) }),
           new List<Fact>(new[] { new Fact(walks, bad) }));
        var r98 = new Rule("R98", "Активность хозяина низкая, при этом он живёт в частном доме => Средние",
           new List<Fact>(new[] { new Fact(activity, low1), new(house, yes) }),
           new List<Fact>(new[] { new Fact(walks, medium5) }));
        var r99 = new Rule("R99", "Активность хозяина низкая, при этом он живёт в квартире, но имеет дачу => Средние",
           new List<Fact>(new[] { new Fact(activity, low1), new(dacha, yes) }),
           new List<Fact>(new[] { new Fact(walks, medium5) }));
        var r100 = new Rule("R100", "Активность хозяина высокая, при этом он живёт в квартире и не имеет дачу => Средние",
           new List<Fact>(new[] { new Fact(activity, high1), new(house, no), new(dacha, no) }),
           new List<Fact>(new[] { new Fact(walks, medium5) }));
        var r101 = new Rule("R101", "Активность хозяина средняя и он живёт в частном доме => Хорошие",
           new List<Fact>(new[] { new Fact(activity, medium1), new(house, yes) }),
           new List<Fact>(new[] { new Fact(walks, good) }));
        var r102 = new Rule("R102", "Активность хозяина высокая и он живёт в частном доме => Хорошие",
           new List<Fact>(new[] { new Fact(activity, high1), new(house, yes) }),
           new List<Fact>(new[] { new Fact(walks, good) }));
        var r103 = new Rule("R103", "Активность хозяина высокая, при этом он живёт в квартире и имеет дачу => Хорошие",
           new List<Fact>(new[] { new Fact(activity, high1), new(house, no), new(dacha, yes) }),
           new List<Fact>(new[] { new Fact(walks, good) }));
        knowledgeBase.Rules.AddRange(new[] { r96, r97, r98, r99, r100, r101, r102, r103 });

        var r104 = new Rule("R104", "Длина прогулок меньше часа при неспешном темпе => Низкая",
           new List<Fact>(new[] { new Fact(walkDur, lessThanHour), new(walkTemp, unhurried) }),
           new List<Fact>(new[] { new Fact(activity, low1) }));
        var r105 = new Rule("R105", "Длина прогулок меньше часа при активном темпе => Низкая",
           new List<Fact>(new[] { new Fact(walkDur, lessThanHour), new(walkTemp, active) }),
           new List<Fact>(new[] { new Fact(activity, low1) }));
        var r106 = new Rule("R106", "Длина прогулок меньше двух часов при неспешном темпе => Низкая",
           new List<Fact>(new[] { new Fact(walkDur, lessThanTwoHours), new(walkTemp, unhurried) }),
           new List<Fact>(new[] { new Fact(activity, low1) }));
        var r107 = new Rule("R107", "Длина прогулок меньше часа при очень активном темпе => Средняя",
           new List<Fact>(new[] { new Fact(walkDur, lessThanHour), new(walkTemp, VeryActive) }),
           new List<Fact>(new[] { new Fact(activity, medium1) }));
        var r108 = new Rule("R108", "Длина прогулок меньше двух часов при активном темпе => Средняя",
           new List<Fact>(new[] { new Fact(walkDur, lessThanTwoHours), new(walkTemp, active) }),
           new List<Fact>(new[] { new Fact(activity, medium1) }));
        var r109 = new Rule("R109", "Длина прогулок больше двух часов при неспешном темпе => Средняя",
           new List<Fact>(new[] { new Fact(walkDur, moreThanTwoHours), new(walkTemp, unhurried) }),
           new List<Fact>(new[] { new Fact(activity, medium1) }));
        var r110 = new Rule("R110", "Длина прогулок меньше двух часов при очень активном темпе => Высокая",
           new List<Fact>(new[] { new Fact(walkDur, lessThanTwoHours), new(walkTemp, VeryActive) }),
           new List<Fact>(new[] { new Fact(activity, high1) }));
        var r111 = new Rule("R111", "Длина прогулок больше двух часов при активном темпе => Высокая",
           new List<Fact>(new[] { new Fact(walkDur, moreThanTwoHours), new(walkTemp, active) }),
           new List<Fact>(new[] { new Fact(activity, high1) }));
        var r112 = new Rule("R112", "Длина прогулок больше двух часов при очень активном темпе => Высокая",
           new List<Fact>(new[] { new Fact(walkDur, moreThanTwoHours), new(walkTemp, VeryActive) }),
           new List<Fact>(new[] { new Fact(activity, high1) }));
        knowledgeBase.Rules.AddRange(new[] { r104, r105, r106, r107, r108, r109, r110, r111, r112 });

        var r113 = new Rule("R113", "Хозяин живёт в частном доме => У него есть дача",
          new List<Fact>(new[] { new Fact(house, yes) }),
          new List<Fact>(new[] { new Fact(dacha, yes) }));
        knowledgeBase.Rules.Add(r113);
    }

    private void InitializeListViews()
    {
        InitializeListView(RulesListView, new List<string> { "Имя", "Описание" });
        DisplayRules();

        InitializeListView(VariablesListView, new List<string> { "Имя", "Тип", "Домен" });
        DisplayVariables();

        InitializeListView(DomainsListView, new List<string> { "Имя", "Значения" });
        DisplayDomains();
    }

    private void DisplayRules()
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        RulesListView.Items.Clear();

        foreach (var rule in knowledgeBase.Rules)
        {
            AddRuleToListView(rule);
        }

        ResizeListView(RulesListView);
    }

    private void DisplayVariables()
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        VariablesListView.Items.Clear();

        foreach (var variable in knowledgeBase.Variables)
        {
            AddVariableToListView(variable);
        }

        ResizeListView(VariablesListView);
    }

    private void DisplayDomains()
    {
        var knowledgeBase = _expertSystemShell.KnowledgeBase;
        DomainsListView.Items.Clear();

        foreach (var domain in knowledgeBase.Domains)
        {
            AddDomainToListView(domain);
        }

        ResizeListView(DomainsListView);
    }

    private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (var listView in new[] { RulesListView, VariablesListView, DomainsListView })
        {
            listView.SelectedItems.Clear();
        }
    }

    private static ListViewItem GetSelectedItem(ListView listView) => listView.SelectedItems[0];

    private static int GetSelectedIndex(ListView listView) => listView.SelectedIndices.Count > 0 ? listView.SelectedIndices[0] : -1;

    private static void ResizeListView(ListView listView) => listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

    private static void ShowErrorMessageBox(string message) => MessageBox.Show(message, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    #endregion
}