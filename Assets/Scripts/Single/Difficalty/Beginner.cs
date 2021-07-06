public class Beginner : Difficalty
{
    public Beginner(Game game, Board board, IDifficaltySwitcher difficaltySwitcher) : base(game, board, difficaltySwitcher)
    {
    }

    public override void DecisionWhatToDo()
    {
        DecisionFigure = GetRandomFigure();
        DecisionCell = GetRandomCell(DecisionFigure);
    }
}
