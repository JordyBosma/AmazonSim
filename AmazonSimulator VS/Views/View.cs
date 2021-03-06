﻿using Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Views
{
    public abstract class View : IObserver<Command>
    {

        protected WebSocket socket;

        //base constructer
        public View(WebSocket socket)
        {
            this.socket = socket;
        }

        //connection code
        public abstract Task StartReceiving();
        public abstract void SendMessage(string message);
        public virtual void SendCommand(Command c)
        {
            SendMessage(c.ToJson());
        }

        //IObserver Implementation:
        public virtual void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public virtual void OnError(Exception error)
        {
            throw new NotImplementedException();
        }
        public virtual void OnNext(Command value)
        {
            SendCommand(value);
        }
    }
}
