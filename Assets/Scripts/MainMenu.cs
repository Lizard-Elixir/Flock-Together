using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityAtoms.BaseAtoms;

public enum MainMenuPage
{
	Main,
	Options,
	Exposition,
	Instructions,
	Loading,
}

public class MainMenu : MonoBehaviour
{
	[SerializeField] private MainMenuPageVariable Page;
	public GameObject Options;
	public GameObject Instructions;
	public GameObject IntroExposition;
	public GameObject LoadingScreen;

	public void Start()
	{
		ShowMainMenu();
	}

	public void ShowOptions()
	{
		Page.SetValue(MainMenuPage.Options);
	}

	public void ShowInstructions()
	{
		Page.SetValue(MainMenuPage.Instructions);
	}

	public void ShowIntroExposition()
	{
		Page.SetValue(MainMenuPage.Exposition);
	}

	public void PlayGame()
	{
		Page.SetValue(MainMenuPage.Loading);
		SceneManager.LoadScene("Main");
	}

	public void QuitGame()
	{
		Debug.Log("QUIT!");
		Application.Quit();
	}

	public void ShowMainMenu()
	{
		Page.SetValue(MainMenuPage.Main);
	}

	public void SetMenuPage(MainMenuPage page)
	{
		Options.SetActive(false);
		IntroExposition.SetActive(false);
		LoadingScreen.SetActive(false);
		Instructions.SetActive(false);

		switch (page)
		{
			case MainMenuPage.Main:
				break;
			case MainMenuPage.Options:
				Options.SetActive(true);
				break;
			case MainMenuPage.Instructions:
				Instructions.SetActive(true);
				break;
			case MainMenuPage.Exposition:
				IntroExposition.SetActive(true);
				break;
			case MainMenuPage.Loading:
				LoadingScreen.SetActive(true);
				break;
		}
	}
}
