﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public interface IDataMapper
  {
    List<DxRowSet> MapData(DxWorkbook targetWorkbook);
  }
}