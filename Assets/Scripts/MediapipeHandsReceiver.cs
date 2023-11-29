using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;



public class MediapipeHandsReceiver : MonoBehaviour
{
    public int multiply = 1;
    bool activate = false;
    UdpClient client;
    IPEndPoint remoteIpEndPoint;
    Thread receiverThread;
    int port = 21900;
    /*
    public Vector3[] leftHand = new Vector3[21];
    public Vector3[] rightHand = new Vector3[21];
    */
    public Vector3[] hand = new Vector3[21];
    private void Start()
    {
        Listen();
    }
    public void Listen()
    {
        client = new UdpClient(port);
        remoteIpEndPoint = new IPEndPoint(IPAddress.Any, port);
        receiverThread = new Thread(new ThreadStart(ListenUDPThread));
        receiverThread.Name = "UDP Receiver";
        receiverThread.Start();
    }
    private void ListenUDPThread()
    {
        activate = true;
        while (activate)
        {
            try
            {
                byte[] dataPacket = client.Receive(ref remoteIpEndPoint);
                if (dataPacket != null && dataPacket.Length > 0)
                {
                    string receivedMessage = System.Text.Encoding.Default.GetString(dataPacket);
					string[] splitedMessage = receivedMessage.Split(new Char[] { ',' });
                    Vector3[] handLandmarks_1 = new Vector3[21];
                    for (int i = 0; i < 21; i++)
                    {
                        handLandmarks_1[i] =
                            new Vector3(float.Parse(splitedMessage[(i * 3) + 2].ToString()) * 5f - 2.5f,
                                        float.Parse(splitedMessage[(i * 3) + 3].ToString()) * -3f + 1.7f,
                                        float.Parse(splitedMessage[(i * 3) + 4].ToString()) * 0f);
                    }
                    if (splitedMessage[1] == "Left")
                    {
                        hand = handLandmarks_1;
                    }
                    else if (splitedMessage[1] == "Right")
                    {
                        hand = handLandmarks_1;
                    }
                    else Console.WriteLine("error/lost.");

                    /*
                    if (int.Parse(splitedMessage[0].ToString()) == 2)
                    {
                        Vector3[] handLandmarks_2 = new Vector3[21];
                        for (int i = 0; i < 21; i++)
                        {
                            handLandmarks_2[i] =
                                new Vector3(float.Parse(splitedMessage[(21 * 3) + (i * 3) + 3].ToString()),
                                            float.Parse(splitedMessage[(21 * 3) + (i * 3) + 4].ToString()) * -1,
                                            float.Parse(splitedMessage[(21 * 3) + (i * 3) + 5].ToString()));
                        }
                        if (splitedMessage[(21 * 3) + 2] == "Left")
                        {
                            leftHand = handLandmarks_2;
                        }
                        else if (splitedMessage[(21 * 3) + 2] == "Right")
                        {
                            rightHand = handLandmarks_2;
                        }
                        else Console.WriteLine("Hand2 data error/lost.");
                    }
                    if (receivedMessage != null && receivedMessage.Length == 0)
                    {
                        Debug.Log("Received packet is empty");
                    }
                    */

                }
            }
            catch(Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

    }
    /*
    public void Close()
    {
        activate = false;
        receiverThread.Abort();
        if (client != null) client.Close();
    }
    private void OnDestroy()
    {
        Close();
    }
    */
}
