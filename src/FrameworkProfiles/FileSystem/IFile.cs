﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FrameworkProfiles.FileSystem
{
    public interface IFile : IFolderItem
    {
        Stream Open();
    }
}
