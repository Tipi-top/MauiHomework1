using System.Net.Mail;

namespace MauiHomework1;

public partial class MainPage : ContentPage
{
    private readonly DateTime _initialDate = new(2026, 1, 1);
    private bool _isInitialized;

    public MainPage()
    {
        InitializeComponent();
        _isInitialized = true;
        UpdateRequiredHint();
    }

    private void OnRequiredFieldChanged(object? sender, TextChangedEventArgs e)
    {
        // TextChanged может сработать ещё при создании элементов из XAML.
        if (!_isInitialized)
            return;

        UpdateRequiredHint();
        _ErrorLabel_.Text = string.Empty;
        _ErrorLabel_.IsVisible = false;
        _ResultLabel_.Text = string.Empty;
        _ResultStack_.IsVisible = false;
    }

    private void UpdateRequiredHint()
    {
        _RequiredHintLabel_.IsVisible =
            string.IsNullOrWhiteSpace(_NameEntry_.Text) ||
            string.IsNullOrWhiteSpace(_EmailEntry_.Text);
    }

    private async void OnShowResultClicked(object? sender, EventArgs e)
    {
        string name = _NameEntry_.Text?.Trim() ?? string.Empty;
        string email = _EmailEntry_.Text?.Trim() ?? string.Empty;
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add("Введите имя.");
        if (string.IsNullOrWhiteSpace(email))
            errors.Add("Введите email.");
        else if (!MailAddress.TryCreate(email, out var address) || address.Address != email)
            errors.Add("Проверьте email: например, name@example.com.");

        if (errors.Count > 0)
        {
            _ResultLabel_.Text = string.Empty;
            _ResultStack_.IsVisible = false;
            _ErrorLabel_.Text = string.Join(Environment.NewLine, errors);
            _ErrorLabel_.IsVisible = true;
            await _MainScrollView_.ScrollToAsync(_ErrorLabel_, ScrollToPosition.MakeVisible, true);
            return;
        }

        _ErrorLabel_.Text = string.Empty;
        _ErrorLabel_.IsVisible = false;

        string platform = "не выбрана";
        if (_AndroidRadio_.IsChecked)
            platform = _AndroidRadio_.Value.ToString()!;
        else if (_IosRadio_.IsChecked)
            platform = _IosRadio_.Value.ToString()!;
        else if (_WindowsRadio_.IsChecked)
            platform = _WindowsRadio_.Value.ToString()!;

        var technologies = new List<string>();
        if (_DotnetCheckBox_.IsChecked)
            technologies.Add(".NET / C#");
        if (_SqlCheckBox_.IsChecked)
            technologies.Add("SQL");

        string about = string.IsNullOrWhiteSpace(_AboutEditor_.Text)
            ? "не указано" : _AboutEditor_.Text.Trim();
        string direction = _DirectionPicker_.SelectedItem?.ToString() ?? "не выбрано";
        string technologyText = technologies.Count > 0 ? string.Join(", ", technologies) : "не выбраны";
        string news = _NewsSwitch_.IsToggled ? "да" : "нет";
        string date = _StartDatePicker_.Date is DateTime selectedDate
            ? selectedDate.ToString("dd.MM.yyyy") : "не указана";

        _ResultLabel_.Text =
            $"Имя: {name}\n" +
            $"Email: {email}\n" +
            $"О себе: {about}\n" +
            $"Направление: {direction}\n" +
            $"Платформа: {platform}\n" +
            $"Технологии: {technologyText}\n" +
            $"Новости: {news}\n" +
            $"Начало обучения: {date}\n" +
            $"Уровень опыта: {_ExperienceSlider_.Value:F1} из 10";
        _ResultStack_.IsVisible = true;
        await _MainScrollView_.ScrollToAsync(_ResultStack_, ScrollToPosition.Start, true);
    }

    private async void OnClearClicked(object? sender, EventArgs e)
    {
        _NameEntry_.Text = string.Empty;
        _EmailEntry_.Text = string.Empty;
        _AboutEditor_.Text = string.Empty;
        _DirectionPicker_.SelectedIndex = -1;
        _AndroidRadio_.IsChecked = false;
        _IosRadio_.IsChecked = false;
        _WindowsRadio_.IsChecked = false;
        _DotnetCheckBox_.IsChecked = false;
        _SqlCheckBox_.IsChecked = false;
        _NewsSwitch_.IsToggled = false;
        _StartDatePicker_.Date = _initialDate;
        _ExperienceSlider_.Value = 0;
        _ResultLabel_.Text = string.Empty;
        _ResultStack_.IsVisible = false;
        _ErrorLabel_.Text = string.Empty;
        _ErrorLabel_.IsVisible = false;
        UpdateRequiredHint();
        await _MainScrollView_.ScrollToAsync(0, 0, true);
    }
}
