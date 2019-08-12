using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtonSounds : MonoBehaviour, IPointerEnterHandler
{
    AudioSource source;
    [SerializeField] AudioClip hover;
    [SerializeField] AudioClip click;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        button = GetComponentInParent<Button>();
        button.onClick.AddListener(Clicked);

        hover = Resources.Load<AudioClip>("SFX/Goose");
        click = Resources.Load<AudioClip>("SFX/Shoot");
    }

    private void Clicked()
    {
        source.PlayOneShot(click);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        source.PlayOneShot(hover);
    }
}
