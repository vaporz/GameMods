using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeiDouController : MonoBehaviour
{
    private Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            PlayOpenAnimation();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayCloseAniamtion();
        }

    }

    public void PlayOpenAnimation()
    {
        ani.SetBool("IsOpen", true);
    }

    public void PlayCloseAniamtion()
    {
        ani.SetBool("IsOpen", false);
    }
}
