public class FigureSingle : Figure
{
    protected override void MoveUp()
    {
        Select();
        _board.ShowAvaliblePositions(this);
    }
    public override void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
