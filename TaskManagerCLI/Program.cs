using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerCLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists("data.json"))
            {
                Console.WriteLine("File.json created,all normal!");
            }
            else
            {
                Console.WriteLine("File.json doesnt exist");
            }
            Console.WriteLine("Hello you are using TASKMANAGER APP");
            Console.WriteLine("Select 1-7");
            Console.WriteLine("1)Show Tasks \n2)Add Task \n3)Update Task \n4)Delete Task \n5)Show done tasks. \n6)Show undone tasks.\n7)Show inprogress tasks");
            CustomTaskRepository customTaskRepository = new CustomTaskRepository();

            string selection = Console.ReadLine();
            int parsedSelection;

            if (int.TryParse(selection, out parsedSelection))
            {
                Console.WriteLine($"You entered a valid selection: {parsedSelection}");
            }
            else
            {
                Console.WriteLine("Invalid input, please enter a valid number.");
            }

            switch (parsedSelection)
            {
                case 1:
                    customTaskRepository.ShowTasks();



                    break;
                case 2:
                    Console.WriteLine("Write Task name");
                    string taskname = Console.ReadLine();
                    Console.WriteLine("Write task description");
                    string taskDescription = Console.ReadLine();
                    Console.WriteLine("Write status 0 -undone,1-done,2-inprogress");
                    string taskStatusSelection = Console.ReadLine();
                    int taskStatus;

                    if (int.TryParse(taskStatusSelection, out taskStatus))
                    {
                        if (taskStatus >= 0 && taskStatus <= 2)
                        {

                            Console.WriteLine($"You selected status: {taskStatus}");

                        }
                        else
                        {
                            Console.WriteLine("Please choose a valid option (0, 1, or 2).");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                        return;
                    }

                    if (taskname != null && taskDescription != null && taskStatus != null)
                    {
                        customTaskRepository.AddTask(taskname, taskDescription, taskStatus);
                        
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong.");
                    }
                    string jsonFilePath = "data.json";

                    break;


                case 3:
                    Console.WriteLine("Write id,new description,new status");
                    ulong mainId;
                    try
                    {
                        mainId = ulong.Parse(Console.ReadLine());
                        Console.WriteLine($"ID entered: {mainId}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("fuck this id");
                        return;
                    }
                    string newTaskDescription = Console.ReadLine();
                    Console.WriteLine("Write status 0 -undone,1-done,2-inprogress");
                    string newtaskStatusSelection = Console.ReadLine();
                    int taskStatus1;
                    if (int.TryParse(newtaskStatusSelection, out taskStatus1))
                    {
                        if (taskStatus1 >= 0 && taskStatus1 <= 2)
                        {

                            Console.WriteLine($"You selected status: {taskStatus1}");

                        }
                        else
                        {
                            Console.WriteLine("Please choose a valid option (0, 1, or 2).");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                    customTaskRepository.UpdateTask(mainId, newTaskDescription, taskStatus1);
                    break;
                case 4:
                    Console.WriteLine("Select what id u need to delete");
                    ulong idForDeletion;
                    
                    try
                    {
                        idForDeletion= ulong.Parse(Console.ReadLine());
                        Console.WriteLine($"ID entered: {idForDeletion}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("No such id");
                        return;
                    }
                    customTaskRepository.RemoveTask(idForDeletion);
                    break;
                case 5:
                    Console.WriteLine("All done tasks.");
                    customTaskRepository.DoneShowTasks();
                    break;
                case 6:
                    Console.WriteLine("All undone tasks.");
                    customTaskRepository.UndoneShowTasks();
                    break;
                case 7:
                    Console.WriteLine("All inProgress tasks.");
                    customTaskRepository.InProgressShowTasks();
                    
                    break;
            }

        }
    }
}