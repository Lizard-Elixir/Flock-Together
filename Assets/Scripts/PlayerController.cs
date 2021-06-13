using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float SquawkRadius = 12.5f;
	public float SquawkCooldown = 2f;
	public bool Talking = false;

	private GameObject SquawkGfx;
	private float LastSquawkTime;
	[SerializeField] private VarObject recruitedBeopleVar;
	private PauseControl pauseControl;

	// Start is called before the first frame update
	void Start()
	{
		SquawkGfx = GameObject.Find("Squawk");
		SetSquawkRadius(SquawkRadius);
		SquawkGfx.SetActive(false);
		pauseControl = FindObjectOfType<PauseControl>();
	}

	void SetSquawkRadius(float radius)
	{
		SquawkGfx.transform.localScale = new Vector3(SquawkRadius * 2, SquawkGfx.transform.localScale.y, SquawkRadius * 2);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && !Talking && !pauseControl.gameIsPaused)
		{
			Squawk();
		}

		if (Time.time - LastSquawkTime > SquawkCooldown)
		{
			SquawkGfx.SetActive(false);
		}
	}

	/**
	 * Checks within a radius for recruitable NPCs.
	 * Then, calls a `.Recruit()` method on each of them and adds to them to the beeple recruited count. 
	 */
	void Squawk()
	{
		if (Time.time - LastSquawkTime < SquawkCooldown)
		{
			return;
		}

		SquawkGfx.SetActive(true);
		SquawkGfx.GetComponent<AudioSource>().Play();

		GameObject[] beople = GameObject.FindGameObjectsWithTag("Berson");
		GameObject[] leaders = GameObject.FindGameObjectsWithTag("BersonLeader");

		foreach (GameObject berson in beople)
		{
			float distance = Vector3.Distance(transform.position, berson.transform.position);
			if (distance <= SquawkRadius)
			{
				berson.GetComponent<BirdController>().Recruit();
			}
		}

		foreach (GameObject leader in leaders)
		{
			float distance = Vector3.Distance(transform.position, leader.transform.position);
			BirdLeaderController leaderController = leader.GetComponent<BirdLeaderController>();
			if (distance <= SquawkRadius && !leaderController.IsRecruited)
			{
				Input.ResetInputAxes();
				leaderController.TriggerDialogue();
				HandleRecruitment(leader);
				return;
			}
		}
		

		LastSquawkTime = Time.time;
	}

	void HandleRecruitment(GameObject berson)
	{
		switch (berson.name)
		{
			case "ChickenLeader":
				recruitedBeopleVar.currentNum += 10;
				break;
			case "SparrowLeader":
				ThirdPersonMovement movementScript = GetComponentInParent<ThirdPersonMovement>();
				movementScript.speed *= 2f;
				break;
			case "MagpieLeader":
				SetSquawkRadius(SquawkRadius *= 1.5f);
				break;
		}
	}

	public void SetTalking(bool isTalkingNow)
	{
		Talking = isTalkingNow;
	}
}
