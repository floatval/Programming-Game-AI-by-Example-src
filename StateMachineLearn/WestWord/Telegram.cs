namespace StateMachineLearn;

/// <summary>
/// 消息载体
/// </summary>
public struct Telegram : IComparable<Telegram>
{
    public struct TelegramBuilder
    {
        public TelegramBuilder()
        {
            m_telegram = new Telegram();
        }

        /// <summary>
        /// 构造发送者的实例 id
        /// </summary>
        /// <param name="sendInsId"></param>
        /// <returns></returns>
        public TelegramBuilder BuildSenderInsId(int sendInsId)
        {
           m_telegram.SenderInsId = sendInsId;
            return this; 
        }
        
        /// <summary>
        /// 构造接收者的实例id
        /// </summary>
        /// <param name="receiverInsId"></param>
        /// <returns></returns>
        public TelegramBuilder BuildReceiverInsId(int receiverInsId)
        {
            m_telegram.ReceiverInsId = receiverInsId;
            return this;
        }

        /// <summary>
        /// 构造发送时间
        /// </summary>
        /// <param name="delaySeconds"></param>
        /// <returns></returns>
        public TelegramBuilder BuildDispatchTime(double delaySeconds)
        {
            m_telegram.DispatchTime = DateTime.Now.AddSeconds(delaySeconds);
            return this;
        }

        /// <summary>
        /// 构造消息类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public TelegramBuilder BuildMessageType(ConstDefine.MessageType type)
        {
            m_telegram.MessageType = type;
            return this;
        }

        /// <summary>
        /// 构造额外信息
        /// </summary>
        /// <param name="extraInfo"></param>
        /// <returns></returns>
        public TelegramBuilder BuildExtraInfo(object? extraInfo)
        {
            m_telegram.ExtraInfo = extraInfo;
            return this;
        }

        /// <summary>
        /// 流程最后一步调用
        /// </summary>
        /// <returns></returns>
        public Telegram Build()
        {
            return m_telegram;
        }
        
        #region 数据成员

        private Telegram m_telegram;

        #endregion
    }
    
    /// <summary>
    /// 发送者 id
    /// </summary>
    public int SenderInsId { get; private set; }
    
    /// <summary>
    /// 接收者 Id
    /// </summary>
    public int ReceiverInsId { get; private set; }
    
    /// <summary>
    /// 延迟的秒数
    /// </summary>
    public DateTime DispatchTime { get; private set; }

    /// <summary>
    /// 消息类型
    /// </summary>
    public ConstDefine.MessageType MessageType{ get; private set; }
    
    /// <summary>
    /// 额外的信息
    /// </summary>
    public object? ExtraInfo { get; private set; }

    #region Relational members

    /// <summary>Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.</summary>
    /// <param name="other">An object to compare with this instance.</param>
    /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings:
    /// <list type="table"><listheader><term> Value</term><description> Meaning</description></listheader><item><term> Less than zero</term><description> This instance precedes <paramref name="other" /> in the sort order.</description></item><item><term> Zero</term><description> This instance occurs in the same position in the sort order as <paramref name="other" />.</description></item><item><term> Greater than zero</term><description> This instance follows <paramref name="other" /> in the sort order.</description></item></list></returns>
    public int CompareTo(Telegram other)
    {
        // todo: 如果要忽略很短时间内派发的大量的相同的消息，可以通过在这个地方添加额外的逻辑来实现
        return DispatchTime.CompareTo(other.DispatchTime);
    }

    #endregion
}