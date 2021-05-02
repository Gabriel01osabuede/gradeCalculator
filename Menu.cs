using System;
using System.Collections.Generic;
using System.Text;

namespace gradeCalculator
{
    public class Menu
    {
        // a property/variable that represents the current stage the user is in.
        private int currentStage;


        /// <summary>
        /// this is the constructor.
        /// </summary>
        public Menu()
        {

        }

        public Menu(int initialStage)
        {
            this.currentStage = initialStage;
        }



        public static void PromptUser(string prompt)
        {
            Console.WriteLine(prompt);
        }

       public int getCurrentStage()
        {
            return this.currentStage;
        }
        public void setCurrentStage(int stage)
        {
            this.currentStage = stage;
        }
    }
}