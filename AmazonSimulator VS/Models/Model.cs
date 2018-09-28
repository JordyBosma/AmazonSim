using Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Models
{
    public abstract class Model : IObservable<Command>, IUpdatable
    {
        protected List<Object3D> _worldObjects = new List<Object3D>();
        protected List<IObserver<Command>> observers = new List<IObserver<Command>>();
        protected NodeGrid _nodeGrid;

        public List<Object3D> worldObjects { get { return _worldObjects; } }
        public NodeGrid nodeGrid { get { return _nodeGrid; } }

        public IDisposable Subscribe(IObserver<Command> observer)
        {
            if (!observers.Contains(observer))
            {
                SendCreationCommandsToObserver(observer);

                observers.Add(observer);
            }
            return new Unsubscriber<Command>(observers, observer);
        }

        protected void SendCommandToObservers(Command c)
        {
            for (int i = 0; i < this.observers.Count; i++)
            {
                this.observers[i].OnNext(c);
            }
        }

        protected void SendCreationCommandsToObserver(IObserver<Command> obs)
        {
            foreach (Object3D m3d in worldObjects)
            {
                obs.OnNext(new UpdateModel3DCommand(m3d));
            }
        }

        public bool Update(int tick)
        {
            for (int i = 0; i < worldObjects.Count; i++)
            {
                Object3D u = worldObjects[i];

                if (u is IUpdatable)
                {
                    bool needsCommand = ((IUpdatable)u).Update(tick);

                    if (needsCommand)
                    {
                        SendCommandToObservers(new UpdateModel3DCommand(u));
                    }
                }
            }

            return true;
        }

        // logic here:
        Random rnd = new Random();
        public List<TasksForRobot> tasksForRobot = new List<TasksForRobot>();
        public List<LogicTask> logicTasks = new List<LogicTask>();

        public void Logic()
        {
            GetTasks();
            while(logicTasks != null)
            {
                logicTasks.First().RunTask(this);
                logicTasks.RemoveAt(0);
            }
        }

        public void GetTasks()
        {
            foreach (Object3D obj in worldObjects)
            {
                if(obj is Robot)
                {
                    if (((Robot)obj).isMoving)
                    {
                        logicTasks.Add(new RobotTaskRequest((Robot)obj));
                    }
                }
                if(obj is ExportVehicle)
                {
                    if (((ExportVehicle)obj).isDone)
                    {
                        SendCommandToObservers(new DeleteModel3DCommand(obj));
                        
                        int interval = rnd.Next(89, 180) * 1000;
                        SetVehicleInboundTimer(interval, new ExportVehicleRequest());
                    }
                }
            }
        }

        private List<System.Timers.Timer> VehicleInboundTimers;
        
        private void SetVehicleInboundTimer(int interval, LogicTask task)
        {
            // Create a timer with a two second interval.
            System.Timers.Timer aTimer = new System.Timers.Timer(interval);
            VehicleInboundTimers.Add(aTimer);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += (e, v) => {
                logicTasks.Add(task);
                aTimer.Dispose();
            };
            aTimer.Enabled = true;
        }

    }

    public class Unsubscriber<Command> : IDisposable
    {
        private List<IObserver<Command>> _observers;
        private IObserver<Command> _observer;

        internal Unsubscriber(List<IObserver<Command>> observers, IObserver<Command> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
