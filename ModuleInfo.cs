/*
 * Copyright (C) 2026 Roman Stolyarov <rshome@mail.ru>
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 */
using OutfitTool.API1;
using OutfitTool.API1.Dto;

namespace Timer;

internal class ModuleInfo : IModuleInfo
{
    public string Name => "timer";
    public string AssemblyName => "Timer";
    public string DisplayName => "Timer";
    public string Description => "Таймер";
    public ModuleVersion Version => new (1, 1, "release");
    public string Changes => "1.1: исправлены остановка таймеров и повторная инициализация";
    public string Author => "Stolyarov Roman";
    public string AuthorContacts => "rshome@mail.ru";
    public int RequireApiVersion => 1;
}