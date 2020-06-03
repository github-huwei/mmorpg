﻿using Models;
using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : MonoSingleton<UIMain> {


	public Text AvatarName;
	public Text Avatarlevel;
	// Use this for initialization
	protected void  OnStart () {
		this.UpdateAvatar();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateAvatar()
	{
		AvatarName.text = string.Format("{0}[{1}]", User.Instance.CurrentCharacter.Name, User.Instance.CurrentCharacter.Id);
		Avatarlevel.text = User.Instance.CurrentCharacter.Level.ToString();
	}

	public void BackToCharSelect()
	{
		SceneManager.Instance.LoadScene("CharSelect");
		UserService.Instance.sendGameLeave();
	}

	public void OnClickTest()
	{
		UITest test = UIManagers.Instance.Show<UITest>();

		test.setTitle("这是一个测试UI");

		test.OnClose += Test_OnClose;
	}

	private void Test_OnClose(UIWindows sender, UIWindows.WinowResult result)
	{
		MessageBox.Show("点击了对话框的: " + result, "对话框响应结果", MessageBoxType.Information);
	}
}