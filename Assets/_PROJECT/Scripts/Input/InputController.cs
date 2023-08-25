using System.Collections;

using UnityEngine;

namespace FantasyHordes.Input
{
	/// <summary>
	/// A base class for defining controllers that handle input.
	/// These are governed by a <see cref="InputManager"/>.
	/// </summary>
	public abstract class InputController : MonoBehaviour
	{
		#region PROPERTIES
		protected Camera Camera { get => InputManager.instance.Camera; }
		#endregion


		#region UNITY EVENTS
		IEnumerator Start()
		{
			yield return new WaitUntil(() => InputManager.instance != null);
			InputManager.instance.RegisterController(this);
		}

		void OnDestroy()
		{
			InputManager.instance?.DeregisterController(this);
		}
		#endregion


		#region HELPER FUNCTIONS
		/// <summary>
		/// Must be override to implement our input handling.
		/// </summary>
		public abstract void ProcessInput();

		protected Vector3 ParallelToCameraView(float z)
		{
			return (transform.position - InputManager.instance.Camera.transform.position).normalized * z;
		}

		protected Vector3 NormalToCameraView(float x)
		{
			return Vector3.Cross((transform.position - InputManager.instance.Camera.transform.position).normalized, Vector3.down) * x;
		}
		public override string ToString()
		{
			return $"{base.ToString()} : {name}";
		}
		#endregion
	}
}
