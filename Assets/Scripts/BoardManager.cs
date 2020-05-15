using UnityEngine;
using System;
using System.Collections.Generic;         //Allows us to use Lists.
using Random = UnityEngine.Random;         //Tells Random to use the Unity Engine random number generator.
using System.IO;

namespace Completed

{

    public class BoardManager : MonoBehaviour
    {
        public GameObject exit;                                            //Prefab to spawn for exit.
        public GameObject[] floorTiles;                                    //Array of floor prefabs.
        public GameObject[] wallTiles;                                    //Array of wall prefabs.
        public GameObject[] outerWallTiles;                                //Array of outer tile prefabs.

        public string[][] mapMatrix;

        private Transform boardHolder;                                    //A variable to store a reference to the transform of our Board object.

        //Sets up the outer walls and floor (background) of the game board.
        void BoardSetup()
        {
            //Instantiate Board and set boardHolder to its transform.
            boardHolder = new GameObject("Board").transform;

            //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
            for (int y =0; y < mapMatrix.Length; y++)
            {
                //Loop along y axis, starting from -1 to place floor or outerwall tiles.
                for (int x = 0; x < mapMatrix[y].Length; x++)
                {
                    //Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                    if (mapMatrix[y][x]=="X")
                        toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                    else if (mapMatrix[y][x] == "P")
                        toInstantiate = wallTiles[Random.Range(0, wallTiles.Length)];
                    else if (mapMatrix[y][x] == "E")
                        toInstantiate = exit;

                    //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, mapMatrix.Length - y, 0f), Quaternion.identity) as GameObject;

                    //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                    instance.transform.SetParent(boardHolder);
                }
            }
        }

        void ParseMapString(string filePath)//habrá que adaptarlo, de momento pilla un csv y lo parsea. En el futuro tiene que recibir el string de la BBDD
        {
            StreamReader sr = new StreamReader(filePath);
            var lines = new List<string[]>();
            int Row = 0;
            while (!sr.EndOfStream)
            {
                string[] Line = sr.ReadLine().Split(',');
                lines.Add(Line);
                Row++;
                Console.WriteLine(Row);
            }

            this.mapMatrix = lines.ToArray();
        }

        //SetupScene initializes our level and calls the previous functions to lay out the game board
        public void SetupScene(int level)
        {
            ParseMapString(@"C:\Users\pepv\Desktop\mapa1.csv");//Es una ruta absoluta, lo sé, es para testear.

            //Creates the outer walls and floor.
            BoardSetup();
        }
    }
}