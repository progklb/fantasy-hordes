using FantasyHordes.Characters;

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerCharacter))]
public class PlayerInput : MonoBehaviour
{
	[SerializeField]
	private NavMeshAgent m_Agent;
	[SerializeField]
	private PlayerCharacter m_Player;
	[SerializeField]
	private float m_LookAheadDistance = 0.5f;

	void Update()
	{
		//https://medium.com/geekculture/navmeshagent-movement-via-keyboard-8ea09fcb4e17

		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		}
	}

	void Move(float x, float z)
	{
		// Project a target position near the player based on input.
		m_Player.ignoreMoveIfBelowThreshold = false;
		m_Agent.destination = transform.position + (ParallelToCameraView(z) + NormalToCameraView(x)) * (m_Agent.speed + m_LookAheadDistance);
	}

	Vector3 ParallelToCameraView(float z)
	{
		return (transform.position - Camera.main.transform.position).normalized * z;
	}

	Vector3 NormalToCameraView(float x)
	{
		return Vector3.Cross((transform.position - Camera.main.transform.position).normalized, Vector3.down) * x;
	}
}
