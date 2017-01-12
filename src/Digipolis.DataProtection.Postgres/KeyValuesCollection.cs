using System;

namespace Digipolis.DataProtection.Postgres
{
    internal class KeyValuesCollection
    {
        public int Id { get; set; }
        public Guid AppId { get; set; }
        public Guid InstanceId { get; set; }
        public string Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
