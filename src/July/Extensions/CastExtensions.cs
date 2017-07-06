using System;
using System.Collections.Generic;
using System.Text;

namespace July.Extensions
{
    public static class CastExtensions
    {
        public static bool CastAndMatch<TCastTo>(this object source, Predicate<TCastTo> predicate)
            where TCastTo : class
        {
            if (source is TCastTo)
            {
                return predicate(source as TCastTo);
            }

            return false;
        }            
    }
}
