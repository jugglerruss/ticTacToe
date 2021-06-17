public class FigureSingle : Figure
{
    protected override void MoveUp()
    {
        BeginDrag();
        Board.My.ShowAvaliblePositions(this);
    }
    public override void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
