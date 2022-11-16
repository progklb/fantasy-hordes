using UnityEngine;
using UnityEngine.AI;

using Utilities.StateMachine.Conditions;

using FantasyHordes.Characters;

namespace FantasyHordes.States.Conditions
{
	/// <summary>
	/// Determines whether the target character is running.
	/// </summary>
	[AddComponentMenu("Fantasy Hordes/State Machine/Conditions/Is Character Running Condition")]
	public class IsCharacterRunningCondition : ConditionBehaviour
	{
		#region TYPES
		private enum Mode
		{
			Running,
			Idle
		}
		#endregion


		#region VARIABLES
		[SerializeField]
		private NavMeshAgent m_NavMeshAgent;
		[SerializeField]
		private MovableCharacter m_Character;

		[SerializeField]
		private Mode TransitionWhen;
		#endregion


		#region PUBLIC API
		public override void OnUpdate()
		{
			if (TransitionWhen == Mode.Idle && m_Character.agent.remainingDistance < 0.2f)
			{
				isSatisfied = true;
			}

			if (TransitionWhen == Mode.Running && m_Character.agent.remainingDistance >= 0.2f)
			{
				isSatisfied = true;
			}
		}
		#endregion
	}
}