using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class SceneLoader
    {
        private float _loadingTime = 3f;

        public enum Scene { 
            Loading, Menu, Level1, Level2, Level3, Level4, Level5
        }

        public async void LoadWithLoadingScene(Scene scene)
        {
            Load(Scene.Loading);

            await Task.Delay((int)(_loadingTime * 1000));   

            Load(scene);
        }

        private void Load(Scene scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }

    }
}
