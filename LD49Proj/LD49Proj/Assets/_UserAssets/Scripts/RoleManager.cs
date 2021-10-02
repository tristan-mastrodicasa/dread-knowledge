using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleManager : MonoBehaviour
{

    public enum Roles {Sleep, Guard, Heal}

    public List<DraggableName> draggableNames;


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)){
            Debug.Log("Roles:");

            
        }
    }
    

    
}
