using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class FirstPersonLook : MonoBehaviour
    {
        public MouseLook mouseLook = new MouseLook();

        private float m_YRotation;
		public Camera cam;



		private void Start()
        {
            mouseLook.Init (transform, cam.transform);
        }


        private void Update()
        {
            RotateView();
        }

		

        private Vector2 GetInput()
        {
            Vector2 input = new Vector2
                {
                    x = CrossPlatformInputManager.GetAxis("Horizontal"),
                    y = CrossPlatformInputManager.GetAxis("Vertical")
                };
            return input;
        }


        private void RotateView()
        {
			//avoids the mouse looking if the game is effectively paused
			if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

			// get the rotation before it's changed

			mouseLook.LookRotation(transform, cam.transform);
		}
    }
}
