using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBToFile.Service
{
    public class MessageService
    {
        private static Message message;
        private static readonly object locker = new object();

        private MessageService() { }

        public static Message GetInstance()
        {
            if (message == null)
            {
                lock (locker)
                {
                    if (message == null)
                    {
                        message = new Message();
                    }
                }
            }
            return message;
        }
    }

}
