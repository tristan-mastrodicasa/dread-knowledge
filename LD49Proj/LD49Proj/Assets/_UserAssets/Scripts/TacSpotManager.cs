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


    public CharIndicator charIndicator;
    private Animator charAnim;
    
    private void Awake() {
        charIndicator = GetComponentInChildren<CharIndicator>();
        charAnim = GetComponentInChildren<Animator>();
    }



    private void Update() {
        int spotIndex = RoleManager.instance.spots.IndexOf(this);
        DraggableName d = RoleManager.instance.draggableNames[spotIndex];
        Character c = d.character;
        if (c == null){return;}
        int sanity = c.sanity;
        charAnim.SetInteger("PoseIndex", sanity);
    }
}
