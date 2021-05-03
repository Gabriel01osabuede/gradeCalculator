using System;

namespace gradeCalculator
{
    
   public class gradePointCalculator
   {
       public double gpaCalculatorMethod(double TotalUnitOfCourse,double totalpoints )
       {
           return TotalUnitOfCourse/totalpoints;
          
       }

       public double unitCalculator(int unit,int gradepoint)
       {
           return unit * gradepoint;
       }
   } 
}