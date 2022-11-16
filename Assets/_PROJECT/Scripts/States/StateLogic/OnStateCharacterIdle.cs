using UnityEngine;

using Utilities;
using Utilities.StateMachine;
using Utilities.Timers;

namespace FantasyHordes.States.StateLogic
{
	public class OnStateCharacterIdle : OnStateCharacterLogic
	{
		#region PROPERTIES
		private CallbackTimer idleTimer { get; set; }
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
			idleTimer.goalTime = Random.Range(15f, 35f);
		}

		void PlayIdleAnimation()
		{
			Log.Info(this, $"Character idle for {string.Format("{0:F2}", idleTimer.accumulatedTime)}s. Playing random idle animation.");
			m_Player.animator.SetTrigger(m_Player.animationKeys.idles.GetRandomIdle());
		}
		#endregion
	}
}