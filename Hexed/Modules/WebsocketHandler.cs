using WebSocketSharpManaged;
using WebSocketSharpManaged.Server;

namespace Hexed.Modules
{
    internal class WebsocketHandler
    {
        public class Client : WebSocketBehavior
        {
            protected override void OnMessage(MessageEventArgs Message)
            {
                string Data = Message.Data;
                Wrappers.Logger.Log($"{Data}", Wrappers.Logger.LogsType.Bot);
            }
        }

        internal class Server
        {
            public static WebSocketServer WSServer;

            public static void Initialize()
            {
                WSServer = new WebSocketServer("ws://localhost:6666");
                WSServer.AddWebSocketService<Client>("/Bot");
                WSServer.Log.Level = LogLevel.Fatal;
                WSServer.Start();
                Wrappers.Logger.Log("Starting Bot Server", Wrappers.Logger.LogsType.Bot);
            }

            public static void SendMessage(string Message)
            {
                if (WSServer != null) WSServer.WebSocketServices.Broadcast(Message);
            }
        }
    }
}
