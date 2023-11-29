using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    MediapipeHandsReceiver K;
    Disk1 disk1;
    Disk2 disk2;
    int timer;
    public int music;
	public int hold;

    // Start is called before the first frame update
    void Start()
    {
        K = GameObject.Find("MediapipeHandsReceiver").GetComponent<MediapipeHandsReceiver>();
        disk1 = GameObject.Find("Disk1").GetComponent<Disk1>();
        disk2 = GameObject.Find("Disk2").GetComponent<Disk2>();
        timer = 0;
        music = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float distance_disk1;
        float distance_disk2;
        transform.position = new Vector3(K.hand[0].x, 3f, K.hand[0].y);

        distance_disk1 = distance2(disk1.transform.position, transform.position);
		distance_disk2 = distance2(disk2.transform.position, transform.position);
        if (Hold() == 1)
        {
            //timer++;
            //if (timer < 650)
            //{
            //    timer++;
            //    //Change Color
            //}
            if (distance_disk1 < 0.3)
            {
                disk1.transform.position = new Vector3(transform.position.x, (float)0.8, transform.position.z);
                disk1.GetComponent<Rigidbody>().velocity = Vector3.zero;
                if (IsInTable_1(disk1) == 1)
                {
                    disk1.transform.position = new Vector3(0, (float)0.4, 0);
                    music = 1;
                }
            }
            else if (distance_disk2 < 0.3)
            {
                disk2.transform.position = new Vector3(transform.position.x, (float)0.8, transform.position.z);
                disk2.GetComponent<Rigidbody>().velocity = Vector3.zero;
                if (IsInTable_2(disk2) == 1)
                {
                    disk2.transform.position = new Vector3(0, (float)0.4, 0);
                    music = 2;
                }
            }
            if (IsInTable_1(disk1) != 1 && IsInTable_2(disk2) != 1)
                music = 0;
        }
        else
            timer = 0;
    }

    float distance2(Vector3 a, Vector3 b)
    {
        return ((a.x - b.x) * (a.x - b.x) + (a.z - b.z) * (a.z - b.z));
    }

    float distance3(Vector3 a, Vector3 b)
    {
        return ((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z));
    }

    int IsInTable_1(Disk1 disk1)
    {
        if (disk1.transform.position.x >= -0.2 && disk1.transform.position.x <= 0.1)
            {
                if (disk1.transform.position.z >= -0.2 && disk1.transform.position.z <= 0.02)
                    return (1);
            }
        return (0);
    }

    int IsInTable_2(Disk2 disk2)
    {
        if (disk2.transform.position.x >= -0.2 && disk2.transform.position.x <= 0.1)
            {
                if (disk2.transform.position.z >= -0.2 && disk2.transform.position.z <= 0.02)
                    return (1);
            }
        return (0);
    }

    int Hold()
    {
        //if (Input.GetMouseButton(0))
		//{
		//	hold = 1;
        //    return (1);
		//}
        //else
		//{
		//	hold = 0;
        //    return (0);
		//}

        //거리 : 숫자만 조절하면 됨, 해보면서 조절해야할듯!!
        if (distance3(K.hand[4], K.hand[8]) < 0.05)
		{
			hold = 1;
            return (1);
		}
        else
		{
			hold = 0;
            return (0);
		}
    }
}
