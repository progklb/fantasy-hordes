using FantasyHordes.Characters;

using UnityEngine;
using UnityEngine.AI;

using Utilities.StateMachine.Conditions;

namespace FantasyHordes.States.Conditions
{
	/// <summary>
	/// Condition that governs checks whether the target character is running.
	/// </summary>
	[AddComponentMenu("Fantasy Hordes/State Machine/Conditions/Is Character Running Condition")]
	public class IsCharacterRunningCondition : ConditionBehaviour
	{
		#region CONSTANTS
		private float RUN_SPEED_THRESH = 0.2f;
		private float RUN_DIST_THRESH = 0.2f;
		#endregion


		#region TYPES
		private enum Mode
		{
			Running,
			Idle
		}
		#endregion


		#region PROPERTIES
		private NavMeshAgent agent { get => m_Character.agent; }
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
			// If we are checking to go to idle...
			if (TransitionWhen == Mode.Idle &&
				// and our agent is set to stopped, or is stationary.
				(agent.isStopped || agent.remainingDistance < RUN_DIST_THRESH || agent.velocity.magnitude < RUN_SPEED_THRESH))
			{
				isSatisfied = true;
			}

			// If we are checking to go to running state and our agent is not set to stopped...
			if (TransitionWhen == Mode.Running && !agent.isStopped &&
				// and we are not stationary and not at the destination.
				(agent.remainingDistance >= RUN_DIST_THRESH || agent.velocity.magnitude > RUN_SPEED_THRESH))
			{
				isSatisfied = true;
			}
		}
		#endregion
	}
}
