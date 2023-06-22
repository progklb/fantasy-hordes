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
		#endregion


		#region UNITY EVENTS
		protected override void Start()
		{
			base.Start();

			agent = GetComponent<NavMeshAgent>();
		}
		#endregion


		#region PUBLIC API
		public bool MoveTo(Vector3 position)
		{
			agent.destination = position;

			Debug.DrawLine(transform.position, agent.destination, Color.yellow, 2f);

			return agent.pathStatus != NavMeshPathStatus.PathInvalid;
		}
		#endregion
	}
}