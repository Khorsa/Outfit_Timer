/*
 * Copyright (C) 2026 Roman Stolyarov <rshome@mail.ru>
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 */
using System.Media;
using System.Windows;

namespace Timer;

internal class Beep
{
    public static void Sound()
    {
        var uri = new Uri("pack://application:,,,/Timer;component/Resources/clock_alarm.wav", UriKind.Absolute);
        var res =Application.GetResourceStream(uri);
        if (res == null) return;
        var sndPing = new SoundPlayer(res.Stream);
        sndPing.Play();
    }
}