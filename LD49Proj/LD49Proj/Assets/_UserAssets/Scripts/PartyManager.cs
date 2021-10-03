using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{


    private Transform daisTransform;

    private void Awake() {
        daisTransform = GameObject.Find("Dais").transform;
    }

    public int currTile = 0;

    public void AdvanceTile(){
        currTile = (currTile + 1)%TileManager.GetTiles().Count;
    }



    public float movementSpeed = 2f;

    private void Update() {
        /*
        daisTransform.position = Vector3.MoveTowards(daisTransform.position,
                TileManager.GetTiles()[currTile].GetDaisPosition(), movementSpeed * Time.deltaTime);
        */
        

        if (Input.GetKeyDown(KeyCode.Return)){
            AdvanceTile();
        }
    }
}
