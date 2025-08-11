using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class SceneLoader
    {
        public enum Scene { Loading, Menu }

        private const float LOADING_TIME = 3f;

        private readonly CoroutineUsager _coroutineUsager;

        public SceneLoader(CoroutineUsager coroutineUsager) => 
            _coroutineUsager = coroutineUsager;

        public void LoadWithLoadingScene(Scene scene) => 
            _coroutineUsager.StartCoroutine(LoadWithLoading(scene));

        public void LoadWithLoadingScene(string sceneName) => 
            _coroutineUsager.StartCoroutine(LoadWithLoading(sceneName));

        private IEnumerator LoadWithLoading(Scene scene)
        {
            Load(Scene.Loading);
            yield return new WaitForSecondsRealtime(LOADING_TIME);
            Load(scene);
        }
        
        private IEnumerator LoadWithLoading(string sceneName)
        {
            Load(Scene.Loading);
            yield return new WaitForSecondsRealtime(LOADING_TIME);
            Load(sceneName);
        }

        public void Load(Scene scene) => 
            SceneManager.LoadScene(scene.ToString());

        private void Load(string sceneName) => 
            SceneManager.LoadScene(sceneName);
    }
}
