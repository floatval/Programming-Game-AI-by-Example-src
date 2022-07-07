namespace StateMachineLearn;

public static class ConstDefine
{
    public static class MinerBehavior
    {
        /// <summary>
        /// 最大舒适度 
        /// </summary>
        public const int ComfortLevelMaxVal = 5;
        
        /// <summary>
        /// 一次挖矿中身上最多保存的金子数量
        /// </summary>
        public const int NuggetsMaxVal = 3;
        
        /// <summary>
        /// 最大饥渴度
        /// </summary>
        public const int ThirstLevelMaxVal = 5;
        
        /// <summary>
        /// 最大疲劳度 
        /// </summary>
        public const int TirednessThresholdMaxVal = 5;
    }

    public static class Location
    {
        /// <summary>
        /// 位置
        /// </summary>
        public enum LocationType
        {
            /// <summary>
            /// 无效位置 - 代码逻辑使用
            /// </summary>
            None,
            
            /// <summary>
            /// 金矿
            /// </summary>
            Goldmine,
            
            /// <summary>
            /// 酒吧
            /// </summary>
            Saloon,
            
            /// <summary>
            /// 银行
            /// </summary>
            Bank,
            
            /// <summary>
            /// 家里
            /// </summary>
            Home,
            
            /// <summary>
            /// 洗手间
            /// </summary>
            BathRoom,
            
            /// <summary>
            /// 客厅
            /// </summary>
            LivingRoom,
            
            /// <summary>
            /// 厨房
            /// </summary>
            Kitchen,
        }
    }
    
    public static class MinerState
    {
        /// <summary>
        /// 状态
        /// </summary>
        public enum MinerStateType
        {
            /// <summary>
            /// 无效状态 - 代码逻辑使用
            /// </summary>
            None,
            
            /// <summary>
            /// 去矿洞挖矿 
            /// </summary>
            EnterMineAndDigForNugget,
            
            /// <summary>
            /// 去银行存钱
            /// </summary>
            VisitBankAndDepositGold,
            
            /// <summary>
            /// 回家休息
            /// </summary>
            GoHomeAndSleepTilRested,
            
            /// <summary>
            /// 解决口渴
            /// </summary>
            QuenchThirst
        }
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 宝，我回来了
        /// </summary>
        HiHoneyImHome,
        
        /// <summary>
        /// 炖肉好了，准备开恰⑧
        /// </summary>
        StewReady
    }

}