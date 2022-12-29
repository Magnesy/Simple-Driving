using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_ANDROID  // kodun sadece androidde çalışmasını istiyorsak.
using Unity.Notifications.Android;
#endif
using UnityEngine;

public class AndroidNotificationHandler : MonoBehaviour
{
    private const string ChannelId = "notification_channel";
    

    public void ScheduleNotification(DateTime dateTime)
    {
        #if UNITY_ANDROID  // kodun sadece androidde çalışmasını istiyorsak.
        AndroidNotificationCenter.CancelAllNotifications();
        
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel
        {
            Id = ChannelId,
            Name = "Notification Channel",
            Description = "Some random description",
            Importance = Importance.Default
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        AndroidNotification notification = new AndroidNotification
        {
            Title = "Energy Recharged!",
            Text = "Your energy has recharged, come back and play again!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = dateTime
        };

        AndroidNotificationCenter.SendNotification(notification, ChannelId);

    }

    #endif
}
