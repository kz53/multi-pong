using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class BallController : MonoBehaviour
{
	Rigidbody2D myRb;
	bool setSpeed;
	[SerializeField] float speedUp;
	float xSpeed;
	float ySpeed;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PhotonNetwork.IsMasterClient)
            return;
    	if(GameController.instance.inPlay == true)
    	{
			if(!setSpeed) 
	        {
	        	setSpeed = true;

	        	xSpeed = Random.Range(1f, 2f) * Random.Range(1, 2) * 2 - 1;
	        	ySpeed = -1*(Random.Range(1f, 2f) * Random.Range(1, 2) * 2 - 1);
	        }
	        MoveBall();
    	}        
    }

    void MoveBall()
    {
    	myRb.velocity = new Vector2(xSpeed, ySpeed);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log(other.transform.tag);
    	if(other.transform.tag == "Wall")
    	{
            Debug.Log("bbb");
    		xSpeed = xSpeed * -1;
    	}

    	if(other.transform.tag == "Paddle") 
    	{
            Debug.Log("ccc");
    		ySpeed = ySpeed * -1;

    		if(ySpeed > 0)
    		{
    			ySpeed += speedUp;
    		}
    		else
    		{
    			ySpeed -= speedUp;
    		}

    		if(xSpeed > 0)
    		{
    			xSpeed += speedUp;
    		}
    		else
    		{
    			xSpeed -= speedUp;
    		}
    	}
	}

    void OnTriggerEnter2D(Collider2D other)
    {
    	Debug.Log(other.tag);
        if(other.tag == "EndOne")
    	{
    		GameController.instance.scoreOne++;
            if(PhotonNetwork.IsMasterClient)
            {
                GameController.instance.textOne.text = GameController.instance.scoreOne.ToString();
            }
            else 
            {
                GameController.instance.textTwo.text = GameController.instance.scoreOne.ToString();
            }
            Debug.Log(GameController.instance.scoreOne);
    		GameController.instance.inPlay = false;
    		setSpeed = false;
    		myRb.velocity = Vector2.zero;
    		this.transform.position = Vector2.zero;
    	}
    	else if(other.tag == "EndTwo")
    	{
			GameController.instance.scoreTwo++;
            if(PhotonNetwork.IsMasterClient)
            {
                GameController.instance.textTwo.text = GameController.instance.scoreTwo.ToString();
            }
            else 
            {
                GameController.instance.textOne.text = GameController.instance.scoreTwo.ToString();
            }
            Debug.Log(GameController.instance.scoreTwo);
    		GameController.instance.inPlay = false;
    		setSpeed = false;
    		myRb.velocity = Vector2.zero;
    		this.transform.position = Vector2.zero;
    	}
    }
}
