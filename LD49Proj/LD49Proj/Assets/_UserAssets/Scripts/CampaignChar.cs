using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignChar : MonoBehaviour
{

    private void Start() {
        SetSanity(Random.Range(1, 7));
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)){
            SetSanity(Random.Range(1, 7));
        }
    }


    public void SetSanity(int sanity){
        sanity = Mathf.Clamp(sanity, 1, 6);

        GetComponent<Animator>().SetInteger("PoseIndex", sanity);
    }
}
