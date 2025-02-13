using Game;

namespace Sound
{
    public class MenuSoundHandler : SoundHandler
    {
        public override void InitVoluem(Game.SaveData saveData, SaveSystem saveSystem)
        {
            if (saveData.MenuVoluem == 0)
                CurrentVoluem = saveSystem.Load().MenuVoluem;
            else
                return;
        }

        public override void ChangeVoluem(float value, Game.SaveData saveData, SaveSystem saveSystem)
        {
            CurrentVoluem = value;
            saveData.MenuVoluem = CurrentVoluem;
            saveSystem.Save(saveData);
        }

    }
}
