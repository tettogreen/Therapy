using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	[RequireComponent(typeof(Rigidbody2D))]
    public class AutoMoveAndRotate : MonoBehaviour
    {
        public Vector2andSpace moveForce;
        public Vector2andSpace rotateDegreesPerSecond;
//        public bool ignoreTimescale;
//        private float m_LastRealTime;
		public float directionChangeTime;

		private float timeOfLastDirectionChange;
		private Rigidbody2D rigidb;


        private void Start()
        {
//            m_LastRealTime = Time.realtimeSinceStartup;
			timeOfLastDirectionChange = Time.time;
			rigidb = GetComponent<Rigidbody2D>();
        }


        // Update is called once per frame
        private void FixedUpdate()
        {
//            if (ignoreTimescale)
//            {
//                deltaTime = (Time.realtimeSinceStartup - m_LastRealTime);
//                m_LastRealTime = Time.realtimeSinceStartup;
//            }
			ChangeMovementDirection();
			Move();
        }

        void Move () {
			float deltaTime = Time.deltaTime;
//			transform.Translate(moveUnitsPerSecond.value*deltaTime, moveUnitsPerSecond.space);
			rigidb.AddForce (moveForce.value);
			transform.Rotate(rotateDegreesPerSecond.value*deltaTime, moveForce.space);
        }

        void ChangeMovementDirection ()
		{
			if (directionChangeTime != 0 && Time.time > timeOfLastDirectionChange + directionChangeTime) {
				moveForce.value *= -1;
				timeOfLastDirectionChange = Time.time;
			}
        }

        [Serializable]
        public class Vector2andSpace
        {
            public Vector2 value;
            public Space space = Space.Self;
        }

    }
}