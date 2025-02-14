using UnityEngine;

namespace Game
{
    public class TimeHandler
    {
        private EventBus _eventBus;   

        public TimeHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void StopTime()
        {
            _eventBus.Invoke<OnGamePausedSignal>(new OnGamePausedSignal(true));
            Time.timeScale = 0f;
        }
        public void ResumeTime()
        {
            _eventBus.Invoke<OnGamePausedSignal>(new OnGamePausedSignal(false));
            Time.timeScale = 1f;
        }
    }
}
