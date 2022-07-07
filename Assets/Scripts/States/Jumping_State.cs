using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;

public class Jumping_State : State
{
    private bool grounded;
    public Jumping_State(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.jump();
        grounded = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (grounded)
        {
            Debug.Log("Finished Jumping");
            stateMachine.ChangeState(player.grounded);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        grounded = player.checkGrounded(player.transform.position);
        Debug.Log(grounded);
    }
}
