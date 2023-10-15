using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows.Forms;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Servise;
using lll_seer_launcher.core.Tools;
using lll_seer_launcher.core.Utils;

namespace lll_seer_launcher.core.Controller
{
    class MessageEncryptDecryptController
    {
        #region 变量表
        private ArrayList tempBuff { get; set; }
        private ArrayList chunkBuff { get; set; }

        private AnalyzeRecvDataController analyzeRecvDataController = new AnalyzeRecvDataController();
        #endregion

        #region Api声明
        [DllImport("kernel32.dll")]
        private static extern void RtlMoveMemory(IntPtr lpDestination, IntPtr lpSource, int length);
        #endregion
        public MessageEncryptDecryptController()
        {
            tempBuff = new ArrayList();
            chunkBuff = new ArrayList();
            GlobalVariable.isLogin = false;
            this.InitKey();
        }
        /// <summary>
        /// 设置加密key的字符串
        /// </summary>
        public void SetKey(string key)
        {
            GlobalVariable.keyString = key;
        }

        /// <summary>
        /// 初始化加密key
        /// 获取加密key的字节数组
        /// 获取字节数组的长度
        /// 获取字节数组在内存中的地址
        /// </summary>
        public void InitKey()
        {
            if (GlobalVariable.keyPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(GlobalVariable.keyPtr);
            }
            Console.WriteLine("initKey:" + GlobalVariable.keyString);
            GlobalVariable.Key = UTF8Encoding.UTF8.GetBytes(GlobalVariable.keyString);
            //Console.WriteLine(BitConverter.ToString(this.Key));
            GlobalVariable.keyLen = GlobalVariable.Key.Length;
            GlobalVariable.keyPtr = ByteConverter.GetBytesIntPtr(GlobalVariable.Key);
        }

        /// <summary>
        /// 初始化顺序码sqe
        /// </summary>
        private void InitSeq(int inputSeq)
        {
            GlobalVariable.seq = inputSeq;
            Console.WriteLine($"initSeq:{GlobalVariable.seq}");
        }



