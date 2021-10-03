using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TileSystem {
    public class Tile : MonoBehaviour
    {
        public Tile nextTile;
        public TileAnchor tileAnchor;

        public TileAnchor tileTypeIconAnchor;

        public TileManager.TileType tileType;
    }
}
