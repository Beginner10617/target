using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLose : MonoBehaviour
{
    int LoseCount = 3;
    public List<GameObject> todeactivate;
    public GameObject LoseScreen;
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Respawn"))
        {
            LoseCount--;
            Destroy(other.gameObject);
        }
    }
    void Update()
    {
        if(LoseCount<=0)
        {
            foreach(GameObject obj in todeactivate)
            {
                obj.SetActive(false);
            }
            LoseScreen.SetActive(true);
        }
    }
}
