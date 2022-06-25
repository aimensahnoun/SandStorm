using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SphereMovement : MonoBehaviour
{
    public Rigidbody sphereRb;

    public float moveSpeed = 20f;
    private float collisionForce = 225f;

    private float xInput;
    private float zInput;

    private bool isGameStarted = false;

    private GameObject gameManager;
    private PlayerManager managerScript;

    public PhotonView playerPhotonView;
    
    public Text usernameText;

    private void Start()
    {
        sphereRb = GetComponent<Rigidbody>();
        playerPhotonView = GetComponent<PhotonView>();
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        managerScript = gameManager.GetComponent<PlayerManager>();
        

        playerPhotonView.Owner.NickName = "Player_" + UnityEngine.Random.Range(0, 100).ToString();

        if (playerPhotonView.IsMine)
        {
            usernameText.text = playerPhotonView.Owner.NickName;
            usernameText.color = Color.green;
        } else {
            usernameText.text = playerPhotonView.Owner.NickName;
            usernameText.color = Color.red;
        }
    }

    private void Update()
    {
        if (managerScript.GetStartGame() && !isGameStarted)
        {
            isGameStarted = true;
        }
        
        if (playerPhotonView.IsMine)
        {
            ProcessInputs();
        }
    }

    private void FixedUpdate()
    {
        //if (playerPhotonView.IsMine && isGameStarted)
        //{
        //    Move();
        //}
        Move();
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            Rigidbody collisionRB = c.collider.GetComponent<Rigidbody>();

            Vector3 collisionDirection = c.contacts[0].point - transform.position;
            collisionDirection = -collisionDirection.normalized;
            
            print("PLAYER COLLISION DETECTED");
            
            sphereRb.AddForce(collisionDirection * collisionForce);
            collisionRB.AddForce(-collisionDirection * collisionForce);
        }
    }

    private void ProcessInputs()
    {
        if (!playerPhotonView.IsMine) return;
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
    }

    private void Move()
    {
        if (!playerPhotonView.IsMine) return;
        sphereRb.AddForce(new Vector3(xInput, 0f, zInput) * moveSpeed);
    }
}
