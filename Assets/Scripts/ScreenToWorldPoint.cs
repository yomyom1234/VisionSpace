using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenToWorldPoint : MonoBehaviour
{
    GameObject InvisiblePlane;
    GameObject cursor;

    MeshCollider meshCollider;
    Vector3 worldPosition;
    Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        InvisiblePlane = GameObject.Find("InvisiblePlane");
        cursor = GameObject.Find("cursor");

        meshCollider = InvisiblePlane.GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        ScreenToWorld();
        cursor.transform.position = new Vector3(worldPosition.x, 1, worldPosition.z);
    }

    void ScreenToWorld()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (meshCollider.Raycast(ray, out hit, 1000))
        {
            worldPosition = hit.point;
        }

    }
}
