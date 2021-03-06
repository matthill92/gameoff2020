﻿using UnityEngine;
public class script_launcher : MonoBehaviour
{
    public GameObject line;
    public GameObject projectile;
    public float LINE_MAX_LENGTH = 3f;
    public float MAX_POWER = 1000f;
    public GameObject moon;
    float COOLDOWN = 0.3f;
    float power = 0f;
    float timer = 0f;
    public AudioSource sound;

    // Update is called once per frame
    void Update()
    {
        //Check if we need to draw line
        if (line.activeSelf == true)
        {
            drawLine();
        }
        //Check cooldown timer
        else if (timer > 0) 
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                GetComponent<SphereCollider>().enabled = true;
                moon.gameObject.SetActive(true);
            }
        }
    }

    void OnMouseEnter()
    {
        moon.transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
    }
    private void OnMouseExit()
    {
        moon.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }


    public void OnMouseDown()
    {
        Debug.Log("Click");
        line.SetActive(true);
        moon.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        //The physics require we turn off collision while the projectile is spawned
        //A timer is set to turn it back on
        GetComponent<SphereCollider>().enabled = false;
        timer = COOLDOWN;
    }

    public void OnMouseUp()
    {
        Debug.Log("UnClick");
        line.SetActive(false);

        sound.Play();

        moon.transform.position = transform.position;
        moon.gameObject.SetActive(false);

        //Create a Moon Object and give it a force in the direction of the mouse
        var p = Instantiate(projectile, transform.TransformPoint(0,0,0),Quaternion.Euler(0,0,0));
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        p.transform.LookAt(mousePos);
        p.GetComponent<Rigidbody>().AddForce(-p.transform.forward * MAX_POWER * power,ForceMode.Impulse);
        Debug.Log(MAX_POWER * power * Time.deltaTime);
    }

    void drawLine() //Draws a line from this object to the mouse pointer, obeying max length
    {
        var mousePos = transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mousePos.z = transform.position.z;
        moon.transform.position = (transform.TransformPoint(mousePos) + transform.position * 4) / 5;
        mousePos = -mousePos;
        var lineLength = Mathf.Sqrt(mousePos.x * mousePos.x + mousePos.y * mousePos.y);
        if (lineLength < LINE_MAX_LENGTH)
        {
            line.GetComponent<LineRenderer>().SetPosition(1, mousePos);
            power = lineLength / LINE_MAX_LENGTH;
        }
        else
        {
            var endPos = mousePos / lineLength * LINE_MAX_LENGTH;
            endPos.z = mousePos.z;
            line.GetComponent<LineRenderer>().SetPosition(1, endPos);
            power = 1f;
        }
    }

}
