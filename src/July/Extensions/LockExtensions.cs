using System;
using System.Collections.Generic;
using System.Text;

namespace July.Extensions
{
    public static class LockExtensions
    {
        public static T Lock<T>(this T source, Func<T, T> action)
        {
            lock (source)
            {
                return action(source);
            }
        }
    }
}
