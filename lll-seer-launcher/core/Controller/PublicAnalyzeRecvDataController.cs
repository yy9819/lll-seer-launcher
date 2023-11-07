using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using lll_seer_launcher.core.Dto;

namespace lll_seer_launcher.core.Controller
{
    public class PublicAnalyzeRecvDataController
    {
        private Dictionary<int,Dictionary<long,PublicRecvListener>> publicRecvListenerDic = new Dictionary<int, Dictionary<long, PublicRecvListener>>();
        public delegate void PublicRecvListener(HeadInfo recvDataHeadInfo);
        public void RunAnalyzeRecvDataMethod(HeadInfo recvDataHeadInfo)
        {
            
            lock (GlobalVariable.lockObjs["publicRecvListener"])
            {
                if (publicRecvListenerDic.TryGetValue(recvDataHeadInfo.cmdId, out Dictionary<long, PublicRecvListener> publicRecvListeners))
                {
                    foreach(PublicRecvListener listener in publicRecvListeners.Values)
                    {
                        new Thread(() => { listener(recvDataHeadInfo); }).Start();
                    }
                }
            }
        }
        public void SetRecvEventListener(int cmdId, PublicRecvListener publicRecvListener ,long key)
        {
            lock (GlobalVariable.lockObjs["publicRecvListener"])
            {
                if (!this.publicRecvListenerDic.ContainsKey(cmdId))
                {
                    this.publicRecvListenerDic.Add(cmdId, new Dictionary<long, PublicRecvListener>() { { key, (param) => publicRecvListener(param) } });
                }
                else if (!this.publicRecvListenerDic[cmdId].ContainsKey(key))
                {
                    this.publicRecvListenerDic[cmdId].Add(key, (param) => publicRecvListener(param));
                }
            }
        }

        public void RemoveRecvEventListener(int cmdId , long key)
        {
            lock (GlobalVariable.lockObjs["publicRecvListener"])
            {
                if (this.publicRecvListenerDic.ContainsKey(cmdId)) this.publicRecvListenerDic[cmdId].Remove(key);
            }
        }

        public void RemoveAllEventListener()
        {
            lock (GlobalVariable.lockObjs["publicRecvListener"])
            {
                this.publicRecvListenerDic.Clear();
            }
        }
    }
}
