using System.Collections.Generic;

namespace ChatApp.Common
{
    public class CommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public IList<string> ExtraData { get; set; }

        public CommandResponse(bool succeeded = true, string message = null)
        {
            Succeeded = succeeded;
            Message = message ?? (Succeeded ? "正常に完了しました。" : "正常に完了することができませんでした。");
            ExtraData = new List<string>();
        }

        public CommandResponse AddExtra(string data)
        {
            ExtraData.Add(data);
            return this;
        }
    }
}