using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using lll_seer_launcher.core.Controller;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.PetDto;
using lll_seer_launcher.core.Utils;

namespace lll_seer_launcher.core.Forms
{
    public partial class FightNoteForm : Form
    {
        private PetHeadSetter petHeadSetter = new PetHeadSetter();
        private delegate void InitFormCallback();
        /// <summary>
        /// 上等体力药剂(200HP) 永寒体力药剂(250HP) 全能恢复药剂(150HP,3PP) 高级体力药剂(100HP) 中级体力药剂(50HP) 初级体力药剂(20HP)
        /// </summary>
        private List<int> HPItemList = new List<int>()
        {
            300157,300079,300701,300013,300012,300011
        };
        /// <summary>
        /// 初级活力药剂(5PP)  中级活力药剂(10PP) 高级活力药剂(20PP) 巅峰活力药剂(10PP) 极限活力药剂(20PP)
        /// </summary>
        private List<int> PPItemList = new List<int>()
        {
            300016,300017,300018,300073,300074
        };
        /// <summary>
        /// 无敌精灵胶囊Ω 无敌精灵胶囊 超能胶囊 初级精灵捕捉胶囊(必中) 普通精灵胶囊 中级精灵胶囊 高级精灵胶囊 超级精灵胶囊 特级精灵胶囊
        /// </summary>
        private List<int> catchItemList = new List<int>()
        {
            300010,300006,300007,300505,300001,300002,300003,300004
        };
        /// <summary>
        /// 异常解除道具
        /// </summary>
        private List<int> statusItemList = new List<int>()
        {
            300610,300611,300612,300613,300614,300615,300616,300925,300926,300927,300928,300929,300930,300705
        };
        /// <summary>
        /// 精灵列表
        /// </summary>
        private List<PetInfo> fightPetInfo = new List<PetInfo>();
        /// <summary>
        /// 当前战斗精灵
        /// </summary>
        private int fightPetCatchTime { get; set; }
        /// <summary>
        /// 当前战斗精灵技能列表
        /// </summary>
        private List<int> skillList = new List<int>();

        public FightNoteForm()
        {
            InitializeComponent();
        }

        private void FightNoteForm_Load(object sender, EventArgs e)
        {
            this.HPComboBox.SelectedIndex = 0;
            this.PPComboBox.SelectedIndex = 0;
            this.catchItemComboBox.SelectedIndex = 0;
            this.statusItemComboBox.SelectedIndex = 0;
            InitFormCallback callback = delegate ()
            {
                this.Location = new Point(GlobalVariable.mainForm.Location.X + GlobalVariable.mainForm.Size.Width, GlobalVariable.mainForm.Location.Y);
            };
            this.Invoke(callback);
        }

