﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace MapSearch
{
    class DataEntry
    {
        public string NrCrt;
        public string NumarParcela;
        public string Judet;
        public string Oras;
        public string Firma;
        public string NumeProprietar;
        public string Tarla;
        public string Parcela;
        public string Suprafata;
        public string DataContract;
        public string StatusDosar;
        public string LinkDocument;
        public List<string> AdditionalDocument;

        public int coord_x;
        public int coord_y;

        public DataEntry()
        {
            this.AdditionalDocument = new List<string>();
        }

        public List<String> toList()
        {
            return new List<string> {
                this.NrCrt,
                this.NumarParcela,
                this.Judet,
                this.Firma,
                this.Oras,
                this.NumeProprietar,
                this.Tarla,
                this.Parcela,
                this.StatusDosar,
                this.DataContract,
                this.Suprafata
            };
        }
    }

    class DataSource
    {
        //private string testPath = @"C:\Users\bivolan\source\repos\Test\DOLJ\ANM\BIRCA\config.json";

        public List<List<String>> data;
        public List<DataEntry> rawData;

        public DataSource(List<String> paths)
        {
            this.data = new List<List<String>>();
            this.rawData = new List<DataEntry>();

            List<string> files = findAllJson(new List<string> { @"C:\Users\HP\Desktop\Test\DOLJ", @"C:\Users\HP\Desktop\Test\OLT" });

            foreach (string file in files)
            {
                convertJson(file);
            }
            AddNumber();
        }

        private List<String> findAllJson(List<String> paths)
        {
            List<string> jsonFiles = new List<string>();

            foreach (string path in paths)
            {
                jsonFiles.AddRange(System.IO.Directory.GetFiles(path, "*.json", SearchOption.AllDirectories).ToList());
            }

            return jsonFiles;
        }
        public void convertJson(string jsonPath)
        {
            string[] locatie = jsonPath.Split('\\');
            string oras = locatie[locatie.Count() - 2];
            string firma = locatie[locatie.Count() - 3];
            string judet = locatie[locatie.Count() - 4];

            string inputJson = File.ReadAllText(jsonPath);
            List<DataEntry> newData = JsonConvert.DeserializeObject<List<DataEntry>>(inputJson);

            //int index = 1;
            foreach(DataEntry entry in rawData)
            {
                //entry.NrCrt = index.ToString();
                entry.Oras = oras;
                entry.Judet = judet;
                entry.Firma = firma;

                this.data.Add(entry.toList());
                //index++;
            }

            this.rawData.AddRange(newData);
        }
        public void AddNumber()
        {
            int index = 1;
            foreach(DataEntry item in rawData)
            {
                item.NrCrt = index.ToString();
                this.data.Add(item.toList());
                index++;
            }

        }
    }
}