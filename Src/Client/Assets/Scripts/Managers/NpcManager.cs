using Common.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
	public class NpcManager : Singleton<NpcManager>
	{
		//使用委托注册事件,使用字典来管理
		public delegate bool NpcActionHandler(NpcDefine Npc);
		public Dictionary<NpcFunction, NpcActionHandler> eventMap = new Dictionary<NpcFunction, NpcActionHandler>();
		public Dictionary<int, Vector3> npcPositions = new Dictionary<int, Vector3>();
		public void RegisterNpcEvent(NpcFunction function, NpcActionHandler action)
		{
			if (!eventMap.ContainsKey(function))
			{
				eventMap[function] = action;
			}
			else
				eventMap[function] += action;
		}

		//获取NPC信息
		public NpcDefine GetNpcDefine(int NpcID)
		{
			NpcDefine npc = null;
			DataManager.Instance.Npcs.TryGetValue(NpcID , out  npc);
			return npc;
		}

		//用户交互函数
		public bool Interactive(int npcId)
		{
			if (DataManager.Instance.Npcs.ContainsKey(npcId))
			{
				var npc = DataManager.Instance.Npcs[npcId];
				return Interactive(npc);
			}
			return false;
		}

		public bool Interactive(NpcDefine npcs)
		{
			if (DoTaskInteractive(npcs))
			{
				return true;
			}
			else if (npcs.Type == NpcType.Functional)
			{
				return DoFunctionInteractive(npcs);
			}
			return false;
		}

		public bool DoFunctionInteractive(NpcDefine npcs)
		{
			if (npcs.Type != NpcType.Functional)
			{
				return false;
			}

			if (!eventMap.ContainsKey(npcs.Function))
			{
				return false;
			}
			return eventMap[npcs.Function](npcs);
		}

		//NPC的任务交互
		public bool DoTaskInteractive(NpcDefine npcs)
		{
			var status = QuestManager.Instance.GetQuestStatusByNpc(npcs.ID);
			if (status == NpcQuestStatus.None)
			{
				return false;
			}

			return QuestManager.Instance.OpenNpcQuest(npcs.ID);
		}

		//更新NPC的位置信息 留的接口以后可能会用到
		public void  UpdateNpcPosition(int npc, Vector3 pos)
        {
			this.npcPositions[npc] = pos;
        }
		//获取NPC的位置信息 留的接口以后可能会用到
		public Vector3 GetNpcPosition(int npc)
        {
			return this.npcPositions[npc];
        }
	}


}

