using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Utilities;
using Utilities.Frameworks;

namespace FantasyHordes.Input
{
	/// <summary>
	/// Handles all inputs for the game.
	/// </summary>
	public class InputManager : Singleton<InputManager>
	{
		#region PROPERTIES
		/// Whether the system should be listening for input.
		public bool ListenForInput { get; set; } = true;
		/// The primary camera we are using for relative input.
		public Camera Camera { get => m_Camera; }

		/// The list currently active (registered) input controllers.
		public List<InputController> ActiveControllers { get; set; } = new List<InputController>();
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
		protected override void Awake()
		{
			base.Awake();

			if (m_Camera == null)
			{
				Log.Warning(LogTopics.Input, "Camera not assigned. Falling back to Camera.main");
				m_Camera = Camera.main;
			}

			if (m_HitIndicatorPrefab != null)
			{
				m_HitIndicatorInstance = Instantiate(InputManager.instance.m_HitIndicatorPrefab, Vector3.zero, Quaternion.identity, transform);
				m_HitIndicatorInstance.SetActive(false);
			}
		}

		private void Update()
		{
			if (ListenForInput)
			{
				ActiveControllers.ForEach(x => x.ProcessInput());
			}
		}
		#endregion


		#region CONTROLLERS
		public void RegisterController(InputController controller)
		{
			ActiveControllers.Add(controller);
			Log.Info(LogTopics.Input, $"Registered input controller: {controller}");
		}

		public void DeregisterController(InputController controller)
		{
			if (!ActiveControllers.Remove(controller))
			{
				Log.Error(LogTopics.Input, $"Tried to deregister an unregistered input controller: {controller}");
			}
		}
		#endregion


		#region INDICATORS
		public void ShowIndicator(Pose pose, float timeout = 1f)
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
