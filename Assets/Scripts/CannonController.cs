using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{

    [SerializeField] private Transform cannonHead;
    [SerializeField] private Transform cannonTip;
    [SerializeField] private float shootingCooldown = 1.5f;
    [SerializeField] private float laserPower = 6f;
    private GameObject gameManager;
    private PlayerManager managerScript;

    private bool isGameStarted = false;



    private bool isPlayerInRange = false;

    private GameObject player;

    private float timeLeftToShoot = 0;

    private LineRenderer cannonlaser;

     // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        managerScript = gameManager.GetComponent<PlayerManager>();

        cannonlaser = GetComponent<LineRenderer>();


        cannonlaser.sharedMaterial.color = Color.green;
        cannonlaser.enabled = false;

        timeLeftToShoot = shootingCooldown;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            isPlayerInRange = true;
            cannonlaser.enabled = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            cannonlaser.enabled = false;
            timeLeftToShoot = shootingCooldown;
            cannonlaser.sharedMaterial.color = Color.green;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!managerScript.GetStartGame()) return;
        if (isPlayerInRange)
        {
            cannonHead.transform.LookAt(player.transform);
            cannonlaser.SetPosition(0, cannonTip.transform.position);
            cannonlaser.SetPosition(1, player.transform.position);

            timeLeftToShoot -= Time.deltaTime;
        }

        if (timeLeftToShoot < shootingCooldown * 0.5)
        {
            Color red = Color.red;
            cannonlaser.sharedMaterial.color = red;
        }

        if (timeLeftToShoot <= 0)
        {
            Vector3 shootForce = player.transform.position - cannonTip.transform.position;

            player.GetComponent<Rigidbody>().AddForce(shootForce * laserPower, ForceMode.Impulse);
            timeLeftToShoot = shootingCooldown;
            cannonlaser.sharedMaterial.color = Color.green;
        }
    }
}
