using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationController : MonoBehaviour
{
    public Animator animator;
    public PlayerAttack atckScript;
    public PlayerMovement moveScript;
    public GameObject HittingArm;
    public GameObject AttackHit;

    // Start is called before the first frame update
    void Start()
    {
        AttackHit.transform.parent = HittingArm.transform;
        AttackHit.transform.position = HittingArm.transform.position;
    }

  
    private void Play()
    {
        if (atckScript.enableCollAnim && moveScript.direction.x < 0)
        {
            animator.SetTrigger("AnimationStart");
        } else if(atckScript.enableCollAnim && moveScript.direction.x > 0)
        {
            animator.SetTrigger("AnimationStartRight");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Play();    
    }
}
