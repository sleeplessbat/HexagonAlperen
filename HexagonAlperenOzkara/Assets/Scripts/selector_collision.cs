﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selector_collision : MonoBehaviour
{
    [SerializeField]
    public float mouseX;
    public float temp_mousePosX;
    public List<GameObject> hexs;
    public List<Vector2> hexs_t;
    public float maxAngle = 360f;
    public float zAngle;
    public float angle;
    public float next_zAngle;
    private float speed = 10f;
    private Vector3 centerHexGroup;
    [SerializeField]
    private bool rotating,right,left;
    private IEnumerator coroutine;
    public Sprite normal, revert;
    public SpriteRenderer renderer;

    game_status gs;

   
    void Start()
    {


        next_zAngle = zAngle + 120f;
        temp_mousePosX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
        gs = GameObject.FindGameObjectWithTag("game_status").GetComponent<game_status>();
        if (gs.is_selector_reverted)
        {
            renderer.sprite = revert;
        }
        else {
            renderer.sprite = normal;
        }
        angle = 0;
        Debug.Log("Sebep");


    }

   
    void Update()
    {
        //is selector stops destroy selector
        if (gs.is_selector_active == false) {
            Destroy(gameObject);
        }

        //if angle = 360  and there is no match destroy selector
        if (angle == 360)
        {
            gs.is_selector_active = false;
            Destroy(gameObject);
        }
        //if is matched
        if (gs.is_matched == true) {
            
            Destroy(gameObject);
        }

        if (rotating)
        {
            if (gs.is_matched == false)
            {
                RotateSelector();
            }
          

        }
       //rotate controls
        if (gs.is_selector_active)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //getting mouse first position
                mouseX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                //if mouse going right
                if (Camera.main.ScreenToViewportPoint(Input.mousePosition).x > mouseX)
                {
                    right = true;
                    rotating = true;

                }
                //if mouse going left
                else if (Camera.main.ScreenToViewportPoint(Input.mousePosition).x < mouseX)
                {
                    left = true;
                    rotating = true;

                }
               
               
            }
        }
       

    }
    //select 3 hex's for turn
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "hex")
        {
            if (hexs.Count < 3)
            {
                hexs.Add(coll.gameObject);
               
            }
        }
    }
   

    private void RotateSelector() {

        //if there is 3 hex's we can turn
        if (hexs[0] != null && hexs[1] != null && hexs[2] != null)
        {
            if (hexs.Count == 3 && !gs.is_matched)
            {
                centerHexGroup = (hexs[0].transform.position + hexs[1].transform.position + hexs[2].transform.position) / 3;

                if (right)
                {
                    //turning right
                    if (angle < next_zAngle)
                    {
                        gs.is_selector_rotating = true;
                        transform.RotateAround(transform.position, Vector3.forward, speed);
                        hexs[0].transform.RotateAround(centerHexGroup, Vector3.forward, speed);
                        // hexs[0].transform.eulerAngles += new Vector3(0, 0, speed);
                        hexs[1].transform.RotateAround(centerHexGroup, Vector3.forward, speed);
                       //hexs[1].transform.eulerAngles += new Vector3(0, 0, speed);
                        hexs[2].transform.RotateAround(centerHexGroup, Vector3.forward, speed);
                        // hexs[2].transform.eulerAngles += new Vector3(0, 0, speed);
                        angle += speed;
                        
                        if (angle == next_zAngle)
                        {

                            rotating = false;
                            gs.is_selector_rotating = false;
                            Invoke("turnAgain", 0.3f);

                        }
                    }
                    else
                    {

                    }
                }
                if (left)
                {
                    //turning left
                    if (angle < next_zAngle)
                    {
                        gs.is_selector_rotating = true;
                        transform.RotateAround(transform.position, Vector3.forward, -speed);
                        hexs[0].transform.RotateAround(centerHexGroup, Vector3.forward, -speed);
                       // hexs[0].transform.eulerAngles += new Vector3(0, 0, -speed);
                        hexs[1].transform.RotateAround(centerHexGroup, Vector3.forward, -speed);
                        // hexs[1].transform.eulerAngles += new Vector3(0, 0, -speed);
                        hexs[2].transform.RotateAround(centerHexGroup, Vector3.forward, -speed);
                        //hexs[2].transform.eulerAngles += new Vector3(0, 0, -speed);
                        angle += speed;
                        
                        if (angle == next_zAngle)
                        {

                            rotating = false;
                            gs.is_selector_rotating = false;
                            Invoke("turnAgain", 0.3f);


                        }
                    }
                    else
                    {

                    }
                }
            }
        }
    }
    //turn again after 120 degress
    private void turnAgain()
    {
        next_zAngle += 120f;
        gs.is_selector_rotating = true;
        rotating = true;
    }
 
}
