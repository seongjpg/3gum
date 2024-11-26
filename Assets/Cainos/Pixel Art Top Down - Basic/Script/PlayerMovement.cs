using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
	
    public float maxSpeed;
    void Update()
    {
		float xSpeed = Input.GetAxis("Horizontal");
	    float ySpeed = Input.GetAxis("Vertical");
    }
}
