using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class ChatSystem : MonoBehaviourPunCallbacks
{

    public PhotonView PhotonView;
    public GameObject ChatBar;
    public GameObject ChatFeed;
    public InputField InputField;

    // Start is called before the first frame update
    void Start()
    {
        PhotonView = GetComponent<PhotonView>();
        InputField = GameObject.Find("InputField").GetComponent<InputField>();
        ChatBar = GameObject.Find("ChatBar");
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonView.IsMine){
            if(InputField.isFocused){
                if(InputField.text != "" && InputField.text.Length > 0 && Input.GetKeyDown(KeyCode.LeftAlt)){
                    PhotonView.RPC("SendMessage", RpcTarget.All, InputField.text);
                    InputField.text = "";
                }
            }
        }
    }

    [PunRPC]
    public void SendMessage(string message){
        GameObject newMessage = Instantiate(ChatFeed, Vector3.zero, Quaternion.identity);
        newMessage.transform.SetParent(ChatBar.transform, false);
        newMessage.GetComponent<Text>().text = message;
        newMessage.GetComponent<Text>().color = Color.yellow;
        // Destroy
        Destroy(newMessage, 5f);
    }
}

