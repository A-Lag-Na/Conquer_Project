using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera = null;
    [SerializeField] AudioSource townMusicSource = null;
    [SerializeField] AudioSource forestMusicSource = null;
    [SerializeField] AudioSource desertMusicSource = null;
    [SerializeField] AudioSource mountainsMusicSource = null;
    [SerializeField] AudioSource castleMusicSource = null;
    [SerializeField] AudioSource bossMusicSource = null;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        townMusicSource.enabled = false;
        forestMusicSource.enabled = false;
        desertMusicSource.enabled = false;
        mountainsMusicSource.enabled = false;
        castleMusicSource.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera.transform.position.z > -112 && mainCamera.transform.position.z < 1.9f && mainCamera.transform.position.x > -120.36f && mainCamera.transform.position.x < 119.64f)
        {
            townMusicSource.enabled = true;
            forestMusicSource.enabled = false;
            desertMusicSource.enabled = false;
            mountainsMusicSource.enabled = false;
            castleMusicSource.enabled = false;
            bossMusicSource.enabled = false;
        }
        else if (mainCamera.transform.position.z > 1.9f)
        {
            townMusicSource.enabled = false;
            forestMusicSource.enabled = true;
            desertMusicSource.enabled = false;
            mountainsMusicSource.enabled = false;
            castleMusicSource.enabled = false;
            bossMusicSource.enabled = false;
        }
        else if (mainCamera.transform.position.z <= -112 && mainCamera.transform.position.z > -188 && mainCamera.transform.position.x > -120.36f)
        {
            townMusicSource.enabled = false;
            forestMusicSource.enabled = false;
            desertMusicSource.enabled = false;
            mountainsMusicSource.enabled = false;
            castleMusicSource.enabled = true;
            bossMusicSource.enabled = false;
        }
        else if (mainCamera.transform.position.x <= -120)
        {
            townMusicSource.enabled = false;
            forestMusicSource.enabled = false;
            desertMusicSource.enabled = false;
            mountainsMusicSource.enabled = true;
            castleMusicSource.enabled = false;
            bossMusicSource.enabled = false;
        }
        else if (mainCamera.transform.position.x >= 119.64f)
        {
            townMusicSource.enabled = false;
            forestMusicSource.enabled = false;
            desertMusicSource.enabled = true;
            mountainsMusicSource.enabled = false;
            castleMusicSource.enabled = false;
            bossMusicSource.enabled = false;
        }
        else if (mainCamera.transform.position.z <= -185)
        {
            townMusicSource.enabled = false;
            forestMusicSource.enabled = false;
            desertMusicSource.enabled = false;
            mountainsMusicSource.enabled = false;
            castleMusicSource.enabled = false;
            bossMusicSource.enabled = true;
        }
    }
}
