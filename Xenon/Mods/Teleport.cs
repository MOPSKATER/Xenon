﻿using KinematicCharacterController;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Xenon.Mods
{
    internal class Teleport : Mod
    {
        private readonly KinematicCharacterMotor _motor;
        private readonly Dictionary<int, State> _states = new();

        public Teleport()
        {
            _motor = RM.drifter.Motor;
            if (PersistentDataStore.States != null)
                _states = PersistentDataStore.States;
        }

        void Update()
        {
            if (Keyboard.current.numpad0Key.wasPressedThisFrame)
            {
                if (Keyboard.current.ctrlKey.isPressed)
                    SavePos(0);
                else
                {
                    if (Keyboard.current.shiftKey.isPressed)
                        LoadPos(0, false);
                    else
                        LoadPos(0, true);
                }
            }

            if (Keyboard.current.numpad1Key.wasPressedThisFrame)
            {
                if (Keyboard.current.ctrlKey.isPressed)
                    SavePos(1);
                else
                {
                    if (Keyboard.current.shiftKey.isPressed)
                        LoadPos(1, false);
                    else
                        LoadPos(1, true);
                }
            }

            if (Keyboard.current.numpad2Key.wasPressedThisFrame)
            {
                if (Keyboard.current.ctrlKey.isPressed)
                    SavePos(2);
                else
                {
                    if (Keyboard.current.shiftKey.isPressed)
                        LoadPos(2, false);
                    else
                        LoadPos(2, true);
                }
            }
        }

        private void SavePos(int slot)
        {
            MouseLook[] mouseLooks = FindObjectsOfType<MouseLook>();
            List<(string, int)> cards = new();

            foreach (var card in RM.mechController.GetCurrentHand())
                cards.Add((card.data.cardID, card.GetRank()));

            List<PlayerCard> deck = RM.mechController.GetCurrentDeck();
            foreach (PlayerCard card in deck)
                cards.Add((card.data.cardID, card.GetRank()));

            if (!_states.ContainsKey(slot))
                _states.Add(slot, new State(_motor.TransientPosition, (mouseLooks[0].RotationX, mouseLooks[1].RotationY), cards));
            else
                _states[slot] = new State(_motor.TransientPosition, (mouseLooks[0].RotationX, mouseLooks[1].RotationY), cards);

            PersistentDataStore.States = _states;
        }

        private void LoadPos(int slot, bool loadCards)
        {
            if (!_states.ContainsKey(slot))
                return;

            _states.TryGetValue(slot, out State state);
            MouseLook[] mouseLooks = FindObjectsOfType<MouseLook>();
            Type type = mouseLooks[0].GetType();
            FieldInfo Xinfo = type.GetField("rotationX", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo Yinfo = type.GetField("rotationY", BindingFlags.NonPublic | BindingFlags.Instance);
            Xinfo.SetValue(mouseLooks[0], state.Rotation.Item1);
            Yinfo.SetValue(mouseLooks[1], state.Rotation.Item2);

            _motor.SetTransientPosition(state.Position);

            Debug.Log("Load cards: " + loadCards);

            if (loadCards && state.Cards != null)
            {
                RM.mechController.GetPlayerCardDeck().DiscardAllCards(true);
                for (int i = state.Cards.Count - 1; i > -1; i--)
                {
                    (string, int) card = state.Cards[i];
                    for (int count = 0; count <= card.Item2; count++)
                        GS.AddCard(card.Item1);
                }
            }
        }
    }
}