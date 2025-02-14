using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class SceneLoader
    {
        private float _loadingTime = 3f;

        private CoroutineUsager _coroutineUsager;

        public SceneLoader()
        {
            _coroutineUsager = Object.FindFirstObjectByType<CoroutineUsager>();
        }

        public enum Scene
        {
            Loading, Menu, Level1, Level2, Level3, Level4, Level5
        }

        public void LoadWithLoadingScene(Scene scene)
        {
            _coroutineUsager.StartCoroutine(LoadWithLoading(scene));
        }

        public void LoadWithLoadingScene(string sceneName)
        {
            _coroutineUsager.StartCoroutine(LoadWithLoading(sceneName));
        }

        private IEnumerator LoadWithLoading(Scene scene)
        {
            Load(Scene.Loading);
            yield return new WaitForSecondsRealtime(_loadingTime);
            Load(scene);
        }
        
        private IEnumerator LoadWithLoading(string sceneName)
        {
            Load(Scene.Loading);
            yield return new WaitForSecondsRealtime(_loadingTime);
            Load(sceneName);
        }

        public void Load(Scene scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }

        private void Load(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
