using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Models;
using Views;
using Utility;

namespace Controllers
{
    struct ObservingClient
    {
        public View vw;
        public IDisposable unsubscribe;
    }

    public class SimulationController
    {
        private Model w;
        private List<ObservingClient> views = new List<ObservingClient>();
        private bool running = false;
        private int tickTime = 25;

        public SimulationController(Model w) {
            this.w = w;
        }

        public void AddView(View v)
        {
            ObservingClient oc = new ObservingClient();

            oc.unsubscribe = this.w.Subscribe(v);
            oc.vw = v;

            views.Add(oc);
        }

        public void RemoveView(View v)
        {
            for(int i = 0; i < views.Count; i++) {
                ObservingClient currentOC = views[i];

                if(currentOC.vw == v) {
                    views.Remove(currentOC);
                    currentOC.unsubscribe.Dispose();
                }
            }
        }
        /// <summary>
        /// Starts the simulation on simulation thread.
        /// </summary>
        public void Simulate()
        {
            running = true;

            while(running) {
                w.Update(tickTime);
                Thread.Sleep(tickTime);
            }
        }

        public void EndSimulation()
        {
            running = false;
        }

        /// <summary>
        /// Starts the logic loop on logic thread.
        /// </summary>
        public void StartLogic()
        {
            running = true;
            Thread.Sleep(15);
            while (running)
            {
                w.Logic();
                Thread.Sleep(tickTime);
            }
        }
    }
}