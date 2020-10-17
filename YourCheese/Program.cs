
using HamsterCheese.AmongUsMemory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YourCheese
{
    class Program
    {
        static int tableWidth = Console.WindowWidth -5;


        static List<PlayerData> playerDatas = new List<PlayerData>();

        class Command
        {

            public string Name;
            public Action<string[]> Execute;


            public Command(string Name, Action<string[]> ExecFunc)
            {
                this.Name = Name;
                this.Execute = ExecFunc;
            }



        }

        static List<Command> Commands = new List<Command>();
        static Dictionary<int, string> ColorDict = new Dictionary<int, string>();

        static void WaitForCommand()
        {
            while (true)
            {

                string Output = Console.ReadLine();
                string[] Arguments = Output.Split(' ');
                string Command = Arguments[0];
                Arguments = Arguments.Skip(1).ToArray();
                //Console.WriteLine($"CMD : {Command} , ARG : {string.Join(" | ",Arguments)}");
                foreach (var Cmd in Commands)
                {
                    if (Cmd.Name == Command)
                    {

                        Cmd.Execute(Arguments);

                        break;
                    }
                }
                Thread.Sleep(100);
            }
        }

        static void MurderPlayer( string[] aa )
        {
            //Cheese.GetSelf().MurderPlayer(Cheese.GetAllPlayers()[0]);
        }
        static void getPlayerData(string[] aa)
        {
            //Console.WriteLine($"Players found : { string.Join(" | ",playerDatas.Select(x => Utils.ReadString(x.PlayerInfo.Value.PlayerName)))} ");
            PrintRow("Name", "Colour", "POS X", "POS Y");
            PrintLine();

            foreach (var data in playerDatas)
            {
                if (data.IsLocalPlayer)
                    Console.ForegroundColor = ConsoleColor.Green;
                if (data.PlayerInfo.Value.IsDead == 1)
                    Console.ForegroundColor = ConsoleColor.Red;
                if (data.PlayerInfo.Value.IsImpostor == 1)
                    Console.ForegroundColor = ConsoleColor.Blue;
                if (data.PlayerInfo.Value.IsImpostor == 1 && data.IsLocalPlayer)
                    Console.ForegroundColor = ConsoleColor.Cyan;
                if (data.PlayerInfo.Value.IsImpostor == 1 && data.PlayerInfo.Value.IsDead == 1)
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (data.PlayerInfo.Value.IsImpostor == 1 && data.PlayerInfo.Value.IsDead == 1 && data.IsLocalPlayer)
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                var Name = HamsterCheese.AmongUsMemory.Utils.ReadString(data.PlayerInfo.Value.PlayerName);
                PrintRow($"{ Name }", $"{ ColorDict[data.PlayerInfo.Value.ColorId] }", $"{data.Position.x}",$"{data.Position.y}");
                Console.ForegroundColor = ConsoleColor.White;
                PrintLine();
            }
        }
        static bool Update = false;
        static int LastTop = 0;
        static int LastBottom = 0;
        static Thread updateThread;
        static void UpdateCheat(string[] aaa)
        {
            Update = true;
            LastTop = 0;
            LastBottom = 0;

            updateThread = new Thread(() => {

                while (Update)
                {
                    if (Console.CursorLeft > 0)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    LastBottom = LastBottom > Console.CursorTop ? LastBottom : Console.CursorTop;
                    if (LastTop != 0)
                        Console.SetCursorPosition(0, LastTop);
                    //Console.WriteLine(LastTop);
                    LastTop = Console.CursorTop;
                    getPlayerData(Array.Empty<string>());
                    LastBottom = LastBottom > Console.CursorTop ? LastBottom : Console.CursorTop;
                    Console.SetCursorPosition(Console.CursorLeft, LastBottom);
                    Thread.Sleep(1000);
                }
                LastBottom = 0;
            });
            updateThread.Start();
        }


        static void revive(string[] users)
        {
            PlayerData me = playerDatas.Where(x => x.IsLocalPlayer).First();
            me.WriteMemory_IsDead(0);
        }
        static Thread rtkThread;
        static bool rtkOn = false;
        static void resetKillTLoop(string[] a)
        {
            rtkOn = !rtkOn;
            Console.WriteLine($"reset kill timeout loop {(rtkOn ? "Enabled" : "Disabled")}");
            PlayerData me = playerDatas.Where(x => x.IsLocalPlayer).First();
            if (rtkOn)
            {
                rtkThread = new Thread(() =>
                {

                    while (rtkOn)
                    {
                        if (me.Instance.killTimer > 0)
                            me.WriteMemory_KillTimer(0);
                        Thread.Sleep(400);
                    }

                });
                rtkThread.Start();
            } else
            {
                rtkThread.Abort();
            }
        }
        static void resetKillT(string[] a)
        {
            PlayerData me = playerDatas.Where(x => x.IsLocalPlayer).First();

            me.WriteMemory_KillTimer(0);
        }
        static void editspeed(float speed)
        {
            PlayerData me = playerDatas.Where(x => x.IsLocalPlayer).First();
            Console.WriteLine("wr");
        }
        static void Main(string[] args)
        {
            // Cheat Init
            if (HamsterCheese.AmongUsMemory.Cheese.Init())
            {
                // Update Player Data When Every Game
                HamsterCheese.AmongUsMemory.Cheese.ObserveShipStatus((x) =>
                {
                    
                    //Console.WriteLine(Utils.ReadString(Cheese.AmongUsClient().networkAddress) );

                    foreach (var player in playerDatas)
                    {
                        player.StopObserveState();
                    }
                    if (Update)
                        Console.Clear();


                    playerDatas = HamsterCheese.AmongUsMemory.Cheese.GetAllPlayers();


                    foreach (var player in playerDatas)
                    {
                        player.onDie += (pos, colorId) => {
                            Console.WriteLine("OnPlayerDied! Color ID :" + colorId);
                        };
                        // player state check
                        player.StartObserveState();
                    }


                });

                // commands

                Commands.Add(new Command("getData", getPlayerData));
                Commands.Add(new Command("rev", revive));
                Commands.Add(new Command("rkt", resetKillT));
                Commands.Add(new Command("speed", (aaa)=>editspeed(10f)));
                Commands.Add(new Command("rktl", resetKillTLoop));
                Commands.Add(new Command("start", UpdateCheat));
                Commands.Add(new Command("kill", MurderPlayer));
                Commands.Add(new Command("clr", (aaa) => Console.Clear()));
                Commands.Add(new Command("stop", (aaa) => { Update = false; Console.SetCursorPosition(0, LastBottom + 1); updateThread.Abort(); }));
                Commands.Add(new Command("test", (aaa) => Console.WriteLine("Test works.")));

                // populate colour dictionary
                ColorDict[0] = "red";
                ColorDict[1] = "blue";
                ColorDict[2] = "green";
                ColorDict[3] = "pink";
                ColorDict[4] = "orange";
                ColorDict[5] = "yellow";
                ColorDict[6] = "black";
                ColorDict[7] = "white";
                ColorDict[8] = "purple";
                ColorDict[9] = "brown";
                ColorDict[10] = "cyan";
                ColorDict[11] = "lime";


                // Cheat Logic
                CancellationTokenSource cts = new CancellationTokenSource();
                Task.Factory.StartNew(
                    WaitForCommand
                , cts.Token);
            }
            else
            {
                Console.WriteLine("Unable to find memory for among us.");
            }
            System.Threading.Thread.Sleep(1000000);
        }

        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);


        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}


