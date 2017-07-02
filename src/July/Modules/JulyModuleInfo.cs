using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace July.Modules
{
    public class JulyModuleInfo
    {
        public Assembly Assembly { get; }

        public Type Type { get; }

        public JulyModule Instance { get; internal set; }

        public List<JulyModuleInfo> Dependencies { get; }

        private int _order;

        public int Order
        {
            get
            {
                return _order;
            }
            set
            {
                if (value > _order)
                {
                    _order = value;
                }
            }
        }

        public JulyModuleInfo(Type type)
        {
            Type = type;
            Assembly = type.GetTypeInfo().Assembly;
            Dependencies = new List<JulyModuleInfo>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is JulyModuleInfo))
            {
                return false;
            }

            JulyModuleInfo other = (JulyModuleInfo)obj;

            return Type == other.Type;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
