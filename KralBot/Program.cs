using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Hosting;
using KralBot;


namespace KralBot
{
    class Program
    {
        public static Constructor.license license = new Constructor.license();
        public static DB database=new DB();
        public static TelegramBotClient Bot;
        public static List<Message> list_of_messageAZ = new List<Message>();
        public static List<Message> list_of_messageRU = new List<Message>();
        public static List<string> ilkdortAz  = new List<string>();
        public static List<string> ilkdortRu = new List<string>();

        public static List<Message> channels= new List<Message>();

        public static ChatId kralAz;
        public static ChatId kralRu;
        public static Message mainMessage;

        public static string finalSozAZ;
        public static string finalSozRU;
        public static string mainCodeStr;
        public static string mainmsj;

        public static bool active=false;
        public static bool active2=false;
        public static bool kt=false;  




         static void Main(string[] args)
        {
            Bot = new TelegramBotClient("1853822874:AAHX92sHRzewrtty4miQladHiMMttg18mjk");
            Bot.StartReceiving();
            var me = Bot.GetMeAsync().Result;
            Bot.OnUpdate += Bot_OnUpdate;
            
           // kralAz = -470590003;
            //kralRu = -402990521;
            Console.WriteLine(me.Username);
            new Task(Yenile).Start();
            var builder = new HostBuilder();
            builder.ConfigureWebJobs(b =>
            {
                b.AddAzureStorageCoreServices();
                b.AddTimers();
            });
            IHost host = builder.Build();

            using (host)
            {
                
                host.Run();

            }
                       

            //Yenile();

            Bot.StopReceiving();

            
           
        }
        

        private static async void Bot_OnUpdate(object sender, UpdateEventArgs e)
        {
            kt = false;


            if (e.Update.ChannelPost == mainMessage)
            {
                return;
            }
            var message = e.Update.ChannelPost;
            mainMessage = message;


            if (!active)
            {
                if (license.KeyCheck(message.Text))
                {

                    channels.Add(message);
                    Task.Delay(1000).Wait();
                    await Bot.SendTextMessageAsync(message.Chat.Id, "ACTIVED");
                    
                    
                    Task.Delay(3000).Wait();
                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId + 1);
                    active = true;

                }
                else
                {
                    await Bot.SendTextMessageAsync(message.Chat.Id, "ENTER SECRET KEY");
                    Task.Delay(5000).Wait();
                    active = false;
                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId + 1);
                    return;
                }
            }








            for (int z = 0; z < channels.Count; z++)
            {
                bool correct = false;
                
                if (channels[z].Chat.Id == message.Chat.Id)
                {
                    correct = true;
                    kt = true;
                }
                if (correct)
                {
                  
                    if (message == null || message.Type != MessageType.Text)
                    {
                        return;
                    }

                    string user_name = $"Gonderenin usernamei: GONDEREN GONDERIB  Gonderile mesaj : {message.Text}";
                    Console.WriteLine(user_name);

                    char[] comCharArray = message.Text.ToCharArray();

                    char[] command = new char[7];
                    char[] code = new char[4];
                    char[] mainCode = new char[4];


                    for (int i = 0; i < 4; i++)
                    {

                        try
                        {
                            mainCode[i] = comCharArray[i];
                            mainCodeStr = new string(mainCode);
                        }
                        catch (Exception d)
                        {

                            Console.WriteLine(d);
                        }


                    }
                    string comString;
                    string codeString;

                    for (int i = 0; i < comCharArray.Length; i++)
                    {
                        if (i == 7)
                        {
                            break;
                        }
                        command[i] = comCharArray[i];


                    }
                    comString = new string(command);
                    if (comString == "/delete")
                    {
                        Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                        Console.WriteLine("silinir");

                        for (int i = 8; i < 12; i++)
                        {

                            try
                            {
                                code[i - 8] = comCharArray[i];
                            }
                            catch (Exception b)
                            {
                                Console.WriteLine(b);
                            }



                        }
                        codeString = new string(code);
                        Console.WriteLine(codeString);
                        //sil(codeString);
                         database.silMsjRu(codeString);

                    }
                    int n;
                    if (int.TryParse(mainCodeStr, out n))
                    {

                        ElaveET(message);

                    }


                    switch (message.Text)
                    {
                        case "/ru":
                            kralRu = message.Chat.Id;
                            break;
                        case "/az":
                            kralAz = message.Chat.Id;
                            break;
                        case "/yoxla":
                            Console.WriteLine("111111");

                            database.yoxla();


                            break;
                        case "/test":
                            break;
                    }
                    
                }
                
            }


