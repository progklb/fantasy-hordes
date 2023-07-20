using FantasyHordes.Helpers;

using UnityEngine;
using UnityEngine.AI;

namespace FantasyHordes.Characters
{
	/// <summary>
	/// A <see cref="Character"/> that supports movement.
	/// </summary>
	[RequireComponent(typeof(NavMeshAgent))]
	public class MovableCharacter : Character, IMovable
	{
		#region PROPERTIES
		public NavMeshAgent agent { get; set; }

		/// Whether the <see cref="ignoreMoveDistanceThreshold"/> distance is respected.
		public bool ignoreMoveIfBelowThreshold { get; set; } = true;
		/// The minimum distance from the character that a move command must be issued, else it is ignored.
		/// Only respected if <see cref="ignoreMoveIfBelowThreshold"/> is true.
		public float ignoreMoveDistanceThreshold { get; set; } = 1.1f;
		#endregion


		#region UNITY EVENTS
		protected override void Start()
		{
			base.Start();

			agent = GetComponent<NavMeshAgent>();
		}

		private void OnDrawGizmos()
		{
			// Draw the path to the target.
			if (Application.isPlaying && agent.remainingDistance > 0.1f && agent.path.corners.Length > 1)
			{
				for (int i = 0; i < agent.path.corners.Length - 1; i++)
				{
					Debug.DrawLine(agent.path.corners[i], agent.path.corners[i + 1], Color.green);
				}
			}
		}
		#endregion


		#region PUBLIC API
		public bool MoveTo(Vector3 position)
		{
			if (Vector3.Distance(agent.nextPosition, position) > (ignoreMoveIfBelowThreshold ? ignoreMoveDistanceThreshold : 0f))
			{
				agent.destination = position;
			}

			return agent.pathStatus != NavMeshPathStatus.PathInvalid;
		}
		#endregion
	}
}
