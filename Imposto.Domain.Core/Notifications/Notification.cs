using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Domain.Core.Notifications
{
    public class Notification
    {
        public Guid Id { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }

        public Notification(string key, string value)
        {
            Id = Guid.NewGuid();
            Key = key;
            Value = value;
        }
    }
}
