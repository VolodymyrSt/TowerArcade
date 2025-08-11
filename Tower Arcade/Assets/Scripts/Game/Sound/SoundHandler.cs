using Game;
using UnityEngine;

namespace Sound
{
    public class SoundHandler
    {
        private readonly Camera _camera;

        private readonly float _maxVoluem = 1f;
        protected float CurrentVoluem = 1f;

        public SoundHandler() => 
            _camera = Camera.main;

        public void PlaySound(ClipName clipName, Vector3 position) => 
            Play(clipName, position);

        public void PlaySound(ClipName clipName) => 
            Play(clipName, _camera.transform.position);

        private void Play(ClipName clipName, Vector3 position)
        {
            var clip = Resources.Load<AudioClip>($"Sound/{clipName.ToString()}");
            AudioSource.PlayClipAtPoint(clip, position, CurrentVoluem);
        }

        public float GetMaxVoluem() => _maxVoluem;

        public void InitVoluem(SaveData saveData) => 
            CurrentVoluem = saveData.MenuVoluem;

        public void ChangeVoluem(float value, SaveData saveData)
        {
            CurrentVoluem = value;
            saveData.MenuVoluem = CurrentVoluem;
        }
    }

    public enum ClipName
    {
        BallistaShoot,
        CannonShoot,
        CatapultShoot,
        CatapultExplotion,
        CannonExplotion,
        FireShoot,
        IceShoot,
        Selected,
        Click,
        SoftClick,
        Delate,
        Upgrade,
        Victory,
        Lost,
        WaveAnnounce
    }
}
