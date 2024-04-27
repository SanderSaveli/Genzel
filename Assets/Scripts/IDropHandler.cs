public interface IDropHandler
{
    public bool DropFigure(TileParticle figure);

    public void EnterHover(TileParticle figure);
    public void ExitHover(TileParticle figure);
}
