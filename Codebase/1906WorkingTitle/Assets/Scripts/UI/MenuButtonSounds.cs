using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtonSounds : MonoBehaviour, IPointerEnterHandler
{
    AudioSource source = null;
    [SerializeField] AudioClip hover = null;
    [SerializeField] AudioClip click = null;
    Button button = null;

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<AudioSource>())
            source = GetComponent<AudioSource>();
        if(GetComponent<Button>())
            button = GetComponentInParent<Button>();
        if(button != null)
            button.onClick.AddListener(Clicked);
        hover = Resources.Load<AudioClip>("SFX/hover");
        click = Resources.Load<AudioClip>("SFX/click");
    }

    private void Clicked()
    {
        if(source != null && source.isActiveAndEnabled)
            source.PlayOneShot(click);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.Select();
        source.PlayOneShot(hover);
    }
}
