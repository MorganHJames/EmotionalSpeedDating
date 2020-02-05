////////////////////////////////////////////////////////////
// File: GameHandler.cs
// Author: Morgan Henry James
// Date Created: 11-01-2020
// Brief: Handles most aspect of the game.
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles most aspect of the game.
/// </summary>
public class GameHandler : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The high score value text.
	/// </summary>
	[Tooltip("The high score value text.")]
	[SerializeField] private TextMeshProUGUI highScoreValue;

	/// <summary>
	/// The score value text.
	/// </summary>
	[Tooltip("The score value text.")]
	[SerializeField] private TextMeshProUGUI scoreValue;

	/// <summary>
	/// The get emotion data script that is getting the emotion data within the scene.
	/// </summary>
	[Tooltip("The get emotion data script that is getting the emotion data within the scene.")]
	[SerializeField] private GetEmotionData getEmotionData;

	/// <summary>
	/// The positions for the intro phone to go to.
	/// </summary>
	[Tooltip("The positions for the intro phone to go to.")]
	[SerializeField] private Transform phonePositions;

	/// <summary>
	/// The positions for the dates to go to.
	/// </summary>
	[Tooltip("The positions for the dates to go to.")]
	[SerializeField] private Transform datePositions;

	/// <summary>
	/// The date object.
	/// </summary>
	[Tooltip("The date object.")]
	[SerializeField] private GameObject dateObject;

	/// <summary>
	/// "The tumble weed animator.
	/// </summary>
	[Tooltip("The tumble weed animator.")]
	[SerializeField] private Animator tumbleWeedAnimator;

	/// <summary>
	/// The table object.
	/// </summary>
	[Tooltip("The table object.")]
	[SerializeField] private GameObject tableObject;

	/// <summary>
	/// The phone object.
	/// </summary>
	[Tooltip("The phone object.")]
	[SerializeField] private GameObject phoneObject;

	/// <summary>
	/// How much % the user has to have of the configuration emotion data saved to be passed as depicting said emotion.
	/// </summary>
	private float userAccuracyNeeded = 95.0f;

	/// <summary>
	/// The dates remaining each date is represented by an integer that indicates there child index.
	/// </summary>
	private List<int> datesRemaining = new List<int>();

	/// <summary>
	/// The animator for on the table object.
	/// </summary>
	private Animator tableAnimator;

	/// <summary>
	/// The animator for the positive reaction.
	/// </summary>
	private Animator positiveAnimator;

	/// <summary>
	/// The animator for the negative reaction.
	/// </summary>
	private Animator negativeAnimator;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Set the references for the animators.
	/// Adds the dates to the list of dates remaining.
	/// Starts the introduction.
	/// </summary>
	private void Start()
	{
		highScoreValue.text = PlayerPrefs.GetInt("HighScore").ToString();
		positiveAnimator = dateObject.transform.GetChild(7).GetComponent<Animator>();
		negativeAnimator = dateObject.transform.GetChild(8).GetComponent<Animator>();
		tableAnimator = tableObject.GetComponent<Animator>();
		tableObject.transform.GetChild(6).GetComponent<ButtonWithPositionChange>().onClick.AddListener(() => StartCoroutine(LoadScene("GameScene")));
		tableObject.transform.GetChild(7).GetComponent<ButtonWithPositionChange>().onClick.AddListener(() => StartCoroutine(LoadScene("MenuScene")));

		for (int i = 0; i < 7; i++)
		{
			datesRemaining.Add(i);
		}

		StartCoroutine(Introduction());
	}

	/// <summary>
	/// Loads a scene after playing an animation.
	/// </summary>
	/// <param name="sceneToLoad">The scene to load.</param>
	/// <returns></returns>
	private IEnumerator LoadScene(string sceneToLoad)
	{
		getEmotionData.CloseProcess();

		// Turn off the buttons
		tableObject.transform.GetChild(6).GetComponent<ButtonWithPositionChange>().enabled = false;
		tableObject.transform.GetChild(7).GetComponent<ButtonWithPositionChange>().enabled = false;

		if (sceneToLoad == "MenuScene")
		{
			tableAnimator.Play("MakeTable");
		}
		else
		{
			tableAnimator.Play("MakeTableNoBlur");
		}

		// Move the date to the middle of the screen.
		dateObject.transform.position = datePositions.GetChild(1).position;

		// Move the date and tumble weed to the right of the screen.
		bool dateAtEnd = false;

		tumbleWeedAnimator.SetBool("Exit", true);

		while (!dateAtEnd)
		{
			dateObject.transform.position = Vector2.MoveTowards(dateObject.transform.position, datePositions.GetChild(3).position, (50f));

			if (Vector2.Distance(dateObject.transform.position, datePositions.GetChild(3).position) == 0)
			{
				dateAtEnd = true;
			}

			yield return null;
		}

		// Wait for the animation to finish.
		yield return new WaitForSeconds(0.25f);

		// Stops the background noise.
		AudioManager.instance.backgroundNoiseSource.mute = true;

		// Loads the scene.
		SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
	}

	/// <summary>
	/// Makes a phone object move down to the middle of the screen and wait a while
	/// allowing the player to read how to play and for the camera of the emotion
	/// detection script to turn on.
	/// </summary>
	/// <returns></returns>
	private IEnumerator Introduction()
	{
		// Starts the background noise.
		AudioManager.instance.backgroundNoiseSource.mute = false;

		bool phoneInMiddle = false;

		while (!phoneInMiddle)
		{
			phoneObject.transform.position = Vector3.MoveTowards(phoneObject.transform.position, phonePositions.GetChild(0).position, 10f);

			if (Vector3.Distance(phoneObject.transform.position, phonePositions.GetChild(0).position) == 0)
			{
				phoneInMiddle = true;
			}
			yield return null;
		}

		yield return new WaitForSeconds(7.5f);

		bool phoneAtBottom = false;

		while (!phoneAtBottom)
		{
			phoneObject.transform.position = Vector3.MoveTowards(phoneObject.transform.position, phonePositions.GetChild(1).position, 10f);

			if (Vector3.Distance(phoneObject.transform.position, phonePositions.GetChild(1).position) == 0)
			{
				phoneAtBottom = true;
			}
			yield return null;
		}

		StartCoroutine(GamePlay());
	}

	/// <summary>
	/// Starts the game and runs through all the rounds of speed dating
	/// until the user has no matches or a single match.
	/// </summary>
	/// <returns></returns>
	private IEnumerator GamePlay()
	{
		float gameSpeed = 1f;
		int score = 0;

		// Play the next round noise.
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Bell);

		while (datesRemaining.Count > 1)
		{
			// List of dates to remove.
			List<int> datesToRemove = new List<int>();

			for (int i = 0; i < datesRemaining.Count; i++)
			{
				// Enable the date.
				dateObject.transform.GetChild(datesRemaining[i]).gameObject.SetActive(true);

				// Move the date to the left of the screen.
				dateObject.transform.position = datePositions.GetChild(0).position;

				// Move the date to the middle of the screen.
				bool dateInMiddle = false;

				while (!dateInMiddle)
				{
					dateObject.transform.position = Vector2.MoveTowards(dateObject.transform.position, datePositions.GetChild(1).position, (10f * gameSpeed));

					if (Vector2.Distance(dateObject.transform.position, datePositions.GetChild(1).position) == 0)
					{
						dateInMiddle = true;
					}

					yield return null;
				}

				bool hasImpressedDate = false;

				for (int k = 0; k < ((1f / gameSpeed) * 5f) / 0.05f; k++)
				{
					// Wait in the middle of the screen.
					yield return new WaitForSeconds(0.05f);

					// Check if the date is impressed.
					if (getEmotionData.currentEmotionProfile.isCurrent)
					{
						switch (datesRemaining[i])
						{
							case 0:
								if (getEmotionData.currentEmotionProfile.neutral >= (PlayerPrefs.GetFloat("Neutral") / 100f) * userAccuracyNeeded)
								{
									hasImpressedDate = true;
								}
							break;
							case 1:
								if (getEmotionData.currentEmotionProfile.surprised >= (PlayerPrefs.GetFloat("Surprised") / 100f) * userAccuracyNeeded)
								{
									hasImpressedDate = true;
								}
								break;
							case 2:
								if (getEmotionData.currentEmotionProfile.sad >= (PlayerPrefs.GetFloat("Sad") / 100f) * userAccuracyNeeded)
								{
									hasImpressedDate = true;
								}
								break;
							case 3:
								if (getEmotionData.currentEmotionProfile.happy >= (PlayerPrefs.GetFloat("Happy") / 100f) * userAccuracyNeeded)
								{
									hasImpressedDate = true;
								}
								break;
							case 4:
								if (getEmotionData.currentEmotionProfile.scared >= (PlayerPrefs.GetFloat("Scared") / 100f) * userAccuracyNeeded)
								{
									hasImpressedDate = true;
								}
								break;
							case 5:
								if (getEmotionData.currentEmotionProfile.disgust >= (PlayerPrefs.GetFloat("Disgust") / 100f) * userAccuracyNeeded)
								{
									hasImpressedDate = true;
								}
								break;
							case 6:
								if (getEmotionData.currentEmotionProfile.angry >= (PlayerPrefs.GetFloat("Angry") / 100f) * userAccuracyNeeded)
								{
									hasImpressedDate = true;
								}
								break;
							default:
								break;
						}
					}
				}

				if (hasImpressedDate)
				{
					// Play the correct reaction noise.
					AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.CorrectReaction);

					positiveAnimator.Play("PositiveReaction");
					score++;
					scoreValue.text = score.ToString();

					if (PlayerPrefs.GetInt("HighScore") < score)
					{
						PlayerPrefs.SetInt("HighScore", score);
						highScoreValue.text = score.ToString();
					}
				}
				else
				{
					// Play the incorrect reaction noise.
					AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.IncorrectReaction);
					negativeAnimator.Play("NegativeReaction");
				}

				// Move the date to the right of the screen.
				bool dateAtEnd = false;

				while (!dateAtEnd)
				{
					dateObject.transform.position = Vector2.MoveTowards(dateObject.transform.position, datePositions.GetChild(2).position, (10f * gameSpeed));

					if (Vector2.Distance(dateObject.transform.position, datePositions.GetChild(2).position) == 0)
					{
						dateAtEnd = true;
					}

					yield return null;
				}

				// Disable the date.
				dateObject.transform.GetChild(datesRemaining[i]).gameObject.SetActive(false);

				// Add to list of dates to remove if failed.
				if (!hasImpressedDate)
				{
					datesToRemove.Add(datesRemaining[i]);
				}
			}

			foreach (int dateToRemove in datesToRemove)
			{
				for (int i = datesRemaining.Count; i-- > 0;)
				{
					if (dateToRemove == datesRemaining[i])
					{
						datesRemaining.Remove(dateToRemove);
					}
				}
			}

			if (datesRemaining.Count > 1)
			{
				// Play the next round noise.
				AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Bell);

				// Increase the speed of the game.
				gameSpeed += 0.25f;

				// Increase the game difficulty.
				userAccuracyNeeded += 0.5f;
			}
		}

		// Matched with a date.
		if (datesRemaining.Count == 1)
		{
			// Play the incorrect reaction noise.
			AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Match);
			// Enable the date.
			dateObject.transform.GetChild(datesRemaining[0]).gameObject.SetActive(true);

			// Enable the love heart particles.
			dateObject.transform.GetChild(9).gameObject.SetActive(true);

			// Enable the "you have found love" text.
			tableObject.transform.GetChild(4).gameObject.SetActive(true);

			// Clear the table revealing the text and buttons.
			tableAnimator.Play("ClearTable");

			// Move the date to the left of the screen.
			dateObject.transform.position = datePositions.GetChild(0).position;

			// Move the date to the middle of the screen.
			bool dateInMiddle = false;

			while (!dateInMiddle)
			{
				dateObject.transform.position = Vector2.MoveTowards(dateObject.transform.position, datePositions.GetChild(1).position, (10f));

				if (Vector2.Distance(dateObject.transform.position, datePositions.GetChild(1).position) == 0)
				{
					dateInMiddle = true;
				}

				yield return null;
			}
		}
		// No match :(.
		else
		{
			// Make the tumble weed move across the screen on loop.
			tumbleWeedAnimator.Play("TumbleWeed");

			// Play the incorrect reaction noise.
			AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.NoMatch);

			// Enable the "you have not found love" text.
			tableObject.transform.GetChild(5).gameObject.SetActive(true);

			// Clear the table revealing the text and buttons.
			tableAnimator.Play("ClearTable");
		}
	}
	#endregion
	#region Public

	#endregion
	#endregion
}