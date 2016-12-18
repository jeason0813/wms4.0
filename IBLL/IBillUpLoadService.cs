using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IBLL
{
    public partial interface IBillUpLoadService
    {
        string Save(string[] files);
        MemoryStream ExprotExcel(string billType, Guid billId, string TemplateUrl);

    }
}
