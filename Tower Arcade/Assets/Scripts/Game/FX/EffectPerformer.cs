using System.Collections;
using UnityEngine;

namespace Game
{
    public class EffectPerformer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _upgradeTowerParticle;
        [SerializeField] private ParticleSystem _delateTowerParticle;
        [SerializeField] private ParticleSystem _installTowerParticle;

        public void PlayUpgradeTowerEffect(Vector3 position) => 
            Play(_upgradeTowerParticle, position);

        public void PlayDelateTowerEffect(Vector3 position) => 
            Play(_delateTowerParticle, position);

        public void PlayTowerInstalledEffect(Vector3 position) => 
            Play(_installTowerParticle, position);

        public void PlayEffect(ParticleSystem particleSystem, Vector3 position) => 
            Play(particleSystem, position);

        public void Play(ParticleSystem particleSystem, Vector3 position)
        {
            var particle = Instantiate(particleSystem, transform);

            particle.transform.position = position;

            StartCoroutine(DestroyParticle(particle));
        }

        private IEnumerator DestroyParticle(ParticleSystem particle)
        {
            float waitTime = particle.main.duration + particle.main.startLifetime.constantMax;

            yield return new WaitForSeconds(waitTime);

            particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            Destroy(particle);
        }
    }
}
