using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services.Abstract
{
    public interface IFileContentTypeService
    {
        public string GetContentType(string path);
    }
}
