using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Where the party dais is supposed to go relative to the tile's local pivot.
    public Vector3 daisPosOffset;

    public Vector3 GetDaisPosition(){
        return transform.TransformPoint(daisPosOffset);
    }

}
