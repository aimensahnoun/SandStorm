using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ExitScript :MonoBehaviourPunCallbacks
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void exit()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }
}
