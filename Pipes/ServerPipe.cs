﻿using System;
using System.IO.Pipes;

namespace Pipes
{
    public class ServerPipe : BasicPipe
    {
        public event EventHandler<EventArgs> Connected;

        protected NamedPipeServerStream serverPipeStream;
        protected string PipeName { get; set; }

        public ServerPipe(string pipeName, Action<BasicPipe> asyncReaderStart)
        {
            this.asyncReaderStart = asyncReaderStart;
            PipeName = pipeName;

            serverPipeStream = new NamedPipeServerStream(
                pipeName,
                PipeDirection.InOut,
                NamedPipeServerStream.MaxAllowedServerInstances,
                PipeTransmissionMode.Message,
                PipeOptions.Asynchronous);

            pipeStream = serverPipeStream;
            serverPipeStream.BeginWaitForConnection(PipeConnected, null);
        }

        protected void PipeConnected(IAsyncResult ar)
        {
            serverPipeStream.EndWaitForConnection(ar);
            Connected?.Invoke(this, new EventArgs());
            asyncReaderStart(this);
        }
    }
}
