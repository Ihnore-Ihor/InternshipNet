using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternshipNet.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace InternshipNet.Services
{
    public class JsonDataService
    {
        // Метод для збереження даних у файл
        public void Save(string filePath, ObservableCollection<Internship> data)
        {
            // Налаштування для красивого форматування JSON файлу
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, json);
        }

        // Метод для завантаження даних з файлу
        public ObservableCollection<Internship> Load(string filePath)
        {
            // Якщо файл не існує, повертаємо порожній список
            if (!File.Exists(filePath))
            {
                return new ObservableCollection<Internship>();
            }

            string json = File.ReadAllText(filePath);
            // Якщо файл порожній, також повертаємо порожній список
            if (string.IsNullOrEmpty(json))
            {
                return new ObservableCollection<Internship>();
            }

            return JsonSerializer.Deserialize<ObservableCollection<Internship>>(json);
        }
    }
}
