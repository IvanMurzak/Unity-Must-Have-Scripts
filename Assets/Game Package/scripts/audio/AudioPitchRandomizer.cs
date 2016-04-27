using UnityEngine;
using System.Collections;

namespace Game.Package
{
    public class AudioPitchRandomizer : MonoBehaviour
    {
        private AudioSource[] audios;
        public float randomPitchOffset = 0.5f;

        private float[] originalPitch;

        void OnDrawGizmos()
        {
            randomPitchOffset = Mathf.Max(0f, Mathf.Min(1f, randomPitchOffset));
        }

        protected virtual void Start()
        {
            audios = GetComponents<AudioSource>();
            originalPitch = new float[audios.Length];
            for (int index = 0; index < audios.Length; index++)
            {
                originalPitch[index] = audios[index].pitch;
                audios[index].pitch = CalcRandomPitch(index);
            }
        }

        public void Play()
        {
            int index = Random.Range(0, audios.Length);
            audios[index].pitch = CalcRandomPitch(index);
            if (audios[index].enabled) audios[index].Play();
        }

        float CalcRandomPitch(int index)
        {
            return originalPitch[index] + originalPitch[index] * Random.Range(randomPitchOffset / -2f, randomPitchOffset / 2f);
        }

        public int Count()
        {
            return audios.Length;
        }
    }
}
