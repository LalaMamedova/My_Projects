namespace QuizLib.Configurator;

public class AppUserConfigurator
{
    public int MinPasswordLength { get; set; } = 6;
    public int DefaultLockoutTimeSpan { get; set; } = 5;
    public bool UniqueUsername { get; set; } = false;

}
