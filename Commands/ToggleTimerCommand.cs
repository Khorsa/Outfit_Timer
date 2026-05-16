/*
 * Copyright (C) 2026 Roman Stolyarov <rshome@mail.ru>
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 */
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Windows;
using OutfitTool.API1;
using OutfitTool.API1.Dto;

namespace Timer.Commands;

class ToggleTimerCommand: ICommand
{
    public string Name => "ShowHideTimer";
    public string Description => LocalizationHelper.GetString("Timer");
    public CommandContextMenu? ContextMenu => GetContextMenu();

    public CommandContextMenu? GetContextMenu()
    {
        string resourceName = "timer.png";
        return new CommandContextMenu(
            LocalizationHelper.GetString("Timer"),
            new BitmapImage(
                new Uri("pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/Resources/" + resourceName)
            )
        );
    }

    public void Run()
    {
        if (ModuleController.TimerForm == null)
        {
            return;
        }

        if (ModuleController.TimerForm.Visibility == Visibility.Visible)
        {
            if (ModuleController.TimerForm.timerTabs.SelectedIndex == 0)
            {
                ModuleController.TimerForm.timerTabs.SelectedIndex = 1;
            }
            else
            {
                ModuleController.TimerForm.Hide();
                ModuleController.TimerForm.Visibility = Visibility.Hidden;
            }
        }
        else
        {
            ModuleController.TimerForm.Topmost = true;
            ModuleController.TimerForm.Show();
            ModuleController.TimerForm.Activate();
            ModuleController.TimerForm.timerTabs.SelectedIndex = 1;
            ModuleController.TimerForm.Focus();
        }
    }
}