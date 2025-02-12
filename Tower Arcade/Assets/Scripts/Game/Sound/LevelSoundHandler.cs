using Game;
using UnityEngine;

namespace Sound
{
    public class LevelSoundHandler : SoundHandler
    {
        public override void InitVoluem(Game.SaveData saveData, SaveSystem saveSystem)
        {
            if (saveData.LevelVoluem == 0)
                CurrentVoluem = saveSystem.Load().LevelVoluem;
            else
                return;
        }

        public override void ChangeVoluem(float value, Game.SaveData saveData, SaveSystem saveSystem)
        {
            CurrentVoluem = value;
            saveData.LevelVoluem = CurrentVoluem;
            saveSystem.Save(saveData);
        }

    }
}

