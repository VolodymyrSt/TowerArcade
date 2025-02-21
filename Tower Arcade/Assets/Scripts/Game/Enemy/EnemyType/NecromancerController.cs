using System.Collections;
using UnityEngine;

namespace Game
{
    public class NecromancerController : Enemy
    {
        private const string SPELL_CAST = "Spellcast";

        [SerializeField] private ParticleSystem _spellParticle;

        [SerializeField] private float _abillityRestoreTime = 10f;

        private Animator _animator;
        private float _animationTime = 2.3f;

        public override void ActivateAbilitySystem()
        {
            _animator = GetComponentInChildren<Animator>();

            StartCoroutine(UseAbility(_abillityRestoreTime, LevelDI.Resolve<EffectPerformer>()));
        }

        private IEnumerator UseAbility(float AbilityRestoreTime, EffectPerformer effectPerformer)
        {
            yield return new WaitForSecondsRealtime(AbilityRestoreTime);

            while (true)
            {
                _animator.SetTrigger(SPELL_CAST);

                Agent.speed = 0;

                effectPerformer.PlayEffect(_spellParticle, transform.position);

                IncreaseHealth(50);

                yield return new WaitForSecondsRealtime(_animationTime);

                Agent.speed = EnemyConfig.MoveSpeed;

                yield return new WaitForSecondsRealtime(AbilityRestoreTime);
            }
        }
    }
}
