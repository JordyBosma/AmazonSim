using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Models;
using Views;

namespace Controllers {
    struct ObservingClient {
        public View vw;
        public IDisposable unsubscribe;
    }
    public class SimulationController {
        private Model w;
        private List<ObservingClient> views = new List<ObservingClient>();
        private bool running = false;
        private int tickTime = 50;

        public SimulationController(Model w) {
            this.w = w;
        }

        public void AddView(View v) {
            ObservingClient oc = new ObservingClient();

            oc.unsubscribe = this.w.Subscribe(v);
            oc.vw = v;

            views.Add(oc);
        }

        public void RemoveView(View v) {
            for(int i = 0; i < views.Count; i++) {
                ObservingClient currentOC = views[i];

                if(currentOC.vw == v) {
                    views.Remove(currentOC);
                    currentOC.unsubscribe.Dispose();
                }
            }
        }

        public void Simulate() {
            running = true;

            while(running) {
                w.Update(tickTime);
                Thread.Sleep(tickTime);
            }
        }

        public void EndSimulation() {
            running = false;
        }

        public void Logic()
        {
            List<double[]> pickupTask = new List<double[]>();
            List<double[]> dropoffTask = new List<double[]>();

            pickupTask.Add(new double[2] { 0, 0  });
            pickupTask.Add(new double[2] { 10, 0 });
            pickupTask.Add(new double[2] { 10, 10 });
            pickupTask.Add(new double[2] { 20, 10 });
            pickupTask.Add(new double[2] { 20, 20 });
            pickupTask.Add(new double[2] { 30, 20 });
            pickupTask.Add(new double[2] { 30, 30 });

            dropoffTask.Add(new double[2] { 30, 30 });
            dropoffTask.Add(new double[2] { 30, 20 });
            dropoffTask.Add(new double[2] { 20, 20 });
            dropoffTask.Add(new double[2] { 20, 10 });
            dropoffTask.Add(new double[2] { 10, 10 });
            dropoffTask.Add(new double[2] { 10, 0  });
            dropoffTask.Add(new double[2] { 0,  0  });


            RobotTask rt = new RobotTask(pickupTask, dropoffTask);
            ((World)w).MoveRobot(rt);
        }
    }
}