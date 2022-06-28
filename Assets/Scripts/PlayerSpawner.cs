using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector
    GameObject player;
    public static PlayerSpawner Instance;
    PhotonView photonView;
    public float spawnCoordinateMinX = 5f;
    public float spawnCoordinateMaxX = -5f;
    public float spawnCoordinateMinZ = 5f;
    public float spawnCoordinateMaxZ = -5f;
    // This script will simply instantiate the Prefab when the game starts.

    private void Start()
    {

        Vector3 randomSpawnPosition = new Vector3(Random.Range(spawnCoordinateMinX, spawnCoordinateMaxX), 8f,
                Random.Range(spawnCoordinateMinZ, spawnCoordinateMaxZ));
        player = PhotonNetwork.Instantiate("Player", randomSpawnPosition, Quaternion.identity);
        player.GetComponent<Renderer>().material.color = Color.green;


    }

}