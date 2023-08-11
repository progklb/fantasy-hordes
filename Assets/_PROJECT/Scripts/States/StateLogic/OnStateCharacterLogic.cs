using FantasyHordes.Characters;

using UnityEngine;

using Utilities.StateMachine;
using Utilities.StateMachine.StateEvents;
using Utilities.StateMachine.StateLogic;

namespace FantasyHordes.States.StateLogic
{
	/// <summary>
	/// Base class for defining character state logic.
	/// </summary>
	public abstract class OnStateCharacterLogic : MonoBehaviour, IStateEvent, IStateLogic
	{
		#region PROPERTIES
		protected bool isInitialised { get; set; }
		#endregion


		#region VARIABLES
		[SerializeField]
		protected PlayerCharacter m_Player;
		#endregion


		#region PUBLIC API
		public virtual void Initialise()
		{
			isInitialised = true;
		}

		public virtual void OnBegin(IState previousState, IState currentState)
		{
		}

		public virtual void OnEnd(IState currentState, IState nextState)
		{
		}

		public virtual void OnUpdate(IState currentState)
		{
		}

		public virtual void OnReset()
		{
		}
		#endregion
	}
}
