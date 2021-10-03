using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace TileSystem {
    public class TileNavigator : MonoBehaviour
    {
        public float moveSpeed = 3f;

        public Tile currentTile;
        int tilesToMoveForward = 0;

        void Awake() {
            transform.position = currentTile.tileAnchor.transform.position;
        }


        private bool isAdvancing = false;

        public void MoveForward(int numberOfTilesForward) {
            isAdvancing = true;
            tilesToMoveForward += numberOfTilesForward;
            GameManager.AdvanceTiles(numberOfTilesForward);
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
            else {
                if (isAdvancing){
                    isAdvancing = false;
                    FinishedAdvancing();
                }
            }

        }

        public UnityEvent onFinishedAdvancing;

        public void FinishedAdvancing(){
            onFinishedAdvancing.Invoke();
        }

    }
}