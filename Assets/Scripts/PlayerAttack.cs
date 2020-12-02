using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform AttackHit;
    public LayerMask Enemy;
    public PlayerMovement moveScript;
    public DummyEnemy enemyScript;
    public float atckRange = 0.5f;
    public bool enableCollAnim = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void NormalAttackPressed()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Attack();
            enableCollAnim = true;
            
        } else
        {
            enableCollAnim = false;
        }
        

    }

    private void Attack()
    {

        animator.SetTrigger("AttackPressed");

        
        //RaycastHit2D[] raycastHits = Physics2D.CircleCastAll(AttackHit.position, atckRange, moveScript.direction, 0, Enemy);
       // foreach (RaycastHit2D hit in raycastHits)
       // {
       //     Debug.Log("YES");
       // }
        

    }

    public void AttackHappens()
    {
        Debug.Log("Triggered");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackHit.position, atckRange, Enemy);

        foreach (Collider2D enemy in hitEnemies)
        {
           
            enemyScript.hp -= 20;
            Debug.Log(enemyScript.hp);
            if (enemyScript.hp <= 0)
            {
                enemyScript.Die();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(AttackHit == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(AttackHit.position, atckRange);
    }

    // Update is called once per frame
    void Update()
    {
        NormalAttackPressed();
        
    }
}
