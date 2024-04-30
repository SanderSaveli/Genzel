
namespace CellField2D
{
    public interface IReferedCell : IOwnedCell
    {
        public CellView cellView { get;}
        public TileParticle tile { get; set; }
        public void changeOwner(int ownerID, TileParticle tile);
    }

}

