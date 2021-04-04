using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PaddleController : MonoBehaviour
{
	private PhotonView myPV;

    public string leftKey, rightKey;
	public float speed; 
    private bool IsMine; 

    private void Start()
    {
        // Debug.Log();
        myPV = GetComponent<PhotonView>();
        if (myPV.IsMine)
        {
            Camera.main.transform.rotation = transform.rotation;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(myPV.IsMine)
        {
            PaddleMovement();   
        }
    }
 
    void PaddleMovement() 
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKey(leftKey) && transform.position.x > -2) 
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed, Space.Self);
            }
            if (Input.GetKey(rightKey) && transform.position.x < 2) 
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed, Space.Self);
            }
        }   
        else
        {
            ClientPaddleMovement();
        }


    }

    void ClientPaddleMovement() 
    {
        if (Input.GetKey(leftKey) && transform.position.x < 2) 
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.Self);
        }
        if (Input.GetKey(rightKey) && transform.position.x > -2)     
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.Self);
        }
    }
}
