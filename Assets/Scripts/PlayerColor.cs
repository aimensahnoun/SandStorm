using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerColor : MonoBehaviour
{
    // Start is called before the first frame update

    private bool isAssignedColor = false;
    


    
  
    [PunRPC]
    void changeColor()
    {
        if (isAssignedColor) return;      
        GetComponent<Renderer>().material.color = Color.green;
        assignColor();
    }

    void assignColor()
    {
        isAssignedColor = true;    }

}

