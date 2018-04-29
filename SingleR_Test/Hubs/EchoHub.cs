using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleR_Test.Hubs {
    public class EchoHub : Hub {
        public async Task SendMessage(string message) {
            // 調用所有客戶端的ReceiveMessage方法，並帶入message參數
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
