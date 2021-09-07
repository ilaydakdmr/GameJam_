using UnityEngine;
public class SwerveInput : MonoBehaviour
{
    [SerializeField]private float range;
    [SerializeField] private float smoothness;
    private float _moveX;
    private Vector3 _pos;
    private Camera _cam;
    private void Start()
    {
        _cam = Camera.main;
    }
    private void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.InGame)
        {
            Swerve();
            Move(); 
        }
    }
    private void Move()
    {
        var x = Mathf.Clamp(_moveX,-.5f,.5f)*range; 
        transform.position = Vector3.Lerp(transform.position,new Vector3(x,transform.position.y,transform.position.z),smoothness*Time.deltaTime);
    }
    private void Swerve()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pos = _cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 get = _cam.ScreenToViewportPoint(Input.mousePosition);
            _moveX = get.x - _pos.x;
        }
    }
}
