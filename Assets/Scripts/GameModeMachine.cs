namespace DefaultNamespace
{
    public class GameModeMachine
    {
        public static ModeType CurrentMode { get; set; } = ModeType.Setup;
    }
    
    public enum ModeType
    {
        Setup,
        Play
    }
}