using System;

namespace BuffDecoraters.Abstract
{
    public interface IHasServiceProvider
    {
        IServiceProvider ServiceProvider { get; }
    }
}