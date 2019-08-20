using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScript : BaseNPC
{
    [SerializeField] private Positions[] positions;
    [SerializeField] private GameObject knight = null;
    [SerializeField] private GameObject dialogueTrigger;
    [SerializeField] private Animator anim;

    private Collider col = null;

    private bool walk = false;
    private float speed = 1.5f;
    private Vector3 initialPos;
    private Vector3 movePos;
    private Quaternion qTo = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = positions[0].inital;
        knight.transform.position = initialPos;
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        //NPC walks toward the player
        if (walk)
        {
            knight.transform.position = Vector3.MoveTowards(knight.transform.position, movePos, speed * Time.deltaTime);

            if (knight.transform.position == movePos)
            {
              walk = false;
              anim.SetTrigger("Idle");
            }
        }

    }

    void RotateTo(Vector3 init, Vector3 other)
    {
        Vector3 direction = (init - other).normalized;
        direction.z = -direction.z;
        qTo = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo , 360f);
    }

    public override Dialogue GetDialogue(string dialogue)
    {
        switch (dialogue)
        {
            case "Welcome":
                initialPos = positions[0].inital;
                movePos = positions[0].next;
                RotateTo(initialPos, movePos);
                return dialogues[0];
            case "Forest":
                initialPos = positions[1].inital;
                movePos = positions[1].next;
                RotateTo(initialPos, movePos);
                return dialogues[1];
            default:
                return dialogues[0];
        }
    }

    public override void DoAction()
    {
        gameObject.SetActive(true);
        anim.SetTrigger("Walk");
        walk = true;
    }
    public override void OnDialogueEnd()
    {
        col.enabled = false;
    }
}
