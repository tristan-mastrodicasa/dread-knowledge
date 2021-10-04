using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackDisplay : MonoBehaviour
{
    private void Start() {
        string chance = "MODERATE";
        if (GameManager.GetCurrTileType() == TileManager.TileType.Forest){chance = "HIGH";}
        if (GameManager.GetCurrTileType() == TileManager.TileType.Monsters){chance = "CERTAIN";}
        GetComponent<Text>().text = "Chance of Attack\n" + chance;
    }
}
