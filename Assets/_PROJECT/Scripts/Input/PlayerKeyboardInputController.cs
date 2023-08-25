using FantasyHordes.Characters;

using UnityEngine;

using UInput = UnityEngine.Input;

namespace FantasyHordes.Input
{
	[RequireComponent(typeof(PlayerCharacter))]
	public class PlayerKeyboardInputController : InputController
	{
		#region PROPERTIES
		public bool DebugShowTargetPosition = true;
		#endregion


		#region VARIABLES
		[SerializeField]
		private PlayerCharacter m_Player;
		[SerializeField]
		protected float m_InputDeadzone;
		[SerializeField]
		private float m_LookAheadDistance = 0.5f;

		/// Used to track if we had input last frame, in order to tell the agent to stop moving if he has released input.
		private bool HadInputLastFrame;
		#endregion


		#region INHERITED FUNCTIONS
		public override void ProcessInput()
		{
			if (IsAboveThreshold("Horizontal", m_InputDeadzone) || IsAboveThreshold("Vertical", m_InputDeadzone))
			{
				m_Player.agent.isStopped = false;
				Move(UInput.GetAxis("Horizontal"), UInput.GetAxis("Vertical"));
				HadInputLastFrame = true;
			}
			else if (HadInputLastFrame)
			{
				m_Player.agent.isStopped = true;
				HadInputLastFrame = false;
			}
		}
		#endregion


		#region HELPER FUNCTIONS
		void Move(float x, float z)
		{
			// Note: If we want to drive the velocity manually we can do the following (rough).
			// This does not make use of the agent's system though will require more hands on control.
			// m_Player.agent.velocity = Vector3.Lerp(
			//	m_Player.agent.velocity,
			//	new Vector3(-x, 0f, -z).normalized * m_Player.agent.speed,
			//	m_Player.agent.acceleration * Time.deltaTime);

			// Project a target position near the player based on input.
			m_Player.ignoreMoveIfBelowThreshold = false;

			var targetPos = transform.position + (ParallelToCameraView(z) + NormalToCameraView(x)) * m_LookAheadDistance;
			m_Player.MoveTo(targetPos);

			if (DebugShowTargetPosition)
			{
				InputManager.instance.ShowIndicator(new Pose(targetPos, Quaternion.identity));
			}
		}

		/// <summary>
		/// Whether the specified analog axis currently has input above the specified threshold.
		/// </summary>
		/// <param name="inputAxis">Name of the axis.</param>
		/// <param name="threshold">Threshold value.</param>
		bool IsAboveThreshold(string inputAxis, float threshold)
		{
			return Mathf.Abs(UInput.GetAxis(inputAxis)) > threshold;
		}
		#endregion
	}
}
