using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using ToDoListApp.Entities;

namespace ToDoListApp.Data
{
    public abstract class FileStorage<T> where T : Entity
    {

        protected abstract string FileName { get; }

        private Dictionary<int, T> _entitiesDictionary = new Dictionary<int, T>();
        public FileStorage()
        {
            if (!File.Exists(FileName))
            {
                File.Create(FileName);
            }
            else
            {
                ReadAllAsDictionary();
            }
        }

        public bool IsEmpty()
        {
            return _entitiesDictionary.Count == 0;
        }
        public int GetLastId()
        {
            return _entitiesDictionary.LastOrDefault().Key;
        }
        public void Add(T data)
        {
            if (!_entitiesDictionary.ContainsKey(data.Id))
            {
                _entitiesDictionary.Add(data.Id, data);
                WriteEntitiesToFile();
                return;
            }

            throw new Exception($"Entity with id:{data.Id} already exsists");
        }

        public void Edit(T data)
        {
            if (_entitiesDictionary.ContainsKey(data.Id))
            {
                _entitiesDictionary[data.Id] = data;
                WriteEntitiesToFile();
                return;
            }
            throw new ArgumentException($"Entity with id:{data.Id} does not exist");
        }


        public T Read(int Id)
        {
            return _entitiesDictionary[Id];
        }

        public void Delete(int Id)
        {
            if (_entitiesDictionary.ContainsKey(Id))
            {
                _entitiesDictionary.Remove(Id);
                WriteEntitiesToFile();
                return;
            }

            throw new Exception($"Entity with id:{Id} does not exist");
        }

        public List<T> ReadAll()
        {
            return _entitiesDictionary.Values.ToList();
        }

        private void ReadAllAsDictionary()
        {
            var serializedData = File.ReadAllText(FileName);
            if (string.IsNullOrWhiteSpace(serializedData))
            {
                return;
            }
            var entities = JsonConvert.DeserializeObject<List<T>>(serializedData);
            _entitiesDictionary = entities.ToDictionary(key => key.Id, value => value);
        }

        private void WriteEntitiesToFile()
        {
            string serializedData = JsonConvert.SerializeObject(_entitiesDictionary.Values);
            File.WriteAllText(FileName, serializedData);
        }
    }
}
