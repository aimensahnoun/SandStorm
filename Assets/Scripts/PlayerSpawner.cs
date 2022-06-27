using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public PhotonView view;
    GameObject player;
    public static PlayerSpawner Instance;

    // This script will simply instantiate the Prefab when the game starts.

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (Instance)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
    }



    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Vector3 spawnPosition = new Vector3(Random.Range(-4f, 4), 10, Random.Range(-4f, 4));
        if (PhotonNetwork.InRoom)
        {
            player = PhotonNetwork.Instantiate("Player", spawnPosition , Quaternion.identity) as GameObject;
            view.RPC("ChangeColor", RpcTarget.AllBuffered);
        }

    }

    [PunRPC]
    private void ChangeColor()
    {
        Renderer rend = player.GetComponent<Renderer>();
        Color playerColor = new Color(
        Random.Range(0f, 1f),
        Random.Range(0f, 1f),
        Random.Range(0f, 1f));

        rend.material.color = playerColor;
    }
}