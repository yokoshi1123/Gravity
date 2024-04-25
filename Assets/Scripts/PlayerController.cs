using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    public GravityManager gravityManager; // EnvironmentManager��p�~

    [SerializeField] private float moveSpeed;
    private float jumpForce = 20.0f;

    [SerializeField] private Transform grabPoint;

    private bool isWalking = false;
    private bool isJumping = false;
    private bool isGrabbing = false;
    private bool isReverse = false;
    private int jumpDirection = 1;

    private float rayDistance = 0.2f;
    private GameObject grabObj;
    private float objWeight = 0.0f;
    private RaycastHit2D hit;

    void Awake()
    {
        moveSpeed = gravityManager.M_SPEED;
        rb.gravityScale = gravityManager.G_SCALE;
        isReverse = gravityManager.isReverse;       
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        // Debug.Log(horizontal);
        isWalking = horizontal != 0;

        Vector3 scale = gameObject.transform.localScale;
        if (isWalking && !isGrabbing)
        {
            if (horizontal < 0 && scale.x > 0 || horizontal > 0 && scale.x < 0)
            {
                scale.x *= -1;
            }
            gameObject.transform.localScale = scale;
        }

        if (!isReverse && scale.y == -1)
        {
            // Debug.Log(scale.y);
            scale.y = 1;
            gameObject.transform.localScale = scale;
        }

        //�W�����v
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !isGrabbing && !(rb.velocity.y < -0.5f))
        {
            Jump();
        }
        
        //�v���C���[�̈ړ�
        rb.velocity = new Vector2(horizontal * Mathf.Max(1.0f, moveSpeed - objWeight), rb.velocity.y);
        // Debug.Log(rb.velocity);

        if (Input.GetKeyDown(KeyCode.G))
        {
            Grab();
        }       

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isJumping", isJumping);
    }



    private void Jump()
    {
        isJumping = true;
        // Debug.Log("Jumping");
        jumpDirection = (rb.gravityScale == gravityManager.G_SCALE * (-1.0f)) ? -1 : 1; 
        rb.AddForce(Vector2.up * jumpForce * jumpDirection, ForceMode2D.Impulse);
    }

    private void Grab()
    {
        if (grabObj == null)
        {
            hit = Physics2D.Raycast(grabPoint.position, transform.right, rayDistance);
            if (hit.collider != null && hit.collider.tag == "Movable")
            {
                grabObj = hit.collider.gameObject;
                grabObj.GetComponent<Rigidbody2D>().isKinematic = true;
                grabObj.transform.SetParent(transform);
                objWeight = grabObj.GetComponent<Rigidbody2D>().mass / 10;
                Debug.Log("Mass:" + objWeight);
                isGrabbing = true;
            }
        }
        else
        {
            grabObj.GetComponent<Rigidbody2D>().isKinematic = false;
            grabObj.transform.SetParent(null);
            objWeight = 0.0f;
            grabObj = null;
            isGrabbing = false;
        }

        animator.SetBool("isGrabbing", isGrabbing);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stage"))
        {
            isJumping = false;
            // Debug.Log("On the ground");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("GravityField")) // �d�͏ꒆ�ɂ���Ƃ��AgravityManager�ł̕ύX��ǂݍ���
        {
            moveSpeed = gravityManager.moveSpeed;
            rb.gravityScale = gravityManager.gravityScale;
            isReverse = gravityManager.isReverse;
            Vector3 scale = gameObject.transform.localScale;
            if (isReverse && scale.y == 1)
            {
                scale.y = -1;
                gameObject.transform.localScale = scale;
            }
            // Debug.Log("In the gravity field: " + rb.gravityScale);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("GravityField")) // �d�͏ꂩ��o���Ƃ��A�f�t�H���g�ɖ߂�
        {
            // inField = false;
            //Debug.Log("���蔲���I����");
            moveSpeed = gravityManager.M_SPEED;
            rb.gravityScale = gravityManager.G_SCALE;
            isReverse = false;
        }

        if (collision.CompareTag("Stage"))//�󒆂ɂ���Ƃ���isJumping��true
        {
            isJumping = true;
            // Debug.Log("In the air");
        }
    }
}
