using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAnimation : MonoBehaviour {

	public Slider speedSlider;
	public Animator animSlider1;
	public Animator animSlider2;
	public Animator animSlider3;
	public Animator animSlider4;
	public Animator animSlider5;
	public Animator animSlider6;
	private int lastValue;

	public void OnSliderSpeedChange()
	{
		Debug.Log (speedSlider.value);
		int value = Mathf.FloorToInt (speedSlider.value);
		switch (value) {
		case 0:
			animSlider1.SetBool ("Select", true);
			PlaybackAnimation (lastValue);
			lastValue = 0;
			break;
		case 1:
			animSlider2.SetBool ("Select", true);
			PlaybackAnimation (lastValue);
			lastValue = 1;
			break;
		case 2:
			animSlider3.SetBool ("Select", true);
			PlaybackAnimation (lastValue);
			lastValue = 2;
			break;

		case 3:
			animSlider4.SetBool ("Select", true);
			PlaybackAnimation (lastValue);
			lastValue = 3;
			break;
		case 4:
			animSlider5.SetBool ("Select", true);
			PlaybackAnimation (lastValue);
			lastValue = 4;
			break;
		case 5:
			animSlider6.SetBool ("Select", true);
			PlaybackAnimation (lastValue);
			lastValue = 5;
			break;
		}
	}

	private void PlaybackAnimation(int sliderValue)
	{
		switch (sliderValue) {
		case 0:
			animSlider1.SetBool ("Select", false);
			break;
		case 1:
			animSlider2.SetBool ("Select", false);
			break;
		case 2:
			animSlider3.SetBool ("Select", false);
			break;
		case 3:
			animSlider4.SetBool ("Select", false);
			break;
		case 4:
			animSlider5.SetBool ("Select", false);
			break;
		case 5:
			animSlider6.SetBool ("Select", false);
			break;	
		}
	}


}
