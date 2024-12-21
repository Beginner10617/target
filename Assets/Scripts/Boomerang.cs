using System;
using UnityEngine;
using TMPro;

public class Boomerang : MonoBehaviour
{
    Vector3 mousePosition = new Vector3();
    public bool isMoving = false;
    float radius;
    Vector3 origin;
    public float revolutionSpeed;
    public float rotationSpeed;
    public DottedCircle Guide;
    float angle;
    public TextMeshProUGUI scoreText;
    int score  = 0;
    double theta;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Respawn"))
        {
            score++;
            Destroy(other.gameObject);
        }
    }
    void Start()
    {
        origin = transform.position;
    }
    void Update()
    {
        scoreText.text = "Score: " + score;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition += new Vector3(0,0,10);
        if(!isMoving)
        {
            transform.position = origin;
            transform.rotation = Quaternion.identity;
           // Debug.Log(mousePosition);
           Vector3 direction = mousePosition - origin;
            theta = Mathf.Atan2(direction.x,direction.y);
            radius = Vector3.Distance(origin, mousePosition);
            Guide.radius = radius;
            Guide.gameObject.transform.position = origin + new Vector3((float) Math.Cos(theta), -Mathf.Sin((float) theta),0)*radius;
            if(Input.GetMouseButtonDown(0))
            {
                angle =  Mathf.Rad2Deg * (float) theta;
                isMoving=true;
                return;
            }
        }
        else
        {
            // Update the angle for revolution
            angle += revolutionSpeed * Time.deltaTime;

            // Check if one complete revolution (360 degrees) is done
            if (angle >= 360f + (float) theta)
            {
                isMoving = false;
                transform.position = origin;
                transform.rotation = Quaternion.identity;
                Debug.Log(origin);
            }

            // Calculate the new position for revolution
            float x = Guide.gameObject.transform.position.x - Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float y = Guide.gameObject.transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

            // Set the position for revolution
            transform.position = new Vector3(x, y, transform.position.z);

            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}
