using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSwitch : MonoBehaviour
{
    private void Start() {
        if (GameManager.GetCurrTileType() == TileManager.TileType.Treasure){return;}
        else {gameObject.SetActive(false);}
    }
}
