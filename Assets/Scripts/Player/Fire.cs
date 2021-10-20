using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;
    enum State
    { 
    SkillOff,SkillOn
    }
    State state;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        state = State.SkillOff;
        spriteRenderer.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.SkillOff:
                SkillOff();
                break;
            case State.SkillOn:
                SkillOn();
                break;
        }

    }
    void SkillOff()
    {
        if (Input.GetKeyDown(KeyCode.E)&&PlayerController.Instance.skillState==PlayerController.SkillState.SkillCanUse)
        {
            animator.SetBool("SkillOn", true);
            PlayerController.Instance.skillState = PlayerController.SkillState.SkillOn;
            state = State.SkillOn;
            StartCoroutine("SkillCoroutine");
        }
    }
    void SkillOn()
    {
        if(PlayerController.Instance.skillState==PlayerController.SkillState.SkillCovering)
        {
            animator.SetBool("SkillOn", false);
            state = State.SkillOff;
            spriteRenderer.sprite = null;
        }
    }
    IEnumerator SkillCoroutine()
    {
        yield return null;
        GameObject obj = Instantiate(PlayerController.Instance.weapons[PlayerController.Instance.nowWeaponIndex], PlayerController.Instance.weapons[PlayerController.Instance.nowWeaponIndex].transform.parent);
        obj.transform.position = PlayerController.Instance.weapons[PlayerController.Instance.nowWeaponIndex].transform.position - new Vector3(0, 0.1f, 0);
        obj.GetComponent<WeaponBase>().isCloned = true;
        while(PlayerController.Instance.skillState!=PlayerController.SkillState.SkillCovering)
        {
            if(Input.GetKeyDown(KeyCode.Q)&&PlayerController.Instance.weapons.Count==PlayerController.Instance.maxWeaponAmount)
            {
                Destroy(obj);
                PlayerController.Instance.SkillCd();
                animator.SetBool("SkillOn", false);
                StopCoroutine("SkillCoroutine");
            }    
            yield return null;
        }
        Destroy(obj);
    }
}
