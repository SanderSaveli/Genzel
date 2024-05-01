using System;
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

        public TileParticle tile { get; set; }
        public override void changeOwner(int ownerID)
        {
            if(ownerID == 0)
            {
                tile = null;
            }
            base.changeOwner(ownerID);
            cellView.ChangeOvner(ownerID);

        }

        public void changeOwner(int ownerID, TileParticle tile)
        {
            this.tile = tile;
            changeOwner(ownerID);
        }
    }


}
