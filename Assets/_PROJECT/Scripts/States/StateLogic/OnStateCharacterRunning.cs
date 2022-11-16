using UnityEngine;

using Utilities.StateMachine;

using FantasyHordes.Characters;

namespace FantasyHordes.States.StateLogic
{
	public class OnStateCharacterRunning : OnStateCharacterLogic
	{
		#region PUBLIC API
		public override void OnBegin(IState previousState, IState currentState)
		{
			m_Player.animator.SetBool(m_Player.animationKeys.running, true);
		}

		public override void OnEnd(IState currentState, IState nextState)
		{
			m_Player.animator.SetBool(m_Player.animationKeys.running, false);
		}
		#endregion
	}
}