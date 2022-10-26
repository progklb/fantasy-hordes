using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

using Utilities.Extensions;

using FantasyHordes.Helpers;

namespace FantasyHordes.Characters
{
    /// <summary>
    /// The player-controlled <see cref="Character"/>.
    /// </summary>
    public class PlayerCharacter : MovableCharacter
    {
        #region PROPERTIES
        public Animator animator { get; private set; }
        #endregion

        #region VARIABLES
        [SerializeField, Tooltip("The transform that will hold the player character model.")]
        private Transform m_ModelParent;
		[SerializeField, Tooltip("The rigged/animated character prefab to be used by this instance.")]
		private GameObject m_PlayerPrefab;
        #endregion


        #region UNITY EVENTS
        protected override void Start()
        {
            base.Start();

            InputManager.onClick += OnClick;
            m_ModelParent.DestroyChildren();

			animator = Instantiate(m_PlayerPrefab, m_ModelParent).GetComponent<Animator>();
        }

        protected virtual void Update()
		{
            // TODO Refine this.
            // Use an enum to represent states, and migrate this logic
            // into a proper state machine.
			if (agent.remainingDistance < 0.2f)
			{
				animator.SetBool("Running", false);
			}
            else
			{
				animator.SetBool("Running", true);
			}
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