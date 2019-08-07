using UnityEngine;
using UnityEngine.SceneManagement;
using MiCoRo.Game.Human;

namespace MiCoRo.Game.Level
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class CapsuleColliderBeam : MonoBehaviour
    {

        public Vector3 beamTo;
        public string sceneName;


        /// <summary>
        /// Raises the trigger enter event. Only valid for Gameobjects with Rigidbody and that include a trigger type.
        /// </summary>
        /// <param name="other">Other.</param>
        void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody.gameObject.tag == "Player")
            {
                BeamPlayerToScene();
            }
        }

        private void BeamPlayerToScene()
        {
            PlayerStats.BeamPosition = beamTo;
            SceneManager.LoadScene(sceneName);
        }
    }
}
