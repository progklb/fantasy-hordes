using UnityEngine;

namespace FantasyHordes.Characters
{
    /// <summary>
    /// Represents a generic character in the game world.
    /// </summary>
	public class CharacterAnimationKeys
    {
		#region TYPES
		public class Idles
		{
			public string offensiveIdle { get => "Offensive Idle"; }
			public string stretchIdle { get => "Stretch Idle"; }
			public string fanningIdle { get => "Fanning Idle"; }

			public string[] allIdles { get; private set; }

			public Idles()
				=> allIdles = new string[] { offensiveIdle, stretchIdle, fanningIdle };

			public string GetRandomIdle()
				=> allIdles[Random.Range(0, allIdles.Length)];
		}
		#endregion


		#region PROPERTIES
		public string running { get => "Running"; }

        public Idles idles { get; private set; } = new Idles();
		#endregion
	}
}