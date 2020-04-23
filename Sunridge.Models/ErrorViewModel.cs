using System;

namespace Sunridge.Models
{
    public class ErrorViewModel
    {
        public int ErrorViewModelId { get; set; }
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}