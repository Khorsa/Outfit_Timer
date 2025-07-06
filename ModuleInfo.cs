using OutfitTool.Common;

namespace Timer
{
    internal class ModuleInfo : ModuleInfoInterface
    {
        public string Name => "timer";
        public string AssemblyName => "Timer";
        public string DisplayName => "Timer";
        public string Description => "Таймер";
        public ModuleVersion Version => new ModuleVersion(0, 1, "pre-alpha");
        public ModuleVersion Require => new ModuleVersion(3, 0);
        public string Changes => "Тестовая версия";
        public string Author => "Stolyarov Roman";
        public string AuthorContacts => "rshome@mail.ru";
    }
}
