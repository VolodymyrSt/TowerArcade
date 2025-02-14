using System.Collections;
using UnityEngine;

namespace Game
{
    public class DragonController : Enemy
    {
        [SerializeField] private ParticleSystem _spellParticle;

        public override void ActivateAbilitySystem()
        {
            StartCoroutine(UseAbility(LevelRegistrator.Resolve<EffectPerformer>()));
        }

        private IEnumerator UseAbility(EffectPerformer effectPerformer)
        {
            while (true)
            {
                if (CurrentHealth < EnemyConfig.MaxHealth / 2f)
                {
                    effectPerformer.PlayEffect(_spellParticle, transform.position);
                    Agent.speed = EnemyConfig.MoveSpeed * 2f;
                    break;
                }

                yield return null;
            }
        }
    }
}
