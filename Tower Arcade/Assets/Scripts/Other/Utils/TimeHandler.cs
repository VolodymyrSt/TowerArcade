using UnityEngine;

namespace Game
{
    public class TimeHandler
    {
        public void StopTime() => Time.timeScale = 0f;
        public void ResumeTime() => Time.timeScale = 1f;
    }
}
