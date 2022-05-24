using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Head _head;
	[SerializeField] private Controller _controller;
	[SerializeField] private TextMeshProUGUI _scoreText;
	[SerializeField] private GameObject _mainCanvas;
	[SerializeField] private GameObject _menuScreen;
	[SerializeField] private GameObject _gameScreen;
	[SerializeField] private GameObject _endScreen;

	private int _score = 0;
	private int _screenCount = 0;
	private List<GameObject> _allScreens = new List<GameObject>();


	private void Awake()
	{
		ChangeScreenTo(_menuScreen);

		foreach (Transform screen in _mainCanvas.transform)
			_allScreens.Add(screen.gameObject);

		_screenCount = _allScreens.Count;
	}

	private void OnEnable()
	{
		_head.OnFoodEaten += Score;
		_head.OnSelfCollided += EndGame;
		_head.OnWallCollided += EndGame;
	}
	private void OnDisable()
	{
		_head.OnFoodEaten -= Score;
		_head.OnSelfCollided -= EndGame;
		_head.OnWallCollided -= EndGame;
	}

	private void Score()
	{
		_scoreText.text = "Score :  " + (++_score).ToString();
	}

	private void RestartGame()
	{
		//screen change
		//timescale reset
		//enable controller
		//enable food and respawn
	}
	private void EndGame()
	{
		ChangeScreenTo(_endScreen);
		
		_controller.IsControllable = false;
		Time.timeScale = 0;
	}

	private void ChangeScreenTo(GameObject screen)
	{
		for (int i = 0; i < _screenCount; i++)
		{
			if (_allScreens[i] != screen)
				_allScreens[i].SetActive(false);
			else
				_allScreens[i].SetActive(true);
		}
	}
}
