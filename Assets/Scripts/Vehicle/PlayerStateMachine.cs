using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    //���� �÷��̾��� ���¸� ��Ÿ���� ����
    public PlayerState currentState;
    public PlayerController playerControler;

    private void Awake()
    {
        playerControler = GetComponent<PlayerController>();
    }
    void Start()
    {
        TransittionTioState(new IdleState(this));
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    public void TransittionTioState(PlayerState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
        Debug.Log($"Transitioned to State {newState.GetType().Name}");
    }
}
