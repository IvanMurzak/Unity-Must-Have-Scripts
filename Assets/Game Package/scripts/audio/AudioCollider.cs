using UnityEngine;
using System.Collections;

namespace Game.Package
{
    [RequireComponent(typeof(Collider2D))]
    public class AudioCollider : AudioPitchRandomizer
    {
        public float delay = 0.3f;
        public float maxRandomDelayForLoop = 1f;

        public LayerMask mask = -1;

        private float lastPlay = 0;

        public void Enter(GameObject other)
        {
            bool checkLayer = ((1 << other.gameObject.layer) & mask) != 0;
            if (Count() > 0 && checkLayer)
            {
                if (Time.time > lastPlay + delay)
                {
                    lastPlay = Time.time;
                    Play();
                }
            }
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            Enter(coll.gameObject);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Enter(other.gameObject);
        }
    }
}
