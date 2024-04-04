using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{

    public GameObject prefab;
    [Range(0,10)] public float height=1;
    [Range(-10,10)] public float xOffset=0;
    [Range(-10,10)] public float zOffset=0;
    public bool createOnAwake;

    private GameObject instance;

    // Start is called before the first frame update
    void Start()
    {
        if(createOnAwake)
            CreateIndicator();
    }

    public GameObject CreateIndicator()
    {
        instance = Instantiate(prefab, new Vector3(transform.position.x + xOffset, transform.position.y + height, transform.position.z + zOffset), Quaternion.identity);
        return instance;
    }

    public void DestroyIndicator()
    {
        Destroy(instance);
    }

}
