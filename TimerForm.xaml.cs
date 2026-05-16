/*
 * Copyright (C) 2026 Roman Stolyarov <rshome@mail.ru>
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 */
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Timer;

public partial class TimerForm
{
    private const int STOPWATCH_INTERVAL = 10;
    private const int TIMER_INTERVAL = 100;

    private readonly DispatcherTimer _stopWatchTimer = new();
    private readonly List<DateTime> _cutoffs = [];

    public TimerForm()
    {
        _stopWatchTimer.Interval = TimeSpan.FromMilliseconds(STOPWATCH_INTERVAL);
        _stopWatchTimer.Tick += StopWatchTimer_Tick;

        _timerTimer.Interval = TimeSpan.FromMilliseconds(TIMER_INTERVAL);
        _timerTimer.Tick += TimerTimer_Tick;

        LocalizationHelper.LanguageChanged += LanguageChanged;
        InitializeComponent();
    }

    private void StopWatchTimer_Tick(object? sender, EventArgs e)
    {
        string text = "";
        DateTime first = _cutoffs.FirstOrDefault(DateTime.Now);
        foreach (DateTime dt in _cutoffs.GetRange(1, _cutoffs.Count - 1))
        {
            text += FormatTimerOutput(dt - first) + "\n";
        }
        text += FormatTimerOutput(DateTime.Now - first);
        TimerText.Text = text;
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel = true;
        this.Hide();
    }

    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        if (_stopWatchTimer.IsEnabled)
        {
            _cutoffs.Add(DateTime.Now);
        }
        else {
            _cutoffs.Clear();
            _cutoffs.Add(DateTime.Now);
            _stopWatchTimer.Start();
            StopButton.IsEnabled = true;
            SetStartButtonText("Cutoff");
        }
    }

    private void StopButton_Click(object sender, RoutedEventArgs e)
    {
        StopButton.IsEnabled = false;
        SetStartButtonText("Start");
        _stopWatchTimer.Stop();
    }


    private void SetStartButtonText(string textKey)
    {
        StartButton.Content = LocalizationHelper.GetString(textKey);
    }



    private string FormatTimerOutput(TimeSpan dateTime)
    {
        string text = "";
        if (dateTime.Days != 0)
        {
            text += dateTime.Days + ":";
        }
        if (dateTime.Hours != 0)
        {
            text += dateTime.Hours.ToString("D2") + ":";
        }
        text += dateTime.Minutes.ToString("D2") + ":" + dateTime.Seconds.ToString("D2") + "." + dateTime.Milliseconds.ToString("D3");

        return text;
    }



    private void LanguageChanged(Object? sender, EventArgs e)
    {
        ResourceDictionary oldDict = (from d in this.Resources.MergedDictionaries
            where d.Source != null && d.Source.OriginalString.Contains("Languages/lang.")
            select d).First();
        if (oldDict != null)
        {
            int ind = this.Resources.MergedDictionaries.IndexOf(oldDict);
            this.Resources.MergedDictionaries.Remove(oldDict);
            this.Resources.MergedDictionaries.Insert(ind, LocalizationHelper.CurrentDictionary);
        }
        else
        {
            this.Resources.MergedDictionaries.Add(LocalizationHelper.CurrentDictionary);
        }

        SetStartButtonText(_stopWatchTimer.IsEnabled ? "Cutoff" : "Start");
    }


    private readonly DispatcherTimer _timerTimer = new();
    private TimeSpan _currentTimer = TimeSpan.Zero;
    private TimeSpan CurrentTimer {
        set
        {
            _currentTimer = value;
            TimerTextBlock.Text = (value.Days * 24 + value.Hours).ToString("D2") + ":" + value.Minutes.ToString("D2") + ":" + value.Seconds.ToString("D2") + "." + (value.Milliseconds / 100).ToString("D1");
        }
        get => _currentTimer;
    }

    private void TimerButtons_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button) return;
        var tag = button.Tag.ToString();
        if (tag == "Reset")
        {
            CurrentTimer = TimeSpan.Zero;
            return;
        }
        var plusSeconds = int.Parse(tag ?? "0");
        CurrentTimer += new TimeSpan(0,0,plusSeconds);
    }

    private void TimerStart_Click(object sender, RoutedEventArgs e)
    {
        _timerTimer.Start();
    }

    private void TimerTimer_Tick(object? sender, EventArgs e)
    {
        CurrentTimer -= new TimeSpan(0,0,0,0, TIMER_INTERVAL);
        if (CurrentTimer >= TimeSpan.Zero) return;
        _timerTimer.Stop();
        CurrentTimer = TimeSpan.Zero;
        Beep.Sound();
    }


}