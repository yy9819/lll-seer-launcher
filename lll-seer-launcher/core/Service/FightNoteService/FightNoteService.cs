using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.PetDto;

namespace lll_seer_launcher.core.Service.FightNoteService
{
    public class FightNoteService
    {
        public static Dictionary<string, FightPetInfo> OnNoteStartFight(HeadInfo recvDataHeadInfo)
        {
            FightPetInfo loginPlayerPetInfo = new FightPetInfo();
            FightPetInfo otherPlayerPetInfo = new FightPetInfo();
            int index = 8;
            index = loginPlayerPetInfo.SetFightInfo(index, recvDataHeadInfo.decryptData);
            otherPlayerPetInfo.SetFightInfo(index, recvDataHeadInfo.decryptData);
            Dictionary<string, FightPetInfo> players = new Dictionary<string, FightPetInfo>();
            players.Add("loginPlayer", loginPlayerPetInfo.userId  == GlobalVariable.loginUserInfo.userId ? loginPlayerPetInfo : otherPlayerPetInfo);
            players.Add("otherPlayer", otherPlayerPetInfo.userId  != GlobalVariable.loginUserInfo.userId ? otherPlayerPetInfo : loginPlayerPetInfo);
            return players;
        }
        public static ChangePetInfo OnChangePet(HeadInfo recvDataHeadInfo)
        {
            ChangePetInfo changPetInfo = new ChangePetInfo();
            int index = 0;
            changPetInfo.SetChangPetInfo(index, recvDataHeadInfo.decryptData);
            return changPetInfo;
        }
        public static Dictionary<string,AttackValueInfo> OnNoteUseSkill(HeadInfo recvDataHeadInfo)
        {
            AttackValueInfo fristPlayerInfo = new AttackValueInfo();
            AttackValueInfo secondPlayerInfo = new AttackValueInfo();
            int index = 0;
            index = fristPlayerInfo.SetAttackValueInfo(index, recvDataHeadInfo.decryptData);
            secondPlayerInfo.SetAttackValueInfo(index,recvDataHeadInfo.decryptData);
            Dictionary<string, AttackValueInfo> players = new Dictionary<string, AttackValueInfo>();
            players.Add("loginPlayer", fristPlayerInfo.userId  == GlobalVariable.loginUserInfo.userId ? fristPlayerInfo : secondPlayerInfo);
            players.Add("otherPlayer", fristPlayerInfo.userId  == GlobalVariable.loginUserInfo.userId ? secondPlayerInfo : fristPlayerInfo);
            return players;
        }
    }
}
