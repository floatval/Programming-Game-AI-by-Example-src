namespace StateMachineLearn;

/// <summary>
/// 消息发送者
/// </summary>
public interface IMessageDispatcher
{
    /// <summary>
    /// 从发送者中发送信息到接收者
    /// </summary>
    /// <param name="sendInsId"></param>
    /// <param name="receiverInsId"></param>
    /// <param name="msgType"></param>
    /// <param name="extraData"></param>
    /// <returns>发送者、接收者不存在时返回 false</returns>
    public bool DispatchMessage(int sendInsId, int receiverInsId, ConstDefine.MessageType msgType, object? extraData);

    /// <summary>
    /// 通过名字发消息
    /// </summary>
    /// <param name="receiverName"></param>
    /// <param name="senderName"></param>
    /// <param name="msgType"></param>
    /// <param name="delaySeconds"></param>
    /// <param name="extraData"></param>
    /// <returns>发送者、接收者不存在时返回 false</returns>
    public bool DispatchMessage(EntityName receiverName, EntityName senderName, ConstDefine.MessageType msgType,
        double delaySeconds,
        object? extraData);

    
    /// <summary>
    /// 发送延时消息
    /// </summary>
    public void DispatchDelayMessage();

    /// <summary>
    /// 消息优先队列
    /// 使用者需要按照需求自定义排序
    /// </summary>
    public SortedSet<Telegram> PriorityMessageQueue { get; }
}

public class MessageDispatcher : IMessageDispatcher
{
    #region Implementation of IMessageDispatcher

    /// <summary>
    /// 从发送者中发送信息到接收者
    /// </summary>
    /// <param name="sendInsId"></param>
    /// <param name="receiverInsId"></param>
    /// <param name="msgType"></param>
    /// <param name="extraData"></param>
    /// <returns>发送者、接收者不存在时返回 false</returns>
    public bool DispatchMessage(int sendInsId, int receiverInsId, ConstDefine.MessageType msgType, object? extraData)
    {
        // 1. 前置检查
        if(!m_sendMessagePreCheckByInsId(sendInsId, receiverInsId))
        {
            return false;
        }

        // 2. 获取消息接收者
        var receiver = EntityManger.TryGetEntity(receiverInsId);
        
        // 3. 构造消息
        var message = new Telegram.TelegramBuilder().BuildSenderInsId(sendInsId)
            .BuildReceiverInsId(receiverInsId)
            .BuildDispatchTime(SendMessageImmediately)
            .BuildMessageType(msgType)
            .BuildExtraInfo(extraData)
            .Build();
        
        // 4. 发送消息
        receiver!.HandleMessage(in message);
        
        return true;
    }

    /// <summary>
    /// 通过名字发消息
    /// </summary>
    /// <param name="receiverName"></param>
    /// <param name="senderName"></param>
    /// <param name="msgType"></param>
    /// <param name="delaySeconds"></param>
    /// <param name="extraData"></param>
    /// <returns>发送者、接收者不存在时返回 false</returns>
    public bool DispatchMessage(EntityName receiverName,
        EntityName senderName, ConstDefine.MessageType msgType, double delaySeconds, object? extraData)
    {
        // 1. 前置检查
        if(!m_sendMessagePreCheckByName(senderName, receiverName))
        {
            return false;
        }

        // 2. 获取消息接收者
        var receiver = EntityManger.TryGetEntityByEntityName(receiverName);
        var sender = EntityManger.TryGetEntityByEntityName(senderName);
        // 3. 构造消息
        var message =  m_buildMessage(sender!.InsId, receiver!.InsId, msgType, delaySeconds, extraData!);
        
        // 4. 延时消息
        if (delaySeconds != 0)
        {
            PriorityMessageQueue.Add(message);
            return true;
        }
        
        // 5. 发送消息
        receiver.HandleMessage(in message);
        
        return true;
    }

