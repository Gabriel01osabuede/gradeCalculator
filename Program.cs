using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace gradeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // a program that calculates gradePoint average 
            // Input -->a series of  course codes,a series of scores for each course code.
            // a gradePointCalculator that calculates the GradePoint of a course given its score
            // some sort of dashboard that prints grade points
            //a menu that prompts the users on what they want to do
            // i can probably use an enum to check my current stage
            Menu menu = new Menu();
            gradePointCalculator mygradePointCalculator = new gradePointCalculator();
            menu.setCurrentStage(1);
            Db appDb = Db.Initialize();

            bool appIsRunning = true;
            // always perform this when the user is in top menu
            while (appIsRunning)
            {

                while (menu.getCurrentStage() == 1)
                {
                    Menu.PromptUser("What Would you like to Do Today : ?");
                    Menu.PromptUser("1. Add Courses To DB : ");
                    Menu.PromptUser("2. Calculate Grade Point Averages Of Courses In Db : ");
                    Menu.PromptUser("3. Exit");
                    string selectedMenuOption = Console.ReadLine();
                    switch (selectedMenuOption)
                    {
                        case "1":
                            menu.setCurrentStage(2);
                            break;
                        case "2":
                            menu.setCurrentStage(3);
                            break;
                        case "3":
                            Environment.Exit(0);
                            break;
                        default:
                            break;
                    }
                    Console.Clear();
                }
                while (menu.getCurrentStage() == 2)
                {
                    // we want to tell the user to add a set of courses and their scores
                    // and then we want to store those courses in some sort of Database/ collection
                    // we need to know how many courses the user is entering a score for ..
                    // we can design a prompt that runs for each of the courses 

                    Menu.PromptUser("Select An Option :");
                    Menu.PromptUser("Enter Number Of Courses (enter a number): ");
                    Menu.PromptUser("Back To Main Menu (b)");
                    string input = Console.ReadLine();
                    int numberOfCourses;
                    if (input.ToLower() == "b")
                    {
                        menu.setCurrentStage(1);
                    }
                    else if (Int32.TryParse(input, out numberOfCourses))
                    {
                        for (int i = 1; i <= numberOfCourses; i++)
                        {
                            Menu.PromptUser($"Enter A Course Code For Course {i} : ");
                            string courseCode = Console.ReadLine();
                            Menu.PromptUser($"Enter a Score for {courseCode} : ");
                            double score = Convert.ToDouble(Console.ReadLine());

                            Menu.PromptUser($"Enter a number Of Units for {courseCode} : ");
                            int units = Convert.ToInt32(Console.ReadLine());
                            appDb.AddCourse(new Course(courseCode, score, units));
                        }
                        
                        Console.WriteLine("Data stored succefully");
                        Thread.Sleep(3000);
                        menu.setCurrentStage(1);
                    }
                    Console.Clear();
                    
                }

                while (menu.getCurrentStage() == 3)
                {
                    IEnumerable<Course> courseFromDb = new List<Course>();
                    List<double> temporaryHoldScores = new List<double>();
                    List<string> temporaryHoldCourseCode = new List<string>();
                    List<double> temporaryHoldUnit = new List<double>();
                    double calculate = 0;
                    double totalGradePoint= 0;

                    courseFromDb = appDb.getAllCourses();

                    if (courseFromDb.Count() == 0) 
                    {
                        Console.WriteLine("YOu dont have any course in database.");
                        menu.setCurrentStage(1);
                        break;
                    }
                     Console.WriteLine("Course Code || Course Scores || Number Of Unit || Grade Points");
                    foreach(var item in courseFromDb)
                    {
                        if(item.CourseScore >= 70 && item.CourseScore <= 100)
                        {    
                            calculate = item.NumberOfUnits * 5;
                        }
                        else if(item.CourseScore >= 60 && item.CourseScore <=69)
                        {
                            calculate = item.NumberOfUnits * 4;
                        }
                         else if(item.CourseScore  >= 50 && item.CourseScore <= 59)
                        {
                            
                            calculate = item.NumberOfUnits * 3;
                        }
                        else if(item.CourseScore >= 45 && item.CourseScore <= 49)
                        {
                            calculate = item.NumberOfUnits * 2;
                        }
                        else if(item.CourseScore >= 40 && item.CourseScore <= 44)
                        {
                            
                            calculate = item.NumberOfUnits * 1;
                        }
                        else if(item.CourseScore >= 0 && item.CourseScore <=39)
                        {
                           
                            calculate = item.NumberOfUnits * 0;
                        }
                         
                       
                         totalGradePoint += calculate;
                         temporaryHoldCourseCode.Add(item.CourseCode);
                         temporaryHoldScores.Add(item.CourseScore);
                         temporaryHoldUnit.Add(item.NumberOfUnits);
                        
                         Console.WriteLine($"     {item.CourseCode}               {item.CourseScore}               {item.NumberOfUnits}               {calculate}");
                    } 
                   
                    double totalScore = temporaryHoldScores.Sum();
                    double totalUnit = temporaryHoldUnit.Sum();

                    Console.WriteLine($"\nYour Total Course Score is : {totalScore}");
                    Console.WriteLine($"Your Total Number Of Unit is : {totalUnit}");

                    double gpa = mygradePointCalculator.gpaCalculatorMethod(totalGradePoint,totalUnit);

                    Console.WriteLine($"The Gpa for all courses inputed : {gpa:F2}\nThank you.\n=================\n");
                    Thread.Sleep(5000);  

                    menu.setCurrentStage(1); 
                }
            }
        }
}
        
}
