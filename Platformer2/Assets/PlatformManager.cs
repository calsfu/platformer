using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{

    public static PlatformManager Instance = null;
    [SerializeField]
    GameObject platformPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy (gameObject);
    }

    // Update is called once per frame
    void Start()
    {
        Instantiate (platformPrefab, new Vector2 (-3.5f, -2.5f), platformPrefab.transform.rotation);
        Instantiate (platformPrefab, new Vector2 (0f, -2.5f), platformPrefab.transform.rotation);
        Instantiate (platformPrefab, new Vector2 (3.5f, -2.5f), platformPrefab.transform.rotation);
    }

    IEnumerator SpawnPlatform(Vector2 spawnPosition)
    {
        yield return new WaitForSeconds(2f);
        Instantiate (platformPrefab, spawnPosition, platformPrefab.transform.rotation);
    }
}
