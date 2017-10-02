using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CanvasScript : MonoBehaviour
{
	public Camera playerCamera;
	public GameObject interactableIcon;
	public Text saidField;
	public Button[] answerButtons = new Button[4];
	public Image EmotionField;

	public GameObject dialogPannel;

	private bool isActive = false;
	private Transform interactableTransform;

	public Sprite Angry;
	public Sprite Sad;
	public Sprite Happy;
	public Sprite Scared;

	public Sprite Fight;
	public Sprite Flight;
	public Sprite Confront;

	void Start()
	{
		interactableTransform = interactableIcon.transform;
        playerCamera = Camera.main;
    }

	void LateUpdate()
	{
		interactableIcon.SetActive(isActive);
		isActive = false;
	}

	public void SetInteractable(Vector3 worldPos)
	{
		isActive = true;
		Vector3 NewPos = playerCamera.WorldToScreenPoint(worldPos);
	    interactableTransform.position = NewPos;
	}

	public void SetDialogBox(string text)
	{
		saidField.text = text;
		dialogPannel.SetActive(true);

	}

	public void DisableAllButtons()
	{
		foreach (Button b in answerButtons)
		{
			b.gameObject.SetActive(false);
		}
	}

	public void EnableAllButtons()
	{
		foreach (Button b in answerButtons)
		{
			b.gameObject.SetActive(true);
		}
	}

	public void RemoveDialog()
	{
		if (dialogPannel.activeSelf)
		{
            dialogPannel.SetActive(false);
			DisableAllButtons();
		}
	}

	public void SetEmotionImage(NPCController.Emotion emotion)
	{
		if (emotion == NPCController.Emotion.Angry) EmotionField.sprite = Angry;
		if (emotion == NPCController.Emotion.Sad) EmotionField.sprite = Sad;
		if (emotion == NPCController.Emotion.Happy) EmotionField.sprite = Happy;
		if (emotion == NPCController.Emotion.Scared) EmotionField.sprite = Scared;
	}

	public void SetSolutionType(NPCController.SolutionTypes solution)
	{
		if (solution == NPCController.SolutionTypes.Fight) EmotionField.sprite = Fight;
		if (solution == NPCController.SolutionTypes.Flight || solution == NPCController.SolutionTypes.Ignore) EmotionField.sprite = Flight;
		if (solution == NPCController.SolutionTypes.Confrontation) EmotionField.sprite = Confront;
	}
}
