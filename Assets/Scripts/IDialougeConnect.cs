using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.IO;
public abstract class IDialougeConnect
{


    // static IDialougeConnect ConnectFormat;
    public List<string[]> m_dialougeData;
    public List<string[]> m_OptionsData;
    abstract public string[] FindRowWithUID(int UID);
    abstract public string FindDataWithName(int UID, string DataName);
    abstract public void loadFile(string path, string fileName);
    abstract public int RowLength();
    abstract public int ColLength();
}
