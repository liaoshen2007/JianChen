

namespace DataModel
{
    public class RewardVo
    {
        public int Num;
        public string Name;
        public string IconPath;

        public int Id;
        public ResourceType Resource;

        private bool _autoUpdateData;

        /// <summary>
        /// 奖励数据
        /// </summary>
        /// <param name="award"></param>
        /// <param name="autoUpdateData">自动更新数据</param>
        public RewardVo(AwardData award, bool autoUpdateData = false)
        {
            _autoUpdateData = autoUpdateData;
            InitAward(award);
        }

        //todo 之后要给这些东西的名字配表
        private void InitAward(AwardData award)
        {
            Id = award.ResourceId;
            Resource = award.ResourceType;

            switch (award.ResourceType)
            {
                case ResourceType.Exp:
                    IconPath = "MiniMapIcon/Shop-Weapon";
                    break;
                case ResourceType.Gold:
                    IconPath = "Props/coin-icon";
                    if (_autoUpdateData)
                        GlobalData.PlayerData.UpdatePlayerMoney(award.Num);
                    break;
                case ResourceType.Item:
                    IconPath = "Prop/" + award.ResourceId;
//                    if (_autoUpdateData)
//                    {
//                        GlobalData.CardModel.AddUserPuzzle(award);
//                    }
                    break;
                case ResourceType.Equip:
                    IconPath = "Props/Equip/" + award.ResourceId;
//                    Id = PropConst.PowerIconId;
//                    if (_autoUpdateData)
//                        GlobalData.PlayerModel.AddPower(award.Num);
                    break;
                
            }



            Num = award.Num;
        }
    }
}