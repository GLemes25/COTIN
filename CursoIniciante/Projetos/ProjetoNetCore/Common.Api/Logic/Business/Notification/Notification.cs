﻿using Common.Api.Logic.Business.Intefaces;

namespace Common.Api.Logic.Business.Notification
{
    public class Notification : INotification
    {
        public IList<object> List { get; } = new List<object>();
        public bool HasNotifications => List.Any();
        public bool Includes(Description error)
        {
            return List.Contains(error);
        }
        public void Add(Description description)
        {
            List.Add(description);
        }
    }
}