using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Org.Cfg.Messaging
{
  public class MessageBody
  {
    public JObject TransactionJson { get; set; }
    public TransactionBase Transaction { get; set; }
  
    public MessageBody()
    {
      this.TransactionJson = null;
      this.Transaction = new TransactionBase();
    }
  }
}
