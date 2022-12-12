using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class ConnectObject : MonoBehaviour
{
    private coin thisObject;
    private int counter;

    private void Awake()
    {
        thisObject = GetComponent<coin>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCollider"))
        {
            PlayerPrefs.SetInt(thisObject.id, PlayerPrefs.GetInt(thisObject.id) + 1);
            Destroy(gameObject);
            counter = counter + 1;

        }
        if (collision.CompareTag("Finish"))
        {
            if (counter == 4)
            {
                Debug.Log("Counter: " + counter);
                Debug.Log("You Win!");
                
            }
            else
            {
                Debug.Log("You lose!");
            }
        }
    }
}

