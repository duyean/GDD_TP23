using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using States;

public class Jumping_State : State
{
    public bool grounded;
    public Jumping_State(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.jump();
        grounded = false;
        stateMachine.ChangeState(player.inAir);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.currentVelocity = player.rb.velocity;
    }
}
