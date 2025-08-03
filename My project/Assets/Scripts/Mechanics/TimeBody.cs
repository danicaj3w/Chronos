using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    // A simple struct to hold the state of our object at a specific time.
    public struct PointInTime
    {
        public Vector3 position;
        public Vector2 velocity;
        public bool spriteIsFlipped;

        public PointInTime(Vector3 pos, Vector2 vel, bool flipped)
        {
            position = pos;
            velocity = vel;
            spriteIsFlipped = flipped;
        }
    }

    public class TimeBody : MonoBehaviour
    {
        public bool isRewinding = false;
        public float recordTime = 4f; // How many seconds to store

        private List<PointInTime> pointsInTime;
        private KinematicObject kinematicObject;
        private SpriteRenderer spriteRenderer;

        void Awake()
        {
            pointsInTime = new List<PointInTime>();
            kinematicObject = GetComponent<KinematicObject>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void FixedUpdate()
        {
            // We run this in FixedUpdate because it's tied to the physics simulation.
            if (isRewinding)
            {
                Rewind();
            }
            else
            {
                Record();
            }
        }

        void Record()
        {
            // If our list has too many entries, remove the oldest one.
            if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
            {
                pointsInTime.RemoveAt(0);
            }

            // Add the current state to the end of the list.
            pointsInTime.Add(new PointInTime(transform.position, kinematicObject.velocity, spriteRenderer.flipX));
        }

        void Rewind()
        {
            if (pointsInTime.Count > 0)
            {
                // Get the most recent state from the list.
                PointInTime lastState = pointsInTime[pointsInTime.Count - 1];

                // Restore the state.
                transform.position = lastState.position;
                kinematicObject.velocity = lastState.velocity;
                spriteRenderer.flipX = lastState.spriteIsFlipped;

                // Remove the state we just used.
                pointsInTime.RemoveAt(pointsInTime.Count - 1);
            }
            else
            {
                // If we've run out of points, stop rewinding.
                isRewinding = false;
            }
        }

        public void StartRewind()
        {
            isRewinding = true;
        }

        public void StopRewind()
        {
            isRewinding = false;
            
        }
    }
}