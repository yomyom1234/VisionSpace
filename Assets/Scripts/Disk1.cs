using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Disk1 : MonoBehaviour
{
    cursor c;
    public VideoPlayer video;

    // Start is called before the first frame update
    void Start()
    {
        c = GameObject.Find("cursor").GetComponent<cursor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (c.music == 1)
        {
            video.Play();
            transform.Rotate(new Vector3(0, 1, 0) * 180 * Time.deltaTime * (float)0.3);
        }
        else
        {
            video.Pause();
            ResetMaterial();
        }
    }

    public void ResetMaterial()
    {
        video.Stop();
    }
}
