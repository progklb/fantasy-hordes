using UnityEngine;

using Utilities;
using Utilities.StateMachine;
using Utilities.Timers;

namespace FantasyHordes.States.StateLogic
{
	/// <summary>
	/// A character's idle state.
	/// Triggers a random idle animation at random time frames.
	/// </summary>
	public class OnStateCharacterIdle : OnStateCharacterLogic
	{
		#region PROPERTIES
		private CallbackTimer idleTimer { get; set; }
		#endregion


		#region VARIABLES
		[SerializeField]
		private float m_PlayIdleMinWaitTime = 15f;
		[SerializeField]
		private float m_PlayIdleMaxWaitTime = 25f;
		#endregion


		#region UNITY CALLLBACKS
		private void OnValidate()
		{
			Debug.Assert(m_PlayIdleMinWaitTime < m_PlayIdleMaxWaitTime, "Minimum wait time cannot be great than max wait time!");
		}
		#endregion


		#region PUBLIC API
		public override void Initialise()
		{
			if (!isInitialised)
			{
				idleTimer = new CallbackTimer(99f);
				idleTimer.AddCallback(PlayIdleAnimation);

				base.Initialise();
			}
		}

		public override void OnBegin(IState previousState, IState currentState)
		{
			ResetIdleTimer();
		}

		public override void OnUpdate(IState currentState)
		{
			idleTimer.Update();

			if (idleTimer.GetProgress() > 1f)
			{
				ResetIdleTimer();
			}
		}
		#endregion


		#region HELPER FUNCTIONS
		void ResetIdleTimer()
		{
			idleTimer.Reset();
			idleTimer.goalTime = Random.Range(m_PlayIdleMinWaitTime, m_PlayIdleMaxWaitTime);
		}

		void PlayIdleAnimation()
		{
			Log.Info(LogTopics.Player, $"Character idle for {string.Format("{0:F2}", idleTimer.accumulatedTime)}s. Playing random idle animation.");
			m_Player.animator.SetTrigger(m_Player.animationKeys.idles.GetRandomIdle());
		}
		#endregion
	}
}
