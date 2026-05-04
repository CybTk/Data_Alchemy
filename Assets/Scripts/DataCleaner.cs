using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

public class DataCleaner : MonoBehaviour
{
    public static DataCleaner instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 1. Logika Null Purge
    public string ExecuteNullPurge(string rawData)
    {
        if (string.IsNullOrEmpty(rawData)) return "Data Kosong";

        string[] lines = rawData.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        StringBuilder cleanedData = new StringBuilder();

        if (lines.Length > 0) cleanedData.AppendLine(lines[0]); // Header

        for (int i = 1; i < lines.Length; i++)
        {
            if (!string.IsNullOrEmpty(lines[i]))
            {
                if (!lines[i].Contains("NULL"))
                {
                    cleanedData.AppendLine(lines[i]);
                }
            }
        }
        return cleanedData.ToString().TrimEnd();
    }

    // 2. Logika Duplicate Dissolver
    // 2. Logika Duplicate Dissolver (Diperbarui agar lebih tahan banting terhadap spasi)
   // 2. Logika Duplicate Dissolver yang disesuaikan untuk sistem Anda
    public string ExecuteDuplicateDissolver(string rawData)
    {
        if (string.IsNullOrEmpty(rawData)) return "Data Kosong";

        // Bersihkan string dari karakter \n dan \t yang terbaca sebagai teks
        rawData = rawData.Replace("\\n", "\n").Replace("\\t", "\t");

        // Pisahkan menjadi baris-baris
        string[] lines = rawData.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        List<string> uniqueLines = new List<string>();

        if (lines.Length > 0) uniqueLines.Add(lines[0]); // Header

        HashSet<string> seenNames = new HashSet<string>();

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (!string.IsNullOrEmpty(line))
            {
                // Ambil kata pertama (biasanya nomor ID) atau gabungan Name & Age
                // Kita akan membagi baris berdasarkan spasi atau tab
                string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                // Pastikan format baris valid (misal: "1 Andi 21")
                if (parts.Length >= 3)
                {
                    // Ambil kombinasi nama dan umur untuk mendeteksi duplikat
                    string uniqueKey = parts[1].Trim().ToLower() + "_" + parts[2].Trim();

                    if (!seenNames.Contains(uniqueKey))
                    {
                        seenNames.Add(uniqueKey);
                        uniqueLines.Add(line);
                    }
                }
                else
                {
                    // Jika baris tidak sesuai format, tetap masukkan untuk keamanan data
                    uniqueLines.Add(line);
                }
            }
        }
        return string.Join(Environment.NewLine, uniqueLines);
    }
}