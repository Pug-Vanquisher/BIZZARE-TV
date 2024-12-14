namespace Balance
{
    public static class Scenes
    {
        public const string BOOT = "BalanceBoot";
        public const string GAMEPLAY = "BalanceGameplay";
        public const string LEVEL_LIST = "BalanceLevelList";

        public static bool IsBalanceGameScene(string name)
        {
            return name == BOOT || name == GAMEPLAY || name == LEVEL_LIST;
        }
    }
}
