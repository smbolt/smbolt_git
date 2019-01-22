using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.QB.QBXML
{
  public class QBXmlFactory : IDisposable
  {
    public QbXmlBase Create(TransactionType transType)
    {
      try
      {
        switch (transType)
        {
          case TransactionType.CustomerAddRq:
            return new CustomerAddRq();
          case TransactionType.CustomerQueryRq:
            return new CustomerQueryRq();

        }

        throw new Exception("The creation of objects of type '" + transType.ToString() + "' is not yet implemented.");
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create an object of type '" + transType + "'.", ex);
      }
    }


    public void Dispose()
    {

    }
  }
}
