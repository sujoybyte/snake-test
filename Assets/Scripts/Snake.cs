using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
	[SerializeField] private GameObject _head;
	[SerializeField] private GameObject _body;

	private List<Transform> _fullBody = new List<Transform>();
	private GameObject _bodyCopy;
	private Vector3 _lastBodyPosition;

	public GameObject Head { get { return _head; } }
	public List<Transform> FullBody { get { return _fullBody; } }
	public Vector3 LastBodyPosition { get { return _lastBodyPosition; } set { _lastBodyPosition = value; } }


	private void Awake()
	{
		_fullBody.Add(_body.transform);
	}

	private void OnEnable()
	{
		_head.GetComponent<Head>().OnFoodEaten += GenerateBody;
	}

	private void OnDisable()
	{
		_head.GetComponent<Head>().OnFoodEaten -= GenerateBody;
	}

	private void GenerateBody()
	{
		_bodyCopy = Instantiate(_body, _lastBodyPosition, Quaternion.identity);
		_bodyCopy.transform.parent = transform;
		_bodyCopy.name = "Body";
		_fullBody.Add(_bodyCopy.transform);
	}
}
