using UnityEngine;

namespace FantasyHordes
{
    /// <summary>
    /// Handles all inputs for the game.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        #region EVENTS
        public delegate void OnClick(Layers layer, Vector3 position, Vector3 normal);

        /// <summary>
        /// Is raised with a click occurs within the game world.
        /// </summary>
        public static OnClick onClick;
        #endregion


        #region VARIABLES
        [SerializeField]
        private Camera m_Camera;
        #endregion


        #region UNITY EVENTS
        void Awake()
        {
            // TODO Replace with log with TAG.
            Debug.Assert(m_Camera != null, "Camera must be assigned.");
        }

        void Update()
        {
            // TODO Replace with new input system.
            if (Input.GetMouseButtonDown(0))
            {
                ProcessMouseClick();
            }
        }
        #endregion


        #region HELPER FUNCTIONS
        // TODO Consider using OnMouseDown
        void ProcessMouseClick()
        {
            var ray = m_Camera.ScreenPointToRay(Input.mousePosition);

            

            if (Physics.Raycast(ray, out RaycastHit hit, 100, 1 << (int)Layers.Ground))
            {
                Debug.Log("Hit!");
                Debug.DrawLine(m_Camera.transform.position, hit.point, Color.green, 2f);
                onClick(Layers.Ground, hit.point, hit.normal);
            }
            else
            {
                Debug.DrawRay(m_Camera.transform.position, hit.point, Color.red, 2f);
            }
        }
        #endregion
    }
}