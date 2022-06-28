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
        Color playerColor = new Color(
        Random.Range(0f, 1f),
        Random.Range(0f, 1f),
        Random.Range(0f, 1f));

        GetComponent<Renderer>().material.color = playerColor;
        assignColor();
    }

    void assignColor()
    {
        isAssignedColor = true;    }

}

