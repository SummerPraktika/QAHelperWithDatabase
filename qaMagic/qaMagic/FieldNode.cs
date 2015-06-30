﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace qaMagic
{
    class FieldNode
    {
        HashSet<string> data = new HashSet<string>();
        int type;
        string name;
        string pathToFile;
        string dateFormat;
        int from, to;
        int start, step;

        public FieldNode(int type, string name, string pathToFile)
        {
            this.type = type;
            this.name = name;
            this.pathToFile = pathToFile;
            setData();
        }

        public FieldNode(int type, string name, int from, int to)
        {
            this.type = type;
            this.name = name;
            this.from = from;
            this.to = to;
        }

        public FieldNode(int type, string name, string dateFormat, int from, int to)
        {
            this.type = type;
            this.name = name;
            this.dateFormat = dateFormat;
            this.from = from;
            this.to = to;
        }

        public FieldNode(string name, int type, int start, int step)
        {
            this.type = type;
            this.name = name;
            this.start = start;
            this.step = step;
        }

        void setData()
        {
            string line;

            StreamReader file = new StreamReader(pathToFile, Encoding.Default);
            while ((line = file.ReadLine()) != null)
            {
                this.data.Add(line);
            }
        }

        string getRndString()
        {
            Random rand = new Random();
            return this.data.ElementAt(rand.Next(0, this.data.Count));
        }

        int getRndNumber()
        {
            Random rand = new Random();
            return rand.Next(from, to);
        }

        int getSequenceNumber()
        {
            int number = this.start;
            this.start = this.start + this.step;
            return number;
        }

        string getRndDate()
        {
            Random day = new Random();
            long ticks = new DateTime(this.from, 01, 01, 00, 00, 0).Ticks;
            DateTime date = new DateTime(ticks).AddDays(day.Next(0, (this.to - this.from) * 365));

            return leadToFormat(date);
        }

        string leadToFormat(DateTime date)
        {
            string[] formats = { "dd.MM.yyyy", "MM.dd.yyyy", "dd.MM.yy", "MM.dd.yy", "yyyy.MM.dd", "yyyy.dd.MM", "yy.MM.dd", "yy.dd.MM" };

            foreach (string format in formats)
            {
                if (format.ToLower() == this.dateFormat.ToLower())
                {
                    return date.ToString(format);
                }
            }
            return date.ToString("dd.MM.yyyy");
        }
    }
}