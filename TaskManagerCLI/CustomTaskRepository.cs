using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TaskManagerCLI
{
    internal class CustomTaskRepository
    {
        private Dictionary<ulong, CustomTask> tasksDictionary = new Dictionary<ulong, CustomTask>();
        private ulong nextId = 1;

        public void AddTask(string name, string description, int status)
        {
            LoadTasks();
            if (tasksDictionary.Any())
            {
                nextId = tasksDictionary.Keys.Max() + 1;
            }

            var task = new CustomTask
            {
                Id = nextId++,
                Name = name,
                Description = description,
                CreatedATime = DateTime.Now,
                UpdatedATime = DateTime.Now,
                status = status

            };
            
            if (status == 1 || status == 0 || status == 2)
            {

                tasksDictionary.Add(task.Id, task);

                string jsonData = JsonConvert.SerializeObject(tasksDictionary, Formatting.Indented);

                File.WriteAllText("data.json", jsonData);
                Console.WriteLine("Task added successfully!");

            }
            else
            {
                Console.WriteLine("Status doest exist");
            }
        }


        public void RemoveTask(ulong id)
        {
            LoadTasks();

            if (tasksDictionary.ContainsKey(id))
            {
                tasksDictionary.Remove(id);

                try
                {
                    string jsonData = JsonConvert.SerializeObject(tasksDictionary, Formatting.Indented);
                    File.WriteAllText("data.json", jsonData);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong...");
                }
            }
            else
            {
                Console.WriteLine("No such id");
            }
        }

        public void LoadTasks()
        {
            if (File.Exists("data.json"))
            {
                try
                {
                    string jsonData = File.ReadAllText("data.json");
                    tasksDictionary = JsonConvert.DeserializeObject<Dictionary<ulong, CustomTask>>(jsonData) ??
                                      new Dictionary<ulong, CustomTask>();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong..");
                }
            }
        }

        public void ShowTasks()
        {
            if (File.Exists("data.json"))
            {
                try
                {
                    string jsonData = File.ReadAllText("data.json");
                    tasksDictionary = JsonConvert.DeserializeObject<Dictionary<ulong, CustomTask>>(jsonData) ??
                                      new Dictionary<ulong, CustomTask>();
                    Console.WriteLine("YourTasks");
                    if (tasksDictionary.Count == 0)
                    {
                        Console.WriteLine("No tasks available");
                    }

                    foreach (var task in tasksDictionary.Values)
                    {
                        Console.WriteLine($"ID:{task.Id}");
                        Console.WriteLine($"Task:{task.Name}");
                        Console.WriteLine($"Description:{task.Description}");
                        Console.WriteLine($"Created at:{task.CreatedATime}");
                        Console.WriteLine($"Updated at:{task.UpdatedATime}");
                        Console.WriteLine($"Status:" + GetStatusText(task.status));
                    }


                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong...");
                }

            }
        }

        public string GetStatusText(int status)
        {
            switch (status)
            {
                case 0: return "Undone";
                case 1: return "Done";
                case 2: return "In Progress";
                default: return "Unknown Status";
            }
        }


        public void UpdateTask(ulong id, string description, int status)
        {
            LoadTasks();
            if (!string.IsNullOrWhiteSpace(description))
            {
                description = description;
            }
            else
            {
                Console.WriteLine("Cannot be empty");
                return;
            }

            if (tasksDictionary.ContainsKey(id))
            {
                var task = tasksDictionary[id];
                task.Description = description;
                task.status = status;
                task.UpdatedATime = DateTime.Now;

                try
                {
                    
                    string jsonData = JsonConvert.SerializeObject(tasksDictionary, Formatting.Indented);

                    File.WriteAllText("data.json", jsonData);

                    Console.WriteLine($" {id} task updated");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong...");
                }
            }
            else
            {
                Console.WriteLine($" {id} not found");
            }

        }

     
        public void DoneShowTasks()
        {
            if (File.Exists("data.json"))
            {
                try
                {
                    string jsonData = File.ReadAllText("data.json");
                    tasksDictionary = JsonConvert.DeserializeObject<Dictionary<ulong, CustomTask>>(jsonData) ??
                                      new Dictionary<ulong, CustomTask>();
                    Console.WriteLine("Your done tasks");
                    if (tasksDictionary.Count == 0)
                    {
                        Console.WriteLine("No tasks available");
                    }

                    foreach (var task in tasksDictionary.Values)
                    {
                        if (task.status == 1)
                        {
                            Console.WriteLine($"ID:{task.Id}");
                            Console.WriteLine($"Task:{task.Name}");
                            Console.WriteLine($"Description:{task.Description}");
                            Console.WriteLine($"Created at:{task.CreatedATime}");
                            Console.WriteLine($"Updated at:{task.UpdatedATime}");
                            Console.WriteLine($"Status:" + GetStatusText(task.status));
                        }
                    }


                }
                catch (Exception e)
                {
                    Console.WriteLine("мамаку трахал пола");
                }

            }
        }
       
        public void UndoneShowTasks()
        {
            if (File.Exists("data.json"))
            {
                try
                {
                    string jsonData = File.ReadAllText("data.json");
                    tasksDictionary = JsonConvert.DeserializeObject<Dictionary<ulong, CustomTask>>(jsonData) ??
                                      new Dictionary<ulong, CustomTask>();
                    Console.WriteLine("Your done tasks");
                    if (tasksDictionary.Count == 0)
                    {
                        Console.WriteLine("No tasks available");
                    }

                    foreach (var task in tasksDictionary.Values)
                    {
                        if (task.status == 0)
                        {
                            Console.WriteLine($"ID:{task.Id}");
                            Console.WriteLine($"Task:{task.Name}");
                            Console.WriteLine($"Description:{task.Description}");
                            Console.WriteLine($"Created at:{task.CreatedATime}");
                            Console.WriteLine($"Updated at:{task.UpdatedATime}");
                            Console.WriteLine($"Status:" + GetStatusText(task.status));
                        }
                    }


                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong...");
                }

            }
        }
        public void InProgressShowTasks()
        {
            if (File.Exists("data.json"))
            {
                try
                {
                    string jsonData = File.ReadAllText("data.json");
                    tasksDictionary = JsonConvert.DeserializeObject<Dictionary<ulong, CustomTask>>(jsonData) ??
                                      new Dictionary<ulong, CustomTask>();
                    Console.WriteLine("Your done tasks");
                    if (tasksDictionary.Count == 0)
                    {
                        Console.WriteLine("No tasks available");
                    }

                    foreach (var task in tasksDictionary.Values)
                    {
                        if (task.status == 2)
                        {
                            Console.WriteLine($"ID:{task.Id}");
                            Console.WriteLine($"Task:{task.Name}");
                            Console.WriteLine($"Description:{task.Description}");
                            Console.WriteLine($"Created at:{task.CreatedATime}");
                            Console.WriteLine($"Updated at:{task.UpdatedATime}");
                            Console.WriteLine($"Status:" + GetStatusText(task.status));
                        }
                    }


                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong...");
                }

            }
        }
    }
}
