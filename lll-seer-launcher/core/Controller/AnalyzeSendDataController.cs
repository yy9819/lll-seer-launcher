using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using lll_seer_launcher.core.Service;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.PetDto;
using lll_seer_launcher.core.Utils;

namespace lll_seer_launcher.core.Controller
{
    public class AnalyzeSendDataController
    {
        /// <summary>
        /// 对需在字典里调用的方法设置一个通用格式委托方法
        /// </summary>
        /// <param name="sendDataHeadInfo">封包</param>
        private delegate void AnalyzeSendDataMethod(HeadInfo sendDataHeadInfo);

        /// <summary>
        /// key:cmdId
        /// value:需要执行的封包controller
        /// </summary>
        private Dictionary<int, AnalyzeSendDataMethod> methodDictionary;
        public AnalyzeSendDataController()
        {
            methodDictionary = new Dictionary<int, AnalyzeSendDataMethod>
            {
                /*==========================================精灵相关解析============================================*/
                { CmdId.BATTERY_DORMANT_SWITCH , (param) =>  AnalyzeBatteryDormantSwitch(param)},
                { CmdId.PET_RELEASE, (param) =>  AnalyzePetTakeOut(param)},
            };
        }

        public void RunAnalyzeSendDataMethod(HeadInfo sendDataHeadInfo)
        {

            if (methodDictionary.TryGetValue(sendDataHeadInfo.cmdId, out AnalyzeSendDataMethod method))
            {
                Thread methodThread = new Thread(() => { method(sendDataHeadInfo); });
                methodThread.Start();
            }
            else { }
        }
        private void AnalyzeBatteryDormantSwitch(HeadInfo sendDataHeadInfo)
        {
            AnalyzeSendDataService.OnSwitchBattery(sendDataHeadInfo);
        }

        private void AnalyzePetTakeOut(HeadInfo sendDataHeadInfo)
        {
            AnalyzeSendDataService.OnPetTakeOut(sendDataHeadInfo);
        }
    }
}
