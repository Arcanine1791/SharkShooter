using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class ActionsHelper : MonoBehaviour
{
    [SerializeField] private InputAction action;

    public CinemachineVirtualCamera vm;
    public CinemachineFreeLook flc;

    public Animator anim;
    public bool oncamchange;
    private void OnEnable()
    {
        action.Enable();
    }
    private void OnDisable()
    {
        action.Disable();
    }

    private void Start()
    {
       // action.performed += ctx => SwitchState();
        action.performed += ctx => SwitchPriority();
    }

    public void SwitchState()
    {
        if (oncamchange)
        { anim.Play("fl"); }
        else
            anim.Play("vc");

        oncamchange = !oncamchange;
    }
    public void SwitchPriority()
    {
        if (oncamchange)
        {
            vm.Priority = 1;
            flc.Priority = 0;
        }
        else
        {
            vm.Priority = 0;
            flc.Priority = 1;
        }

        oncamchange = !oncamchange;
    }
}
