using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationDemo
{

    public class NotificationUtil
    {

        private IList<Link2EventObject> eventList;

        #region 构造实例
        private NotificationUtil()
        {
            if (eventList == null)
            {
                eventList = new List<Link2EventObject>();
            }
        }  //私有构造函数
        private static NotificationUtil _object = null; //静态变量

        /// <summary>
        /// 提供类的实例属性
        /// </summary>
        public static NotificationUtil Instance
        {
            get
            {
                lock (typeof(NotificationUtil))
                {
                    if (_object == null)
                    {
                        _object = new NotificationUtil();
                    }
                    return _object;
                }
            }
        }
        #endregion



        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="name">事件名</param>
        /// <param name="func"></param>
        public void regEvent(object obj, string name, Action<object> func)
        {
            Link2EventObject e = new Link2EventObject(name, func, obj.GetType().FullName);
            eventList.Add(e);
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool removeEvent(object obj)
        {
            foreach (Link2EventObject e in eventList)
            {
                if (e.with_type.Equals(obj.GetType().FullName))
                {
                    eventList.Remove(e);
                }
            }
            return false;
        }

        /// <summary>
        /// 推送一个事件
        /// </summary>
        /// <param name="name"></param>
        public void postEvent(string name, object obj)
        {
            foreach (Link2EventObject e in eventList)
            {
                if (e.event_name.Equals(name))
                {
                    e.func(obj);
                }
            }
        }
    }

    public class Link2EventObject
    {

        public Link2EventObject(string n, Action<object> f, string t)
        {
            this.event_name = n;
            this.func = f;
            this.with_type = t;
        }

        /// <summary>
        /// 事件名
        /// </summary>
        public string event_name { get; set; }

        /// <summary>
        /// 事件回调
        /// </summary>
        public Action<object> func { get; set; }

        /// <summary>
        /// 事件所属类
        /// </summary>
        public string with_type { get; set; }
    }
}
