using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

namespace FantasyHordes.Input
{
	/// <summary>
	/// Governs the set of custom input cursors.
	/// </summary>
	public class InputCursor : MonoBehaviour
	{
		#region VARIABLES
		[SerializeField]
		private Texture2D m_Pointer;
		#endregion


		#region UNITY EVENTS
		void Start()
		{
			Set(m_Pointer);
		}
		#endregion


		#region PUBLIC API
		public void Set(Texture2D texture)
		{
			Cursor.SetCursor(texture, new Vector2(), CursorMode.Auto);
		}

		public void Unset()
		{
			Cursor.SetCursor(null, new Vector2(), CursorMode.Auto);
		}
		#endregion
	}
}