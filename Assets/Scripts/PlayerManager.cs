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

    public float deathTriggerHeight = -100f;

    public bool startGame = false;

    public int minNumberOfPlayers = 3;

    private bool hasTheGameStarted = false;




    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
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
