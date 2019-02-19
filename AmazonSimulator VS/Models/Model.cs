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

        // logic down here:
        //Random rnd = new Random();
        private List<TaskForRobot> _tasksForRobot = new List<TaskForRobot>();
        private List<LogicTask> logicTasks = new List<LogicTask>();

        public List<TaskForRobot> tasksForRobot { get { return _tasksForRobot; } }

        /// <summary>
        /// Runs once to all data to find task and will try to execute these. If a task is not succesfully completed it will try it again next time.
        /// </summary>
        public void Logic()
        {
            //Find tasks:
            worldObjects.Where(x => GetTasks(x)).ToList();
            GetPickUpTasks();
            //Try executing tasks:
            if (logicTasks.Count() != 0)
            {
                List<LogicTask> finish = logicTasks.Where(x => x != null ? x.RunTask(this) : true).ToList();
                for (int i = 0; i < finish.Count(); i++)
                {
                    logicTasks.Remove(finish[i]);
                }
            }
        }

        /// <summary>
        /// Checks in Object3d data state if there is a task to be done by logic.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool GetTasks(Object3D obj)              //There should be a interface for this but for now it will do fine
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
        
        /// <summary>
        /// Adds a timer that when elapsed will add the given task to tasks for logic.
        /// </summary>
        /// <param name="task"></param>
        protected void SetInboundTimer(LogicTask task)
        {
            //default interval
            int interval = 2500; //milliseconds   
            // Create a timer with other interval.
            if (task is InboundLogicTask)
            {
                interval = ((InboundLogicTask)task).GetInterval();
            } 

            System.Timers.Timer aTimer = new System.Timers.Timer(interval);
            InboundTimers.Add(aTimer);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += (e, v) => {
                logicTasks.Add(task);
                InboundTimers.Remove(aTimer);
                aTimer.Dispose();
            };
            aTimer.Enabled = true;
        }

        /// <summary>
        /// Checks if there are Crates placed in inport storagenodes who haven't yet been added to the logictask to be transported by a robot to a refinery.
        /// </summary>
        protected void GetPickUpTasks()
        {
            foreach (Node node in _nodeGrid.nodes)
            {
                if (node is StorageNode)
                {
                    if (((StorageNode)node).GetIsDone())
                    {
                        if (((StorageNode)node).GetCrate().refined == false)
                        {
                            logicTasks.Add(new PickUpUnRefinedCrateRequest((StorageNode)node));
                            ((StorageNode)node).SetIsDone();
                        }
                    }
                }
            }
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
