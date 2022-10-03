using UnityEngine;

public class SlingShot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;

    public Vector3 currentPosition;

    public float maxLenth;

    private bool isClick;

    public GameObject ballPrefab;
    public GameObject aim;
    public GameObject aim1;
    public GameObject aim2;

    public float ballPositionOffset;
    Rigidbody2D ball;
    Rigidbody2D ball1;
    Rigidbody2D ball2;
    Collider2D ballCollider;
    Collider2D ballCollider1;
    Collider2D ballCollider2;

    float force = 10;
    public static SlingShot _instance;

    bool itemOn;

    void Start()
    {
        _instance = this;
        itemOn = false;
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);
        aim.SetActive(false);
        aim1.SetActive(false);
        aim2.SetActive(false);
        CreateBall();
    }

    void CreateBall()
    {
        ball = Instantiate(ballPrefab).GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<Collider2D>();
        ballCollider.enabled = false;
        ball.isKinematic = true;
        
        ResetStrips();
    }

    void Update()
    {
        if (isClick)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position 
                + Vector3.ClampMagnitude(currentPosition - center.position, maxLenth);
            Vector3 aimPosition = (currentPosition - center.position) * 2 * -1;

            aim.transform.position = aimPosition + new Vector3(0, -1, 0);
            if (itemOn)
            {
                aim1.transform.position = aimPosition + new Vector3(-1, -1, 0);
                aim2.transform.position = aimPosition + new Vector3(1, -1, 0);
            }
            SetStrips(currentPosition);
            if (ballCollider)
            {
                ballCollider.enabled = true;
            }
            if (ballCollider1)
            {
                ballCollider1.enabled = true;
            }
            if (ballCollider2)
            {
                ballCollider2.enabled = true;
            }
        }
        else
        {
            ResetStrips();
        }
    }
    private void OnMouseDown()
    {
        isClick = true;
        aim.SetActive(true);
        if (itemOn)
        {
            aim1.SetActive(true);
            aim2.SetActive(true);
        }
    }

    private void OnMouseUp()
    {
        isClick = false;
        Shoot();
        aim.SetActive(false);
        aim1.SetActive(false);
        aim2.SetActive(false);
    }

    void Shoot()
    {
        if (itemOn)
        {
            ball.isKinematic = false;
            Vector3 ballForce1 = (currentPosition - center.position) * force * -1;
            ball.velocity = ballForce1;
            ball = null;
            ballCollider = null;

            ball1.isKinematic = false;
            ball1.velocity = ballForce1 + new Vector3(-1, 0, 0);
            ballCollider1 = null;
            ball1 = null;
            ball2.isKinematic = false;
            ball2.velocity = ballForce1 + new Vector3(1, 0, 0);
            ball2 = null;
            ballCollider2 = null;
        }
        else
        {
            ball.isKinematic = false;
            Vector3 ballForce = (currentPosition - center.position) * force * -1;
            ball.velocity = ballForce;
            ball = null;
            ballCollider = null;
        }
        CreateBall();
        itemOn = false;
    }

    void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }

    void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);

        if (ball)
        {
            Vector3 dir = position - center.position;
            ball.transform.position = position + dir.normalized * ballPositionOffset;
            ball.transform.up = -dir.normalized;
        }
        if (ball1)
        {
            Vector3 dir = position - center.position;
            ball1.transform.position = position + dir.normalized * ballPositionOffset;
            ball1.transform.up = -dir.normalized;
        }

        if (ball2)
        {
            Vector3 dir = position - center.position;
            ball2.transform.position = position + dir.normalized * ballPositionOffset;
            ball2.transform.up = -dir.normalized;
        }
    }

    public void SlingshotItme()
    {
        itemOn = true;
        
        ball1 = Instantiate(ballPrefab).GetComponent<Rigidbody2D>();
        ballCollider1 = ball1.GetComponent<Collider2D>();
        ballCollider1.enabled = false;
        ball1.isKinematic = true;

        ball2 = Instantiate(ballPrefab).GetComponent<Rigidbody2D>();
        ballCollider2 = ball2.GetComponent<Collider2D>();
        ballCollider2.enabled = false;
        ball2.isKinematic = true;
        SpiderGameManager._instance.bag.SetActive(false);
    }

}
