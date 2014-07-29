﻿using System.Collections.Generic;

namespace Illumina.BaseSpace.SDK
{
    
    public interface IApiResponse<TResponse> : IHasNotifications
    {
        TResponse Response { get; }
    }

    public interface INotification<TItem>
    {
        string Type { get; set; }

        TItem Item { get; set; }
    }
    public interface IHasNotifications
    {
        IList<INotification<object>> Notifications { get; set; }
    }
    
}
