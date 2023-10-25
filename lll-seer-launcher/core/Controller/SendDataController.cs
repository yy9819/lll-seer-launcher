using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lll_seer_launcher.core.Servise;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Dto;

namespace lll_seer_launcher.core.Controller
{
    /// <summary>
    /// 封包发送controller
    /// 可调用相应格式的封包发送方法进行封包发送
    /// </summary>
    public class SendDataController
    {
        private SendDataServise sendDataService;
        public SendDataController()
        {
            this.sendDataService = new SendDataServise();
        }

        /// <summary>
        /// 发送16进制文本封包
        /// </summary>
        /// <param name="hexStringData"></param>
        public void SendHexStringData(string hexStringData)
        {
            sendDataService.SendHexStringData(hexStringData);;
        }
        
        /// <summary>
        /// 将字节数组发送到游戏服务器
        /// </summary>
        /// <param name="hexBytesData"></param>
        public void SendHexBytesData(byte[] hexBytesData)
        {
            ByteConverter.HexToBytes(ByteConverter.DecimalToHex(GlobalVariable.loginUserInfo.userId, 4)).CopyTo(hexBytesData,9);
            sendDataService.SendHexBytesData(hexBytesData);
        }
        /// <summary>
        /// 通过cmdId与16进制文本的主体数据进行封包发送
        /// </summary>
        /// <param name="cmdId"></param>
        /// <param name="hexString"></param>
        public void SendDataByCmdIdAndHexString(int cmdId , string hexString)
        {
            sendDataService.SendDataByCmdIdAndHexString(cmdId ,hexString);
        }

        /// <summary>
        /// 通过cmdId与字节数组的主体数据进行封包发送
        /// </summary>
        /// <param name="cmdId"></param>
        /// <param name="hexBytes"></param>
        public void SendDataByCmdIdAndHexBytes(int cmdId, byte[] hexBytes)
        {
            sendDataService.SendDataByCmdIdAndBytes(cmdId, hexBytes);
        }

        /// <summary>
        /// 通过cmdId与int数组的主体数据进行封包发送
        /// </summary>
        /// <param name="cmdId"></param>
        /// <param name="intList"></param>
        public void SendDataByCmdIdAndIntList(int cmdId, int[] intList)
        {
            sendDataService.SendDataByCmdIdAndIntList(cmdId, intList);
        }
    }
}
