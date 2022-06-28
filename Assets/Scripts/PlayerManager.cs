using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] playerArray;

    public float deathTriggerHeight = -10f;

    public bool startGame = false;

    public int minNumberOfPlayers = 2;

    private bool hasTheGameStarted = false;




    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }



    PhotonView managerView;
    void FixedUpdate()
    {
        if (playerArray.Length < 6 || !hasTheGameStarted)
        {
            PlayerAddCheck();
        }
    }

    public bool GetStartGame()
    {
        return startGame;
    }

    private void LateUpdate()
    {
        managerView = PhotonView.Get(this);
        
        if (playerArray.Length >= minNumberOfPlayers)
        {
            managerView.RPC("SetGameStart", RpcTarget.All);
        }

        if (startGame && playerArray.Length == 1)
        {
            if (playerArray[0].GetComponent<PhotonView>().IsMine)
            {
                PhotonNetwork.AutomaticallySyncScene = false;
                SceneManager.LoadScene("YouWinScene");
            }
            return;
        }
    }

    private void Update()
    {
        

        PlayerDeathCheck();
    }

    private void PlayerAddCheck()
    {
        //Returns an array of active players.
        playerArray = GameObject.FindGameObjectsWithTag("Player");
    }

    private void PlayerDeathCheck()
    {
        foreach (var player in playerArray)
        {
            if (player.transform.position.y < deathTriggerHeight)
            {
                if (player.GetComponent<PhotonView>().IsMine)
                {
                    PhotonNetwork.AutomaticallySyncScene = false;
                    SceneManager.LoadScene("YouLoseScene");
                }
                
                Destroy(player);
            }
        }
    }
    
    

    [PunRPC]
    public void SetGameStart()
    {
        startGame = true;
    }

}
