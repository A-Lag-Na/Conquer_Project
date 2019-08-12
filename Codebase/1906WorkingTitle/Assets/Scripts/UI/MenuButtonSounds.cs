using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonSounds : MonoBehaviour
{

    AudioSource source;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        button = GetComponentInParent<Button>();
        button.onClick.AddListener(Clicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        
    }

    private void Clicked()
    {
        source.PlayOneShot(source.clip);
    }
}
