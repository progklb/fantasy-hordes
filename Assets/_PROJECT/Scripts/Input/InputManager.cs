using UnityEngine;

using Utilities;

using UInput = UnityEngine.Input;

namespace FantasyHordes.Input
{
	/// <summary>
	/// Handles all inputs for the game.
	/// </summary>
	public class InputManager : MonoBehaviour
	{
		#region EVENTS
		public delegate void OnClick(Layers layer, Vector3 position, Vector3 normal);

		/// <summary>
		/// Is raised with a click occurs within the game world.
		/// </summary>
		public static OnClick onClick;
		#endregion


		#region VARIABLES
		[SerializeField]
		private Camera m_Camera;

		[Space]
		[SerializeField]
		private GameObject m_HitIndicatorPrefab;
		[SerializeField]
		private float m_HitIndicatorTimeout;
		#endregion


		#region UNITY EVENTS
		void Awake()
		{
			Log.Assert(LogTopics.Input, m_Camera != null, "Camera must be assigned.");
		}

		void Update()
		{
			// TODO Replace with new input system.
			if (UInput.GetMouseButtonDown(0))
			{
				ProcessMouseClick();
			}
		}
		#endregion


		#region HELPER FUNCTIONS
		// TODO Consider using OnMouseDown
		void ProcessMouseClick()
		{
			var ray = m_Camera.ScreenPointToRay(UInput.mousePosition);

			if (Physics.Raycast(ray, out RaycastHit hit, 100, 1 << (int)Layers.Ground))
			{
				Debug.DrawLine(m_Camera.transform.position, hit.point, Color.green, 2f);
				onClick(Layers.Ground, hit.point, hit.normal);

				if (m_HitIndicatorPrefab != null)
				{
					SpawnIndicator(new Pose(hit.point, Quaternion.Euler(hit.normal)), m_HitIndicatorTimeout);
				}
			}
			else
			{
				Debug.DrawRay(m_Camera.transform.position, hit.point, Color.red, 2f);
			}
		}

		void SpawnIndicator(Pose pose, float timeout = 1f)
		{
			var indicator = Instantiate(m_HitIndicatorPrefab, pose.position, pose.rotation, transform);
			Destroy(indicator, timeout);
		}
		#endregion
	}
}