        private void FightNoteForm_Closing(object sender,FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        public void OnUseSkill(Dictionary<string, AttackValueInfo> playersInfo)
        {
            if (this.Disposing || this.IsDisposed || !this.Visible) return;
            new Task(() =>
            {
                InitFormCallback callback = delegate ()
                {
                    AttackValueInfo loginPlayer = playersInfo["loginPlayer"];
                    AttackValueInfo otherPlayer = playersInfo["otherPlayer"];
                    //设置伤害note
                    this.fightNoteTextBox.AppendText(
                        $"\r\n*************** 第{GlobalVariable.fightTurn}回合 ***************\r\n\r\n" +
                        $"[我方]使用技能[{loginPlayer.skillId}-{SkillNameDic.GetSkillName(loginPlayer.skillId)}] " +
                        $"{(loginPlayer.atkTimes == 0 && loginPlayer.skillId != 0 ? "攻击MISS！" : "")}" +
                        $"{(loginPlayer.isCrit == 1 ? "打出了致命一击!" : "")}" +
                        $"造成[{loginPlayer.lostHP}]点伤害{(loginPlayer.realHurtHp > 0 ? $",造成[{loginPlayer.realHurtHp}点真实伤害]":"")}" +
                        $"{(loginPlayer.gainHP > 0 ? $",回复{loginPlayer.gainHP}血" : "")}\r\n" +
                        $"{(loginPlayer.maxHP == 0 ? "我方当前出战精灵[阵亡]!\r\n" : "")}"+

                        $"[对方]使用技能[{otherPlayer.skillId}-{SkillNameDic.GetSkillName(otherPlayer.skillId)}] " +
                        $"{(otherPlayer.atkTimes == 0  && otherPlayer.skillId != 0 ? "攻击MISS！" : "")}" +
                        $"{(otherPlayer.isCrit == 1 ? "打出了致命一击!" : "")}" +
                        $"造成[{otherPlayer.lostHP}]点伤害{(otherPlayer.realHurtHp > 0 ? $",造成[{otherPlayer.realHurtHp}点真实伤害]" : "")}" +
                        $"{(otherPlayer.gainHP > 0 ? $",回复{otherPlayer.gainHP}血" : "")}\r\n" +
                        $"{(otherPlayer.maxHP == 0 ? "对方当前出战精灵[阵亡]!\r\n" : "")}"
                    );
                    this.fightNoteTextBox.ScrollToCaret();

                    //更新双方血条
                    //$"[对方]--使用[{otherPlayer.skillId}-{otherPlayer.petName}]登场！\r\n";
                    this.loginPlayerPetHP.Value = loginPlayer.maxHpSelf == 0 ? 0 :(int)((long)loginPlayer.remainHP * 100 / loginPlayer.maxHpSelf);
                    this.otherPlayerPetHP.Value = otherPlayer.maxHpSelf == 0 ? 0 :(int)((long)otherPlayer.remainHP * 100 / otherPlayer.maxHpSelf);
                    this.loginPlayerHPLabel.Text = loginPlayer.maxHpSelf == 0 ? "" : $"{loginPlayer.remainHP}/{loginPlayer.maxHpSelf}";
                    this.otherPlayerHPLabel.Text = otherPlayer.maxHpSelf == 0 ? "" : $"{(int)((long)otherPlayer.remainHP * 100 / otherPlayer.maxHpSelf)}%";

                    //更新双方强化等级以及异常状态
                    this.loginPlayerPetAbilityLabel.Text = "";
                    this.otherPlayerPetAbilityLabel.Text = "";
                    this.loginPlayerPetStatusLabel.Text = "";
                    this.otherPlayerPetStatusLabel.Text = "";

                    int id = 0;
                    foreach(var parm in loginPlayer.status)
                    {
                        StatusInfo.Status[0].TryGetValue(id++, out string statusName);
                        
                        if (parm != 0)
                        {
                            this.loginPlayerPetStatusLabel.Text += $"{statusName}{ parm } ";
                        }

                    }
                    foreach (var status in loginPlayer.sideEffects)
                    {
                        if (status.type == 1 && status.parm != 6)
                        {
                            StatusInfo.Status[1].TryGetValue(status.id, out string statusName);
                            this.loginPlayerPetAbilityLabel.Text += $"{statusName}{ status.parm - 6} ";
                        }
                    }

                    id = 0;
                    foreach (var parm in otherPlayer.status)
                    {
                        StatusInfo.Status[0].TryGetValue(id++, out string statusName);
                        if (parm != 0)
                        {
                            this.otherPlayerPetStatusLabel.Text += $"{statusName}{ parm } ";
                        }
                    }
                    foreach (var status in otherPlayer.sideEffects)
                    {
                        if (status.type == 1 && status.parm != 6)
                        {
                            StatusInfo.Status[1].TryGetValue(status.id, out string statusName);
                            this.otherPlayerPetAbilityLabel.Text += $"{statusName}{ status.parm - 6} ";
                        }
                    }

                    //更新技能pp
                    if (this.Disposing || this.IsDisposed || !this.Visible) return;
                    try
                    {
                        for(int i = 0; i < this.fightPetInfo.Count; i++)
                        {
                            if(this.fightPetInfo[i].catchTime == fightPetCatchTime)
                            {
                                this.fightPetInfo[i].skillArray = loginPlayer.skillArray;
                                int j = 0;
                                foreach (var skill in loginPlayer.skillArray.Keys)
                                {
                                    this.skillListBox.Items[j++] = $"{loginPlayer.skillArray[skill].skillName}|{loginPlayer.skillArray[skill].skillPP}";
                                }
                                break;
                            }
                        }
                        
                    }
                    catch { }
                };
                
                this.Invoke(callback);
            }).Start();
        }
        public void OnChangePet(ChangePetInfo changePetInfo)
        {
            if (this.Disposing || this.IsDisposed || !this.Visible) return;
            new Task(() =>
            {
                InitFormCallback callback = delegate ()
                {
                    if (changePetInfo.userId == GlobalVariable.loginUserInfo.userId)
                    {
                        //更新我方出战精灵
                        this.fightPetCatchTime = changePetInfo.catchTime;
                        //更新当前出战精灵的技能
                        foreach(var pet in this.fightPetInfo)
                        {
                            if(pet.catchTime == changePetInfo.catchTime)
                            {
                                this.skillList.Clear();
                                this.skillListBox.Items.Clear();
                                foreach (var skill in pet.skillArray.Keys)
                                {
                                    this.skillList.Add(skill);
                                    this.skillListBox.Items.Add($"{pet.skillArray[skill].skillName}|{pet.skillArray[skill].skillPP}");
                                    this.skillListBox.SelectedIndex = 0;
                                }
                                break;
                            }
                        }
                        this.fightNoteTextBox.AppendText(
                            $"\r\n\r\n*************** 第{GlobalVariable.fightTurn + 1}回合 ***************\r\n\r\n" +
                            $"[我方]切换精灵[{changePetInfo.petId}-{changePetInfo.petName}] "
                        );
                        this.loginPlayerPetInfo.Text = $"{changePetInfo.petId}\n{changePetInfo.petName}\n{changePetInfo.level}";
                        this.loginPlayerHPLabel.Text = $"{changePetInfo.hp}/{changePetInfo.maxHp}";
                        this.loginPlayerPetHP.Value = (int)((long)changePetInfo.hp * 100 / changePetInfo.maxHp);
                        this.SetLoginPlayerPetHead(changePetInfo.petId);

                    }
                    else
                    {
                        this.fightNoteTextBox.AppendText(
                            $"\r\n\r\n*************** 第{GlobalVariable.fightTurn + 1}回合 ***************\r\n\r\n" +
                            $"[对方]切换精灵[{changePetInfo.petId}-{changePetInfo.petName}] "
                        );
                        this.otherPlayerPetInfo.Text = $"{changePetInfo.petId}\n{changePetInfo.petName}\n{changePetInfo.level}";
                        this.otherPlayerHPLabel.Text = $"{(int)((long)changePetInfo.hp * 100 / changePetInfo.maxHp)}%";
                        this.otherPlayerPetHP.Value = (int)((long)changePetInfo.hp * 100 / changePetInfo.maxHp);
                        this.SetOtherPlayerPetHead(changePetInfo.petId);
                    }
                    this.fightNoteTextBox.ScrollToCaret();
                };
                this.Invoke(callback);
            }).Start();
        }
        public void SetPetFightNote(Dictionary<string, FightPetInfo> fightPlayersInfo)
        {
            if (this.Disposing || this.IsDisposed) return;
            new Task(() =>
            {
                InitFormCallback callback = delegate ()
                {
                    FightPetInfo loginPlayer = fightPlayersInfo["loginPlayer"];
                    FightPetInfo otherPlayer = fightPlayersInfo["otherPlayer"];
                    //设置当前出战精灵
                    this.fightPetCatchTime = loginPlayer.catchTime;
                    this.fightPetInfo.Clear();
                    this.petList.Items.Clear();

                    if (GlobalVariable.pets.TryGetValue(this.fightPetCatchTime, out PetInfo petInfo)) { }
                    else
                    {
                        petInfo = GlobalVariable.awaitPets[this.fightPetCatchTime];
                    }
                    this.fightPetInfo.Add(petInfo);
                    this.petList.Items.Add(petInfo.petName);

                    this.skillList.Clear();
                    this.skillListBox.Items.Clear();
                    foreach (var skill in petInfo.skillArray.Keys)
                    {
                        this.skillList.Add(skill);
                        this.skillListBox.Items.Add($"{petInfo.skillArray[skill].skillName}|{petInfo.skillArray[skill].skillPP}");
                        this.skillListBox.SelectedIndex = 0;
                    }

                    foreach(var otherPet in loginPlayer.changehps)
                    {
                        if (GlobalVariable.pets.TryGetValue(otherPet["id"], out PetInfo otherPetInfo)){ }
                        else
                        {
                            otherPetInfo = GlobalVariable.awaitPets[otherPet["id"]];
                        }
                        this.petList.Items.Add(otherPetInfo.petName);
                        this.fightPetInfo.Add(otherPetInfo);
                        
                    }
                    //设置双方出战精灵头像
                    this.SetLoginPlayerPetHead(loginPlayer.petId);
                    this.SetOtherPlayerPetHead(otherPlayer.petId);
                    //设置双方出战精灵基本信息
                    this.SetLoginPlayerPetFightInfo(loginPlayer);
                    this.SetOtherPlayerPetFightInfo(otherPlayer);
                    //重置双方出战精灵能力提升状态及异常状态
                    this.loginPlayerPetAbilityLabel.Text = "";
                    this.otherPlayerPetAbilityLabel.Text = "";
                    this.loginPlayerPetStatusLabel.Text = "";
                    this.otherPlayerPetStatusLabel.Text = "";
                    //更新对战note
                    this.fightNoteTextBox.Text = 
                        $"*************** 第{GlobalVariable.fightTurn}回合 ***************\r\n\r\n" +
                        $"[我方][{loginPlayer.petId}-{loginPlayer.petName}]登场！\r\n" +
                        $"[对方][{otherPlayer.petId}-{otherPlayer.petName}]登场！\r\n";
                };
                this.Invoke(callback);
            }).Start();
        }
        private void SetLoginPlayerPetHead(int headID)
        {
            Image loginPlayerPetImg;
            try
            {
                string headPath = this.petHeadSetter.GetHeadPath(headID);
                //int count = 0;
                //while (true)
                //{
                //    if (File.Exists(headPath) || count++ > 5) break;
                //    Thread.Sleep(1000);
                //}
                loginPlayerPetImg = Image.FromFile(headPath);
            }
            catch
            {
                loginPlayerPetImg = Properties.Resources._214;
            }
            this.loginPlayerPetHead.Image = loginPlayerPetImg;
        }
        private void SetOtherPlayerPetHead(int headID)
        {
            Image otherPlayerPetImg;
            try
            {
                string headPath = this.petHeadSetter.GetHeadPath(headID);
                //int count = 0;
                //while (true)
                //{
                //    if (File.Exists(headPath) || count++ > 5) break;
                //    Thread.Sleep(1000);
                //}
                otherPlayerPetImg = Image.FromFile(headPath);
            }
            catch
            {
                otherPlayerPetImg = Properties.Resources._214;
            }
            Bitmap flippedImage = new Bitmap(otherPlayerPetImg.Width, otherPlayerPetImg.Height);

            // 创建 Graphics 对象来绘制翻转后的图像
            using (Graphics graphics = Graphics.FromImage(flippedImage))
            {
                // 翻转绘制
                graphics.DrawImage(otherPlayerPetImg, new Rectangle(0, 0, otherPlayerPetImg.Width, otherPlayerPetImg.Height),
                    new Rectangle(otherPlayerPetImg.Width, 0, -otherPlayerPetImg.Width, otherPlayerPetImg.Height), GraphicsUnit.Pixel);
            }

            this.otherPlayerPetHead.Image = flippedImage;
        }
        private void SetLoginPlayerPetFightInfo(FightPetInfo fightPlayerInfo)
        {
            this.loginPlayerPetInfo.Text = $"{fightPlayerInfo.petId}\n{fightPlayerInfo.petName}\n{fightPlayerInfo.level}";
            this.loginPlayerHPLabel.Text = $"{fightPlayerInfo.hp}/{fightPlayerInfo.maxHp}";
            this.loginPlayerPetHP.Value = (int)((long)fightPlayerInfo.hp * 100 / fightPlayerInfo.maxHp);
        }
        private void SetOtherPlayerPetFightInfo(FightPetInfo fightPlayerInfo)
        {
            this.otherPlayerPetInfo.Text = $"{fightPlayerInfo.petId}\n{fightPlayerInfo.petName}\n{fightPlayerInfo.level}";
            this.otherPlayerHPLabel.Text = $"{(int)((long)fightPlayerInfo.hp * 100 / fightPlayerInfo.maxHp)}%";
            this.otherPlayerPetHP.Value = (int)((long)fightPlayerInfo.hp * 100 / fightPlayerInfo.maxHp);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            this.fightNoteTextBox.Text = "";
        }

        private void useHPItem_Click(object sender, EventArgs e)
        {
            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_PET_ITEM, new int[3]
            {
                this.fightPetCatchTime,this.HPItemList[this.HPComboBox.SelectedIndex],0
            });
        }

