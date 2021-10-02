using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TileSystem {
    public class TileAnchor : MonoBehaviour
    {
        void OnDrawGizmos() {
            Gizmos.color = Color.yellow;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireSphere(Vector3.zero, 0.1f);
        }
    }
}