using UnityEngine;

namespace Sound
{
    public class SoundHandler
    {
        private Camera _camera;

        private float _maxVoluem = 1f;
        private float _currentVoluem = 1f;

        public SoundHandler() => _camera = Camera.main;
    
        public void PlaySound(ClipName clipName, Vector3 position)
        {
            Play(clipName, position);
        }
        
        public void PlaySound(ClipName clipName)
        {
            Play(clipName, _camera.transform.position);
        }

        private void Play(ClipName clipName, Vector3 position)
        {
            var clip = Resources.Load<AudioClip>($"Sound/{clipName.ToString()}");
            AudioSource.PlayClipAtPoint(clip, position, _currentVoluem);
        }

        public float GetVoluem() => _currentVoluem;
        public float GetMaxVoluem() => _maxVoluem;
        public void ChangeVoluem(float value) => _currentVoluem = value;
    }

    public enum ClipName { 
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
