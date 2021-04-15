using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    [SerializeField] GameObject human;
    [SerializeField] GameObject robot;
    [SerializeField] GameObject camera;

    void Update()
    {
        if (human.activeInHierarchy == true)
        {
            camera.transform.position = human.transform.position + new Vector3(0, 30, 0);
        }
        else
        {
            camera.transform.position = robot.transform.position + new Vector3(0, 30, 0);
        }
    }
}
