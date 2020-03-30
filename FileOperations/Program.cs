﻿using System;
using System.IO;

namespace FileOperations
{
    class Program
    {
        StreamReader read;
        StreamWriter write;
        static void Main(string[] args)
        {
            Program p = new Program();
            int choice;
            do
            {
                Console.WriteLine("Select from following options: \n 1.Create a new file \n 2.Display contents of a file \n 3.Rename a file \n 4.Copy a file \n 5.Concatenate two files \n 6. Exit");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        p.Create();
                        break;
                    case 2:
                        p.Display();
                        break;
                    case 3:
                        p.Rename();
                        break;
                    case 4:
                        p.Copy();
                        break;
                    case 5:
                        p.Concatenate();
                        break;
                    case 6:
                        Console.WriteLine("Exit");
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
            }
            while (choice != 6);

        }

        public void Create()
        {
            try
            {
                Console.WriteLine("Enter the name of file - ");
                string FileName = Console.ReadLine();

                Console.WriteLine("Enter the content of file - ");
                string content = Console.ReadLine();

                write = new StreamWriter("E://" + FileName + ".txt");
                write.WriteLine(content);
                write.Flush();
                write.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:" + e);
            }
        }

        public void Display()
        {
            try
            {
                Console.WriteLine("Enter file name - ");
                string FileName = Console.ReadLine();
                using (read = new StreamReader("E://" + FileName + ".txt"))
                {
                    string ln;

                    while ((ln = read.ReadLine()) != null)
                    {
                        Console.WriteLine(ln);
                    }
                    read.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:" + e);
            }
        }

        public void Rename()
        {
            try
            {
                Console.WriteLine("Enter the file which you want to rename");
                string FileName = Console.ReadLine();
                Console.WriteLine("Enter new file name");
                string Rename = Console.ReadLine();
                FileInfo file = new FileInfo("E://" + FileName + ".txt");

                if (file.Exists)
                {
                    file.MoveTo("E://" + Rename + ".txt");
                    Console.WriteLine("File renamed");
                }
                Console.WriteLine("There is no such file");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:" + e);
            }
        }

        public void Copy()
        {
            try
            {
                Console.WriteLine("Enter the filename from which you want to copy");
                string sourceFile = Console.ReadLine();
                Console.WriteLine("Enter the filename to which you want to copy the file");
                string destFile = Console.ReadLine();

                File.Copy("E://" + sourceFile + ".txt", "E://" + destFile + ".txt");
                Console.WriteLine("File copied ");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:" + e);
            }
        }

        public void Concatenate()
        {
            try
            {
                string[] sourceFiles = new string[2];

                Console.WriteLine("Enter the first source filename ");
                sourceFiles[0] = Console.ReadLine();
                Console.WriteLine("Enter the second source filename ");
                sourceFiles[1] = Console.ReadLine();
                Console.WriteLine("Enter target filename");
                string targetFile = Console.ReadLine();

                using (write = new StreamWriter("E://" + targetFile + ".txt"))
                {
                    for (int i = 0; i < sourceFiles.Length; i++)
                    {
                        using (read = new StreamReader("E://" + sourceFiles[i] + ".txt"))
                        {
                            string ln;

                            while ((ln = read.ReadLine()) != null)
                            {
                                write.WriteLine(ln);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:" + e);
            }
        }
    }
}