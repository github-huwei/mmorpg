using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UIWindows : MonoBehaviour {


	public delegate void CloseHandle(UIWindows sender, WinowResult result);
	public event CloseHandle OnClose;

	public GameObject Root;
	public virtual System.Type Type
	{
		get 
		{
			return this.GetType();
		}
	}
	public enum WinowResult
	{
		None =0,
		Yes,
		No
	}


	public void Close(WinowResult result = WinowResult.None)
	{
		SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Win_Close);
		UIManagers.Instance.Close(this.Type);
		if (this.OnClose != null)
		{
			this.OnClose(this, result);
		}
		this.OnClose = null;
	}

	public virtual void OnCloseClick()
	{
		this.Close();
	}
	public virtual void OnYesClick()
	{
		this.Close(WinowResult.Yes);
	}

	public virtual void OnNoClick()
	{
		this.Close(WinowResult.No);
	}

	void OnMouseDown()
	{
		Debug.LogFormat(this.name + "Clicked");
	}
}
