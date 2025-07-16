using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : StateMachineBehaviour
{
    float timer;
    public float idleTime = 0f;

    Transform Player;

    public float DetectionAreaRadius = 18f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        Player = GameObject.FindGameObjectWithTag("Player").transform;


    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > idleTime)
        {
            animator.SetBool("isPatroling",true);
        }
       // SoundManager.instance.zombieChannel.Play();
        float distanceFromPlayer = Vector3.Distance(Player.position,animator.transform.position);

        if(distanceFromPlayer < DetectionAreaRadius)
        {
            animator.SetBool("isChasing", true);
        }

    }

}
