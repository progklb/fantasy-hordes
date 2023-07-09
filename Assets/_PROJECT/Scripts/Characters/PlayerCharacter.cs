using FantasyHordes.Input;

using UnityEngine;

using Utilities;
using Utilities.Animations;
using Utilities.Audio;
using Utilities.Extensions;

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

		public CharacterAnimationKeys animationKeys { get; private set; } = new CharacterAnimationKeys();
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

			// Create player model and get refs to components.
			var model = Instantiate(m_PlayerPrefab, m_ModelParent);
			animator = model.GetComponent<Animator>();
			animationEventNotifier = model.GetComponent<AnimationEventNotifier>();
			animationEventNotifier.onStringEventReceived += OnAnimationEventReceived;
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
					Log.Error(LogTopics.Player, "Unsupported animation event received.");
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
