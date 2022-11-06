using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

using Utilities;
using Utilities.Animations;
using Utilities.Audio;
using Utilities.Extensions;

using FantasyHordes.Input;
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
        public AnimationEventNotifier animationEventNotifier { get; private set; }
		#endregion


		#region VARIABLES
		[SerializeField, Tooltip("The transform that will hold the player character model.")]
        private Transform m_ModelParent;
		[SerializeField, Tooltip("The rigged/animated character prefab to be used by this instance.")]
		private GameObject m_PlayerPrefab;

		[SerializeField, Tooltip("The audio controller used by this character.")]
		private AudioController m_AudioController;
		#endregion


        #region UNITY EVENTS
		protected override void Start()
        {
            base.Start();

            // Listen for control input.
            InputManager.onClick += OnClick;

            m_ModelParent.DestroyChildren();

            // Crate player model and get refs to components.
            var model = Instantiate(m_PlayerPrefab, m_ModelParent);
			animator = model.GetComponent<Animator>();
            animationEventNotifier = model.GetComponent<AnimationEventNotifier>();
            animationEventNotifier.onStringEventReceived += OnAnimationEventReceived;
		}

        protected virtual void Update()
		{
            // TODO Refine this.
            // Use an enum to represent states, and
			// migrate this logic into a proper state machine.
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
			animationEventNotifier.onStringEventReceived -= OnAnimationEventReceived;
		}
		#endregion


		#region EVENT HANDLERS
		void OnAnimationEventReceived(string eventName)
		{
            switch (eventName)
            {
                case "Step":
                    // Note that the audio controller has the correct audio
					// bank set based on the terrain the user is running on.
					// All we do here is tell it to play the current bank.
                    m_AudioController.Play();
                    break;

                default:
                    Log.Error(this, "Unsupported animation event received.");
                    break;
            }
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