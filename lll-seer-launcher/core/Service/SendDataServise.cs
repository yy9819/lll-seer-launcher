using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Controller;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.PetDto;

namespace lll_seer_launcher.core.Service
{
    class SendDataService
    {
        /// <summary>
        /// 封包发送主函数！
        /// 将字节数组发送到游戏服务器
        /// </summary>
        /// <param name="bytesData">欲发送的字节数组</param>
        public void SendHexBytesData(byte[] bytesData)
        {
            //设置欲发送数据长度
            //1：欲发送数据不符合封包发送条件，在字节数组末尾添加一个0
            //0：欲发送数据符合封包发送条件
            byte[] bytes = new byte[bytesData.Length + (bytesData.Length % 2 != 0 ? 1 : 0)];
            bytesData.CopyTo(bytes, 0);
            //对未加密数据进行加密(此时不进行seq计算，在安装的hook中监测到该包之后会自动计算seq)
            byte[] encryptBytes = EncryptService.Encrypt(bytes);
            HookControl.send(GlobalVariable.gameSocket, ByteConverter.GetBytesIntPtr(encryptBytes), encryptBytes.Length, 0);
            //Logger.Log("SendData", $"发送封包：{BitConverter.ToString(bytes)}");
        }

        /// <summary>
        /// 发送16进制文本封包
        /// </summary>
        /// <param name="hexStringData">16进制文本封包</param>
        public void SendHexStringData(string hexStringData)
        {
            hexStringData = hexStringData.Replace(" ", "").Replace("　", "");
            SendHexBytesData(ByteConverter.HexToBytes(hexStringData));
        }
        
        /// <summary>
        /// 通过cmdId与16进制文本的主体数据进行封包发送
        /// </summary>
        /// <param name="cmdId"></param>
        /// <param name="hexDataString"></param>
        public void SendDataByCmdIdAndHexString(int cmdId,string hexDataString)
        {
            int dataLen = 17 + hexDataString.Length / 2;
            byte[] dataBytes = new byte[dataLen];
            byte[] body = ByteConverter.HexToBytes(hexDataString);
            this.PackHead(cmdId, dataLen, body).CopyTo(dataBytes, 0);
            SendHexBytesData(dataBytes);
        }

        /// <summary>
        /// 通过cmdId与字节数组的主体数据进行封包发送
        /// </summary>
        /// <param name="cmdId"></param>
        /// <param name="hexDataBytes"></param>
        public void SendDataByCmdIdAndBytes(int cmdId,byte[] hexDataBytes)
        {
            int dataLen = 17 + hexDataBytes.Length;
            byte[] dataBytes = new byte[dataLen];
            this.PackHead(cmdId, dataLen, hexDataBytes).CopyTo(dataBytes, 0);
            SendHexBytesData(dataBytes);
        }

        /// <summary>
        /// 通过cmdId与int数组的主体数据进行封包发送
        /// </summary>
        /// <param name="cmdId"></param>
        /// <param name="dataList"></param>
        public void SendDataByCmdIdAndIntList(int cmdId,int[] dataList)
        {
            int dataLen = 17 + dataList.Length * 4;
            byte[] dataBytes = new byte[dataLen];
            byte[] body = new byte[dataList.Length * 4];
            int index = 0;
            for (int i = 0; i < dataList.Length; i++)
            {
                ByteConverter.HexToBytes(ByteConverter.DecimalToHex(dataList[i], 4)).CopyTo(body, index);
                index += 4;
            }
            this.PackHead(cmdId, dataLen, body).CopyTo(dataBytes, 0);

            SendHexBytesData(dataBytes);
        }

        /// <summary>
        /// 对封包进行一个组包
        /// </summary>
        /// <param name="cmdId"></param>
        /// <param name="dataLen"></param>
        /// <param name="body"></param>
        /// <returns>组包完成的封包字节数组</returns>
        private byte[] PackHead(int cmdId , int dataLen ,byte[] body)
        {
            byte[] dataBytes = new byte[dataLen];
            int index = 0;
            ByteConverter.HexToBytes(ByteConverter.DecimalToHex(dataLen, 4)).CopyTo(dataBytes, index);

            index += 4;
            ByteConverter.HexToBytes(ByteConverter.DecimalToHex(31, 1)).CopyTo(dataBytes, index);

            index += 1;
            ByteConverter.HexToBytes(ByteConverter.DecimalToHex(cmdId, 4)).CopyTo(dataBytes,index);

            index += 4;
            ByteConverter.HexToBytes(ByteConverter.DecimalToHex(GlobalVariable.loginUserInfo.userId, 4)).CopyTo(dataBytes, index);

            index += 4;
            ByteConverter.HexToBytes(ByteConverter.DecimalToHex(0, 4)).CopyTo(dataBytes, index);
            if (dataLen > 17)
            {
                index += 4; 
                body.CopyTo(dataBytes, index);
            }

            return dataBytes;
        }
    }

    class AnalyzeSendDataService
    {
        public static void OnSwitchBattery(HeadInfo sendData)
        {
            GlobalVariable.mainForm.SetBatteryStatus($"当前电池{(ByteConverter.BytesTo10(sendData.decryptData) == 0 ? "关闭" : "开启")}中");
        }

        public static void OnPetTakeOut(HeadInfo sendData)
        {
            lock (GlobalVariable.lockObjs["petList"])
            {
                int catchTime = ByteConverter.BytesTo10(ByteConverter.TakeBytes(sendData.decryptData, 0, 4));
                int flag = ByteConverter.BytesTo10(ByteConverter.TakeBytes(sendData.decryptData, 4, 4));
                if (GlobalVariable.petCatchTimeDic.TryGetValue(catchTime, out PetInfo pet))
                {
                    PetListInfo petListInfo = new PetListInfo();
                    petListInfo.catchTime = catchTime;
                    petListInfo.petId = pet.petId;
                    petListInfo.level = pet.level;
                    petListInfo.petName = pet.petName;
                    petListInfo.inBag = flag == 1 || flag == 2;
                    long key = GlobalUtil.GetKey();
                    GlobalVariable.analyzeRecvDataController.SetRecvEventListener(CmdId.PET_RELEASE, (HeadInfo recvData) =>
                    {
                        GlobalVariable.analyzeRecvDataController.RemoveRecvEventListener(CmdId.PET_RELEASE,key);
                        if (recvData.decryptData.Length <= 0) return;
                        if (GlobalVariable.petList[GlobalVariable.loginUserInfo.userId].ContainsKey(pet.petId))
                        {
                            if (!GlobalVariable.petList[GlobalVariable.loginUserInfo.userId][pet.petId].ContainsKey(catchTime))
                            {

                                GlobalVariable.petList[GlobalVariable.loginUserInfo.userId][pet.petId].Add(catchTime, petListInfo);
                            }
                            else
                            {
                                GlobalVariable.petList[GlobalVariable.loginUserInfo.userId][pet.petId][catchTime] = petListInfo;
                            }
                        }
                        else
                        {
                            GlobalVariable.petList[GlobalVariable.loginUserInfo.userId].Add(
                                petListInfo.petId, new Dictionary<int, PetListInfo>() { { pet.catchTime, petListInfo } });
                        }
                        //if (!petListInfo.inBag)
                        //{
                        //    if (GlobalVariable.pets.ContainsKey(pet.catchTime)) GlobalVariable.pets.Remove(pet.catchTime);
                        //    if (GlobalVariable.awaitPets.ContainsKey(pet.catchTime)) GlobalVariable.pets.Remove(pet.catchTime);
                        //}
                    },key);
                }
            }
        }
    }
}
