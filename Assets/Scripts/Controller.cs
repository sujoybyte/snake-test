using UnityEngine;

[RequireComponent(typeof(Snake))]
public class Controller : MonoBehaviour
{
    private Snake _snake;
    private GameObject _snakeHead;
    private bool _isControllable = true;

    public bool IsControllable { get { return _isControllable; } set { _isControllable = value; } }


    private void Awake()
	{
		_snake = GetComponent<Snake>();
        _snakeHead = _snake.Head;
	}

	private void Update()
    {
        if (!_isControllable) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveSnake(Vector3.left);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveSnake(Vector3.right);
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            MoveSnake(Vector3.forward);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            MoveSnake(Vector3.back);
    }

    private void MoveSnake(Vector3 direction)
    {
        MoveBody(MoveHead(direction));
    }

    private Vector3 MoveHead(Vector3 direction)
	{
        Vector3 oldHeadPosition = _snakeHead.transform.position;
        _snakeHead.transform.position += direction;

        return oldHeadPosition;
    }
    private void MoveBody(Vector3 position)
	{
        var fullBody = _snake.FullBody;
        var fullBodyCount = fullBody.Count;

        for (int i = 0; i < fullBodyCount; i++)
        {
            (position, fullBody[i].position) = (fullBody[i].position, position);
        }

        _snake.LastBodyPosition = position;
    }
}
