GNU GPL v3 license — you can do anything you want, but you must give me credit and distribute your changes under the same terms.

Timer is a WPF-based time management application implemented as a module for the OutfitTool ecosystem. The application provides two main functions: a stopwatch for measuring elapsed time and a countdown timer for time-limited tasks.

Key features:

- **Dual-mode interface**: Separate tabs for stopwatch and timer functionality within a single window
- **Stopwatch mode**: Measures elapsed time with millisecond precision, displays lap times, and supports cutoff recording
- **Countdown timer**: Configurable timer with second-level increments (+10s, +60s, +5min, +20min, +1h, + 6h), visual countdown display, and audible alarm
- **Audio feedback**: Plays a WAV sound file (clock_alarm.wav) when countdown reaches zero using SoundPlayer
- **Localization support**: Dynamic language switching through LocalizationHelper with resource dictionaries
- **System integration**: Window minimization to system tray with show/hide commands

The application is implemented as an OutfitTool API1 module with ModuleController managing lifecycle and two commands: ToggleStopwatchCommand and ToggleTimerCommand. The UI is built with WPF and uses DispatcherTimer for time updates (10ms interval for stopwatch, 100ms for timer). Time formatting handles days, hours, minutes, seconds, and milliseconds with appropriate zero-padding. The Beep class provides audio notification using embedded resources and Application.GetResourceStream for secure resource access.

---

Лицензия GNU GPL v3 — вы можете делать с этим что угодно, но должны указать авторство и распространять свои изменения на тех же условиях.

Timer — это приложение для управления временем на базе WPF, реализованное как модуль экосистемы OutfitTool. Приложение предоставляет две основные функции: секундомер для измерения прошедшего времени и обратный отсчет для задач с ограниченным временем.

Ключевые особенности:

- **Двухрежимный интерфейс**: Отдельные вкладки для функций секундомера и таймера в одном окне
- **Режим секундомера**: Измерение прошедшего времени с миллисекундной точностью, отображение промежуточных результатов и поддержка записи отрезков времени
- **Обратный отсчет**: Настраиваемый таймер с шагом в секунды (+10с, +60с, +5мин, +20мин, +1ч, +6ч), визуальное отображение обратного отсчета и звуковой сигнал
- **Звуковое сопровождение**: Воспроизведение звукового файла WAV (clock_alarm.wav) при достижении нуля с помощью SoundPlayer
- **Поддержка локализации**: Динамическое переключение языков через LocalizationHelper с использованием словарей ресурсов
- **Интеграция с системой**: Сворачивание окна в системный лоток с командами показать/скрыть

Приложение реализовано как модуль OutfitTool API1, где ModuleController управляет жизненным циклом и предоставляет две команды: ToggleStopwatchCommand и ToggleTimerCommand. Интерфейс построен на WPF и использует DispatcherTimer для обновления времени (интервал 10мс для секундомера, 100мс для таймера). Форматирование времени обрабатывает дни, часы, минуты, секунды и миллисекунды с соответствующим дополнением нулями. Класс Beep обеспечивает звуковое уведомление с использованием встроенных ресурсов и Application.GetResourceStream для безопасного доступа к ресурсам.