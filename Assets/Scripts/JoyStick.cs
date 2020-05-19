using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{

    public Transform player;
    public float speed = 5.0f;

    private bool touchStart = false;

    private Vector2 pointA;
    private Vector2 pointB;


    public Transform circle;
    public Transform outerCircle;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0)){
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            // No screen view da o default value
            circle.transform.position = pointA;
            outerCircle.transform.position = pointA;
            circle.GetComponent<SpriteRenderer>().enabled = true;
            circle.GetComponent<SpriteRenderer>().enabled = true;
        }
        if(Input.GetMouseButton(0)){
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            // No screen view da o default value
        }else{

            touchStart = false;
            circle.GetComponent<SpriteRenderer>().enabled = false;
            circle.GetComponent<SpriteRenderer>().enabled = false;
        }


        //moveCharacter(new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")));
    }


    private void FixedUpdate(){

        if(touchStart){

            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            moveCharacter(direction);

            circle.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);

        }

    }

    void moveCharacter(Vector2 direction){

        player.Translate(direction * speed * Time.deltaTime);// suave durante os frames
        
    }


}
