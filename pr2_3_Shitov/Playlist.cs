using System;
using System.Collections.Generic;

namespace pr2_3_Shitov
{
    public class Playlist
    {
        private List<Song> list;
        private int currentIndex;

        public Playlist()
        {
            list = new List<Song>();
            currentIndex = 0;
        }

        public Song CurrentSong()
        {
            if (list.Count > 0)
                return list[currentIndex];
            else
                throw new IndexOutOfRangeException("Плейлист пуст!");
        }
        //перегрузки добавления песни в плейлист
        public void AddSong(Song song)
        {
            list.Add(song);
        }

        //Перегрузка 2 добавление через 3 параметра 
        public void AddSong(string author, string title, string filename)
        {
            Song newSong;
            newSong.Author = author;
            newSong.Title = title;
            newSong.Filename = filename;
            AddSong(newSong);
        }

        //Перегрузка 3 добавление через 2 параметра (автор и название), файл по умолчанию
        public void AddSong(string author, string title)
        {
            string defaultFilename = "D:\\Shitov\\pr2_3_Shitov\\bin\\Debug\\mp.3";
            AddSong(author, title, defaultFilename);
        }

        public void NextSong()
        {
            if (list.Count == 0)
                throw new IndexOutOfRangeException("Плейлист пуст!");

            if (currentIndex + 1 < list.Count)
                currentIndex++;
            else
                currentIndex = 0;
        }

        public void PreviousSong()
        {
            if (list.Count == 0)
                throw new IndexOutOfRangeException("Плейлист пуст!");

            if (currentIndex - 1 >= 0)
                currentIndex--;
            else
                currentIndex = list.Count - 1;
        }

        public void GoToIndex(int index)
        {
            if (list.Count == 0)
                throw new IndexOutOfRangeException("Плейлист пуст!");

            if (index < 0 || index >= list.Count)
                throw new IndexOutOfRangeException("Индекс вне диапазона!");

            currentIndex = index;
        }

        public void GoToFirst()
        {
            if (list.Count == 0)
                throw new IndexOutOfRangeException("Плейлист пуст!");
            currentIndex = 0;
        }

        //перегрузки удаления песни из плейлиста
        public void RemoveSong(int index)
        {
            if (list.Count == 0)
                throw new IndexOutOfRangeException("Плейлист пуст!");

            if (index < 0 || index >= list.Count)
                throw new IndexOutOfRangeException("Индекс вне диапазона!");

            list.RemoveAt(index);

            if (list.Count == 0)
                currentIndex = 0;
            else if (index <= currentIndex && currentIndex > 0)
                currentIndex--;
        }

        //Перегрузка 2 удаление по объекту Song (вызывает перегрузку 1)
        public bool RemoveSong(Song song)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Author == song.Author &&
                    list[i].Title == song.Title &&
                    list[i].Filename == song.Filename)
                {
                    RemoveSong(i);
                    return true;
                }
            }
            return false;
        }

        //Перегрузка 3 удаление по автору и названию
        public bool RemoveSong(string author, string title)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Author == author && list[i].Title == title)
                {
                    RemoveSong(i);
                    return true;
                }
            }
            return false;
        }

        public void ClearPlaylist()
        {
            list.Clear();
            currentIndex = 0;
        }

        public int Count()
        {
            return list.Count;
        }

        public bool IsEmpty()
        {
            return list.Count == 0;
        }

        public List<Song> GetAllSongs()
        {
            return new List<Song>(list);
        }

        public int GetCurrentIndex()
        {
            return currentIndex;
        }
    }
}