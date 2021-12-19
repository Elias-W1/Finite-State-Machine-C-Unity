using System;
using UnityEngine;

public class SkeletonCharacter : MonoBehaviour
{
    // Character
    public GameObject target;
    public int health = 200;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject healingParticles;
    [SerializeField] private GameObject sword;
    
    // AI
    private FiniteStateMachine fsm;
    private int maxRotation = 10;
    private float closeDistance = 3f;
    private int low_treshold = 30;
    
    private void Start()
    {
        // Defining FSM
        // States
        FsmState idle = new FsmState(null, null, null);
        FsmState heal = new FsmState(CastHealingSpell, null, StopCasting);
        FsmState attack = new FsmState(DrawSword, LookAtTarget, PutAwaySword);
        
        // Transitions
        idle.When(() => health <= low_treshold, heal, null);
        heal.When(new TimeCondition(5f).IsFullfilled, idle, AddHealth);

        Func<bool> playerCloseCondition = () => Vector3.Distance(target.transform.position, transform.position) <= closeDistance;
        Func<bool> notPlayerCloseCondition = () => !playerCloseCondition();
        
        idle.When(playerCloseCondition , attack, null);

        attack.When(notPlayerCloseCondition, idle, null);

        fsm = new FiniteStateMachine(idle);
        fsm.AddStates(idle, heal, attack);
        
        // normal Start()
        animator = this.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        fsm.ExecuteActions(fsm.Tick());
    }
    
    // ---- character methods ----
    private void CastHealingSpell()
    {
        healingParticles.SetActive(true);
        animator.Play("HumanoidHealSpell");
    }

    private void StopCasting()
    {
        healingParticles.SetActive(false);
    }

    private void AddHealth()
    {
        health += 40;
    }
    
    void DrawSword()
    {
        sword.SetActive(true);
    }
    
    void PutAwaySword()
    {
        sword.SetActive(false);
    }

    // Look at target by rotation along the y-axis.
    void LookAtTarget()
    {
        Vector3 lookingVector = target.transform.position - transform.position;
        lookingVector.y = 0; // Dont actually do this in production, create a new vector instead.
        Quaternion lookAt = Quaternion.LookRotation(lookingVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, Time.deltaTime * maxRotation);
    }
}