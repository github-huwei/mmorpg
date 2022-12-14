using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Services;
using SkillBridge.Message;

public class UILogin : MonoBehaviour {
	
	
	public InputField usernameLogin;
	public InputField passwordLogin;
	public Button ButtonLogin;
	// Use this for initialization
	void Start () {
		UserService.Instance.OnLogin = this.OnLogin;
	}
	



	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClickLogin()
	{
		if(string.IsNullOrEmpty(this.usernameLogin.text))
		{
			MessageBox.Show("请输入账号");
			return;
		}
		if(string.IsNullOrEmpty(this.passwordLogin.text))
		{
			MessageBox.Show("请输入密码");
			return;
		}
		SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
		UserService.Instance.SendLogin(this.usernameLogin.text, this.passwordLogin.text);
	}

	public void OnLogin(Result result, string msg)
	{
		if (result == Result.Success)
		{
			SceneManager.Instance.LoadScene("CharSelect");
			SoundManager.Instance.PlayMusic(SoundDefine.Music_Select);
		}
		else
		{
			MessageBox.Show(msg, "错误", MessageBoxType.Error);
		}
		
	}
}
