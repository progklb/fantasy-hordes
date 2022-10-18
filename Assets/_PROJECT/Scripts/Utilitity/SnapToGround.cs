// Credit: https://unity3d.college/2017/09/26/using-unity-editor-extensions-to-snap-to-ground-when-placing-gameobjects/

using UnityEditor;
using UnityEngine;

namespace Utilities
{
    public class SnapToGround : MonoBehaviour
    {
        [MenuItem("Custom/Snap To Ground %g")]
        public static void Ground()
        {
            foreach (var transform in Selection.transforms)
            {
                var hits = Physics.RaycastAll(transform.position + Vector3.up, Vector3.down, 10f);
                foreach (var hit in hits)
                {
                    if (hit.collider.gameObject == transform.gameObject)
                        continue;

                    transform.position = hit.point;
                    break;
                }
            }
        }
    }
}