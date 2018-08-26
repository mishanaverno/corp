using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//класс загрузки карты из файла и конвертация его в массив
namespace Game{
	public class MapLoader{
		public static int[,,] LoadMap(string mapname){
			int Xsize = 0;
			int Zsize = 0;
			int levels = 0;
			int xCount = 0;
			int levelCount = 0;
			int[,,] map;
			string[] Zline;

			StreamReader mapFile = new StreamReader ("maps/"+mapname);
			string line = "";
			line = mapFile.ReadLine ();
			if (line == "map:") {
				line = mapFile.ReadLine ();
				Xsize = int.Parse (line.Substring (line.IndexOf (":") + 1));
				line = mapFile.ReadLine ();
				Zsize = int.Parse (line.Substring (line.IndexOf (":") + 1));
				line = mapFile.ReadLine ();
				levels = int.Parse (line.Substring (line.IndexOf (":") + 1));
				map = new int[levels, Xsize, Zsize];
				Zline = new string[Zsize];
				while (!mapFile.EndOfStream) {
					line = mapFile.ReadLine ();
					if (line.IndexOf ("level:") > -1) {
						levelCount = int.Parse (line.Substring (line.IndexOf (":") + 1));
					} else {
						Zline = line.Split ('|');
						for (int i = 0; i < Zsize; i++) {
							map [levelCount, xCount, i] = int.Parse (Zline [i]);
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