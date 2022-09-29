using NeonTrainer.Mods;

namespace NeonTrainer
{
    internal class PersistentDataStore
    {
        public static Dictionary<int, State> States { get; set; }
    }
}
