using UnityEngine;

namespace NeonTrainer.Mods
{
    public class State
    {
        public Vector3 Position { get; }
        public (float, float) Rotation { get; }
        public List<(string, int)> Cards = new();

        public State(Vector3 position, (float, float) rotation, List<(string, int)> cards)
        {
            Position = position;
            Rotation = rotation;
            Cards = cards;
        }
    }
}
