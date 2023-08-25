using UnityEditor;

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

		[MenuItem("ApricotJams/Log Topics")]
		public static void SelectAsset()
		{
			// This is hardcodes, so don't move this asset! Also we should only ever have one.
			Selection.activeObject = AssetDatabase.LoadMainAssetAtPath("Assets/_PROJECT/Resources/Log/LogTopics.asset");
		}
	}
}
