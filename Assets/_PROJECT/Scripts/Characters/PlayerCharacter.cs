using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

using FantasyHordes.Helpers;

namespace FantasyHordes.Characters
{
    /// <summary>
    /// The player-controlled <see cref="Character"/>.
    /// </summary>
    public class PlayerCharacter : MovableCharacter
    {
        #region UNITY EVENTS
        protected override void Start()
        {
            base.Start();

            InputManager.onClick += OnClick;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            InputManager.onClick -= OnClick;
        }
        #endregion


        #region HELPER FUNCTIONS
        void OnClick(Layers layer, Vector3 pos, Vector3 norm)
        {
            if (layer == Layers.Ground)
            {
                MoveTo(pos);
            }
        }
        #endregion
    }
}