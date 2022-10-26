using UnityEngine;

namespace FantasyHordes.Helpers
{
    /// <summary>
    /// Defines an interface for any objects or agents that can move.
    /// </summary>
    public interface IMovable
    {
        /// <summary>
        /// Instructs the object or agent to move to the specific position.
        /// </summary>
        /// <param name="position">The position to move to.</param>
        /// <returns>Whether the move command was successfully issued.</returns>
        public bool MoveTo(Vector3 position);
    }
}