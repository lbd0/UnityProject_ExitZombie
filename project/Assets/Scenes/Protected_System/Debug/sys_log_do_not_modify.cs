using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class sys_log_do_not_modify : MonoBehaviour {
	string user_log;
	string log_path;

	// Use this for initialization
	void Start () {
		#if UNITY_EDITOR
		user_log = System.DateTime.Now.ToString("yyyy-MM-dd, HH:mm:ss") + " (" + SystemInfo.deviceUniqueIdentifier + ")\n";

		log_path = Path.Combine(Application.dataPath + "/System", "usl.json");

		FileStream file = new FileStream(log_path, FileMode.Append, FileAccess.Write);
		file.Flush();
		StreamWriter sw = new StreamWriter(file);
		sw.Write(user_log);

		sw.Close();
		file.Close();
		#endif
	}
}