            if (kt == false)
            {
                if (license.KeyCheck(message.Text))
                {
                    channels.Add(message);
                    Task.Delay(1000).Wait();
                    Bot.SendTextMessageAsync(message.Chat.Id, "ACTIVATED");
                    
                    Task.Delay(3000).Wait();
                    Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                    Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId + 1);


                }
                else
                {
                    if (license.used == true)
                    {
                       await Bot.SendTextMessageAsync(message.Chat.Id, "KEY IS ALREADY USED. ENTER NEW SECRET KEY");
                    }
                    await Bot.SendTextMessageAsync(message.Chat.Id, "ENTER SECRET KEY");
                    Task.Delay(5000).Wait();
                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId + 1);
                    return;
                }
            }

           
            




        }


        


        public static async void sil(string code)
        {
            ChatId b;

            b = mainMessage.Chat.Id;
            int f, az, ru;
            f = int.Parse(b);
            az = int.Parse(kralAz);
            ru = int.Parse(kralRu);
            if (f == az)
            {
                yoxla();
                for (int i = 0; i < ilkdortAz.Count; i++)
                {
                    if (code == ilkdortAz[i])
                    {
                        ilkdortAz.Remove(ilkdortAz[i]);
                        await Bot.DeleteMessageAsync(kralAz, list_of_messageAZ[i].MessageId);
                        list_of_messageAZ.Remove(list_of_messageAZ[i]);

                        break;
                    }
                }
            }
            if (f == ru)
            {
                yoxla();
                for (int i = 0; i < ilkdortRu.Count; i++)
                {
                    if (code == ilkdortRu[i])
                    {
                        
                            ilkdortRu.Remove(ilkdortRu[i]);
                        int z = list_of_messageRU[i].MessageId;
                            await Bot.DeleteMessageAsync(kralRu, z);

                            list_of_messageRU.Remove(list_of_messageRU[i]);
                            break;
                     
                        
                    }
                }

            }
        }
        public static void Yenile()
        {
            while (true)
            {
                Task.Delay(3600000).Wait();
                database.yoxla();
                Console.WriteLine("tik tak");

            }
        }
        static public void yoxla()
        {
            ilkdortAz.Clear();
            ilkdortRu.Clear();
            if (list_of_messageAZ.Count != 0)
            {
                for (int i = 0; i < list_of_messageAZ.Count; i++)
                {

                    string mesaj = list_of_messageAZ[i].Text;
                    char[] charMsj = mesaj.ToCharArray();
                    char[] ilkDort = new char[4];

                    if (charMsj.Length > 3)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            ilkDort[j] = charMsj[j];
                            string esas_soz = new string(ilkDort);
                            finalSozAZ = esas_soz;
                        }

                        bool f = true;
                        for (int k = 0; k < ilkdortAz.Count; k++)
                        {
                            if (ilkdortAz[k] == finalSozAZ)
                            {
                                f = false;
                                break;
                            }


                        }
                        if (f)
                        {
                            ilkdortAz.Add(finalSozAZ);
                        }
                    }
                }
            }
            if (list_of_messageRU.Count != 0) 
            { 
                for (int i = 0; i < list_of_messageRU.Count; i++)
                {

                    string mesaj = list_of_messageRU[i].Text;
                    char[] charMsj = mesaj.ToCharArray();
                    char[] ilkDort = new char[4];

                    if (charMsj.Length > 3)
                    {


                        for (int j = 0; j < 4; j++)
                        {
                            try
                            {
                                ilkDort[j] = charMsj[j];
                                string esas_soz = new string(ilkDort);
                                finalSozRU = esas_soz;

                            }
                            catch (Exception error)
                            {

                                Console.WriteLine(error);
                            }

                        }
                    
                        bool f = true;
                        for (int k = 0; k < ilkdortRu.Count; k++)
                        {
                            if (ilkdortRu[k] == finalSozRU)
                            {
                                f = false;
                            break;
                            }


                        }
                            if (f)
                            {
                        
                            ilkdortRu.Add(finalSozRU);
                            }

                    }


                }
                


            }
            /* 

             Console.WriteLine("TIK TAK");

             */
            bool x=true, c=true;
            for (int d = 0; d < ilkdortAz.Count; d++)
            {
                if (x)
                {
                    Console.WriteLine("******AZE******");
                    x = false;
                }
                
                Console.WriteLine(ilkdortAz[d]);
            }
            for (int z = 0; z < ilkdortRu.Count; z++)
            {
                if (c)
                {
                    Console.WriteLine("******RUS******");
                    c = false;
                }
                
                Console.WriteLine(ilkdortRu[z]);
            }
        }

        
        static public void ElaveET(Message a)
        {
            
                ChatId b;

                b = a.Chat.Id;
                long f, az, ru;
                f = long.Parse(b);
                az = long.Parse(kralAz);
                ru = long.Parse(kralRu);
                bool t = true, z = true;
            

            if (f == az)
            {
                char[] charMsj = a.Text.ToCharArray();
                char[] ilkDort = new char[4];
                
                if (charMsj.Length > 3)
                {


                    for (int j = 0; j < 4; j++)
                    {
                        try
                        {
                            ilkDort[j] = charMsj[j];
                            string esas_soz = new string(ilkDort);
                            mainmsj = esas_soz;

                        }
                        catch (Exception error)
                        {

                            Console.WriteLine(error);
                        }

                    }
                    if (list_of_messageAZ.Count == 0)
                    {
                        list_of_messageAZ.Add(a);

                    }
                    database.azAddData(mainmsj, a.MessageId);

                }



                for (int i = 0; i < list_of_messageAZ.Count; i++)
                {

                    if (list_of_messageAZ[i]==a)
                    {
                        z = false;
                        break;
                    }

                    
                
                }
                if (z)
                {
                    list_of_messageAZ.Add(a);
                }
                   
                    
             }
            
            if (f == ru)
            {

                char[] charMsj = a.Text.ToCharArray();
                char[] ilkDort = new char[4];

                if (charMsj.Length > 3)
                {


                    for (int j = 0; j < 4; j++)
                    {
                        try
                        {
                            ilkDort[j] = charMsj[j];
                            string esas_soz = new string(ilkDort);
                            mainmsj = esas_soz;

                        }
                        catch (Exception error)
                        {

                            Console.WriteLine(error);
                        }

                    }
                    /* if (list_of_messageAZ.Count == 0)
                     {
                         list_of_messageAZ.Add(a);

                     }*/
                    database.ruAddData(mainmsj, a.MessageId);
                   
                }
                for (int i = 0; i < list_of_messageRU.Count; i++)
                {
                    if (list_of_messageRU[i] == a)
                    {
                        t = false;
                        break;
                    }

                    

                }
                if (t)
                {
                    list_of_messageRU.Add(a);
                }
                    
             }    

        }
        static public void FinalStep()
        {
            List<Message> silinenler = new List<Message>();
            List<string> silinenlerStr = new List<string>();
            for (int i = 0; i < ilkdortAz.Count; i++)
            {
                bool f = true;
                for (int j = 0; j < ilkdortRu.Count; j++)
                {
                    if (ilkdortAz[i] == ilkdortRu[j])
                    {
                        f = false;
                        

                    }
                }
                    
               if (f)
               {

                    
                    silinenler.Add(list_of_messageAZ[i]);
                    silinenlerStr.Add(ilkdortAz[i]);
                            
                }
                            
             }
            for (int k = 0; k < silinenler.Count; k++)
            {
                int index = list_of_messageAZ.FindIndex(a=> a.MessageId == silinenler[k].MessageId);
                Bot.DeleteMessageAsync(kralAz, list_of_messageAZ[index].MessageId);
                ilkdortAz.Remove(silinenlerStr[k]);
                list_of_messageAZ.Remove(silinenler[k]);
            }

        }

        public void delMessage(int messageid)
        {
            try
            {
                Bot.DeleteMessageAsync(kralAz, messageid);

            }
            catch (Exception c)
            {

                Console.WriteLine(c);
            }
        }
        public void delMessageRU(int messageid)
        {
            try
            {
                Bot.DeleteMessageAsync(kralRu, messageid);

            }
            catch (Exception c)
            {

                Console.WriteLine(c);
            }
            
        }

     }
}

