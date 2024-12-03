using Code.Core;
using UnityEngine;

namespace Code.Render
{
    public class CameraCenteringManager : MonoBehaviour
    {
        private void Awake()
        {
            CoreEventsProvider.LevelStarted += Centralize;
        }

        private void Centralize()
        {
            float x = CoreManager.Instance.Map.Width / 2f;
            x -= 0.5f;
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
    }
}