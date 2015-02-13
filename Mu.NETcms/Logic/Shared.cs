using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mu.NETcms.Logic
{
    public class ResetResult
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public bool Failed(string error)
        {
            message = error;
            Succeeded = false;
            return !Succeeded;
        }
        public ResetResult Create(bool state, string msg)
        {
            var rr = new ResetResult();
            rr.Succeeded = state;
            rr.message = msg;
            return rr;
        }
    }
    public enum GameMessageId
    {
        ResetSuccess,
        ResetFailZen,
        ResetFailLevel,
        ResetFailCap,
        AccountConnected,
        Error
    }
}