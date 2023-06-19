using UnityEngine;

namespace FantasyHordes
{
	[CreateAssetMenu(fileName = "LogTopics", menuName = "Fantasy Hordes/Log Topics")]
	public class LogTopicsAsset : Utilities.LogTopics
	{
		public bool Input;
		public bool Player;
	}
}
