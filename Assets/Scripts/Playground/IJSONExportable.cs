using UnityEngine;
using System.Collections.Generic;

public interface IJSONExportable {
	Dictionary<string,double> GetExportableValues();
}