        private void usePPItem_Click(object sender, EventArgs e)
        {
            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_PET_ITEM, new int[3]
            {
                this.fightPetCatchTime,this.PPItemList[this.PPComboBox.SelectedIndex],0
            });
        }

        private void useCatchItem_Click(object sender, EventArgs e)
        {
            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.CATCH_MONSTER, new int[1]
            {
                this.catchItemList[this.catchItemComboBox.SelectedIndex]
            });
        }

        private void petList_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void petList_DoubleClick(object sender, EventArgs e)
        {
            if (this.petList.SelectedIndex >= 0 && this.fightPetCatchTime != this.fightPetInfo[this.petList.SelectedIndex].catchTime)
            {
                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.CHANGE_PET, new int[1]
                    {
                        this.fightPetInfo[this.petList.SelectedIndex].catchTime
                    });
            }
        }

        private void useStatusItem_Click(object sender, EventArgs e)
        {
            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_PET_ITEM, new int[3]
                {
                    this.fightPetCatchTime,this.statusItemList[this.statusItemComboBox.SelectedIndex],0
                });
        }

        private void lowerHPButton_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.isLogin || GlobalVariable.gameConfigFlag.inFight) return;
            GlobalVariable.mainForm.lowerHpToolStripMenuItem_Click(sender, e);
        }

        private void escFightButton_Click(object sender, EventArgs e)
        {
            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.ESCAPE_FIGHT, new int[0]);
        }

        private void curePetButton_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.isLogin || GlobalVariable.gameConfigFlag.inFight) return;
            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.PET_CURE_FREE, new int[0]);
        }

        private void skillListBox_DoubleClick(object sender, EventArgs e)
        {
            if (this.skillListBox.SelectedIndex < 0) return;
            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_SKILL,
                        new int[1] { this.skillList[this.skillListBox.SelectedIndex] });
        }

        private void loopUseSkillCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.skillList.Count < 1)
            {
                this.loopUseSkillCheckBox.Checked = false;
                return;
            }
            try
            {
                if (loopUseSkillCheckBox.Checked)
                {
                    GlobalVariable.gameConfigFlag.autoUseSkillId = this.skillList[this.skillListBox.SelectedIndex];
                    GlobalVariable.gameConfigFlag.autoUseSkillFlg = true;
                    GlobalVariable.gameConfigFlag.autoUseSkillPetCatchTime = this.fightPetCatchTime;
                }
                else
                {
                    GlobalVariable.gameConfigFlag.autoUseSkillId = 0;
                    GlobalVariable.gameConfigFlag.autoUseSkillFlg = false;
                    GlobalVariable.gameConfigFlag.autoUseSkillPetCatchTime = 0;
                }
            }
            catch
            {
                GlobalVariable.gameConfigFlag.autoUseSkillId = 0;
                GlobalVariable.gameConfigFlag.autoUseSkillFlg = false;
                GlobalVariable.gameConfigFlag.autoUseSkillPetCatchTime = 0;
            }
        }

        private void autoAddPPCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVariable.gameConfigFlag.autoUseSkillAddPPFlg = this.autoAddPPCheckBox.Checked;
        }

        public void StopLoopUseSkill()
        {
            if (!this.IsDisposed)
            {
                InitFormCallback callback = delegate ()
                {
                    this.autoAddPPCheckBox.Checked = this.loopUseSkillCheckBox.Checked = false;
                };
                this.Invoke(callback);
            }
            
        }
        public void InitFightNote()
        {
            if (!this.IsDisposed && this.Visible)
            {
                InitFormCallback callback = delegate ()
                {
                    this.fightNoteTextBox.AppendText("\n\n*************** 战斗结束! ***************\n\n");
                    this.fightNoteTextBox.ScrollToCaret();
                    this.autoAddPPCheckBox.Checked = this.loopUseSkillCheckBox.Checked = false;
                    this.petList.Items.Clear();
                    this.fightPetInfo.Clear();
                    this.skillList.Clear();
                    this.skillListBox.Items.Clear();
                };
                this.Invoke(callback);
            }

        }

        private void skillListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(GlobalVariable.gameConfigFlag.autoUseSkillFlg) GlobalVariable.gameConfigFlag.autoUseSkillId = this.skillList[this.skillListBox.SelectedIndex];
        }

        private void hideFightModuleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVariable.gameConfigFlag.shouldDisableRecv  = this.hideFightModuleCheckBox.Checked;
        }

        private void notUseSkillButton_Click(object sender, EventArgs e)
        {
            if (GlobalVariable.gameConfigFlag.inFight)
            {
                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_SKILL, new int[1] { 0 });
            }
        }
    }
}
