using System.Collections;

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

		[SerializeField]
		private GameObject m_HitIndicatorPrefab;
		[SerializeField]
		private float m_HitIndicatorTimeout;

		private GameObject m_HitIndicatorInstance;
		private Coroutine m_HitIndicatorRoutine;
		#endregion


		#region UNITY EVENTS
		void Awake()
		{
			Log.Assert(LogTopics.Input, m_Camera != null, "Camera must be assigned.");

			if (m_HitIndicatorPrefab != null)
			{
				m_HitIndicatorInstance = Instantiate(m_HitIndicatorPrefab, Vector3.zero, Quaternion.identity, transform);
				m_HitIndicatorInstance.SetActive(false);
			}
		}

		void Update()
		{
			// TODO Replace with new input system.
			if (UInput.GetMouseButton(0))
			{
				ProcessMouseClick();
			}
		}
		#endregion


		#region HELPER FUNCTIONS
		void ProcessMouseClick()
		{
			var ray = m_Camera.ScreenPointToRay(UInput.mousePosition);

			if (Physics.Raycast(ray, out RaycastHit hit, 100, 1 << (int)Layers.Ground))
			{
				Debug.DrawLine(m_Camera.transform.position, hit.point, Color.white, 2f);
				Log.Info(LogTopics.Input, $"Click at position {hit.point}");

				onClick(Layers.Ground, hit.point, hit.normal);

				ShowIndicator(new Pose(hit.point, Quaternion.Euler(hit.normal)), m_HitIndicatorTimeout);
			}
			else
			{
				Debug.DrawRay(m_Camera.transform.position, hit.point, Color.red, 2f);
			}
		}

		void ShowIndicator(Pose pose, float timeout = 1f)
		{
			if (m_HitIndicatorInstance == null)
			{
				return;
			}

			if (m_HitIndicatorRoutine != null)
			{
				StopCoroutine(m_HitIndicatorRoutine);
			}

			m_HitIndicatorRoutine = StartCoroutine(ShowIndicatorRoutine(pose, timeout));
		}

		IEnumerator ShowIndicatorRoutine(Pose pose, float timeout = 1f)
		{
			m_HitIndicatorInstance.transform.position = pose.position;
			m_HitIndicatorInstance.SetActive(true);
			yield return new WaitForSeconds(timeout);
			m_HitIndicatorInstance.SetActive(false);
			m_HitIndicatorRoutine = null;
		}
		#endregion
	}
}
