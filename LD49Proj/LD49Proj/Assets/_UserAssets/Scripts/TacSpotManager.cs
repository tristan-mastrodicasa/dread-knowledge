using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacSpotManager : MonoBehaviour
{


    public void ShowCharacter(Character c){
        SkinnedMeshRenderer[] renderers = this.GetComponentsInChildren<SkinnedMeshRenderer>(true);

        foreach(SkinnedMeshRenderer s in renderers){
            if (s.gameObject.activeSelf){
                s.gameObject.SetActive(false);
            }
        }

        if (c == null){return;}

        renderers[c.modelIndex].gameObject.SetActive(true);
        
    }
}
