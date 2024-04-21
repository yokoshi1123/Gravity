using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GravityManager : MonoBehaviour
{
    public float M_SPEED = 10.0f;
    public float G_SCALE = 5.0f;
    public float moveSpeed;
    public float gravityScale;

    private Vector2 startMPosition = Vector2.zero;
    private Vector2 endMPosition = Vector2.zero;
    private float CAMERAZPOSITION = -20f;
    private float GFIELDHEIGHT = 16f; // �d�͏�̏c��
    private GameObject destroyGF;

    private int gScale = 2;

    public TextMeshProUGUI gravityText;

    void Awake()
    {
        moveSpeed = M_SPEED;
        gravityScale = G_SCALE;
    }


    void Update()
    {
        GameObject gravityField = (GameObject)Resources.Load("GravityField");

        if (Input.GetMouseButtonDown(0)) // �}�E�X�̍��{�^�������������̍��W���擾
        { 
            startMPosition = Input.mousePosition;
            // �X�N���[�����W���烏�[���h���W�ɕϊ�
            startMPosition = Camera.main.ScreenToWorldPoint(new Vector3(startMPosition.x, startMPosition.y, CAMERAZPOSITION)); 
            // Debug.Log("Start:(" + startMPosition.x + ", " + startMPosition.y + ")");

        }

        if (Input.GetMouseButtonUp(0)) // �}�E�X�̍��{�^���𗣂������̍��W���擾
        {
            endMPosition = Input.mousePosition;
            // �X�N���[�����W���烏�[���h���W�ɕϊ�
            endMPosition = Camera.main.ScreenToWorldPoint(new Vector3(endMPosition.x, endMPosition.y, CAMERAZPOSITION));
            // Debug.Log("End:(" + endMPosition.x + ", " + endMPosition.y + ")");

            if (startMPosition.x > endMPosition.x) // startMPosition.x > endMPosition.x �Ȃ�l�����ւ�
            {
                // Debug.Log(startMPosition + ", " + endMPosition);
                (startMPosition, endMPosition) = (endMPosition, startMPosition);
                // Debug.Log(startMPosition + ", " + endMPosition);
            }

            // ����GravityField�̃N���[��������΍폜
            destroyGF = GameObject.FindWithTag("GravityField");
            if (destroyGF != null) 
            {
                Destroy(destroyGF);
            }
            // GravityField�̃N���[�����쐬
            GameObject gField = (GameObject)Instantiate(gravityField, (startMPosition + endMPosition) / 2, Quaternion.identity);
            // �������h���b�O�������ɕύX
            gField.transform.localScale = new Vector2(Mathf.Abs(endMPosition.x - startMPosition.x), GFIELDHEIGHT);

        }

        // �㉺�L�[��gravityScale�̕ύX
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gScale = (gScale + 399999) % 4;
            ChangeGravity();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gScale = (gScale + 400001) % 4;
            ChangeGravity();
        }
    }

    void ChangeGravity()
    {
        // Debug.Log("gscale: " + gScale);
        // jumpDirection = (gScale == 0) ? -1 : 1;

        switch (gScale)
        {
            case 0: // x(-1.0)
                gravityScale = G_SCALE * (-1.0f);
                moveSpeed = M_SPEED;
                gravityText.text = "Gravity * (-1.0)";
                break;
            case 1: // x0.5
                gravityScale = G_SCALE * 0.5f;
                moveSpeed = M_SPEED * 1.3f;
                gravityText.text = "Gravity * 0.5";
                break;
            case 2: // x1.0
                gravityScale = G_SCALE;
                moveSpeed = M_SPEED;
                gravityText.text = "Gravity * 1.0";
                break;
            case 3: // x2.0
                gravityScale = G_SCALE * 2.0f;
                moveSpeed = M_SPEED * 0.7f;
                gravityText.text = "Gravity * 2.0";
                break;
            default:
                Debug.Log("ChangeGravity�ŃG���[");
                break;
        }


    }
}
