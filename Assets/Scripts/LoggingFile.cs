using System.IO;
using UnityEngine;

public class LoggingFile
{
    private string _filePath = Application.dataPath + "/Logging_Files/" + "Log" + ".txt";
    public void CreateTextFile()
    {
        Directory.CreateDirectory(Application.dataPath + "/Logging_Files/");
        string _filePath = Application.dataPath + "/Logging_Files/" + "Log" + ".txt";

        if (!File.Exists(_filePath)) 
        {
            File.WriteAllText(_filePath, "Время старта и окончания последней игры:\n");
            SaveTime(TimeGame.Start);
        }
        else
        {
            File.Delete(_filePath);
            CreateTextFile();
        }
            
    }

    public void SaveTime(TimeGame timeGame)
    {
        File.AppendAllText(_filePath, SaveTimeStart(timeGame));
    }

    private string SaveTimeStart(TimeGame timeGame)
    {
        string title;
        if (timeGame == TimeGame.Start)
            title = "[Начало игры]";
        else title = "[Конец игры]";
        return $"{title}{System.DateTime.Now.Hour}:{System.DateTime.Now.Minute}:{System.DateTime.Now.Second}\n";
    }
}
