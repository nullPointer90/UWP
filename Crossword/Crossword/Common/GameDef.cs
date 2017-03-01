namespace Crossword.Common
{
    public class GameDef
    {
        public const int COUNT_CHARACTER_INTEL = 3;
        public const string COLOR_SELECT = "LightBlue";
        public const string COLOR_NOT_SELECT = "Red";
        public const int COUNT_DOWN = 10;
        public const string IMAGE_PLAY_PATH = "ms-appx:///Assets/Play/Play.png";
        public const string IMAGE_PAUSE_PATH = "ms-appx:///Assets/Play/Pause.png";

        public const string IMAGE_LOCK_PATH = "ms-appx:///Assets/Lock/Lock.png";
        public const string IMAGE_UNLOCK_PATH = "ms-appx:///Assets/Lock/Unlock.png";

        public enum Type
        {
            NONE,
            CLASSIC,
            INTELLIGEN
        }

        public enum State
        {
            NONE,
            START,
            STOP
        }

        public enum Difficult
        {
            EASY,
            MEDIUM,
            DIFFICULT
        }

        public enum LifeDefault
        {
            CLASSIC_LIFE = 5,
            INTELLIGEN_LIFE = 1
        }

    }
}
