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
        protected NodeGrid _nodeGrid = new NodeGrid();
        protected bool showGrid = false; 

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
            if (showGrid)   //default is off
            {
                obs.OnNext(new ShowGridCommand(_nodeGrid));
            }
        }

        public bool Update(int tick)
        {
            worldObjects.Where(u =>
            {
                if (u is IUpdatable)
                {
                    bool needsCommand = ((IUpdatable)u).Update(tick);

                    if (needsCommand)
                    {
                        SendCommandToObservers(new UpdateModel3DCommand(u));
                    }
                }
                return false;
            }).ToList();
            return true;
        }

        // logic here:
        Random rnd = new Random();
        public List<TaskForRobot> tasksForRobot = new List<TaskForRobot>();
        public List<LogicTask> logicTasks = new List<LogicTask>();

        public void Logic()
        {
            worldObjects.Where(x => GetTasks(x)).ToList();
            if(logicTasks.Count() != 0)
            {
                logicTasks = logicTasks.Where(x => x != null ? x.RunTask(this) : false).ToList();
            }
        }

        public bool GetTasks(Object3D obj)
        {
            if(obj is Robot)
            {
                if (((Robot)obj).isDone)
                {
                    ((Robot)obj).SetIsDone();
                    logicTasks.Add(new RobotTaskRequest((Robot)obj));
                }
            }
            else if(obj is ExportVehicle)
            {
                if (((ExportVehicle)obj).isDone)
                {
                    SendCommandToObservers(new DeleteModel3DCommand(obj));
                    worldObjects.Remove(obj);
                    SetInboundTimer(new ExportVehicleRequest(obj.x, obj.z));
                }
            }
            else if (obj is ImportVehicle)
            {
                if (((ImportVehicle)obj).isDone)
                {
                    SendCommandToObservers(new DeleteModel3DCommand(obj));
                    worldObjects.Remove(obj);
                    SetInboundTimer(new ImportVehicleRequest(obj.x, obj.y, obj.z, obj.rotationX, obj.rotationY, obj.rotationZ));
                }
            }
            else if (obj is Crate)
            {
                if (((Crate)obj).isDone)
                {
                    SendCommandToObservers(new DeleteModel3DCommand(obj));
                    worldObjects.Remove(obj);
                }
            }
            else if (obj is Refinery)
            {
                List<Crate> refinedCrates = ((Refinery)obj).GetRefinedList();
                if (refinedCrates.Count() != 0)
                {
                    refinedCrates.Where(x =>
                    {
                        logicTasks.Add(new PickUpRefinedCrateRequest((PickUpTarget)obj, x));
                        refinedCrates.Remove(x);
                        return false;
                    }).ToList();
                }
            }
            return true;
        }

        protected List<System.Timers.Timer> InboundTimers = new List<System.Timers.Timer>();
        
        protected void SetInboundTimer(LogicTask task)
        {
            //default interval
            int interval = 0;   
            // Create a timer with other interval.
            if (task is ExportVehicleRequest)
            {
                interval = ((ExportVehicleRequest)task).interval;
            } 
            if (task is ImportVehicleRequest)
            {
                interval = ((ImportVehicleRequest)task).interval;
            }

            System.Timers.Timer aTimer = new System.Timers.Timer(interval);
            InboundTimers.Add(aTimer);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += (e, v) => {
                logicTasks.Add(task);
                aTimer.Dispose();
                InboundTimers.Remove(aTimer);
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