        /// <summary>
        /// 获取指定数据包内的信息
        /// </summary>
        /// <param name="inputData">欲读取的数据包</param>
        /// <returns>读取完成的包信息对象</returns>
        public HeadInfo GetHeadInfo(byte[] inputData)
        {
            HeadInfo headInfo = new HeadInfo();
            int index = 0;
            headInfo.packageLen = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            headInfo.version = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 1));
            index += 1;
            headInfo.cmdId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            headInfo.userId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            headInfo.seq = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            headInfo.decryptData = ByteConverter.TakeBytes(inputData, index, inputData.Length - index);
            return headInfo;
        }

        public HeadInfo PackHeadInfo(HeadInfo headInfo)
        {
            byte[] encryptData = new byte[headInfo.packageLen];
            //写入包长
            int index = 0;
            ByteConverter.RepalaceBytes(encryptData, ByteConverter.HexToBytes(ByteConverter.DecimalToHex(headInfo.packageLen, 4)), index);
            //写入version
            index += 4;
            ByteConverter.RepalaceBytes(encryptData, ByteConverter.HexToBytes(ByteConverter.DecimalToHex(headInfo.version, 1)), index);
            //写入cmdId
            index += 1;
            ByteConverter.RepalaceBytes(encryptData, ByteConverter.HexToBytes(ByteConverter.DecimalToHex(headInfo.cmdId, 4)), index);
            //写入userid
            index += 4;
            ByteConverter.RepalaceBytes(encryptData, ByteConverter.HexToBytes(ByteConverter.DecimalToHex(headInfo.userId, 4)), index);
            //写入seq顺序码
            index += 4;
            if (headInfo.cmdId > 1000)
            {
                byte[] dataBody = headInfo.decryptData;
                int crc8Val = 0;
                if (dataBody.Length != 0)
                {
                    //与欲发送的主体数据进行异或异与计算
                    for (int i = 0; i < dataBody.Length; i++)
                    {
                        crc8Val = (crc8Val ^ dataBody[i]) & 255;
                    }
                }
                //获取重新计算后的顺序码
                GlobalVariable.seq = EncryptService.MSerial(GlobalVariable.seq, headInfo.packageLen - 1, crc8Val, headInfo.cmdId);
                string seqHex = ByteConverter.DecimalToHex(GlobalVariable.seq, 4);
                ByteConverter.RepalaceBytes(encryptData, ByteConverter.HexToBytes(seqHex), index);
            }
            else
            {
                ByteConverter.RepalaceBytes(encryptData, ByteConverter.HexToBytes(ByteConverter.DecimalToHex(0, 4)), index);
            }
            headInfo.seq = GlobalVariable.seq;
            //写入解密数据，完成组包
            index += 4;
            ByteConverter.RepalaceBytes(encryptData, headInfo.decryptData, index);

            //将组包完成的数据进行加密
            headInfo.encryptData = this.Encrypt(encryptData);
            return headInfo;
        }





        /// <summary>
        /// 加载未解密的原始数据并进行沾包处理
        /// </summary>
        /// <param name="dataPtr">指针</param>
        /// <param name="dataSize">长度</param>
        public void LoadBasicMessage(IntPtr dataPtr, int dataSize)
        {
            int tmpBuffSize;
            int realSize;
            byte[] tmpBytes = new byte[dataSize];
            Marshal.Copy(dataPtr, tmpBytes, 0, dataSize);
            tempBuff = GlobalUtil.CombineArrayList(tempBuff, GlobalUtil.BytesToArray(tmpBytes));
            //Console.WriteLine(BitConverter.ToString(tmpBytes));
            //return;

            while (true)
            {
                tmpBuffSize = tempBuff.Count;
                if (tmpBuffSize >= 4)
                {
                    byte[] realBytes = new byte[tmpBuffSize];
                    realBytes = (byte[])tempBuff.ToArray(typeof(byte));
                    realSize = ByteConverter.BytesTo10(ByteConverter.TakeBytes(realBytes, 0, 4));

                    if (realSize <= tmpBuffSize)
                    {
                        chunkBuff = tempBuff.GetRange(0, realSize);
                        tempBuff = tempBuff.GetRange(realSize, tmpBuffSize - realSize);
                        realBytes = (byte[])chunkBuff.ToArray(typeof(byte));
                        this.RecvHandle(realBytes);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 将经过沾包处理后的一条完整数据进行解密
        /// </summary>
        /// <param name="bytes">一条完整的加密包</param>
        /// <returns></returns>
        private void RecvHandle(byte[] bytes)
        {
            int packVer = (int)bytes[4];
            //Console.WriteLine("recvBefore:" + BitConverter.ToString(bytes));
            if (packVer != 0 && packVer != 62)
            {
                bytes = this.Decrypt(bytes);
                byte[] decryptBytes = ByteConverter.TakeBytes(bytes, 0, bytes.Length - 1);
                //Console.WriteLine("decryptedRecv:" + BitConverter.ToString(decryptBytes));
                HeadInfo headInfo = this.GetHeadInfo(decryptBytes);
                if (headInfo.cmdId == CmdId.LOGIN_IN)
                {
                    GlobalVariable.userId = headInfo.userId;
                    this.OnLogin(decryptBytes);
                    this.InitSeq(headInfo.seq);
                    GlobalVariable.isLogin = true;
                    Logger.Log("gameLogin", $"user:{GlobalVariable.userId} 登录成功！");

                }
                else if(headInfo.cmdId > 100000 && !GlobalVariable.gameReloadFlg)
                {
                    Thread onLoginThread = new Thread(() =>
                    {
                        Logger.Error($" 当前cmdId‘{headInfo.cmdId}’大于100000，登录加密key初始化失败！");
                        GlobalVariable.gameReloadFlg = true;
                        MessageBox.Show("游戏掉线/登录加密key初始化失败！将刷新游戏，请重新登录！");
                    });
                    onLoginThread.Start();
                }
                else
                {
                    this.analyzeRecvDataController.RunAnalyzeRecvDataMethod(headInfo);
                }
            }


        }

        /// <summary>
        /// 在登录时，更新加密key
        /// </summary>
        /// <param name="bytes">登录信息包</param>
        /// <returns></returns>
        private void OnLogin(byte[] bytes)
        {
            byte[] keyBytes = ByteConverter.TakeBytes(bytes, bytes.Length - 4, 4);
            string tmpKey = BitConverter.ToString(keyBytes).Replace("-", string.Empty);
            int tmpNum = Convert.ToInt32(tmpKey, 16);
            tmpNum = tmpNum ^ GlobalVariable.userId;
            byte[] hashBytes = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(tmpNum.ToString()));
            tmpKey = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            this.SetKey(tmpKey.Substring(0, 10));
            this.InitKey();

        }


        public byte[] Decrypt(byte[]bytes)
        {
            return DecryptService.Decrypt(bytes);
        }

        public byte[] Encrypt(byte[] bytes)
        {
            return EncryptService.Encrypt(bytes);
        }
    }
}
