using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    MediapipeHandsReceiver K;
	public Disk disk_1;
	public Disk disk_2;
	public Disk disk_3;

    public int music;
	public int hold;

    // Start is called before the first frame update
    void Start()
    {
        K = GameObject.Find("MediapipeHandsReceiver").GetComponent<MediapipeHandsReceiver>();
        music = 0;
    }

    // Update is called once per frame
    void Update()
    {
		float distance_disk_1;
		float distance_disk_2;
		float distance_disk_3;
        transform.position = new Vector3(K.hand[0].x, 3f, K.hand[0].y);

		distance_disk_1 = distance2(disk_1.transform.position, transform.position);
		distance_disk_2 = distance2(disk_2.transform.position, transform.position);
		distance_disk_3 = distance2(disk_3.transform.position, transform.position);
        if (Hold() == 1)
        {
            if (distance_disk_1 < 0.3)
            {
                disk_1.transform.position = new Vector3(transform.position.x, (float)0.8, transform.position.z);
                disk_1.GetComponent<Rigidbody>().velocity = Vector3.zero;
                if (IsInTable(disk_1) == 1)
                {
                    disk_1.transform.position = new Vector3(0, (float)0.4, 0);
                    disk_1.music = 1;
                }
            }
            else if (distance_disk_2 < 0.3)
            {
                disk_2.transform.position = new Vector3(transform.position.x, (float)0.8, transform.position.z);
                disk_2.GetComponent<Rigidbody>().velocity = Vector3.zero;
                if (IsInTable(disk_2) == 1)
                {
                    disk_2.transform.position = new Vector3(0, (float)0.4, 0);
                    disk_2.music = 1;
                }
            }
            else if (distance_disk_3 < 0.3)
            {
                disk_3.transform.position = new Vector3(transform.position.x, (float)0.8, transform.position.z);
                disk_3.GetComponent<Rigidbody>().velocity = Vector3.zero;
                if (IsInTable(disk_3) == 1)
                {
                    disk_3.transform.position = new Vector3(0, (float)0.4, 0);
                    disk_3.music = 1;
                }
            }
            if (IsInTable(disk_1) != 1 && IsInTable(disk_2) != 1 && IsInTable(disk_3) != 1)
			{
				disk_1.music = 0;
				disk_2.music = 0;
				disk_3.music = 0;
			}
        }
    }

    float distance2(Vector3 a, Vector3 b)
    {
        return ((a.x - b.x) * (a.x - b.x) + (a.z - b.z) * (a.z - b.z));
    }

    float distance3(Vector3 a, Vector3 b)
    {
        return ((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z));
    }

    int IsInTable(Disk disk)
    {
        if (disk.transform.position.x >= -0.2 && disk.transform.position.x <= 0.1)
            {
                if (disk.transform.position.z >= -0.2 && disk.transform.position.z <= 0.02)
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
