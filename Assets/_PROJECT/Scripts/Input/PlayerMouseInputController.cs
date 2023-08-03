using FantasyHordes.Characters;

using UnityEngine;

using Utilities;

using UInput = UnityEngine.Input;

namespace FantasyHordes.Input
{
	[RequireComponent(typeof(PlayerCharacter))]
	public class PlayerMouseInputController : InputController
	{
		#region VARIABLES
		[SerializeField]
		private PlayerCharacter m_Player;
		#endregion


		#region INHERITED FUNCTIONS
		public override void ProcessInput()
		{
			if (UInput.GetMouseButton(0))
			{
				ProcessMouseClick();
			}
		}
		#endregion


		#region HELPER FUNCTIONS
		void ProcessMouseClick()
		{
			var ray = Camera.ScreenPointToRay(UInput.mousePosition);

			if (Physics.Raycast(ray, out RaycastHit hit, 100, 1 << (int)Layers.Ground))
			{
				Debug.DrawLine(Camera.transform.position, hit.point, Color.white, 2f);
				Log.Info(LogTopics.Input, $"Click at position {hit.point}");

				m_Player.agent.isStopped = false;
				m_Player.ignoreMoveIfBelowThreshold = true;
				m_Player.MoveTo(hit.point);

				InputManager.instance.ShowIndicator(new Pose(hit.point, Quaternion.Euler(hit.normal)));
			}
			else
			{
				Debug.DrawRay(Camera.transform.position, hit.point, Color.red, 2f);
			}
		}
		#endregion
	}
}
