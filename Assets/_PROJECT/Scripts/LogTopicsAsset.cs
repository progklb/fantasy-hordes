using UnityEngine;

using Utilities.Audio;

namespace FantasyHordes
{
	[CreateAssetMenu(fileName = "LogTopics", menuName = "Fantasy Hordes/Log Topics")]
	public class LogTopicsAsset : Utilities.LogTopics
	{
		public bool Audio;
		public bool Input;
		public bool Player;

		private void OnValidate()
		{
			// Here we reach out to external systems to set up logging based on our flags.

			AudioController.enableLogging = Audio;
		}
	}
}
