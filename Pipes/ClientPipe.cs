﻿using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text;

namespace Pipes
{
    public class ClientPipe : BasicPipe
    {
        protected NamedPipeClientStream clientPipeStream;

        public ClientPipe(string serverName, string pipeName, Action<BasicPipe> asyncReaderStart)
        {
            this.asyncReaderStart = asyncReaderStart;
            clientPipeStream = new NamedPipeClientStream(serverName, pipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
            pipeStream = clientPipeStream;
        }

        public void Connect()
        {
            clientPipeStream.Connect();
            asyncReaderStart(this);
        }
    }
}
