using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombie : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int HP = 100;
    public Animator animator;
    private NavMeshAgent navAgent;

    public bool isDead;
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if(HP <= 0)
        {
            int randomValue = Random.Range(0, 2);

            if (randomValue == 0)
                animator.SetTrigger("DIE1");
            else
                animator.SetTrigger("DIE2");
            
        }

        else
        {
            animator.SetTrigger("DAMAGE");
        }

        isDead = true;

    }

    private void Update()
    {
       
    }

}
