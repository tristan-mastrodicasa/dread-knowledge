using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TileSystem {
    public class TileNavigator : MonoBehaviour
    {
        public float moveSpeed = 3f;

        public Tile currentTile;
        int tilesToMoveForward = 0;

        void Awake() {
            transform.position = currentTile.tileAnchor.transform.position;
        }

        public void MoveForward(int numberOfTilesForward) {
            tilesToMoveForward += numberOfTilesForward;
        }

        void Update() {
            if(tilesToMoveForward != 0) {
                if (currentTile.nextTile) {
                    Vector3 positionOfNextTilesAnchor = currentTile.nextTile.tileAnchor.transform.position;

                    if (transform.position == positionOfNextTilesAnchor) {
                        tilesToMoveForward--;
                        currentTile = currentTile.nextTile;
                    } else {
                        transform.position = Vector3.MoveTowards(transform.position, positionOfNextTilesAnchor, moveSpeed * Time.deltaTime);
                    }
                } else {
                    tilesToMoveForward = 0;
                    SceneManager.LoadScene("EndGame", LoadSceneMode.Single);
                }
            } 
            if (Input.GetKeyDown(KeyCode.Return)){
                MoveForward(1);
            }
        }
    }
}