    /// <summary>
    /// 发送延时消息
    /// </summary>
    /// <param name="sendInsId"></param>
    /// <param name="receiverInsId"></param>
    /// <param name="msgType"></param>
    /// <param name="extraData"></param>
    /// <param name="delaySeconds"></param>
    /// <returns>发送者、接收者不存在时返回 false</returns>
    public bool AddDelayMessage(int sendInsId, int receiverInsId, ConstDefine.MessageType msgType, object? extraData, double delaySeconds)
    {
        if(!m_sendMessagePreCheckByInsId(sendInsId, receiverInsId))
        {
            return false;
        }
        
        // 2. 构造消息
        var message = new Telegram.TelegramBuilder().BuildSenderInsId(sendInsId)
            .BuildReceiverInsId(receiverInsId)
            .BuildDispatchTime(delaySeconds)
            .BuildMessageType(msgType)
            .BuildExtraInfo(extraData)
            .Build();
        
        // 3. 添加信息到优先队列
        PriorityMessageQueue.Add(message);

        return true;
    }

    /// <summary>
    /// 发送延时消息
    /// </summary>
    public void DispatchDelayMessage()
    {
        // 1. 没有等待派发的消息
        if (!PriorityMessageQueue.Any())
        {
            return;
        }
        
        // 2. 获取当前时间
        var now = DateTime.Now;
        
        // 3. 遍历找到要派发的消息
        var willDispatchMessages =
            PriorityMessageQueue.Where(msg => msg.DispatchTime <= now && msg.DispatchTime > DateTime.MinValue).ToList();
        // 3.1 派发消息
        willDispatchMessages.ForEach(DispatchMessage);
        
        // 4. 删除已派发的消息
        willDispatchMessages.ForEach(msg => PriorityMessageQueue.Remove(msg));
    }

    /// <summary>
    /// 消息优先队列
    /// 使用者需要按照需求自定义排序
    /// </summary>
    public SortedSet<Telegram> PriorityMessageQueue { get; private set; } = new();

    /// <summary>
    /// 实例管理者缓存
    /// </summary>
    private static GameEntityManger EntityManger
    {
        get
        {
            return GameEntityManger.Instance;
        }
    }
    
    /// <summary>
    /// 立即发送消息
    /// </summary>
    private static int SendMessageImmediately = 0;

    #endregion

    #region 内部函数

    /// <summary>
    /// 发送消息的通用检查
    /// </summary>
    /// <returns>发送者，接收者任何一个不存在返回 false，其他返回 true</returns>
    private static readonly Func<int,int,bool> m_sendMessagePreCheckByInsId = (sendInsId, recInsId) =>
    {
        var sendIns = GameEntityManger.Instance.TryGetEntity(sendInsId);
        var recIns = GameEntityManger.Instance.TryGetEntity(recInsId);
        
        return sendIns != null && recIns != null;
    };
    
    /// <summary>
    /// 发消息的通用检查
    /// </summary>
    private static readonly Func<EntityName, EntityName, bool> m_sendMessagePreCheckByName = (sendName, recName) =>
    {
        var sendIns = GameEntityManger.Instance.TryGetEntityByEntityName(sendName);
        var recIns = GameEntityManger.Instance.TryGetEntityByEntityName(recName);
        
        return sendIns != null && recIns != null;
    };
    
    /// <summary>
    /// 构造消息
    /// </summary>
    private static readonly Func<int, int, ConstDefine.MessageType, double, object, Telegram> m_buildMessage = (sendInsId, recInsId, messageType, delaySeconds, extraData) =>
    {
        var message = new Telegram.TelegramBuilder().BuildSenderInsId(sendInsId)
            .BuildReceiverInsId(recInsId)
            .BuildMessageType(messageType)
            .BuildDispatchTime(delaySeconds)
            .BuildExtraInfo(extraData)
            .Build();
        
        return message;
    };

    /// <summary>
    /// 发送信息
    /// </summary>
    /// <param name="message"></param>
    private void DispatchMessage(Telegram message)
    {
        // 1. 获取消息接收者
        var receiver = EntityManger.TryGetEntity(message.ReceiverInsId);
        
        // 2. 派发消息
        receiver!.HandleMessage(in message);
    }
    
    #endregion

    #region Singleton

    /// <summary>
    /// 对象缓存
    /// </summary>
    private static MessageDispatcher? m_dispatcher;

    /// <summary>
    /// 获取对象的接口
    /// </summary>
    public static MessageDispatcher Instance
    {
        get
        {
            if (m_dispatcher == null)
            {
               m_dispatcher = new MessageDispatcher(); 
            }

            return m_dispatcher;
        } 
    }

    #endregion
}