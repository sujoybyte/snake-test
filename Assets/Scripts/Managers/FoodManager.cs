using UnityEngine;

public class FoodManager : MonoBehaviour
{
	[SerializeField] private GameObject _foodPrefab;
	[SerializeField] private Head _head;

	private GameObject _food;
	private Vector3 _respawnedPosition;
	private Vector3 _newRespawnedPosition;
	private FoodType _currentFoodType;

	private int _foodTypeLength = System.Enum.GetNames(typeof(FoodType)).Length;
	private float _movementValue = 0.0f;
	private bool _isMovementHorizontal = false;
	private bool _isMovementVertical = false;

	private enum FoodType
	{
		Static,
		Moving
	}


	private void Start()
	{
		_food = Instantiate(_foodPrefab);
		_food.name = "food";

		Respawn();
	}

	private void OnEnable()
	{
		_head.OnFoodEaten += Respawn;
		_head.OnSelfCollided += DisableFood;
		_head.OnWallCollided += DisableFood;
	}
	private void OnDisable()
	{
		_head.OnFoodEaten -= Respawn;
		_head.OnSelfCollided -= DisableFood;
		_head.OnWallCollided -= DisableFood;
	}

	private void Update()
	{
		if (_currentFoodType == FoodType.Static)
		{
			// do nothing
			return;
		}
		else if ( _currentFoodType == FoodType.Moving)
		{
			// TODO: move h or v
			/*_food.transform.position += 
				new Vector3( 0.5f * Mathf.Sin(Time.time), _startPosition.y);*/

			_movementValue = Mathf.PingPong(Time.time * 5.0f, 16.0f) - 8.0f;

			if (_isMovementHorizontal)
			{
				_food.transform.position = new Vector3(
					_movementValue,
					_respawnedPosition.y,
					_respawnedPosition.z);
			}
			else if (_isMovementVertical)
			{
				_food.transform.position = new Vector3(
					_respawnedPosition.x,
					_respawnedPosition.y,
					_movementValue);
			}
		}
	}

	private void Respawn()
	{
		do _newRespawnedPosition = new Vector3(Random.Range(-8, 8), 0.5f, Random.Range(-8, 8));
		while (_newRespawnedPosition == _head.transform.position);

		_food.transform.position = _newRespawnedPosition;
		_respawnedPosition = _food.transform.position;

		_currentFoodType++;
		if ((int) _currentFoodType >= _foodTypeLength) 
			_currentFoodType = 0;

		if (_currentFoodType == FoodType.Moving)
			SetMovementDirection();
	}

	private void DisableFood()
	{
		//disable food when game over
	}

	private void SetMovementDirection()
	{
		_isMovementHorizontal = Random.value > 0.5f;
		_isMovementVertical = !_isMovementHorizontal;
	}
}
