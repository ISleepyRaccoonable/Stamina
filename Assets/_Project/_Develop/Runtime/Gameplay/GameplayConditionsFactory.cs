using Assets.Project._Develop.Runtime.Gameplay;

public class GameplayConditionsFactory
{
    private Typer _typer;

    public GameplayConditionsFactory(Typer typer)
    {
        _typer = typer;
    }

    public IGameCondition CreateWinCondition(string generatedSequence) =>
        new MatchingCondition(_typer, generatedSequence);

    public IGameCondition CreateDefeatCondition(string generatedSequence) =>
        new NotMatchingCondition(_typer, generatedSequence);
}