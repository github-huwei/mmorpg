using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Services;
using SkillBridge.Message;
using ProtoBuf;
using Managers;

public class LoadingManager : MonoBehaviour
{

    public GameObject UITips;
    public GameObject UILoading;
    public GameObject UILogin;

    public Slider progressBar;
    public Text progressText;
    public Text progressNumber;

    // Use this for initialization
    IEnumerator Start()
    {
        //初始化日志库
        log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo("log4net.xml"));
        UnityLogger.Init();
        Common.Log.Init("Unity");
        Common.Log.Info("LoadingManager start");

        //初始化loading界面
        UITips.SetActive(true);
        UILoading.SetActive(false);
        UILogin.SetActive(false);
        yield return new WaitForSeconds(2f);
        UILoading.SetActive(true);
        yield return new WaitForSeconds(1f);
        UITips.SetActive(false);

        //加载配置表文件
        yield return DataManager.Instance.LoadData();

        //Init basic services
        MapService.Instance.Init();
        UserService.Instance.Init();
        FriendService.Instance.Init();
        StatusService.Instance.Init();
        TeamService.Instance.Init();
        GuildServcie.Instance.Init();
        ChatService.Instance.Init();
        ShopManager.Instance.Init();
        SoundManager.Instance.PlayMusic(SoundDefine.Music_Login);

        // Fake Loading Simulate
        for (float i = 50; i < 100;)
        {
            i += Random.Range(0.1f, 1.5f);
            progressBar.value = i;
            progressNumber.text = Mathf.Ceil(progressBar.value) + "%";

            yield return new WaitForEndOfFrame();
        }

        UILoading.SetActive(false);
        UILogin.SetActive(true);
        yield return null;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
