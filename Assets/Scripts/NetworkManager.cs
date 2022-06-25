using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    public GameObject loadingText;
    public GameObject createRoomButton;
    public GameObject joinRoomButton;
    private string joinGameId = "";
    public InputField newRoomInputField;
    public InputField existingRoomInputField;
    private string newRoomId = "";


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting to our server");
        PhotonNetwork.ConnectUsingSettings();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Joining the lobby once the user is connected to the server
    public override void OnConnectedToMaster()
    {
        Debug.Log("Joining Lobby");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Ready to start the multiplayer");
        loadingText.SetActive(false);
        createRoomButton.SetActive(true);
        joinRoomButton.SetActive(true);
        
    }

    //Input management for joining a lobby
    public void ReadExistingLobbyId(string input)
    {
        joinGameId = existingRoomInputField.text;
        Debug.Log(joinGameId);
    }

    //Input management for joining a lobby
    public void ReadExistingLobbyId2(string input)
    {
        newRoomId = newRoomInputField.text;
        Debug.Log(newRoomId);
    }


    //Creating new room
    public void makeRoom() {

        if(newRoomId.Length == 0)
        {
            Debug.LogError("Room ID cannot be empty");
            return;
        }
        
       
            RoomOptions roomOptions = new RoomOptions
            {
                IsOpen = true,
                MaxPlayers = 6
            };

            PhotonNetwork.CreateRoom(newRoomId, roomOptions);
            
        
        
    }

    //Showing error in case smth went wrong
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(message);
    }

    //Sending user to Game scene when room created
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }

    //Joining room
    public void joinRoom()
    {
        if(joinGameId.Length == 0)
        {
            Debug.LogError("Room ID cannot be empty");
            return;
        }

        PhotonNetwork.JoinRoom(joinGameId);
    }

    //Showing error in case smth went wrong when joining room
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError(message);
    }

}
