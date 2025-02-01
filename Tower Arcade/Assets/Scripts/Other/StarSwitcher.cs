using UnityEngine;

namespace Game
{
    public class StarSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject _filledStar;
        [SerializeField] private GameObject _unfilledStar;

        public void SetStar(bool filled)
        {
            if (filled)
            {
                _filledStar.SetActive(true);
                _unfilledStar.SetActive(false);
            }
            else
            {
                _filledStar.SetActive(false);
                _unfilledStar.SetActive(true);
            }
        }
    }
}
