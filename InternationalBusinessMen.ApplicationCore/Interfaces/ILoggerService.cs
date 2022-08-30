﻿
using System;

namespace InternationalBusinessMen.ApplicationCore.Interfaces
{
    public interface ILoggerService
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogError(Exception exception);
    }
}
