﻿namespace NLayer.Core.DTOs
{
    public class ErrorViewDto
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
