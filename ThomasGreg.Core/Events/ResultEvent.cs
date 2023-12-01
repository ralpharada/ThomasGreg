using System.Text.Json.Serialization;

namespace ThomasGreg.Core.Events
{
    public class ResultEvent : IEvent
    {
        public bool Success { get; private set; }
        public object Data { get; private set; } = null!;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? TotalRows { get; private set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Id { get; private set; }
        public ResultEvent(bool success, object data, long? totalRows = null, int? id = null)
        {
            Success = success;
            Data = data;
            TotalRows = totalRows;
            Id = id;
        }
    }
}
