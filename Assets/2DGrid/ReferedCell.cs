using UnityEngine;

namespace CellField2D
{
    public class ReferedCell : OwnedCell, IReferedCell
    {
        public ReferedCell(Vector2Int coordinates, int ownerID, CellView cellView) : base(coordinates, ownerID)
        {
            this.cellView = cellView;
        }

        public CellView cellView { get; private set; }

        public override void changeOwner(int ownerID)
        {
            base.changeOwner(ownerID);
            cellView.ChangeOvner(ownerID);

        }
    }


}
