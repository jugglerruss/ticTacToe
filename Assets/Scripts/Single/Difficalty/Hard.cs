public class Hard : Difficalty
{
    public Hard(Game game, Board board, IDifficaltySwitcher difficaltySwitcher) : base(game, board, difficaltySwitcher)
    {
    }

    public override void DecisionWhatToDo()
    {
        throw new System.NotImplementedException();
    }
}
