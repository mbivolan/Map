using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Collections;



namespace MapSearch
{
    using RenderedDataList = System.Collections.Generic.List<System.Collections.Generic.List<System.String>>;
    class DataEntry
    {
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

        public Dictionary<String, String> toHash()
        {
            return new Dictionary<String, String>() {
                { "NumarParcela", this.NumarParcela} ,
                { "Judet", this.Judet },
                { "Firma", this.Firma },
                { "Oras", this.Oras },
                { "Nume", this.NumeProprietar },
                { "Tarla", this.Tarla },
                { "Parcela", this.Parcela },
                { "Status", this.StatusDosar },
                { "Data", this.DataContract },
                { "Suprafata", this.Suprafata }
            };
        }

    }

    class DataSource
    {
        public List<List<String>> data;
        public List<DataEntry> rawData;

        public DataSource(List<String> paths)
        {
            this.data = new List<List<String>>();
            this.rawData = new List<DataEntry>();

            List<string> files = findAllJson(new List<string> { 
                @"C:\Users\bivolan\Documents\Projects\Test\DOLJ", @"C:\Users\bivolan\Documents\Projects\Test\OLT"
                // @"C:\Users\bivolan\Documents\Projects\Test\DOLJ", @"C:\Users\bivolan\Documents\Projects\Test\OLT"
            });

            foreach (string file in files)
            {
                convertJson(file);
            }
            // AddNumber();
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

            //
            foreach(DataEntry entry in newData)
            {
                //entry.NrCrt = index.ToString();
                entry.Oras = oras;
                entry.Judet = judet;
                entry.Firma = firma;
                //this.data.Add(entry.toList());
                //index++;
            }

            this.rawData.AddRange(newData);
        }

        public RenderedDataList renderData(List<DataEntry> data)
        {
            RenderedDataList renderedData = new RenderedDataList();

            int index = 1;
            foreach (DataEntry entry in data)
            {
                List<String> finalList = entry.toList();
                finalList.Insert(0, index.ToString());
                renderedData.Add(finalList);
                index++;
            }

            return renderedData;
        }

        public List<String> getUniqueValues(List<DataEntry> data, string key)
        {
            List<string> values = new List<string>();

            foreach (DataEntry entry in data)
            {
                Dictionary<String, String> entryDict = entry.toHash();
                if (!values.Contains(entryDict[key]))
                {
                    values.Add(entryDict[key]);
                }
            }

            return values;
        }

        public List<DataEntry> filterData(List<DataEntry> data, string key, string value)
        {
            List<DataEntry> filteredData = new List<DataEntry>();

            foreach (DataEntry entry in data)
            {
                if (entry.toHash()[key] == value)
                {
                    filteredData.Add(entry);
                }
            }

            return filteredData;
        }

    }
}