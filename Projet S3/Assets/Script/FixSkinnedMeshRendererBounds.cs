using UnityEngine;

namespace _Framework.Scripts.Util
{
    public class FixSkinnedMeshRendererBounds : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

        private void OnRenderObject()
        {
            skinnedMeshRenderer.localBounds = skinnedMeshRenderer.sharedMesh.bounds;
        }
    }
}
