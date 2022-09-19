using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class code : MonoBehaviour
{   Rigidbody2D rb;
    public float speed;
    public float JumpForce;
    bool grounded;
    public Transform GroundCheck;
    public float rad;
    public LayerMask ground;
    bool isRight = true;
    SpriteRenderer sprite;
    string currAnim;
    Animator anim;
    const string IDLE_ANIM = "idle";
    const string WALK_ANIM = "walk";
    const string JUMP_ANIM = "jump";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite=GetComponent<SpriteRenderer>();
        anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // rb= GetComponent<Rigidbody2D>();
        moveplayer();
        playerjump();
    }
    
    void moveplayer()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        if(isRight && Input.GetKey(KeyCode.A))
        {
            isRight = false;
            sprite.flipX = true;
        }
        else if (!isRight && Input.GetKey(KeyCode.D))
        {
            isRight= true;
            sprite.flipX = false;    
        }


        if(rb.velocity.x !=0 && rb.velocity.y ==0)
        {
            playanim(WALK_ANIM);
        }
        else if (rb.velocity.x ==0 && rb.velocity.y ==0)
        {
            playanim(IDLE_ANIM);
        }
    }

    void playerjump()
    {
        grounded = Physics2D.OverlapCircle(GroundCheck.position, rad, ground);
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            playanim(JUMP_ANIM);
        }
    }

    void playanim(string nextanim)
    {
        if (currAnim == nextanim)
            return;

        anim.Play(nextanim);
        currAnim = nextanim;
    }

}
