using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//класс загрузки карты из файла и конвертация его в массив
namespace Game{
	public class MapLoader{
		public static int[,,] LoadMap(string mapname){
			int Xsize = 0;
			int Ysize = 0;
			int levels = 0;
			int xCount = 0;
			int levelCount = 0;
			int[,,] map;
			string[] Yline;

			StreamReader mapFile = new StreamReader ("maps/"+mapname);
			string line = "";
			line = mapFile.ReadLine ();
			if (line == "map:") {
				line = mapFile.ReadLine ();
				Xsize = int.Parse (line.Substring (line.IndexOf (":") + 1));
				line = mapFile.ReadLine ();
				Ysize = int.Parse (line.Substring (line.IndexOf (":") + 1));
				line = mapFile.ReadLine ();
				levels = int.Parse (line.Substring (line.IndexOf (":") + 1));
				map = new int[levels, Xsize, Ysize];
				Yline = new string[Ysize];
				while (!mapFile.EndOfStream) {
					line = mapFile.ReadLine ();
					if (line.IndexOf ("level:") > -1) {
						levelCount = int.Parse (line.Substring (line.IndexOf (":") + 1));
					} else {
						Yline = line.Split ('|');
						for (int i = 0; i < Ysize; i++) {
							map [levelCount, xCount, i] = int.Parse (Yline [i]);
						}
						xCount++;

					}

				}
				return map;

			} else {
				return new int[0, 0, 0];
			}

			mapFile.Close();
		}


	}
}