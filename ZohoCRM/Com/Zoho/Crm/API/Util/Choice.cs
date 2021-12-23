using System;

using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Util
{
    public class Choice<T>
    {
        T value;

        Choice()
        {
        }

        public Choice(T value)
        {
            this.value = value;
        }

        public T Value
        {
            get
            {
                return value;
            }
        }
    }
}