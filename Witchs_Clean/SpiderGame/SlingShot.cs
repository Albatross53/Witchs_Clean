using UnityEngine;

public class SlingShot : MonoBehaviour
{
    //새총 스트랩
    public LineRenderer[] lineRenderers;
    //스트랩 자리
    public Transform[] stripPositions;
    //중앙
    public Transform center;
    //기본위치
    public Transform idlePosition;

    //현재 클릭 위치
    public Vector3 currentPosition;

    //새총 길이 제한
    public float maxLenth;

    //클릭확인
    private bool isClick;

    //에임과 총알
    public GameObject ballPrefab;
    public GameObject aim;
    public GameObject aim1;
    public GameObject aim2;

    //총알 기본 자리
    public float ballPositionOffset;
    //총알 물리 Rigidbody2D, Collider2D
    Rigidbody2D ball;
    Rigidbody2D ball1;
    Rigidbody2D ball2;
    Collider2D ballCollider;
    Collider2D ballCollider1;
    Collider2D ballCollider2;

    //총알물리값
    float force = 10;
    //글로벌화
    public static SlingShot _instance;
    //아이템 사용
    bool itemOn;

    void Start()
    {
        _instance = this;
        //아이템 사용 리셋
        itemOn = false;
        //스트랩 연결
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);
        //에임비활성화
        aim.SetActive(false);
        aim1.SetActive(false);
        aim2.SetActive(false);
        //총알생성
        CreateBall();
    }

    /// <summary>
    /// 총알아이템 생성
    /// </summary>
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

    /// <summary>
    /// 총알 발사
    /// </summary>
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

    /// <summary>
    /// 총알 재장전
    /// </summary>
    void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }

    /// <summary>
    /// 스트랩 위치 조정
    /// </summary>
    /// <param name="position"> 클릭 위치</param>
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

    /// <summary>
    /// 아이템 사용시
    /// </summary>
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
