//-------------------------------------------------------------------------
// <copyright file="Recipe.cs" company="Universidad Católica del Uruguay">
// Copyright (c) Programación II. Derechos reservados.
// </copyright>
//-------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;

namespace Full_GRASP_And_SOLID
{
    public class Recipe : IRecipeContent // Modificado por DIP
    {
        // Cambiado por OCP
        private IList<BaseStep> steps = new List<BaseStep>();

        public Product FinalProduct { get; set; }

        public bool Cooked { get; private set; }
        

        

        public int GetCookTime()
        {
            int totalTimeSteps = 0;
            foreach (BaseStep step in steps)
            {
                totalTimeSteps += step.Time;
            }
            return totalTimeSteps;
        }

        // Agregado por Adapter para usar el método Register de la 
        // clase CountDownTimer sin modificar el tipo de Recipe.
        // Para esto se crea la clase Adapter en donde se modifica el método.
        public void Cook()
        {
            CountdownTimer timer = new CountdownTimer();
            Adapter recipeAdapter = new Adapter(timer);
            recipeAdapter.RecipeTime(GetCookTime());

            Cooked = true;
        }

        // Agregado por Creator
        public void AddStep(Product input, double quantity, Equipment equipment, int time)
        {
            Step step = new Step(input, quantity, equipment, time);
            this.steps.Add(step);
        }

        // Agregado por OCP y Creator
        public void AddStep(string description, int time)
        {
            WaitStep step = new WaitStep(description, time);
            this.steps.Add(step);
        }

        public void RemoveStep(BaseStep step)
        {
            this.steps.Remove(step);
        }

        // Agregado por SRP
        public string GetTextToPrint()
        {
            string result = $"Receta de {this.FinalProduct.Description}:\n";
            foreach (BaseStep step in this.steps)
            {
                result = result + step.GetTextToPrint() + "\n";
            }

            // Agregado por Expert
            result = result + $"Costo de producción: {this.GetProductionCost()}";

            return result;
        }

        // Agregado por Expert
        public double GetProductionCost()
        {
            double result = 0;

            foreach (BaseStep step in this.steps)
            {
                result = result + step.GetStepCost();
            }

            return result;
        }
    }
}